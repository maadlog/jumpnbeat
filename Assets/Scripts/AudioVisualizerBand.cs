using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualizerBand : MonoBehaviour
{
    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    float timeskip = 1f;
    float previousScale = 1f;

    public bool timed = false;
    public float scalingSpeed = 1f;
    public float timeskipBase = 2f;
    // Update is called once per frame
    void Update()
    {
        float scale;
        Color color;
        if (useBuffer)
        {
            scale = (AudioPeer.audioBandBuffer[band] * scaleMultiplier /* Noise()*/) + startScale;
            color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
        }
        else
        {
            scale = (AudioPeer.audioBand[band] * scaleMultiplier) + startScale;
            color = new Color(AudioPeer.audioBand[band], AudioPeer.audioBand[band], AudioPeer.audioBand[band]);
        }
        if (float.IsNaN(scale)) { return; }
        if (timed)
        {
            timeskip -= Time.deltaTime;
            if (timeskip < 0)
            {
                previousScale = scale;
                timeskip = timeskipBase;
            } else
            {
                scale = previousScale;
            }
            var desired = new Vector3(
                transform.localScale.x
                , scale
                , transform.localScale.z
            );
            transform.localScale = Vector3.MoveTowards(transform.localScale, desired, scalingSpeed * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector3(
                transform.localScale.x
                , scale * scalingSpeed
                , transform.localScale.z
            );
        }
        
        

        material.SetColor("_EmissionColor", color);
    }
}
