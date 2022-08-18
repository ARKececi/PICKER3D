using System;
using Enums;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<StationBoolParams> onStation = delegate { };
        public UnityAction onPlay = delegate { };

        public Func<int> onPoolLevelID = delegate { return 0; };
        
        public UnityAction<IsTouching> onIsTouching = delegate {  };

    }
}