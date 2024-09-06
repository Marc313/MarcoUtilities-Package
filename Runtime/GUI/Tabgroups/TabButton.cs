using UnityEngine.UI;

namespace MarcoUtilities.GUI.Tabgroups
{
    public class TabButton : Button
    {
        /// <summary>
        /// This functions allows code to simulate pressing the button. Used by TabGroup.
        /// </summary>
        public virtual void SimulateClick()
        {
            Select();
            onClick?.Invoke();
        }
    }
}
