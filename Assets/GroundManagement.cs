using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManagement : MonoBehaviour
{
    MeshRenderer[] childMeshes;
    // Start is called before the first frame update
    void Start()
    {
        childMeshes = GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < 8; i++)
        {
            if (childMeshes[i] != null)
            {
                childMeshes[i].material.SetColor("_Color", color);
            }
        }
    }
}
