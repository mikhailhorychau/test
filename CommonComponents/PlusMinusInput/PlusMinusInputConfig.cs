using System;

namespace UIScripts.CommonComponents.PlusMinusInput
{
    [Serializable]
    public struct PlusMinusInputConfig
    {
        public int value;
        public int min;
        public int max;
        public string postfix;

        public PlusMinusInputConfig(int value, int min, int max)
        {
            this.value = value;
            this.min = min;
            this.max = max;
            this.postfix = "";
        }

        public PlusMinusInputConfig(int value, int min, int max, string postfix)
        {
            this.value = value;
            this.min = min;
            this.max = max;
            this.postfix = postfix;
        }
    }
}