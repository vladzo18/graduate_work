using Gameplay.VehicleSystem;
using TMPro;
using UnityEngine;

namespace Gameplay.HudSystem
{
    public class ItemCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private ItemsCollector _itemsCollector;

        private int _current;
        
        private void OnDestroy() => 
            _itemsCollector.OnItemsCollected -= OnItemCollidedHandler;

        public void Initialize(ItemsCollector itemsCollector)
        {
            _itemsCollector = itemsCollector;
            _itemsCollector.OnItemsCollected += OnItemCollidedHandler;
        }

        private void OnItemCollidedHandler(int amount)
        {
            _current += amount;
            _text.text =_current.ToString();
        }
    }
}