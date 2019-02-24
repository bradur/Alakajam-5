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

    private GameConfig gameConfig;

    void Awake()
    {
        main = this;
    }


    GameObject menu;

    void Start()
    {
        levelConfig = ConfigManager.main.GetConfig("LevelConfig") as LevelConfig;
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        if (levelConfig.CurrentScene == levelConfig.FirstScene)
        {
            levelConfig.CurrentSceneNumber = 0;
        }
        else
        {
            levelConfig.CurrentSceneNumber = levelConfig.CurrentScene.buildIndex;
        }
        if (levelConfig.CurrentSceneIsLast)
        {
            AudioManager.main.FadeInBossMusic();
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

    void Update()
    {
        if (KeyManager.main.GetKeyDown(PlayerAction.Restart))
        {
            levelConfig.RestartingScene = true;
        }
        if (KeyManager.main.GetKeyDown(PlayerAction.Quit))
        {
            if (menu == null)
            {
                menu = GameObject.FindGameObjectWithTag("Menu");
            }
            if (menu == null)
            {
                menu = Instantiate(gameConfig.MenuPrefab);
            }
            else
            {
                menu.SetActive(!menu.activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && menu != null && menu.activeSelf)
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(levelConfig.CurrentSceneNumber);
    }
}
