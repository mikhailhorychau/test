using System;
using System.Collections.Generic;

namespace UIScripts.CommonComponents.BetterTabContainer
{
    public class NumericTabController : IDisposable
    {
        public event Action OnBecameEmpty;
        public event Action<int> OnSelectionChanged;

        private int _count = 0;
        private int _currentNumber = 0;
        private TabGroup _currentGroup = null;
        private Dictionary<int, TabGroup> _groups = new Dictionary<int, TabGroup>();

        public int CurrentNumber => _currentNumber;
        public TabGroup CurrentGroup => _currentGroup;

        public void AddGroup(TabGroup group)
        {
            _count++;
            _groups.Add(_count, group);
        }

        public void SetActivityByNumber(int number)
        {
            if (_groups.Count == 0)
                return;
            
            if (_groups.TryGetValue(number, out var group) && group != _currentGroup)
            {
                group.Button.Select();

                _currentNumber = number;
                OnSelectionChanged?.Invoke(number);
            }
        }

        public void RemoveGroup(TabGroup group)
        {
            
        }

        public void RemoveGroupByNumber(int number)
        {
            if (number > _groups.Count) return;
            
            
        }
            
        public void Dispose()
        {
            foreach (var groupsValue in _groups.Values)
            {
                groupsValue.Dispose();
            }
        }
    }

    public class TabGroup : IDisposable
    {
        public ITabButton Button { get; }
        public ITabContainer Container { get; }

        public TabGroup(ITabButton button, ITabContainer container)
        {
            Button = button;
            Container = container;

            button.OnSelectionChanged += container.SetActivity;
        }

        public void Dispose()
        {
            Button.OnSelectionChanged -= Container.SetActivity;
        }
    }

    public interface ITabButton
    {
        public event Action<bool> OnSelectionChanged;

        public void Select();
    }

    public interface ITabContainer
    {
        public void SetActivity(bool isActive);
        public void Show();
        public void Hide();
    }
}