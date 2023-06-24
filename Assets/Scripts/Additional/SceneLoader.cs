using UnityEngine.SceneManagement;

namespace Additional
{
    public static class SceneLoader
    {
        public const int MENU_SCENE_INDEX = 1;
        public const int GAME_SCENE_INDEX = 2;
        
        public static void LoadScene(int index) => 
            SceneManager.LoadScene(index);
    }
}