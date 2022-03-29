using System;
using UnityEngine;

namespace UIScripts.TitleValue
{
    [Serializable]
    public  class TitleValueProps<TTitle, TValue>
    {
        [SerializeField] protected TTitle title;
        [SerializeField] protected TValue value;

        public TitleValueProps()
        {
        }

        public TitleValueProps(TTitle title, TValue value)
        {
            this.title = title;
            this.value = value;
        }

        public TTitle Title
        {
            get => title;
            set => title = value;
        }

        public TValue Value
        {
            get => value;
            set => this.value = value;
        }
    }
}