using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MicrophoneTest : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private string micName;
    [SerializeField] private int frequency = 44100; //The frequency that we record the sound at.
    [SerializeField] private float[] samples; //The length of this array needs to be a power of 2
 
    void Start()
    {
        string listMics = "Microphones:\n";
        foreach (string mic in Microphone.devices)
        {
            listMics += mic + "\n";
        }
        Debug.Log(listMics);

        micName = Microphone.devices[0];
        Debug.Log("Start recording using " + micName);
        audioSource.loop = true;
        audioSource.clip = Microphone.Start(micName, true, 10, frequency);
        while(!(Microphone.GetPosition(null) > 0)) {}
        audioSource.Play();
    }


    void Update()
    {
        audioSource.GetOutputData(samples, 0);
    }
}
