using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class setfps : MonoBehaviour
    {
        private void Start()
        {
            [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
            static void InitFrameRate()
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
            }
        }
        void update()
        {
        
        }
    }
}