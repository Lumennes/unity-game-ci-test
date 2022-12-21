using UnityEngine;
using System.Collections;
namespace CheesyRun
{
  public class Pause : MonoBehaviour
  {
    public GameObject pauseMenu;
    public GameObject pauseBtn;

    void OnMouseDown()
    {
      transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    void OnMouseUp()
    {
      transform.localScale = new Vector3(1, 1, 1);

      if (Time.timeScale > 0)
      {
        Time.timeScale = 0;

        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
      }
    }
  }
}
