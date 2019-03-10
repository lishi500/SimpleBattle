using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffUtils : MonoBehaviour
{
    private static BuffUtils _instance;
    public static BuffUtils Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public GameObject createBuff(string buffName, ) {

    }
}
