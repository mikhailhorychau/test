using System;
using System.Collections.Generic;
using System.Linq;
using Generators.Game.Week.AI.Chassis;
using Helpers;
using UIScripts.Arch;
using UIScripts.Observable;
using UIScripts.Screens.Panels.HeaderMenu.Bonuses;
using UIScripts.Screens.SessionPanel;
using UIScripts.Screens.SessionPanel.PlayerDriverBlock;
using UIScripts.Screens.V2.BonusRequirement;

namespace UIScripts.RaceAction
{
    public class RaceCommandsInteractor : Interactor
    {
        public RaceBonusesBehaviour BonusesBehaviour { get; private set; } = new RaceBonusesBehaviour();
        public RaceCommandsBehaviour CommandsBehaviour { get; private set; } = new RaceCommandsBehaviour();

        public RaceCommandsTickBehaviour TickBehaviour { get; private set; }

        public override void OnStart()
        {
            
        }

        public override void Initialize()
        {
            var playerDriverStorage = SessionPanel.Scene.GetStorage<PlayerDriversStorage>();

            var sessionProps = SessionPanel.Scene.GetStorage<SessionStorage>().GetSessionProps();
            var driversCtx = new Dictionary<int, RaceCommandsContext>();
            var firstDriver = playerDriverStorage.GetPropsByDriverNumber(DriverNumber.First);
            var secondDriver = playerDriverStorage.GetPropsByDriverNumber(DriverNumber.Second);
            driversCtx[firstDriver.DriverID] = firstDriver.CommandsContext;
            driversCtx[secondDriver.DriverID] = secondDriver.CommandsContext;

            BonusesBehaviour.Initialize(sessionProps.Bonuses);
            CommandsBehaviour.Initialize(driversCtx, BonusesBehaviour);
            TickBehaviour = new RaceCommandsTickBehaviour(CommandsBehaviour.CommandModels);
            TickBehaviour.OnCooldownEnd += v => CommandsBehaviour.UpdateModels();
        }
    }
    
    public class RaceCommandsTickBehaviour
    {
        public event Action<int> OnCooldownEnd;
        public event Action<int> OnCommandEnd;

        private List<RaceCommandModel> _commands;

        public RaceCommandsTickBehaviour(List<RaceCommandModel> commands)
        {
            _commands = commands;
        }

        public void Tick()
        {
            _commands.ForEach(command =>
            {
                if (command.State.Value == RaceCommandState.Common) return;
                
                command.Duration.Value--;
                
                if (command.Duration.Value > 0) return;
                    
                var newState = command.State.Value == RaceCommandState.Active
                    ? RaceCommandState.Cooldown
                    : RaceCommandState.Common;
                
                if (command.State.Value == RaceCommandState.Active)
                    OnCommandEnd?.Invoke(command.ID);
                
                if (newState == RaceCommandState.Common)
                    OnCooldownEnd?.Invoke(command.ID);

                command.State.Value = newState;
            });
        }
    }

    public class RaceBonusesBehaviour
    {
        private Dictionary<BonusType, ObservableInt> _bonuses;
        private Dictionary<BonusType, BonusModel> _models;

        public event Action<Dictionary<BonusType, BonusModel>> OnModelsInitialize;

        public void Initialize(Dictionary<BonusType, int> initData)
        {
            _bonuses = initData.ToDictionary(
                item => item.Key, 
                item => new ObservableInt(item.Value));

            _models = _bonuses.ToDictionary(
                item => item.Key, 
                item => new BonusModel(item.Value.Value.ToString()));
            
            foreach (var (key, value) in _bonuses)
            {
                value.AddSubscriber(_models[key].Value.SetValue);
            }
            
            OnModelsInitialize?.Invoke(_models);
        }

        public bool CanBeChanged(BonusType type, int value)
        {
            if (_bonuses.TryGetValue(type, out var bonus))
            {
                var isNonNegative = bonus + value >= 0;

                return isNonNegative;
            }

            return false;
        }

        public bool ChangeBonus(BonusType type, int value)
        {
            if (_bonuses.TryGetValue(type, out var bonus))
            {
                var isNonNegative = bonus + value >= 0;
                if (isNonNegative)
                    bonus.Value += value;

                return isNonNegative;
            }

            return false;
        }

        public bool ChangeBonus(int id, int value) => ChangeBonus((BonusType) id, value);
    }

    public class RaceCommandsBehaviour
    {
        public event Action<int> OnUseCommand;

        public event Action<Dictionary<int, RaceCommandsModel>> OnModelsInitialize;
        public event Action<int, RaceCommandsModel> OnModelInitialize;

