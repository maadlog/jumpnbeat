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
    float previousNoise = 1f;
    float Noise()
    {
        timeskip -= Time.deltaTime;
        if (timeskip < 0)
        {
            previousNoise = Random.Range(0f, 2f);
            timeskip = 1f;
            return previousNoise;
        } else
        {
            return previousNoise;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useBuffer)
        {
            float scale = (AudioPeer.audioBandBuffer[band] * scaleMultiplier /* Noise()*/) + startScale;
            if (float.IsNaN(scale)) { return; }
            transform.localScale = new Vector3(
                transform.localScale.x
                , scale
                , transform.localScale.z
            );

            var color = new Color(AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band], AudioPeer.audioBandBuffer[band]);
            material.SetColor("_EmissionColor", color);
        }
        else
        {
            float scale = (AudioPeer.audioBand[band] * scaleMultiplier) + startScale;
            if (float.IsNaN(scale)) { return; }

            transform.localScale = new Vector3(
                transform.localScale.x
                , scale
                , transform.localScale.z
            );
            var color = new Color(AudioPeer.audioBand[band], AudioPeer.audioBand[band], AudioPeer.audioBand[band]);
            material.SetColor("_EmissionColor", color);
        }

    }
}
