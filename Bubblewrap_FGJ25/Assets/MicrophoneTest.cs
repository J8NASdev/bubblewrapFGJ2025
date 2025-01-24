using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MicrophoneTest : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private AudioClip audioClip;
    private string micName;
    private float recordingTime = 10;
    private bool testDone = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Microphones");
        foreach (string mic in Microphone.devices)
        {
            Debug.Log(mic);
        }
        Debug.Log("end of list");
        micName = Microphone.devices[0];

        Debug.Log("Start recording using " + micName);
        audioClip = Microphone.Start(micName, false, (int)recordingTime, 44100);

    }



    // Update is called once per frame
    void Update()
    {
        if (testDone) return;

        recordingTime -= Time.deltaTime;
        if (recordingTime > 0) return;

        //This happens after the recording time has expired
        Microphone.End(micName);

        Debug.Log("Stoped recoding sound");

        audioSource.clip = audioClip;
        audioSource.Play();
        testDone = true;

    }
}
