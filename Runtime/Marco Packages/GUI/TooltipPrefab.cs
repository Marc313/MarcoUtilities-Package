using TMPro;
using UnityEngine;

namespace MarcoUtilities.GUI
{
    [RequireComponent(typeof(RectTransform))]
    public class TooltipPrefab : MonoBehaviour
    {
        public RectTransform RectTransform;
        public TextMeshProUGUI Title;
        public TextMeshProUGUI Description;
    }
}
