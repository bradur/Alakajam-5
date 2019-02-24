using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asd : MonoBehaviour
{
    bool run = true;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
        {
            AudioManager.main.FadeInBossMusic();
            run = false;
        }
        
    }
}
