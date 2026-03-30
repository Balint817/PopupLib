using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupLib.UI.Windows.Interfaces
{
    public interface IListWindow
    {
        public event SelectionChangedHandler? OnSelectionChanged;

        public delegate void SelectionChangedHandler(IListWindow window, int objectIndex);
    }
}
