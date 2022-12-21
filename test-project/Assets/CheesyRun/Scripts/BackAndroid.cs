using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace CheesyRun
{
  public class BackAndroid : MonoBehaviour
  {
    public string ParentLeveleName;

    void Update()
    {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        if (ParentLeveleName != "Quit")
          SceneManager.LoadScene(ParentLeveleName);
        else
          Application.Quit();
      }
    }
  }
}
