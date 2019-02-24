// Date   : 24.02.2019 07:03
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager main;

    private LevelConfig levelConfig;

    void Awake()
    {
        main = this;
    }


    void Start()
    {
        levelConfig = ConfigManager.main.GetConfig("LevelConfig") as LevelConfig;
        if (levelConfig.CurrentScene == levelConfig.FirstScene)
        {
            levelConfig.CurrentSceneNumber = 0;
        }
        else
        {
            levelConfig.CurrentSceneNumber = levelConfig.CurrentScene.buildIndex;
        }
    }

    public void LoadNextScene()
    {
        if (levelConfig.CurrentSceneIsLast)
        {
            Debug.Log("The end!");
        }
        else
        {
            levelConfig.CurrentSceneNumber += 1;
            levelConfig.LoadingNextScene = true;
        }
    }

    public void ReloadScene() {
        SceneManager.LoadScene(levelConfig.CurrentSceneNumber);
    }
}
