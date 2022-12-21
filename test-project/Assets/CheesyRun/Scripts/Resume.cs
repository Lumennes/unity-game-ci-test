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

    bool close;

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable()
    {
      if (rewarded)
      {
        YandexGame.RewardVideoEvent += Rewarded;
        //YandexGame.CloseVideoEvent += CloseVideoEvent;
      }
    }

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable()
    {
      if (rewarded)
      {
        YandexGame.RewardVideoEvent -= Rewarded;
        //YandexGame.CloseVideoEvent -= CloseVideoEvent;
      }
    }

    //void CloseVideoEvent()
    //{
    //  close = true;

    //  //print("CloseVideoEvent");
      
    //}

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
      //print("Rewarded");

      //if (close)
      {
        GameObject.FindObjectOfType<GameManager>().SetTime();
        ResumeGame();
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

      if (rewarded)
        YandexGame.RewVideoShow(0);
      else
        ResumeGame();
    }

    void ResumeGame()
    {
      Time.timeScale = 1;

      pauseBtn.SetActive(true);
      pauseMenu.SetActive(false);
    }
  }
}
