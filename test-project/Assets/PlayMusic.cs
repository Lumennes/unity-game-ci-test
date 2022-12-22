using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
  AudioYB audioYB;
  public string mainMenuSound;
  public bool loop = true;

  // Start is called before the first frame update
  void Start()
  {
    audioYB = GetComponent<AudioYB>();
    audioYB.Play(mainMenuSound);
    audioYB.loop = loop;
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

  // Update is called once per frame
  //void Update()
  //{

  //}
}
