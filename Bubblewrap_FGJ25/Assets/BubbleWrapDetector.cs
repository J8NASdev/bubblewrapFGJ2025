using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleWrapDetector : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private string micName;
    [SerializeField] private int frequencyOfBubbleWrap = 44100; //The frequency that we record the sound at.
    [SerializeField] private int numberOfSamples = 32; //How many samples we need. This number needs to be a power of 2. 
    [SerializeField] private int soundDuration = 1; //The duration in seconds for the desired sound.
    [SerializeField] private float okSoundDiff = .001f; //How much can each specific sound sample differ from the profile to count as a success.
    [SerializeField] private float successPercentageRequired = .8f; //How many of the sound samples need to be a success for it to count as bubble wrap sound.


    private float[] samples; 
    private float[] bubbleWrapProfile; 
    void Start()
    {
        samples = new float[numberOfSamples];
        bubbleWrapProfile = new float[numberOfSamples];

        //Get and list of the names of all available microphones
        string listMics = "Microphones:\n";
        foreach (string mic in Microphone.devices)
        {
            listMics += mic + "\n";
        }
        Debug.Log(listMics);


        //Sets up a microphone to start streaming sound to an audio clip which is played by the audio source on this game object.
        micName = Microphone.devices[0];
        Debug.Log("Start recording using " + micName);
        audioSource.loop = true;
        audioSource.clip = Microphone.Start(micName, true, soundDuration, frequencyOfBubbleWrap);
        while(!(Microphone.GetPosition(null) > 0)) {}
        audioSource.Play();
    }

    void Update()
    {
        audioSource.GetOutputData(samples, 0); //This creates a sample of the sound from the microphone that we can use to detect bubble wrap


        //Pressing space down sets the bubble wrap profile. This profile is what the system thinks bubble wrap will sound like.
        //This has some weird delays, but does kind of work.'
        //TODO We need to replace this with a better system for recording and saving the profile.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.GetOutputData(bubbleWrapProfile, 0);
        }

        //Checking every frame if is bubble wrap. This is here for testing.
        if(IsBubbleWrapSound(samples, bubbleWrapProfile, okSoundDiff, successPercentageRequired)) Debug.Log("Success");

        //Pressing B down will stop the microphone and replay the bubble wrap profile on loop. You need to restart the game after this. This is just here for testing.
        if(Input.GetKeyDown(KeyCode.B))
        {
            Microphone.End(micName);
            audioSource.clip.SetData(bubbleWrapProfile, 0);
        }
    }

    /// <summary>
    /// This method compares the sound sampled form the microphone with the bubble wrap profile. It returns true if it matches within the bounds.
    /// This sorts the arrays of samples and profile samples and then compare them. If enough samples are close to the profile then it return a success (true).
    /// </summary>
    /// <param name="samples"> The sound samples form the microphone
    /// <param name="bubbleWrapProfile"> The samples of the bubble wrap profile
    /// <paramref name="okSoundDiff"/> The maximum difference between an instance of the sample and the profile to count as a success
    /// <paramref name="successPercentageRequired"/> How large percentage of the samples that need to be successes for the method to succeed. A value between 0 and 1 when 1 is 100% of the samples.
    /// <returns></returns>
    bool IsBubbleWrapSound(float[] samples, float [] bubbleWrapProfile, float okSoundDiff, float successPercentageRequired)
    {
        List<float> tempSample = samples.ToList<float>();
        List<float> tempProfile = bubbleWrapProfile.ToList<float>();

        tempSample.Sort();
        tempProfile.Sort();
        int succeeded = 0;
        int failed = 0;

        for (int i = 0; i < numberOfSamples; i++)
        {
            if(Mathf.Abs(tempProfile[i]) < okSoundDiff || Mathf.Abs(tempProfile[i]) < okSoundDiff) continue;

            float diff = Mathf.Abs(tempProfile[i] - tempSample[i]);
            if(diff < okSoundDiff) succeeded++;
            else failed++;
        }

        float successRate = (float)succeeded/((float)failed+succeeded);
        //Debug.Log("Test success rate: " + (successRate*100));
        return  successRate > successPercentageRequired;
    }

    //I need to go to bed now, it is reeeeaally late :/ 
    //Hope this helps.
    //TODO sleep
}
