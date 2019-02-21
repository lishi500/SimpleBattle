using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrippingEffect : Effects
{

    private void Awake()
    {
        particle = gameObject.GetComponentInChildren<ParticleSystem>();
    }


    // Use this for initialization
    void Start () {
        StartCoroutine(Free());
    }
	
	// Update is called once per frame
	void Update () {
    }
}
