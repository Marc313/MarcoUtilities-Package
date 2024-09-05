using MarcoUtilities.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarcoUtilities.GUI
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
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TooltipPrefab tooltipPrefabOverride;
        [SerializeField] private string startTitle;
        [SerializeField] [TextArea] private string startDescription;
        [SerializeField] private bool allowOverflow;
        [field: SerializeField] private TooltipDirection tooltipDirection;
        [SerializeField] private bool followMouse;

        private TooltipPrefab tooltipInstance;
        private RectTransform rectTransform;
        private bool isInPointer;

        private void Awake()
        {
            if (tooltipPrefabOverride == null)
                throw new NotImplementedException("Tooltip: Assinging a tooltipPrefabOverride is required! " +
                    "A default tooltip has not yet been implemented.");

            rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!isInPointer) return;

            // Make the tooltip follow the mouse
            if (followMouse)
                tooltipInstance.RectTransform.position += Input.mousePositionDelta;
            if (!allowOverflow)
                HandleOverflow();
        }

        public void SetTitle(string newTitle)
        {
            tooltipInstance.Title.text = newTitle;
        }

        public void SetDescription(string newDescription)
        {
            tooltipInstance.Description.text = newDescription;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isInPointer = true;

            if (!tooltipInstance)
                InstantiateTooltip();
            else
                tooltipInstance.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isInPointer = false;

            tooltipInstance.gameObject.SetActive(false);
        }

        public void InstantiateTooltip()
        {
            tooltipInstance = Instantiate(tooltipPrefabOverride, transform);
            tooltipInstance.Title.text = startTitle;
            tooltipInstance.Description.text = startDescription;

            Canvas.ForceUpdateCanvases();
            PositionTooltip();
        }

        public void PositionTooltip()
        {
            Vector2 offsetVector = GetOffsetVector();
            Debug.Log($"OffsetVector: {offsetVector}");
            Vector2 tooltipOffset = new(offsetVector.x * tooltipDirection.ToScreenDirection().x, offsetVector.y * tooltipDirection.ToScreenDirection().y);
            Vector3 initialPosition = followMouse ? Input.mousePosition : rectTransform.position;
            tooltipInstance.RectTransform.position = initialPosition + tooltipOffset.ToVector3();
        }

        public Vector2 GetOffsetVector()
        {
            // Size of the TooltipPrefab and the this object.
            return new Vector2(
                tooltipInstance.RectTransform.rect.size.x + rectTransform.rect.width,
                tooltipInstance.RectTransform.rect.size.y + rectTransform.rect.height) / 2;
        }

        public void HandleOverflow()
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
