using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MarcoUtilities.Editor
{
    internal class SceneShortcutWindow : EditorWindow
    {
        private SceneShortcutsSO sceneShortcuts;

        [MenuItem("Window/Helpers/SceneShortcuts")]
        private static void ShowWindow()
        {
            GetWindow<SceneShortcutWindow>();
        }

        private void OnGUI()
        {
            if (sceneShortcuts == default)
                CacheValue();

            CreateSceneButtons();

            if (GUILayout.Button("Refresh"))
                CacheValue();
        }

        private void CreateSceneButtons()
        {
            foreach (SceneShortcutEntry shortcut in sceneShortcuts.sceneShortcutEntries)
            {
                if (shortcut.isHeader)
                    GUILayout.Label(shortcut.Name);
                else
                {
                    if (GUILayout.Button(shortcut.Name))
                        EditorSceneManager.OpenScene(shortcut.scenePath);
                }
            }
        }

        // Load from resources
        private void CacheValue()
        {
            sceneShortcuts = Resources.Load<SceneShortcutsSO>("SceneShortcutsSO");
        }
    }
}
