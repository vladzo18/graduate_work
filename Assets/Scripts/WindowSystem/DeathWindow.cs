using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WindowSystem
{
    public class DeathWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _metersAmountText;
        [SerializeField] private TMP_Text _pointsAmountText;
        [SerializeField] private Button _backToMenuButton;

        public event Action OnBackButtonClicked;
        
        private void OnEnable() => 
            _backToMenuButton.onClick.AddListener((() => OnBackButtonClicked?.Invoke()));

        private void OnDisable() =>
            _backToMenuButton.onClick.RemoveAllListeners();
        
        public void SetMetersAmount(int amount) =>
            _metersAmountText.text = $"Meters amount: {amount}";
        
        public void SetPointsAmount(int amount) =>
            _pointsAmountText.text = $"Points amount: {amount}";
    }
}