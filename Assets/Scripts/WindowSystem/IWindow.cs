namespace WindowSystem
{
    public interface IWindow
    {
        bool IsVisible { get; }
        void Show();
        void Hide();
    }
}