        public Dictionary<int, RaceCommandsContext> Commands { get; private set; } = new Dictionary<int, RaceCommandsContext>();
        public List<RaceCommandModel> CommandModels => _commandsModels.Values.ToList();
        public Dictionary<int, RaceCommandsModel> Models { get; private set; } = new Dictionary<int, RaceCommandsModel>();
        private Dictionary<int, Pair<RaceCommandsContext, RaceCommandsModel>> _pairs = new Dictionary<int, Pair<RaceCommandsContext, RaceCommandsModel>>();

        private Dictionary<int, Dictionary<BonusType, int>> _requirements =
            new Dictionary<int, Dictionary<BonusType, int>>();

        private Dictionary<int, RaceCommandModel> _commandsModels = new Dictionary<int, RaceCommandModel>();
        private Dictionary<int, RaceCommandInfo> _commandsInfo = new Dictionary<int, RaceCommandInfo>();
        private RaceBonusesBehaviour _bonuses;

        public void Initialize(Dictionary<int, RaceCommandsContext> commands, RaceBonusesBehaviour bonuses)
        {
            _bonuses = bonuses;
            commands.ToList().ForEach(item =>
            {
                var (id, ctx) = item;
                
                var model = new RaceCommandsModel()
                {
                    Overtake = ctx.Overtake.ToPairModel(),
                    Tyres = ctx.Tyres.ToPairModel(),
                    Engine = ctx.Engine.ToPairModel()
                };

                model.AsEnumerable().ToList().ForEach(pair =>
                {
                    _commandsModels[pair.First.ID] = pair.First.CommandModel;
                    _commandsModels[pair.Second.ID] = pair.Second.CommandModel;
                });

                _pairs[id] = new Pair<RaceCommandsContext, RaceCommandsModel>(ctx, model);
                
                ctx.AsList().ForEach(pair =>
                {
                    var first = pair.First;
                    var firstID = first.ID;

                    var second = pair.Second;
                    var secondID = second.ID;

                    _requirements[firstID] = first.Requirements;
                    _requirements[secondID] = second.Requirements;
                    _commandsInfo[firstID] = first;
                    _commandsInfo[secondID] = second;
                });
                Models[id] = model;
            });
            
            foreach (var (id, model) in _commandsModels)
            {
                model.State.OnValueChange += state =>
                {
                    if (state == RaceCommandState.Cooldown)
                    {
                        model.Duration.Value = _commandsInfo[id].Cooldown;
                    }
                };
            }
            
            UpdateModels();
            OnModelsInitialize?.Invoke(Models);
            foreach (var (id, model) in Models)
            {
                OnModelInitialize?.Invoke(id, model);
            }
        }

        public void UpdateModels()
        {
            _pairs.Values.ToList().ForEach(pair =>
            {
                var info = pair.First.AsEnumerable().ToList();
                var model = pair.Second.AsEnumerable().ToList();

                for (var i = 0; i < info.Count; i++)
                {
                    CheckRequirementsDone(model[i], info[i]);
                }
            });
        }

        public void UseCommand(int reqID)
        {
            if (_requirements.TryGetValue(reqID, out var requirements))
            {
                requirements.ToList().ForEach(item =>
                {
                    var (key, value) = item;
                    _bonuses.ChangeBonus(key, -value);
                });
                _commandsModels[reqID].State.Value = RaceCommandState.Active;
                _commandsModels[reqID].Duration.Value = _commandsInfo[reqID].Duration;
                OnUseCommand?.Invoke(reqID);
            }
            UpdateModels();
        }

        private void CheckRequirementsDone(RaceCommandPairModel model, RaceCommandPairInfo info)
        {
            var id = model.ID;
            var canBeUsed = model.First.CommandModel.Duration == 0 && model.Second.CommandModel.Duration == 0;
            
            UpdateRequirements(model.First.ButtonData.Requirements, info.First);
            UpdateRequirements(model.Second.ButtonData.Requirements, info.Second);
            
            var firstReqDone = IsRequirementsDone(model.First.ButtonData.Requirements);
            var secondReqDone = IsRequirementsDone(model.Second.ButtonData.Requirements);

            model.First.ButtonData.RequirementsDone.Value = canBeUsed && firstReqDone;
            model.Second.ButtonData.RequirementsDone.Value = canBeUsed && secondReqDone;
        }

        private void UpdateRequirements(List<RequirementModel> requirements, RaceCommandInfo info)
        {
            requirements.ForEach(req =>
            {
                var reqType = (BonusType) req.ID;
                var reqValue = info.Requirements[reqType];
                req.CanBeUsed.Value = _bonuses.CanBeChanged(reqType, -reqValue);
            });
        }

        private bool IsRequirementsDone(List<RequirementModel> requirements) =>
            requirements.TrueForAll(req => req.CanBeUsed);
    }
}