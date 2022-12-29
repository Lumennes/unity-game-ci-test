using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCurosrLock : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    //Cursor.lockState = CursorLockMode.Locked;
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnApplicationFocus(bool focus)
  {
    print($"OnApplicationFocus: {focus}");
    Cursor.lockState = focus ? CursorLockMode.Locked : CursorLockMode.None;
    print($"Cursor.lockState: {CursorLockMode.Locked}");
  }



}
