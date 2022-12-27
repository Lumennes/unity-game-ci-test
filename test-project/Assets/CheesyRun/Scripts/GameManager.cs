using UnityEngine;
using System.Collections;
using YG.Example;
using YG;
using UnityEngine.UI;
using TMPro;

namespace CheesyRun
{
  public class GameManager : MonoBehaviour
  {
    public Mouse mouse;
    public GameObject[] enemy;
    public GameObject[] cheese;

    public TMP_Text scoreText;
    public Transform timeBar;
    public float increaseSpeed;
    public Slider timeBarSlider;

    public float score;
    public TextMesh GameOverScoreText;

    private float lastTime;

    public GameObject pauseMenu;
    public GameObject pauseBtn;

    public Sprite audioOn;
    public Sprite audioOff;
    public Image audioImage;

    //public int heartId = -1;
    //public bool chestLocked;

    //public void RandomHeartId()
    //{
    //  heartId = Random.Range(0,3);
    //}

    //public AudioYB inGameSound;
    //public string inGameSoundName;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Start()
    {
      score = 0;
      SetTime();
      //		CreateEnemy();
      Instantiate(enemy[Random.Range(0, enemy.Length - 1)], new Vector3(13, -2.58f, 0), Quaternion.identity);
      InvokeRepeating(nameof(CreateEnemy), 1, 3);

      //inGameSound.clip = inGameSoundName;
      //inGameSound.Play(inGameSoundName);
      //inGameSound.loop = true;

      InvokeRepeating(nameof(SaveScore), 5, 5);

      // Проверяем запустился ли плагин
      if (YandexGame.SDKEnabled == true)
      {
        // Если запустился, то запускаем Ваш метод для загрузки
        GetLoad();

        // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
        // но он запустится при вызове события GetDataEvent, после прогрузки плагина
      }
    }

    public void GetLoad()
    {
      // Получаем данные из плагина и делаем с ними что хотим
      // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
      //sp = GetComponent<SpriteRenderer>();

      //if (PlayerPrefs.GetInt("Mute", 0) == 1)
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

    public void SaveScore()
    {
      var intScore = (int)score;

      if (YandexGame.Instance && intScore > YandexGame.savesData.maxScore)
      {
        //print($"SaveScore: {Time.realtimeSinceStartup} - score: {score} - maxScore: {YandexGame.savesData.maxScore}");

        YandexGame.NewLeaderboardScores("LeaderBoard", intScore);
        YandexGame.savesData.maxScore = intScore;
        YandexGame.SaveProgress();
      }
    }

    public void Pause()
    {
      if (Time.timeScale > 0)
      {
        Time.timeScale = 0;

        pauseMenu.SetActive(true);
        pauseBtn.SetActive(false);
      }
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
          audioImage.sprite = audioOn;
        }
        else
        {
          AudioListener.volume = 1;
          //PlayerPrefs.SetInt("Mute", 0);
          YandexGame.savesData.mute = false;
          audioImage.sprite = audioOff;
        }

        YandexGame.SaveProgress();
      }
    }

    //private void OnApplicationPause(bool pause)
    //{
    //  if (inGameSound)
    //  {
    //    if (pause)
    //    {
    //      inGameSound.Pause();
    //    }
    //    else
    //    {
    //      inGameSound.UnPause();
    //    }
    //  }
    //}

    public void SetTime()
    {
      //lastTime = Time.time;
      timeBarSlider.value = 1;
    }

    void LateUpdate()
    {
      if (Time.timeScale > 0)
      {
        score += 0.5f;

        scoreText.text = ((int)score).ToString();
        GameOverScoreText.text = ((int)score).ToString();        

        increaseSpeed = score / 100000;
        if (increaseSpeed >= 0.15f)
          increaseSpeed = 0.15f;

        timeBarSlider.value -= increaseSpeed / 100;//Time.time - lastTime;

        // timeBar.localScale = new Vector2((40 - (Time.time - lastTime)) / 40, 1.8f);

        //if (timeBar.localScale.x < 0.01f)
        if (timeBarSlider.value < 0.01f)
        {
          mouse.TimesUp();
        }
      }
    }

    void CreateEnemy()
    {
      Instantiate(enemy[Random.Range(0, enemy.Length - 1)], new Vector3(Random.Range(26, 30), -2.58f, 0), Quaternion.identity);

      if ((int)Random.Range(0, 2) < 1)
      {
        Instantiate(cheese[Random.Range(0, cheese.Length - 1)], new Vector3(Random.Range(26, 30), 0.7f, 0), Quaternion.identity);
      }
    }
  }
}
