using UnityEngine;
using System.Collections;
namespace CheesyRun
{
  public class PlayAudioCat : MonoBehaviour
  {
    public string AudioCatName;

    AudioYB audioYB;

    private void Start()
    {
      audioYB = GetComponent<AudioYB>();
    }

    void PlaySound()
    {
      //GetComponent<AudioSource>().Play();
      //GetComponent<AudioYB>().clip = AudioCatName;
      audioYB.Play(AudioCatName);
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
