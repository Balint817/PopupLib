using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopupLib.UI.Windows.Interfaces
{
    public interface IResultWindow<T>
    {
        T Result { get; }
    }
}
