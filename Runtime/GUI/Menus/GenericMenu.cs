using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.GUI.Menus
{
    public abstract class GenericMenu<T1, T2> : MonoBehaviour
        where T1 : GenericMenuCell<T2>
        where T2 : MenuData
    {
        [SerializeField] private Transform contentContainer;
        [SerializeField] private T1 cellPrefab;

        protected List<T1> cells;

        public void AddEntry(T2 newEntry)
        {
            CreateCellsListIfNull();

            T1 newCell = Instantiate(cellPrefab, contentContainer);
            newCell.SetData(newEntry);
            cells.Add(newCell);
        }

        public T1 GetMenuCellAtIndex(int index)
        {
            if (index < 0 || index >= cells.Count)
                throw new System.IndexOutOfRangeException("Menu.GetMenuCellAtIndex(): " +
                    $"tried to access cell {index}, while there are only {cells.Count} cells!");

            return cells[index];
        }

        // TODO: Implement Object Pooling.
        public void Clear()
        {
            CreateCellsListIfNull();
            for (int i = cells.Count - 1; i >= 0; i--)
                Destroy(cells[i]);

            cells.Clear();
        }

        private void CreateCellsListIfNull()
        {
            cells ??= new List<T1>();
        }
    }
}
