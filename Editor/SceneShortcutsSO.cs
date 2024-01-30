using UnityEngine;

namespace MarcoUtilities.Editor
{
    [CreateAssetMenu(menuName = "Scriptable Objects/SceneShortcuts")]
    public class SceneShortcutsSO : ScriptableObject
    {
        public SceneShortcutEntry[] sceneShortcutEntries;
    }

    [System.Serializable]
    public struct SceneShortcutEntry
    {
        public bool isHeader;
        public string Name;
        public string scenePath;
    }
}
