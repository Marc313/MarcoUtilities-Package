using MarcoUtilities.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarcoUtilities.GUI.Tooltips
{
    /// <summary>
    /// A flexible Tooltip. Allows choosing a direction the tooltip will be offsetted in. 
    /// The tooltip will also resize when a longer description is added.
    /// When the tooltip is offscreen, it will try to change the direction it is offsetted in. 
    /// After 1 attempt to get the tooltip on screen, it will give up.
    /// Success of this script also relies on the structure of the prefab for the tooltip that is provided.
    /// 
    /// Optimization TODO: Now there are multiple tooltips being instantiated.
    /// In an ideal optimized world, we use the TooltipManager to instantiate and place the tooltip.
    /// Then we only need one tooltip rather than 1 per cell.
    /// </summary>
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string title;
        [SerializeField] [TextArea] private string description;
        [SerializeField] private Tooltip tooltipPrefabOverride;

        [Header("Extras")]
        [field: SerializeField] private TooltipDirection tooltipDirection;
        [SerializeField] private bool followMouse;
        [SerializeField] private float additionalOffset;
        [SerializeField] private bool allowOverflow;

        private Tooltip tooltipInstance;
        private RectTransform rectTransform;
        private bool isInPointer;

        private void Awake()
        {
            if (tooltipPrefabOverride != null)
                throw new NotImplementedException("Tooltip: Using a tooltipPrefabOverride is not implemented yet! ");

            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!isInPointer) return;

            // Make the tooltip follow the mouse
            if (!allowOverflow)
                HandleOverflow();
            if (followMouse)
                TooltipManager.Instance.Reposition(tooltipInstance.RectTransform.position + Input.mousePositionDelta);
        }

        public void SetTitle(string newTitle)
        {
            title = newTitle;
        }

        public void SetDescription(string newDescription)
        {
            description = newDescription;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isInPointer = true;

            tooltipInstance = TooltipManager.Instance.Display(title, description);
            PositionTooltip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isInPointer = false;

            TooltipManager.Instance.Hide();
        }

        private void PositionTooltip()
        {
            Canvas.ForceUpdateCanvases();
            Vector2 offsetVector = GetOffsetVector();
            Vector2 tooltipOffset = new(offsetVector.x * tooltipDirection.ToScreenDirection().x, offsetVector.y * tooltipDirection.ToScreenDirection().y);
            Vector3 initialPosition = followMouse ? Input.mousePosition : rectTransform.position;
            TooltipManager.Instance.Reposition(initialPosition + tooltipOffset.ToVector3());
        }

        private Vector2 GetOffsetVector()
        {
            // Size of the TooltipPrefab and the this object.
            return new Vector2(
                tooltipInstance.RectTransform.rect.size.x + rectTransform.rect.width,
                tooltipInstance.RectTransform.rect.size.y + rectTransform.rect.height) / 2
                + new Vector2(additionalOffset, additionalOffset);
        }

        private void HandleOverflow()
        {
            Vector3[] corners = new Vector3[4];
            List<TooltipDirection> directionsOffScreen = new();
            tooltipInstance.RectTransform.GetWorldCorners(corners);

            // Check each corner of the RectTransform
            foreach (Vector3 corner in corners)
            {
                //Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, corner);
                if (corner.x < 0 && !directionsOffScreen.Contains(TooltipDirection.Left))
                    directionsOffScreen.Add(TooltipDirection.Left);
                if (corner.x > Screen.width && !directionsOffScreen.Contains(TooltipDirection.Right))
                    directionsOffScreen.Add(TooltipDirection.Right);
                if (corner.y < 0 && !directionsOffScreen.Contains(TooltipDirection.Down))
                    directionsOffScreen.Add(TooltipDirection.Down);
                if (corner.y > Screen.height && !directionsOffScreen.Contains(TooltipDirection.Up))
                    directionsOffScreen.Add(TooltipDirection.Up);
            }

            if (directionsOffScreen.Count >= 1)
            {
                // Allow Overflow after we already tried to remedy the situation.
                allowOverflow = true;
                tooltipDirection = directionsOffScreen[0].GetOpposite();
                PositionTooltip();
            }
        }
    }
}
