// Date   : 22.02.2019 23:21
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class BezierCurve : MonoBehaviour
{

    private Vector3[] points;

    private Vector3 lineStart;

    private LineRenderer line;

    private GameConfig gameConfig;
    private PlayerConfig playerConfig;

    private Transform[] debugPoints;

    private Vector2 currentOffset;
    private Vector2 originalOffset = Vector2.zero;
    private Material lineMaterial;

    void Start()
    {
        Initialize();
    }

    private void Update() {
        currentOffset.x -= playerConfig.OffsetAnimationSpeed * Time.deltaTime;
        lineMaterial.SetTextureOffset("_MainTex", currentOffset);
    }

    public void Initialize()
    {
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        playerConfig = ConfigManager.main.GetConfig("PlayerConfig") as PlayerConfig;
        line = GetComponent<LineRenderer>();
        line.positionCount = playerConfig.CurveSteps;
        line.startColor = playerConfig.CurveColor;
        line.endColor = playerConfig.CurveColor;
        lineMaterial = line.material;
        currentOffset = originalOffset;
        lineMaterial.SetTextureOffset("_MainTex", currentOffset);
    }

    public void Enable() {
        line.enabled = true;
    }

    public void Disable() {
        line.enabled = false;
    }

    public void SetPoints(Vector3[] newPoints) {
        points = newPoints;
        DebugPoints();
        DrawLine();
    }

    void DebugPoints() {
        if (gameConfig.VisualDebug) {
            if (debugPoints == null) {
                debugPoints = new Transform[3] {
                    Instantiate(playerConfig.TeleportPointDebugPrefab),
                    Instantiate(playerConfig.TeleportPointDebugPrefab),
                    Instantiate(playerConfig.TeleportPointDebugPrefab)
                };
            }
            debugPoints[0].position = points[0];
            debugPoints[0].name = string.Format("P1 {0}", points[0]);
            debugPoints[1].position = points[1];
            debugPoints[1].name = string.Format("P2 {0}", points[1]);
            debugPoints[2].position = points[2];
            debugPoints[2].name = string.Format("P3 {0}", points[2]);
        }
    }

    public Vector3 GetPoint(float t)
    {
        //return transform.TransformPoint(LerpPoint(points[0], points[1], points[2], t));
        return LerpPoint(points[0], points[1], points[2], t);
    }

    private Vector3 LerpPoint(Vector3 point0, Vector3 point1, Vector3 point2, float time)
    {
        return Vector3.Lerp(
            Vector3.Lerp(point0, point1, time),
            Vector3.Lerp(point1, point2, time),
            time
        );
    }

    private void DrawLine()
    {
        lineStart = GetPoint(0f);
        for (int index = 0; index < playerConfig.CurveSteps; index++)
        {
            Vector3 lineEnd = GetPoint(index / (float)playerConfig.CurveSteps);
            if (gameConfig.VisualDebug) {
                Debug.DrawLine(lineStart, lineEnd, Color.red);
            }
            line.SetPosition(index, lineEnd);
            lineStart = lineEnd;
        }
    }
}
