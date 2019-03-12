// Date   : 24.02.2019 17:24
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimateTextList : MonoBehaviour
{

    /*[SerializeField]
    private Color fadeInColor;

    [SerializeField]
    private Color fadeOutColor;*/

    [SerializeField]
    private Text txtTarget;

    [TextArea]
    [SerializeField]
    private List<string> textList;

    private int currentText = 0;

    [SerializeField]
    private float textFadeTime = 0.2f;

    [SerializeField]
    private bool isGameEnd = false;
    public bool IsGameEnd { get { return isGameEnd; } }

    [SerializeField]
    private GameObject destroyThis;
    public GameObject DestroyThis { get { return destroyThis; } }

    [SerializeField]
    private Runtime runtime;

    private float currentAlpha;

    private LevelConfig levelConfig;

    private void Start()
    {
        levelConfig = ConfigManager.main.GetConfig("LevelConfig") as LevelConfig;
        FadeInText();
        if (isGameEnd)
        {
            runtime.Running = false;
        }
    }

    public void FadeOutText()
    {
        currentAlpha = 1f;
        StartCoroutine(FadeOut());
    }

    public void FadeInText()
    {
        if (currentText < textList.Count)
        {
            txtTarget.text = textList[currentText];
            currentAlpha = 0f;
            currentText += 1;
            StartCoroutine(FadeIn());
        }
        else
        {
            if (isGameEnd)
            {
                gameObject.SetActive(false);
                if (destroyThis != null) {
                    destroyThis.SetActive(false);
                }
            }
            else
            {
                levelConfig.CurrentSceneNumber = 1;
                levelConfig.LoadingNextScene = true;
                
                runtime.Running = true;
                runtime.Offset = Time.time;
                runtime.LastValue = -1;
            }
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, 0f);
            FadeInText();
        }
    }

    IEnumerator FadeIn()
    {
        float startAlpha = txtTarget.color.a;
        while (currentAlpha < 1f)
        {
            currentAlpha += Time.unscaledDeltaTime / textFadeTime;
            txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, currentAlpha);
            yield return null;
        }
        FadeOutText();
    }

    IEnumerator FadeOut()
    {
        float startAlpha = txtTarget.color.a;
        while (currentAlpha > 0)
        {
            currentAlpha -= startAlpha * Time.unscaledDeltaTime / textFadeTime;
            txtTarget.color = new Color(txtTarget.color.r, txtTarget.color.g, txtTarget.color.b, currentAlpha);
            yield return null;
        }
        FadeInText();
    }
}
