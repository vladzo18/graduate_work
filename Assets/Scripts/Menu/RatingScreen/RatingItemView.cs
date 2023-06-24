using TMPro;
using UnityEngine;

namespace Menu.RatingScreen
{
    public class RatingItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _numberText;
        [SerializeField] private TMP_Text _pointsText;

        public void SetItemValue(int number, int metersAmount)
        {
            _numberText.text = number.ToString();
            _pointsText.text = metersAmount.ToString();
        }
    }
}