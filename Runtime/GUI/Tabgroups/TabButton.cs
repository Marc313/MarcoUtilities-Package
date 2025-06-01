using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarcoUtilities.GUI.Tabgroups
{
    public class TabButton : Selectable, IPointerClickHandler
    {
        public event Action OnClick;

        public Color normalColor;
        public Color highlightColor;
        public Color selectedColor;
        public Color disabledColor;
        public bool isInteractable = true;

        protected bool isSelected;

        protected override void Start()
        {
            transition = Selectable.Transition.None;
            base.Start();
            if (!isInteractable)
                targetGraphic.color = disabledColor;
        }

        /// <summary>
        /// This functions allows code to simulate pressing the button. 
        /// Used by TabGroup.
        /// </summary>
        public virtual void SimulateClick()
        {
            Select();
            OnClick?.Invoke();
        }

        public void SetSelected(bool toggle)
        {
            if (!isInteractable)
                return;

            isSelected = toggle;
            if (isSelected)
                targetGraphic.color = selectedColor;
            else
                targetGraphic.color = normalColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isInteractable)
                OnClick?.Invoke();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            if (!isSelected && isInteractable)
                targetGraphic.color = highlightColor;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            if (!isSelected && isInteractable)
                targetGraphic.color = normalColor;
        }
    }
}
