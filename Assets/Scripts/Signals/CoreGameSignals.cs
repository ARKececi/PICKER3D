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
        public UnityAction onReset = delegate { };
        public UnityAction onWin =delegate { };
        public UnityAction<WinLevelParams> onWinLevelID = delegate { };
        public UnityAction onCameraMovePosition = delegate { };
        public UnityAction onLoaderLevel = delegate { };
        public UnityAction onClearLevel = delegate { };

        public Func<int> onPoolLevelID = delegate { return 0; };
        
        public UnityAction<IsTouching> onIsTouching = delegate {  };

    }
}