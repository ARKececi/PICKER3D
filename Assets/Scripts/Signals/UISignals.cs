using Enums;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction onFail =delegate { };
        public  UnityAction onPoolEnable = delegate {  };
    }
}