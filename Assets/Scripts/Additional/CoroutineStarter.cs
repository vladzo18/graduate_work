using System.Collections;
using UnityEngine;

namespace Additional
{
    public sealed class CoroutineStarter : MonoBehaviour
    {
        private static CoroutineStarter _instance;
        
        private static CoroutineStarter Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("[COROUTINE STARTER]");
                    _instance = go.AddComponent<CoroutineStarter>();
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        public static Coroutine Start(IEnumerator routine) => 
            Instance.StartCoroutine(routine);

        public static void Stop(Coroutine routine) => 
            Instance.StopCoroutine(routine);
    }
}