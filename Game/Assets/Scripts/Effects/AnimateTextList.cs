// Date   : 24.02.2019 17:24
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AnimateTextList : MonoBehaviour {

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

    private float currentAlpha;

    private LevelConfig levelConfig;

    private void Start() {
        levelConfig = ConfigManager.main.GetConfig("LevelConfig") as LevelConfig;
        FadeInText();
    }

    public void FadeOutText() {
        currentAlpha = 1f;
        StartCoroutine(FadeOut());
    }

    public void FadeInText() {
        if (currentText < textList.Count) {
            txtTarget.text = textList[currentText];
            currentAlpha = 0f;
            currentText += 1;
            StartCoroutine(FadeIn());
        } else {
            levelConfig.CurrentSceneNumber = 1;
            levelConfig.LoadingNextScene = true;
        }
    }

    void Update() {
        if (Input.anyKeyDown) {
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
