using UnityEngine;
using System.Collections;
using YG.Example;
using YG;

namespace CheesyRun
{
  public class GameManager : MonoBehaviour
  {
    public Mouse mouse;
    public GameObject[] enemy;
    public GameObject[] cheese;

    public TextMesh scoreText;
    public Transform timeBar;
    public float increaseSpeed;

    public float score;
    public TextMesh GameOverScoreText;

    private float lastTime;

    //public int heartId = -1;
    //public bool chestLocked;

    //public void RandomHeartId()
    //{
    //  heartId = Random.Range(0,3);
    //}

    //public AudioYB inGameSound;
    //public string inGameSoundName;

    void Start()
    {
      score = 0;
      SetTime();
      //		CreateEnemy();
      Instantiate(enemy[Random.Range(0, enemy.Length - 1)], new Vector3(13, -2.58f, 0), Quaternion.identity);
      InvokeRepeating("CreateEnemy", 1, 3);

      //inGameSound.clip = inGameSoundName;
      //inGameSound.Play(inGameSoundName);
      //inGameSound.loop = true;
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
      lastTime = Time.time;
    }

    void LateUpdate()
    {
      if (Time.timeScale > 0)
      {
        score += 0.5f;

        scoreText.text = ((int)score).ToString();
        GameOverScoreText.text = ((int)score).ToString();

        timeBar.localScale = new Vector2((40 - (Time.time - lastTime)) / 40, 1.8f);

        if (timeBar.localScale.x < 0.01f)
        {
          mouse.TimesUp();
        }

        increaseSpeed = score / 100000;
        if (increaseSpeed >= 0.15f)
          increaseSpeed = 0.15f;
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
