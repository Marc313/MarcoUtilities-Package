using UnityEngine;

namespace MarcoUtilities.GUI.Menus
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GenericMenuCell<T> : MonoBehaviour where T : MenuData
    {
        protected T MenuData { get; private set; }

        public void SetData(T data)
        {
            MenuData = data;
            UpdateVisuals();
        }

        /// <summary>
        /// Meant to be called when data has changed, so that the cell can update all of its visuals appropriately.
        /// </summary>
        public abstract void UpdateVisuals();
    }

    /// <summary>
    /// Data to be linked to an entry of a menu. 
    /// For example: When displaying an Item Menu, MenuData holds information about 1 specific weapon,
    /// and the menu cell will display this data to the player, using text or images.
    /// </summary>
    public class MenuData
    {
        public MenuData() { }
    }
}
