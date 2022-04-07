using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string dontDestroyObjectTag;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(dontDestroyObjectTag);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

//        Debug.Log("Object " + gameObject.name + " destroyed.");
        DontDestroyOnLoad(this.gameObject);
    }
}
