using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered" + this.name);
        //collision.other.GetComponent<PlayerController>?.SetPlatform(this.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited" + this.name);
        //collision.other.GetComponent<PlayerController>?.RemovePlatform();
    }

    Vector3 normal = new Vector3(0, 1, 0);
    public Vector3 GetNormal()
    {
        return this.transform.up;
        
    }

   
}
