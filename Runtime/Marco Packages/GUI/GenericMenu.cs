using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.GUI
{
    public abstract class GenericMenu<T1, T2> : MonoBehaviour
        where T1 : GenericMenuCell<T2>
        where T2 : MenuData
    {
        [SerializeField] private Transform contentContainer;
        [SerializeField] private T1 cellPrefab;

        private List<T1> cells;

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

        private void CreateCellsListIfNull()
        {
            cells ??= new List<T1>();
        }
    }
}
