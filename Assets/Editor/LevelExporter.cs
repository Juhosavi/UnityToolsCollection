using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

public class LevelExporter : EditorWindow
{
    [MenuItem("Tools/Level Exporter")]
    public static void ShowWindow()
    {
        GetWindow<LevelExporter>("Level Exporter");
    }

    private string exportFileName = "leveldata.json";
    private string targetTag = "Collectible";

    void OnGUI()
    {
        GUILayout.Label("Export level data to JSON", EditorStyles.boldLabel);
        exportFileName = EditorGUILayout.TextField("Export File Name", exportFileName);
        targetTag = EditorGUILayout.TextField("Target Tag", targetTag);

        if (GUILayout.Button("Export"))
        {
            ExportLevelData();
        }
    }

    void ExportLevelData()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag);
        List<ExportedObjectData> exportedData = new List<ExportedObjectData>();

        foreach (GameObject obj in objects)
        {
            exportedData.Add(new ExportedObjectData
            {
                name = obj.name,
                position = obj.transform.position,
                rotation = obj.transform.eulerAngles
            });
        }

        string json = JsonHelper.ToJson(exportedData.ToArray(), true);
        string path = Path.Combine(Application.dataPath, "ExportedData", exportFileName);

        File.WriteAllText(path, json);
        Debug.Log($"Exported {exportedData.Count} objects to {path}");
    }

    [System.Serializable]
    public class ExportedObjectData
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
    }
}
