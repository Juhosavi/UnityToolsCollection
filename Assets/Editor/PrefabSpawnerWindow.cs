using UnityEngine;
using UnityEditor;

public class PrefabSpawnerWindow : EditorWindow
{
    GameObject prefabToSpawn;

    Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(0, 0, 0),
        new Vector3(2, 0, 0),
        new Vector3(0, 0, 2),
        new Vector3(-2, 0, 0)
    };

    [MenuItem("Tools/Prefab Spawner")]
    public static void ShowWindow()
    {
        GetWindow<PrefabSpawnerWindow>("Prefab Spawner");
    }

    private void OnGUI()
    {
        GUILayout.Label("Spawn a Prefab into the Scene", EditorStyles.boldLabel);

        prefabToSpawn = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToSpawn, typeof(GameObject), false);

        if (prefabToSpawn != null)
        {
            for (int i = 0; i < spawnPositions.Length; i++)
            {
                if (GUILayout.Button($"Spawn at Position {i + 1} ({spawnPositions[i]})"))
                {
                    SpawnPrefab(spawnPositions[i]);
                }
            }
        }
    }

    private void SpawnPrefab(Vector3 position)
    {
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefabToSpawn);
        instance.transform.position = position;
        Undo.RegisterCreatedObjectUndo(instance, "Spawn Prefab");
        Selection.activeGameObject = instance;
    }
}
