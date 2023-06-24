using ServiceLocator;

namespace GameState
{
    public interface IGameState : IService
    {
        SettingsStateData SettingsStateData { get; }
        UserStateData UserStateData { get; }
        void SaveState();
    }
}