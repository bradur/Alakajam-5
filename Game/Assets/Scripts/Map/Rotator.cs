using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1f;
    [SerializeField]
    private bool rotateClockwise = true;

    private bool stopped = false;

    // Start is called before the first frame update
    void Start()
    {
        stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        //float oldAngle = 0f;
        //Vector3 axis = Vector3.up;
        //transform.rotation.ToAngleAxis(out oldAngle, out axis);
        if (!stopped)
        {
            float dir = rotateClockwise ? 1 : -1;
            transform.Rotate(Vector3.up, rotationSpeed * dir * Time.deltaTime);
        }
        //Debug.Log(oldAngle);
    }

    public void Stop()
    {
        stopped = true;
    }
}
