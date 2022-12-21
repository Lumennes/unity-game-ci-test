using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG.Example;
using YG;
using UnityEngine.UI;

namespace CheesyRun
{
  public class ChestsManager : MonoBehaviour
  {
    public Sprite openChest;
    public Sprite closeChest;
    public SpriteRenderer[] chests;
    public GameObject[] hearts;
    public GameObject[] videos;

    public bool isClicked;
    public Chest other1;
    public Chest other2;

    public GameObject buttonsMenu;
    public GameObject chestsMenu;
    public GameObject gameOverMenu;

    bool setTime;
    int countDown;

    public int heartId;

    public int num;

    // Подписываемся на событие открытия рекламы в OnEnable
    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    // Отписываемся от события открытия рекламы в OnDisable
    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    // Start is called before the first frame update
    void Start()
    {
      heartId = Random.Range(0, 3);
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
      //OpenChest(id);

      // Если ID = 1, то выдаём "+100 монет"
      //if (id == 0)
      //  AddMoney();

      ////// Если ID = 2, то выдаём "+оружие".
      //else if (id == 1)
      //  AddWeapon();

      //else if (id == 2)
      //  AddWeapon();



      chests[id].sprite = openChest;

      if (heartId == id)
      {
        hearts[id].SetActive(true);

        StartCoroutine(ResumeGame(true));
      }
      else
      {
        if (num > 0)
          StartCoroutine(ResumeGame(false));
        else
        {
          for (int i = 0; i < videos.Length; i++)
          {
            if (id != i) videos[i].SetActive(true);
          }
          isClicked = false;
          num++;
        }

      }


    }

    IEnumerator ResumeGame(bool resume)
    {
      yield return new WaitForSecondsRealtime(0.5f);

      if (resume)
      {
        gameOverMenu.SetActive(false);
        chestsMenu.SetActive(false);
        GameObject.FindObjectOfType<GameManager>().SetTime();
        Time.timeScale = 1;
      }
      else
      {
        chestsMenu.SetActive(false);
        buttonsMenu.SetActive(true);
      }

      heartId = Random.Range(0, 3);
      isClicked = false;
      num = 0;
    }

    //void OpenChest(int id)
    //{

    //}

    // Метод для вызова видео рекламы
    public void OpenChestRewardAd(int id)
    {
      //chests[id].GetComponent<Button>().interactable = false;

      // Вызываем метод открытия видео рекламы
      if (num > 0)
        YandexGame.RewVideoShow(id);
      else
        Rewarded(id);
    }
  }
}
