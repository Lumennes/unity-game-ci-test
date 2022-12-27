using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YG;
using UnityEngine.SceneManagement;

public class FirstGameManager : MonoBehaviour
{
  [SerializeField] GameObject bestScoreText;
  [SerializeField] TMP_Text scoreText;

  public string levelName;

  public Sprite audioOn;
  public Sprite audioOff;
  public Image audioImage;

  private void OnEnable()
  {
    YandexGame.GetDataEvent += GetLoad;
  }

  private void OnDisable()
  {
    YandexGame.GetDataEvent -= GetLoad;
  }

  private void Awake()
  {
    bestScoreText.SetActive(false);
    scoreText.gameObject.SetActive(false);
  }

  // Start is called before the first frame update
  void Start()
  {
    if (YandexGame.SDKEnabled)
      GetLoad();
  }

  void GetLoad()
  {
    bestScoreText.SetActive(true);
    scoreText.gameObject.SetActive(true);
    scoreText.text = YandexGame.savesData.maxScore.ToString();

    if (YandexGame.savesData.mute)
    {
      AudioListener.volume = 0;
      audioImage.sprite = audioOff;
    }
    else
    {
      AudioListener.volume = 1;
      audioImage.sprite = audioOn;
    }
  }

  public void Play()
  {
    Time.timeScale = 1;
    SceneManager.LoadScene(levelName);
  }


  public void Audio()
  {
    if (YandexGame.Instance)
    {
      if (!YandexGame.savesData.mute)
      {
        AudioListener.volume = 0;
        //PlayerPrefs.SetInt("Mute", 1);
        YandexGame.savesData.mute = true;
        audioImage.sprite = audioOff;
      }
      else
      {
        AudioListener.volume = 1;
        //PlayerPrefs.SetInt("Mute", 0);
        YandexGame.savesData.mute = false;
        audioImage.sprite = audioOn;
      }

      YandexGame.SaveProgress();
    }
  }

  // Update is called once per frame
  //void Update()
  //{

  //}
}
