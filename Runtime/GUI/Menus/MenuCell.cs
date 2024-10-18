using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.GUI.Menus
{
    /// <summary>
    /// Use this menu cell if you do not mind specifying the class of data you are expecting, 
    /// or if you do not need to cast MenuData at all.
    /// </summary>
    public abstract class MenuCell : GenericMenuCell<MenuData>
    {
        public T2 GetCastedMenuData<T2>() where T2 : MenuData
        {
            return MenuData as T2;
        }
    }

    // Note: This has not been finished yet!

    /// <summary>
    /// This menu also supports menu's that have different cells.
    /// </summary>
    public abstract class MultiCelledMenu : MonoBehaviour
    {
        List<MenuCell> cells;

        public MenuCell GetMenuCellAtIndex()
        {
            return cells[0];
        }

        public T GetMenuCellAtIndex<T>() where T : MenuData
        {
            return cells[0] as T;
        }
    }
}
