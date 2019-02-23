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
        debugRenderer.transform.localScale = scale;
    }

}

public class FireSource : MonoBehaviour
{

    private FireConfig fireConfig;
    private GameConfig gameConfig;

    [SerializeField]
    private float range = 2f;

    [HideInInspector]
    public float distanceToCursor;

    public float Range { get { return range; } }
    private SpriteRenderer debugRenderer;

    private bool isLit;
    public bool IsLit { get { return isLit; } }

    void Start()
    {
        fireConfig = ConfigManager.main.GetConfig("FireConfig") as FireConfig;
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        debugRenderer = GetComponentInChildren<SpriteRenderer>();
        FireSourceManager.main.AddFireSource(this);
    }

    void Update()
    {
        debugRenderer.enabled = gameConfig.VisualDebug;
    }

    public void Extinguish()
    {
        isLit = true;
    }

    public void Light()
    {
        isLit = false;
    }
}
