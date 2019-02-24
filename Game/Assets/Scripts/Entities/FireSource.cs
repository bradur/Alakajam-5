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
    private bool interactable = true;
    public bool Interactable { get { return interactable; } }

    [SerializeField]
    private bool isLevelEnd = false;
    public bool IsLevelEnd { get { return isLevelEnd; } }

    [SerializeField]
    private bool isLevelStart = false;
    public bool IsLevelStart { get { return isLevelStart; } }


    [SerializeField]
    private GameObject fire;

    [SerializeField]
    private GameObject light;

    [SerializeField]
    private GameObject torch;

    private FireConfig fireConfig;

    private TeleportPosition teleportPosition;
    public Vector3 TeleportPosition { get { return teleportPosition.transform.position; } }

    private GameConfig gameConfig;

    [SerializeField]
    private float range = 2f;

    [HideInInspector]
    public float distanceToCursor;

    public float Range { get { return range; } }
    private SpriteRenderer debugRenderer;

    void Start()
    {
        teleportPosition = GetComponentInChildren<TeleportPosition>();
        Debug.Log(teleportPosition);
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
        if (isLevelStart) {
            GameObject player = Instantiate(gameConfig.PlayerPrefab);
            player.transform.position = TeleportPosition;
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
        if (interactable)
        {
            isLit = false;
            if (fire != null)
            {
                fire.SetActive(false);
            }
            light.SetActive(false);
        }
    }

    public void Light()
    {
        if (interactable)
        {
            isLit = true;
            if (fire != null)
            {
                fire.SetActive(true);
            }
            light.SetActive(true);
        }
    }
}
