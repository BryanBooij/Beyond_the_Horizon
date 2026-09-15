using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class SetFPS : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void InitFrameRate()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }
    }
}