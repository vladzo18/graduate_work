using UnityEngine;

namespace GameState
{
    [CreateAssetMenu(fileName = "DefaultGameState", menuName = "DefaultGameState", order = 0)]
    public class DefaultGameStateData : ScriptableObject
    {
        public SettingsStateData settingsStateData;
        public UserStateData userStateData;
    }
}