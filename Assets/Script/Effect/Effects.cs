using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effects : MonoBehaviour {
    [SerializeField]
    public float effectTime = -1;
    public string effectName;
    public ParticleSystem particle;

    public virtual void PlayParticle() {
        if (!particle.isPlaying)
        {
            particle.Play();
        }
    }

    public IEnumerator AutoDestory() {
        if (effectTime > 0) {
            yield return new WaitForSeconds(effectTime);
            Destroy(this.gameObject);
        }
        yield return null;
    }

    public void Free()
    {
        Destroy(this.gameObject);
    }

}
