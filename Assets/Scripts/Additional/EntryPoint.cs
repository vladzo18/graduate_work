using AudioSystem;
using Gameplay.PauseSystem;
using GameState;
using ServiceLocator;
using UnityEngine;

namespace Additional
{
    public class EntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Locator.Inctance.Register<IAudioSystem>(new AudioManager());
            Locator.Inctance.Register<IPauseSystem>(new PauseSystem());
            Locator.Inctance.Register<IGameState>(new GameState.GameState());
        }

        private void Start() => 
            SceneLoader.LoadScene(SceneLoader.MENU_SCENE_INDEX);
    }
}