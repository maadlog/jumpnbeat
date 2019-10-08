using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityZone : MonoBehaviour
{
    public Vector3 gravity;
    // Start is called before the first frame update
    void Start()
    {
        InitializeGravity();
    }
    
    private void InitializeGravity()
    {
        gravity = this.GetComponentInParent<GroundManagement>().transform.up.normalized * -1 * Physics.gravity.magnitude;
    }

    internal Vector3 GetGravity()
    {
        return gravity;
    }

}
