using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.GUI
{
    public class ExampleMenu : GenericMenu<ExampleMenuCell, ExampleMenuData>
    {
        [SerializeField] private List<Sprite> sprites = default;
        
        private void Start()
        {
            Debug.Log("Start");
            CreateTestMenu();
        }

        public void CreateTestMenu()
        {
            foreach (Sprite sprite in sprites)
            {
                Debug.Log("New Entry");
                AddEntry(new ExampleMenuData(sprite, sprite.name));
            }
        }
    }
}
