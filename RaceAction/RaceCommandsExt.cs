using System.Collections.Generic;
using System.Linq;
using UIScripts.Screens.V2.BonusRequirement;

namespace UIScripts.RaceAction
{
    public static class RaceCommandsExt
    {
        public static IEnumerable<RaceCommandPairModel> AsEnumerable(this RaceCommandsModel model)
        {
            return new List<RaceCommandPairModel>()
            {
                model.Overtake,
                model.Tyres,
                model.Engine
            };
        }

        public static IEnumerable<RaceCommandPairInfo> AsEnumerable(this RaceCommandsContext ctx)
        {
            return new List<RaceCommandPairInfo>()
            {
                ctx.Overtake,
                ctx.Tyres,
                ctx.Engine
            };
        }
        
        public static List<RaceCommandPairInfo> AsList(this RaceCommandsContext ctx) => new List<RaceCommandPairInfo>()
        {
            ctx.Overtake,
            ctx.Tyres,
            ctx.Engine
        };

        public static RaceCommandPairModel ToPairModel(this RaceCommandPairInfo info)
        {
            return new RaceCommandPairModel()
            {
                ID = info.ID,
                Title = info.Title,
                First = info.First.ToButtonModel(),
                Second = info.Second.ToButtonModel()
            };
        }

        public static RaceCommandButtonModel ToButtonModel(this RaceCommandInfo info)
        {
            var buttonModel = new RaceCommandButtonModel()
            {
                ID = info.ID,
                CommandModel = info.ToCommandModel(),
                ButtonData = info.ToButtonData()
            };

            return buttonModel;
        }

        public static RaceCommandModel ToCommandModel(this RaceCommandInfo info)
        {
            var commandModel = new RaceCommandModel()
            {
                ID = info.ID,
                Duration =
                {
                    Value = 0,
                },
                State =
                {
                    Value = RaceCommandState.Common
                }
            };

            return commandModel;
        }

        public static RequirementButtonData ToButtonData(this RaceCommandInfo info)
        {
            var buttonData = new RequirementButtonData()
            {
                Requirements = info.Requirements.Select(item =>
                    new RequirementModel()
                    {
                        ID = (int) item.Key,
                        Value = item.Value.ToString(),
                        Icon = App.runtime.SpriteSelector.GetSprite(item.Key)
                    }
                ).ToList()
            };

            return buttonData;
        }
    }
}