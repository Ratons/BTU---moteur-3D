using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneData
{
    public string nameScene;
    public string path;
    public bool isSceneActive;
    public bool isNextFrame;

    public SceneData(string NameScene, string Path, bool IsSceneActive, bool IsNextFrame)
    {
        nameScene = NameScene;
        path = Path;
        isSceneActive = IsSceneActive;
        isNextFrame = IsNextFrame;
    }
    ~SceneData()
    {

    }

}

public class ScenesManager : EditorWindow
{
    private List<SceneData> listeSceneData = new List<SceneData>();

    [MenuItem("Tools / Scenes manager")]
    
    static void ShowWindow()
    {
        ScenesManager window = (ScenesManager)EditorWindow.GetWindow(typeof(ScenesManager), true, "My Empty Window");
    }

    private void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label("hello world!");
        if (GUILayout.Button("click"))
            Debug.Log("The cake is a lie");
        GUILayout.EndVertical();
    }

    void RefreshContent()
    {
        listeSceneData.Clear();
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            listeSceneData[i].path = EditorBuildSettings.scenes[i].path;
            listeSceneData[i].nameScene = System.IO.Path.GetFileNameWithoutExtension(listeSceneData[i].path);
            listeSceneData[i].isSceneActive = false;
            listeSceneData[i].isNextFrame = false;
        }
        for (int j = 0; j < EditorSceneManager.sceneCount; j++)
        {
            listeSceneData[EditorSceneManager.GetSceneAt(j).buildIndex].isSceneActive = true;
            listeSceneData[EditorSceneManager.GetSceneAt(j).buildIndex].isNextFrame = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


