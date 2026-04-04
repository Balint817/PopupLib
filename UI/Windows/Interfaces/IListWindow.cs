namespace PopupLib.UI.Windows.Interfaces
{
    public interface IListWindow
    {
        event SelectionChangedHandler? OnSelectionChanged;

        delegate void SelectionChangedHandler(IListWindow window, int objectIndex);
    }
}
