using UnityEngine;

namespace MarcoUtilities.GUI
{
    public enum TooltipDirection
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3
    }

    public static class TooltipDirectionExtensions
    {
        public static Vector2 ToScreenDirection(this TooltipDirection direction)
        {
            return direction switch
            {
                TooltipDirection.Left => Vector2.left,
                TooltipDirection.Right => Vector2.right,
                TooltipDirection.Up => Vector2.up,
                TooltipDirection.Down => Vector2.down,
                _ => Vector2.zero,
            };
        }

        public static TooltipDirection GetOpposite(this TooltipDirection direction)
        {
            if ((int) direction < 0 || (int) direction > 3)
                Debug.LogError($"TooltipDirection.GetOpposite: Direction {direction} not recognized.");

            if ((int) direction % 2 == 1)
                return direction - 1;
            else
                return direction + 1;
        }
    }
}
