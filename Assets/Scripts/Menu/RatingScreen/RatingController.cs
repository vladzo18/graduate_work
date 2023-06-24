using GameState;
using ServiceLocator;

namespace Menu.RatingScreen
{
    public class RatingController
    {
        private readonly RatingView _ratingView;
        private readonly IGameState _gameState;
        
        public RatingController(RatingView ratingView)
        {
            _ratingView = ratingView;
            _gameState = Locator.Inctance.GetService<IGameState>();
        }

        public void Initialize()
        {
            foreach (var score in _gameState.UserStateData.Rating.RatingScores)
                _ratingView.AddRatingItem(score);
        }
        
        public void Dispose() { }
    }
}