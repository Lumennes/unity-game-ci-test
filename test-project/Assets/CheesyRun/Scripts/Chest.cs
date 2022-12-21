using UnityEngine;
using System.Collections;
using YG.Example;
using YG;

namespace CheesyRun
{
  public class Chest : MonoBehaviour
  {
    public Sprite openChest;
    public Sprite closeChest;
    public GameObject heart;
    public bool isClicked;
    public Chest other1;
    public Chest other2;

    public GameManager gm;
    public GameObject buttonsMenu;
    public GameObject chestsMenu;
    public GameObject gameOverMenu;

    bool setTime;
    int countDown;

    public int id = 0;
    //public bool isHeart = false;

    //public const int idHeart = -1;

    public GameObject VideoFoto;

    //public bool locked;

    public ChestsManager cm;

    // Отписываемся от события открытия рекламы в OnDisable
    //private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;

    void OnEnable()
    {
      countDown = 0;
      isClicked = false;
      setTime = false;
      VideoFoto.SetActive(false);
      //isHeart = false;
      heart.SetActive(false);
      GetComponent<SpriteRenderer>().sprite = closeChest;
      buttonsMenu.SetActive(false);

      //если в других двух нет сердца, то сердце либо в последнем, либо если в первых 2 выпадет 1 в диапозоне от 1до3
      //if (!other1.isHeart && !other2.isHeart)
      //{
      //  isHeart = id == 2 || Random.Range(0, 2) == 0;
      //}

      //if (gm.heartId == -1)
      //  gm.heartId = Random.Range(0, 3);

      //if (idHeart == -1)
      //  idHeart = Random.Range(0, 3);
      //print("Chest: " + id);

      //YandexGame.RewardVideoEvent += Rewarded;
    }

    //void Rewarded(int id)
    //{
    //  if (id == this.id)
    //  {       
    //    isClicked = true;
    //    GetComponent<SpriteRenderer>().sprite = openChest;

    //    if (id == gm.heartId)
    //    {
    //      heart.SetActive(true);
    //      gm.heartId = -1;
    //      setTime = true;
    //    }
    //    else
    //    {
    //      if (!other1.isClicked && !other2.isClicked)
    //      {
    //        other1.VideoFoto.SetActive(true);
    //        other2.VideoFoto.SetActive(true);
    //        gm.chestLocked = false;
    //      }
    //      else
    //      {
    //        gm.heartId = -1;
    //        setTime = true;
    //      }
    //    }
    //  }
    //}

    void OnMouseDown()
    {
      if (!isClicked && !cm.isClicked)
      {
        isClicked = true;
        cm.isClicked = true;
        cm.OpenChestRewardAd(id);
      }

      return;

      //if (!isClicked && !gm.chestLocked/* && !other1.isClicked && !other2.isClicked*/)
      //{
      //  gm.chestLocked = true;
      //  if (other1.isClicked || other2.isClicked)
      //    YandexGame.RewVideoShow(id);
      //  else
      //    Rewarded(id);
      //}
    }

    void Update()
    {
      if (setTime)
      {
        if (countDown > 50)
        {
          if (heart.activeSelf == true)
          {
            GameObject.FindObjectOfType<GameManager>().SetTime();
            Time.timeScale = 1;
            gameOverMenu.SetActive(false);
          }
          else
          {
            chestsMenu.SetActive(false);
            buttonsMenu.SetActive(true);
          }
          //gm.chestLocked = false;
        }

        countDown++;
      }
    }
  }
}
