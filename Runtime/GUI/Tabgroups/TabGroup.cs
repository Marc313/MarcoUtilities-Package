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
        [SerializeField] private List<TabButton> buttons;
        [SerializeField] private List<GameObject> tabPages;
        [SerializeField] private int startingPageIndex = 0;

        private int currentSelectedIndex;
        private GameObject currentSelectedPage;

        protected virtual void Start()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                TabButton button = buttons[i];
                int index = i;  // For some reason I need to copy the int here.
                button.onClick.AddListener(() => OnTabSelected(index));
            }

            // Select the starting page.
            buttons[startingPageIndex].SimulateClick();
        }

        protected virtual void OnDestroy()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                TabButton button = buttons[i];
                button.onClick.RemoveAllListeners();
            }
        }

        protected virtual void OnTabSelected(int index)
        {
            if (currentSelectedPage != null)
                currentSelectedPage.SetActive(false);

            currentSelectedIndex = index;
            currentSelectedPage = tabPages[index];
            currentSelectedPage.SetActive(true);
        }

        protected virtual void Reset()
        {
            buttons[startingPageIndex].SimulateClick();
        }

        // For testing reset functionality!
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Alpha1))
        //        Reset();
        //}
    }
}
