using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeManager : MonoBehaviour
{
    [SerializeField]
    private Runtime runtimeTimer;
    public static RuntimeManager main;

    void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (runtimeTimer.Running)
        {
            runtimeTimer.Value = Time.time;
        }
    }

    public Runtime GetRuntime()
    {
        return runtimeTimer;
    }
}
