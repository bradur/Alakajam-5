// Date   : 22.02.2019 23:05
// Project: Game
// Author : bradur

using UnityEngine;
using System.Collections;

public class TeleportCaster : MonoBehaviour
{

    private PlayerConfig playerConfig;
    private GameConfig gameConfig;

    private BezierCurve curve;
    private SpriteRenderer teleportArea;

    void Start()
    {
        playerConfig = ConfigManager.main.GetConfig("PlayerConfig") as PlayerConfig;
        gameConfig = ConfigManager.main.GetConfig("GameConfig") as GameConfig;
        InitializeCurve();
        teleportArea = Instantiate(playerConfig.TeleportAreaPrefab);
        teleportArea.transform.SetParent(transform.parent);
        teleportArea.color = playerConfig.CurveColor;
    }

    void InitializeCurve() {
        if (playerConfig.DrawCurve && curve == null) {
            curve = Instantiate(playerConfig.CurvePrefab);
            curve.transform.SetParent(transform);
            curve.Initialize();
        } else if(curve != null) {
            if (playerConfig.DrawCurve) {
                curve.Enable();
            } else {
                curve.Disable();
            }
        }
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 endPoint = transform.TransformDirection(Vector3.forward);
        bool rayHitSomething = CastRay(endPoint, out hit);
        if (rayHitSomething)
        {
            Vector3 point = hit.point;
            DrawCurve(point);
            DrawTeleportArea(point);
        }
        else
        {
            Vector3 point = transform.position + transform.forward * playerConfig.MaxDistance;
            point.y = 0f;
            DrawCurve(point);
            DrawTeleportArea(point);
        }
        DebugRayCast(hit, rayHitSomething);
    }

    bool CastRay(Vector3 endPoint, out RaycastHit hit)
    {
        return Physics.Raycast(
            transform.position,
            endPoint,
            out hit,
            playerConfig.MaxDistance,
            playerConfig.CollideMask
        );
    }

    void DrawTeleportArea(Vector3 endPoint) {
        Vector3 teleportAreaPosition = endPoint;
        teleportAreaPosition.y = -0.49f;
        teleportArea.transform.position = teleportAreaPosition;
    }

    void DrawCurve(Vector3 endPoint)
    {
        InitializeCurve();
        if (playerConfig.DrawCurve) { 
            float y = 0f;
            Vector3 startPoint = new Vector3(transform.position.x, y, transform.position.z);
            endPoint.y = -1f;
            Vector3 midPoint = (endPoint + startPoint) / 2f;
            midPoint.y = playerConfig.CurveHeight;
            Vector3[] points = new Vector3[] {
                startPoint,
                //transform.TransformPoint(midPoint),
                midPoint,
                endPoint
            };
            curve.SetPoints(points);
        }
    }

    void DebugRayCast(RaycastHit hit, bool rayHitSomething)
    {
        if (gameConfig.VisualDebug)
        {
            Debug.DrawRay(
                transform.position,
                transform.TransformDirection(Vector3.forward) * (rayHitSomething ? hit.distance : playerConfig.MaxDistance),
                rayHitSomething ? playerConfig.DebugRayColorOnHit : playerConfig.DebugRayColor
            );
        }
    }
}
