using Additional;

namespace Menu.MainScreen
{
    public class MenuController
    {
        private readonly MenuView _menuView;

        public MenuController(MenuView menuView)
        {
            _menuView = menuView;
       
            _menuView.PlayButton.OnButtonClicked += OnPlayButtonClicked;
        }

        public void Dispose()
        {
            _menuView.PlayButton.OnButtonClicked -= OnPlayButtonClicked;
        }

        private void OnPlayButtonClicked() => 
            SceneLoader.LoadScene(SceneLoader.GAME_SCENE_INDEX);
        
    }
}