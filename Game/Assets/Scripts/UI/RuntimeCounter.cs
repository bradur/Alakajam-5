using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeCounter : MonoBehaviour
{
    [SerializeField]
    private Runtime runtimeObject;
    [SerializeField]
    private Text timer;
    [SerializeField]
    private bool showFinal = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showFinal)
        {
            TimeSpan t = TimeSpan.FromSeconds(runtimeObject.LastValue - runtimeObject.Offset);
            timer.text = String.Format("{0:D2}:{1:D2},{2:D3}",
                t.Minutes,
                t.Seconds,
                t.Milliseconds);
        }
        else
        {
            if (runtimeObject.Running)
            {
                TimeSpan t = TimeSpan.FromSeconds(runtimeObject.Value - runtimeObject.Offset);
                timer.text = String.Format("{0:D2}:{1:D2},{2:D3}",
                    t.Minutes,
                    t.Seconds,
                    t.Milliseconds);
            }
            else if (runtimeObject.LastValue < 0)
            {
                runtimeObject.LastValue = runtimeObject.Value;
            }
        }
    }
}
