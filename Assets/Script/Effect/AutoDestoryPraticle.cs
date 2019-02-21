using UnityEngine;
using System.Collections;

public class AutoDestoryPraticle : MonoBehaviour
{
    private ParticleSystem ps;


    public void Start()
    {
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        if (ps == null) {
            ps = gameObject.GetComponent<ParticleSystem>();
        }
    }

    public void Update()
    {
        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}