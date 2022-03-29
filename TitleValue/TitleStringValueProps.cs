using System;

namespace UIScripts.TitleValue
{
    [Serializable]
    public class TitleStringValueProps : TitleValueProps<string, string>
    {
        public TitleStringValueProps(string title, string value)
        {
            this.title = title;
            this.value = value;
        }
    }
}