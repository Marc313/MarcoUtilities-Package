using MarcoUtilities.GUI.Menus;
using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.Samples.Menus
{
    public class ExampleMenu : GenericMenu<ExampleMenuCell, ExampleMenuData>
    {
        [SerializeField] private List<Sprite> sprites = default;
        
        private void Start()
        {
            CreateTestMenu();
        }

        public void CreateTestMenu()
        {
            foreach (Sprite sprite in sprites)
                AddEntry(new ExampleMenuData(sprite, sprite.name));
        }
    }
}
