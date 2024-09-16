using MarcoUtilities.GUI.Menus;
using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.Samples.Menus
{
    public class ExampleMenu : GenericMenu<ExampleMenuCell, ExampleMenuData>
    {
        [SerializeField] private List<Sprite> sprites = default;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                CreateTestMenu();        
        }

        public void CreateTestMenu()
        {
            Clear();

            foreach (Sprite sprite in sprites)
                AddEntry(new ExampleMenuData(sprite, sprite.name));
        }
    }
}
