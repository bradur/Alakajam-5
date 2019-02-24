// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


[CreateAssetMenu(fileName = "NewLevelConfig", menuName = "New LevelConfig")]
public class LevelConfig : ScriptableObject
{
    public int NumberOfScenes { get { return SceneManager.sceneCountInBuildSettings; } }

    private Scene currentScene;
    public Scene CurrentScene { get { return SceneManager.GetActiveScene(); } }

    public Scene FirstScene { get { return SceneManager.GetSceneByBuildIndex(0); } }

    [SerializeField]
    private int currentSceneNumber;
    public int CurrentSceneNumber { get { return currentSceneNumber; } set { currentSceneNumber = value < NumberOfScenes && value >= 0 ? value : 0; } }

    public bool CurrentSceneIsLast { get { return CurrentScene.name == "boss"; } }

    public bool LoadingNextScene = false;

    public bool RestartingScene = false;

}
