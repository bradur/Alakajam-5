// Date   : 23.02.2019 09:43
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

using UnityEditor;

[CustomEditor(typeof(FireSource))]
public class FireSourceEditor : Editor
{
    private FireSource fireSource;

    private SpriteRenderer debugRenderer;

    public void OnSceneGUI()
    {
        fireSource = this.target as FireSource;
        debugRenderer = fireSource.GetComponentInChildren<SpriteRenderer>();
        Vector3 scale = new Vector3(fireSource.Range * 2, fireSource.Range * 2, 1f);
        Vector3 pos = debugRenderer.transform.position;
        pos.y = 0.1f;
        debugRenderer.transform.position = pos;
        debugRenderer.transform.localScale = scale;
    }

}

public class FireSource : MonoBehaviour
{

    [SerializeField]
    private bool isLit = true;
    public bool IsLit { get { return isLit; } }

    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private GameObject torch;

    private FireConfig fireConfig;
    private GameConfig gameConfig;

    [SerializeField]
    private float range = 2f;

    [HideInInspector]
    public float distanceToCursor;

    public float Range { get { return range; } }
    private SpriteRenderer debugRenderer;

    void Start()
    {
        fireConfig = ConfigManager.main.GetConfig("FireConfig") as FireConfig;
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        debugRenderer = GetComponentInChildren<SpriteRenderer>();
        Vector3 pos = debugRenderer.transform.position;
        pos.y = 0.1f;
        debugRenderer.transform.position = pos;
        FireSourceManager.main.AddFireSource(this);
        if (!isLit)
        {
            Extinguish();
        }
    }

    void Update()
    {
        if (debugRenderer != null)
        {
            debugRenderer.enabled = gameConfig.VisualDebug && isLit;
        }
    }

    public void Extinguish()
    {
        isLit = false;
        fire.SetActive(false);
    }

    public void Light()
    {
        isLit = true;
        fire.SetActive(true);
    }
}
