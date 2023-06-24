using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.MapsShop
{
    public class MapItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _lockImage;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private GameObject _priceBox;
        [SerializeField] private TMP_Text _mapNameText;
        
        public void SetLock(bool status)
        {
            _lockImage.enabled = status;
            _priceBox.SetActive(status);
        }

        public void SetPrice(int price) => 
            _priceText.text = price.ToString();

        public void SetIcon(Sprite icon) => 
            _icon.sprite = icon;

        public void SetText(string text) =>
            _mapNameText.text = text;
    }
}