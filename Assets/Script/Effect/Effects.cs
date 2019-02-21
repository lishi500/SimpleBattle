using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour {
    [SerializeField]
    public float effectTime;
    public ParticleSystem particle;

    public virtual void PlayParticle() {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Free()
    {
        yield return new WaitForSeconds(effectTime);
        Destroy(this.gameObject);
    }

}
