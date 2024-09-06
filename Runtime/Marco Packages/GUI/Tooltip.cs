using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarcoUtilities.GUI
{
    [RequireComponent(typeof(RectTransform))]
    //[ExecuteInEditMode]
    public class Tooltip : MonoBehaviour
    {
        public RectTransform RectTransform;
        [SerializeField] private TextMeshProUGUI Title;
        [SerializeField] private TextMeshProUGUI Description;
        [SerializeField] private LayoutElement descriptionLayoutElement;
        [SerializeField] private float characterWrapLimit;

        public virtual void SetTitle(string newTitle)
        {
            Title.gameObject.SetActive(!string.IsNullOrEmpty(newTitle));
            Title.text = newTitle;
        }

        public virtual void SetDescription(string newDescription)
        {
            Description.text = newDescription;
            bool shouldUseLayoutElement = newDescription.Length > characterWrapLimit;
            descriptionLayoutElement.enabled = shouldUseLayoutElement;
        }

        // For testing in the inspector
        //private void Update()
        //{
        //    bool shouldUseLayoutElement = Description.text.Length > characterWrapLimit;
        //    descriptionLayoutElement.enabled = shouldUseLayoutElement;
        //}
    }
}
