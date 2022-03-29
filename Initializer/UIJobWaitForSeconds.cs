using System.Collections;
using UnityEngine;

namespace UIScripts.Initializer
{
    public class UIJobWaitForSeconds : UIJob
    {
        private float _delay;

        public UIJobWaitForSeconds(float seconds) : base(() => {})
        {
            _delay = seconds;
        }

        public override IEnumerator Execute()
        {
            yield return new WaitForSeconds(_delay);
        }
    }
}