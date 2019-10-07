using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    static AudioSource audioSource;

    public static float[] samples = new float[512];
    public float[] freqBand = new float[8];
    public float[] freqBandHighest = new float[8];
    public float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    //float[] bufferIncrease = new float[8];

    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public static void Pause()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InitBufferRatios();
    }
    void InitBufferRatios()
    {
        for (int i = 0; i < 8; i++)
        {
            bufferDecrease[i] = 0.005f;
       //     bufferIncrease[i] = 0.01f;
        }
    }

    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying)
        {
            timer += Time.deltaTime;
        }
        if (timer >= audioSource.clip.length)
        {
            GameManager.Instance.FinishGame();
        }
        
        GetSpectrumAudioSource();
        
        MakeFrequencyBars();
        BandBuffer();
        CreateAudioBands();
        
        
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBand[i];
            }
            audioBand[i] = freqBand[i] / freqBandHighest[i];
            audioBandBuffer[i] = bandBuffer[i] / freqBandHighest[i];
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            /*if (Math.Abs(freqBand[g] - bandBuffer[g]) < 0.0000001f)
            {
                return;
            }*/
            if (freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g]; //+= bufferIncrease[g];
                bufferDecrease[g] = 0.005f;
                //bufferIncrease[g] *= 1.2f;
            }
            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
                //bufferIncrease[g] = 0.005f;
            }
        }
    }

    void MakeFrequencyBars()
    {
        /*
            22050 Hz / 512 = 43Hz per sample

            20 - 60
            60 - 250
            250 - 500
            500 - 2000
            2000 - 4000
            4000 - 6000
            6000 - 20000

        Bands
        0 - 2 samples = 86hz -- 0-86
        1 - 4 = 172hz -- 87-258
        2 - 8 = 344hz  ..
        3 - 16 = 688hz ..
        4 - 32 = 1376hz ..
        5 - 64 = 2752hz ..
        6 - 128 = 5504hz ..
        7 - 256 = 11008hz ..

         */
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = Convert.ToInt32(Mathf.Pow(2, i + 1));
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            freqBand[i] = average * 10; //*10 because average is near 0
        }
    }
}
