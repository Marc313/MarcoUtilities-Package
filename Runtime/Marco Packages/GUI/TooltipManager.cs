using MarcoUtilities.DesignPatterns;
using UnityEngine;

namespace MarcoUtilities.GUI
{
    public class TooltipManager : Singleton<TooltipManager>
    {
        // Dictionary<TooltipPrefab, TooltipPrefab>
        [SerializeField] private Tooltip defaultTooltipPrefab;
        private Tooltip currentTooltip;

        private void Awake()
        {
            Instance = this;
            currentTooltip = defaultTooltipPrefab;
            currentTooltip.gameObject.SetActive(false);
        }

        public Tooltip Display(string title, string description)
        {
            currentTooltip.SetTitle(title);
            currentTooltip.SetDescription(description);
            currentTooltip.gameObject.SetActive(true);
            return currentTooltip;
        }

        public void Hide()
        {
            currentTooltip.gameObject.SetActive(false);
        }

        public void Reposition(Vector3 newPosition)
        {
            currentTooltip.RectTransform.position = newPosition;
        }
    }
}
