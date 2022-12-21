using UnityEngine;
using System.Collections;

namespace CheesyRun
{
  public class AnimSound : MonoBehaviour
  {
    public AudioClip CatSound;
    public AudioClip InGameSound;

    public string CatSoundName;
    public string InGameSoundName;

    AudioYB audioYB;

    private void Start()
    {
      audioYB = GetComponent<AudioYB>();
    }

    public void CatSoundPlay()
    {
      //if (!audio.isPlaying)
      {
        //GetComponent<AudioSource>().clip = CatSound;
        //GetComponent<AudioSource>().Play();

        //GetComponent<AudioYB>().clip = CatSoundName;
        if (audioYB)
          audioYB.Play(CatSoundName);
      }
    }

    public void InGameSoundPlay()
    {
      //if (!audio.isPlaying)
      {
        //GetComponent<AudioSource>().clip = InGameSound;
        //GetComponent<AudioSource>().Play();

        //GetComponent<AudioYB>().clip = InGameSoundName;
        if (audioYB)
          audioYB.Play(InGameSoundName);
      }
    }

    //private void OnApplicationPause(bool pause)
    //{
    //  if (audioYB)
    //  {
    //    if (pause)
    //    {
    //      audioYB.Pause();
    //    }
    //    else
    //    {
    //      audioYB.UnPause();
    //    }
    //  }
    //}


  }
}
