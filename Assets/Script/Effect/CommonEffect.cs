using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEffect : Effects
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestory());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
