// Date   : 24.02.2019 07:03
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BurnScreen : MonoBehaviour
{

    private LevelConfig levelConfig;
    private PlayerHandConfig handConfig;

    private Animator animator;

    void Start()
    {
        levelConfig = ConfigManager.main.GetConfig("LevelConfig") as LevelConfig;
        handConfig = ConfigManager.main.GetConfig("PlayerHandConfig") as PlayerHandConfig;
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (levelConfig.RestartingScene) {
            animator.SetTrigger("Burn");
            levelConfig.RestartingScene = false;
            handConfig.triggerSnap = true;
            AudioManager.main.PlaySound(SoundType.RestartSnap);
        }
    }

    public void Finished()
    {
        LevelManager.main.ReloadScene();
    }
}
