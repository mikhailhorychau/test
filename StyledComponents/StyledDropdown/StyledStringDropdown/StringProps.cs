using System;

namespace UIScripts
{
    [Serializable]
    public class StringProps : IProps
    {
        public int id;
        public string value;

        public StringProps(int id, string value)
        {
            this.id = id;
            this.value = value;
        }

        public int GetId() => id;
        
        
    }
}