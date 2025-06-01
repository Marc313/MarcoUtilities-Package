using System;
using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.GUI.Tabgroups
{
    /// <summary>
    /// Base class for a tabgroup. For more complex behaviour when selecting a tab,
    /// feel free to override OnTabSelected() in a child class.
    /// </summary>
    public class TabGroup : MonoBehaviour
    {
        public event Action<int> OnTabSelectedEvent;

        [SerializeField] private List<TabButton> buttons;
        [SerializeField] private List<GameObject> tabPages;
        [SerializeField] private int startingPageIndex = 0;

        private Action tabSelectedListener;
        private int currentSelectedIndex;
        private GameObject currentSelectedPage;

        protected virtual void Start()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                TabButton button = buttons[i];
                int index = i;  // For some reason I need to copy the int here.
                tabSelectedListener = () => OnTabSelected(index);
                button.OnClick += tabSelectedListener;
            }

            // Select the starting page.
            buttons[startingPageIndex].SimulateClick();
        }

        protected virtual void OnDestroy()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                TabButton button = buttons[i];
                button.OnClick -= tabSelectedListener;
            }
        }

        protected virtual void OnTabSelected(int index)
        {
            if (currentSelectedPage != null)
                currentSelectedPage.SetActive(false);

            currentSelectedIndex = index;
            currentSelectedPage = tabPages[index];
            currentSelectedPage.SetActive(true);

            SelectButtonWithIndex(index);

            OnTabSelectedEvent?.Invoke(currentSelectedIndex);
        }

        protected virtual void Reset()
        {
            buttons[startingPageIndex].SimulateClick();
        }

        private void SelectButtonWithIndex(int index)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                TabButton button = buttons[i];
                button.SetSelected(i == index);
            }
        }

        // For testing reset functionality!
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //        Reset();
        //}
    }
}
