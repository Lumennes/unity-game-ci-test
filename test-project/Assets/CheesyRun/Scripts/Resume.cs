using UnityEngine;
using System.Collections;
using YG;

namespace CheesyRun
{
  public class Resume : MonoBehaviour
  {
    public GameObject pauseMenu;
    public GameObject pauseBtn;
    public bool rewarded;

    public Mouse mouse;

    bool rew;

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable()
    {
      if (rewarded)
      {
        YandexGame.RewardVideoEvent += Rewarded;
        YandexGame.CloseVideoEvent += CloseVideoEvent;
      }
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
      if (rewarded)
      {
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.CloseVideoEvent -= CloseVideoEvent;
      }
    }

    void CloseVideoEvent()
    {
      //close = true;
      if (rew)
      {
        FindObjectOfType<GameManager>().SetTime();
        ResumeGame();
      }
      print("CloseVideoEvent");
      
    }

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
      rew = true;

      //ResumeGame();

      print("Rewarded");

      //rew = true;

      //if (close)
      {

        //close = false;
      }

      // Если ID = 1, то выдаём "+100 монет"
      //if (id == 1)
      //  AddMoney();

      //// Если ID = 2, то выдаём "+оружие".
      //else if (id == 2)
      //  AddWeapon();

    }

    void OnMouseDown()
    {
      transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    void OnMouseUp()
    {
      transform.localScale = new Vector3(1, 1, 1);

      //ResumeGame();

      if (YandexGame.Instance && rewarded)
        YandexGame.RewVideoShow(0);
      else
        ResumeGame();
    }

    void ResumeGame()
    {
      print("ResumeGame");

      Time.timeScale = 1;

      pauseBtn.SetActive(true);
      pauseMenu.SetActive(false);

      if (mouse) mouse.Resume();
    }
  }
}
