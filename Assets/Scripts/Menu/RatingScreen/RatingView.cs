using UIElements;
using UnityEngine;

namespace Menu.RatingScreen
{
    public class RatingView : MonoBehaviour
    {
        [SerializeField] private ViewType _viewType;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIButtonView _backButton;
        [SerializeField] private RatingItemView _ratingItemViewPrefab;
        [SerializeField] private Transform _contentTransform;

        private int _counter;
        
        public UIButtonView BackButton => _backButton;

        public void SetVisibility(bool status) => 
            _canvas.enabled = status;

        public void AddRatingItem(int metersAmount)
        {
            RatingItemView item = Instantiate(_ratingItemViewPrefab, _contentTransform);
            item.SetItemValue(++_counter, metersAmount);
        }
    }
}