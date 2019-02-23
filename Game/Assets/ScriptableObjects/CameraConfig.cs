using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "NewCameraConfig", menuName = "New CameraConfig")]
public class CameraConfig : ScriptableObject
{
    [Header("Mouselook")]

    [SerializeField]
    private Vector2 clampInDegrees = new Vector2(360, 180);
    public Vector2 ClampInDegrees { get { return clampInDegrees; } }

    [SerializeField]
    private bool lockCursor;
    public bool LockCursor { get { return lockCursor; } }

    [SerializeField]
    private Vector2 sensitivity = new Vector2(2, 2);
    public Vector2 Sensitivity { get { return sensitivity; } set { sensitivity = value; } }

    [SerializeField]
    private Vector2 smoothing = new Vector2(3, 3);
    public Vector2 Smoothing { get { return smoothing; } }

    [SerializeField]
    private Vector2 targetDirection;
    public Vector2 TargetDirection { get { return targetDirection; } }

    [SerializeField]
    private Vector2 targetCharacterDirection;
    public Vector2 TargetCharacterDirection { get { return targetCharacterDirection; } }

}
