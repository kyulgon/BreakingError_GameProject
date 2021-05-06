using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource myBb;
    public AudioSource myEe;
    public AudioSource myNn;


    public AudioClip myP;
    public AudioClip myQ;

    public AudioClip myE;
    public AudioClip myN;

    public void EffSound()
    {
        myBb.PlayOneShot(myP);
        myEe.PlayOneShot(myE);
        myNn.PlayOneShot(myN);

    }
    public void ClickSound()
    {
        myBb.PlayOneShot(myQ);
        //myEe.PlayOneShot(myN);

    }



}
