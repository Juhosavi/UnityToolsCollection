using UnityEditor;
using UnityEngine;

public class MissingScriptFinder : EditorWindow
{
    [MenuItem("Tools/Missing Script Finder")]
    public static void ShowWindow()
    {
        GetWindow<MissingScriptFinder>("Missing Script Finder");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts In Scene"))
        {
            FindMissingScriptsInScene();
        }
    }

    private void FindMissingScriptsInScene()
    {
        GameObject[] allObjects = Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"Missing script on GameObject: {obj.name}", obj);
                    count++;
                }
            }
        }

        if (count == 0)
            Debug.Log("No missing scripts found in the scene!");
        else
            Debug.Log($"Total missing scripts found: {count}");
    }
}
