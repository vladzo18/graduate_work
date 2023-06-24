using Save;
using UnityEngine;

namespace GameState
{
    public class GameState : IGameState
    {
        private const string DefaultGameStateFileName = "DefaultGameState";
        private static readonly string SettingsStateSaveDataKey = $"{nameof(SettingsStateData)}Key";
        private static readonly string UserStateSaveDataKey = $"{nameof(UserStateData)}Key";

        private DefaultGameStateData _defaultGameStateData;

        private ObjectPref<SettingsStateData> _settingsState;
        private ObjectPref<UserStateData> _userState;

        private SettingsStateData _settingsStateData;
        private UserStateData _userStateData;

        public SettingsStateData SettingsStateData => 
            _settingsStateData ??= _settingsState.Get();
        
        public UserStateData UserStateData => 
            _userStateData ??= _userState.Get();

        public GameState()
        {
            _defaultGameStateData = Resources.Load<DefaultGameStateData>(DefaultGameStateFileName);
            _settingsState = new ObjectPref<SettingsStateData>(SettingsStateSaveDataKey, _defaultGameStateData.settingsStateData);
            _userState = new ObjectPref<UserStateData>(UserStateSaveDataKey, _defaultGameStateData.userStateData);
        }
        
        public void SaveState()
        {
            _settingsState.Set(SettingsStateData);
            _userState.Set(UserStateData);
        }
    }
}