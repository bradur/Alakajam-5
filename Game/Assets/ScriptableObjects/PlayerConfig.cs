// Date   : 29.12.2018 12:52
// Project: VillageCaveGame
// Author : bradur

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "New PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Teleport/Curve")]
    [SerializeField]

    private bool drawCurve = true;
    public bool DrawCurve { get { return drawCurve; } }

    private int curveSteps = 10;
    public int CurveSteps { get { return curveSteps; } }

    [SerializeField]
    private BezierCurve curvePrefab;
    public BezierCurve CurvePrefab { get { return curvePrefab; } }

    [SerializeField]
    private SpriteRenderer teleportAreaPrefab;
    public SpriteRenderer TeleportAreaPrefab { get { return teleportAreaPrefab; } }

    [SerializeField]
    private float offsetAnimationSpeed = 2f;
    public float OffsetAnimationSpeed { get { return offsetAnimationSpeed; } }

    [SerializeField]
    private float curveHeight = 2f;
    public float CurveHeight { get { return curveHeight; } }

    [SerializeField]
    private Color curveColor = Color.cyan;
    public Color CurveColor { get { return curveColor; } }

    [Header("Teleport/Properties")]
    [SerializeField]
    private float maxDistance = 10f;
    public float MaxDistance { get { return maxDistance; } }

     [SerializeField]
    private Color teleportAreaColorAllowed = Color.green;
    public Color TeleportAreaColorAllowed { get { return teleportAreaColorAllowed; } }

    [SerializeField]
    private Color teleportAreaColorUnallowed = Color.red;
    public Color TeleportAreaColorUnallowed { get { return teleportAreaColorUnallowed; } }

    [SerializeField]
    private float minDistance = 2f;
    public float MinDistance { get { return minDistance; } }

    [Header("Teleport/Debug")]
    [SerializeField]
    private Color debugRayColorOnHit = Color.green;
    public Color DebugRayColorOnHit { get { return debugRayColorOnHit; } }

    [SerializeField]
    private Color debugRayColor = Color.white;
    public Color DebugRayColor { get { return debugRayColor; } }

    [SerializeField]
    private Transform teleportPointDebugPrefab;
    public Transform TeleportPointDebugPrefab { get { return teleportPointDebugPrefab; } }

}
