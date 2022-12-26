using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRGameManager : MonoBehaviour
{
  [SerializeField] GameObject playerGO;
  [SerializeField] Transform playerTr;

  bool timeShift;

  // Start is called before the first frame update
  void Start()
  {
    if (!playerGO)
    {
      playerGO = GameObject.FindGameObjectWithTag("Player");
      if (playerGO)
        playerTr = playerGO.GetComponent<Transform>();
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Alpha1))
      QualitySettings.SetQualityLevel(0);
    if (Input.GetKeyDown(KeyCode.Alpha2))
      QualitySettings.SetQualityLevel(1);
    if (Input.GetKeyDown(KeyCode.Alpha3))
      QualitySettings.SetQualityLevel(2);
    if (Input.GetKeyDown(KeyCode.Alpha4))
      QualitySettings.SetQualityLevel(3);
    if (Input.GetKeyDown(KeyCode.Alpha5))
      QualitySettings.SetQualityLevel(4);
    if (Input.GetKeyDown(KeyCode.Alpha6))
      QualitySettings.SetQualityLevel(5);

    if (Input.GetKeyDown(KeyCode.F5))
      QuickSave();
    if (Input.GetKeyDown(KeyCode.F9))
      QuickLoad();

    if (Input.GetKeyDown(KeyCode.Tab))
    {
      timeShift = !timeShift;
      Time.timeScale = timeShift ? 10 : 1;
    }
  }

  void QuickSave()
  {
    if (playerTr)
    {
      PlayerPrefs.SetFloat("x", playerTr.position.x);
      PlayerPrefs.SetFloat("y", playerTr.position.y);
      PlayerPrefs.SetFloat("z", playerTr.position.z);
      PlayerPrefs.SetInt("quickSaveExist", 1);
      PlayerPrefs.Save();
    }
  }

  void QuickLoad()
  {
    if (PlayerPrefs.HasKey("quickSaveExist"))
    {
      var pos = new Vector3(PlayerPrefs.GetFloat("x"), PlayerPrefs.GetFloat("y"), PlayerPrefs.GetFloat("z"));
      playerTr.position = pos;
    }
  }
}
