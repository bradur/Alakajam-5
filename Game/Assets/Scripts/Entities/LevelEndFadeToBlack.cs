// Date   : 24.02.2019 07:03
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelEndFadeToBlack : MonoBehaviour
{

    private LevelConfig levelConfig;

    private Animator animator;

    void Start()
    {
        levelConfig = ConfigManager.main.GetConfig("LevelConfig") as LevelConfig;
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (levelConfig.LoadingNextScene) {
            levelConfig.LoadingNextScene = false;
            animator.SetTrigger("Fade");
        }
    }

    public void FadeFinished() {
        SceneManager.LoadScene(levelConfig.CurrentSceneNumber);
    }
}
