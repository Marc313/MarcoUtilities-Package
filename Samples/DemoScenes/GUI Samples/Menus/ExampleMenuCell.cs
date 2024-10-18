using MarcoUtilities.GUI.Menus;
using UnityEngine;
using UnityEngine.UI;

namespace MarcoUtilities.Samples.Menus
{
    public class ExampleMenuCell : GenericMenuCell<ExampleMenuData>
    {
        [SerializeField] private Image exampleImage;
        [SerializeField] private Text  exampleText;

        public override void UpdateVisuals()
        {
            exampleImage.sprite = MenuData.ExampleSprite;
            exampleText.text = MenuData.ExampleText;
        }
    }

    public class ExampleMenuData : MenuData
    {
        public Sprite ExampleSprite { get; private set; }
        public string ExampleText { get; private set; }

        public ExampleMenuData(Sprite sprite, string text) : base()
        {
            ExampleSprite = sprite;
            ExampleText = text;
        }
    }
}
