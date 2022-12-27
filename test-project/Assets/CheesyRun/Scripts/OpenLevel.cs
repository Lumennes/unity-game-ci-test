using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
namespace CheesyRun
{
  public class OpenLevel : MonoBehaviour
  {
    public string levelName;

    //void OnMouseDown()
    //{
    //  transform.localScale = new Vector3(0.9f, 0.9f, 1);
    //}

    //void OnMouseUp()
    //{
    //  transform.localScale = new Vector3(1, 1, 1);

    //  Time.timeScale = 1;
    //  SceneManager.LoadScene(levelName);
    //}

    public void Open()
    {
      Time.timeScale = 1;
      SceneManager.LoadScene(levelName);
    }
  }
}
