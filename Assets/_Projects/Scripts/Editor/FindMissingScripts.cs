using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts")]
    public static void ShowWindow()
    {
        GetWindow<FindMissingScripts>("Find Missing Scripts");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in Scene"))
        {
            FindInScene();
        }
    }

    private static void FindInScene()
    {
        GameObject[] allObjects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        List<GameObject> objectsWithMissingScripts = new();

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            foreach (Component component in components)
            {
                if (component == null)
                {
                    objectsWithMissingScripts.Add(go);
                    Debug.LogError($"Missing script found on: {go.name}", go);
                    break;
                }
            }
        }

        if (objectsWithMissingScripts.Count == 0)
        {
            Debug.Log("No missing scripts found in scene");
        }
    }
}