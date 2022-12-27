using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using YG;
using UnityEngine.SceneManagement;
using YG.Example;
using TMPro;

namespace CheesyRun
{
  public class Mouse : MonoBehaviour
  {
    private int maxJump = 10;
    private bool canJump;
    private bool canJump2;

    private Animator anim;

    public GameManager gm;

    //public AudioClip JumpSound;
    //public AudioClip EnemySound;
    //public AudioClip TrapSound;
    //public AudioClip[] CheeseSound;

    public GameObject TrapMenu;
    public GameObject EnemyMenu;
    public GameObject TimeMenu;

    public Sprite trapSprite;
    public Sprite enemySprite;
    public Sprite timeSprite;
    public Sprite resumeSprite;
    public Sprite resumeVideoSprite;
    public GameObject endPanel;
    public Image endImage;
    public Image resumeImage;
    public GameObject gameOverPanel;
    public TMP_Text scoreText;

    //public GameObject chestsMenu;

    public string JumpSoundName;
    public string EnemySoundName;
    public string TrapSoundName;
    public string[] CheeseSoundName;

    AudioYB audioYB;

    Rigidbody2D rb;

    [SerializeField] GameObject jumpButton;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject timeBar;

    bool rewarded;

    //public void JumpSoundPlay()
    //{
    //  //if (!GetComponent<AudioSource>().isPlaying)
    //  //{
    //  //  GetComponent<AudioSource>().clip = JumpSound;
    //  //  GetComponent<AudioSource>().Play();
    //  //}

    //  if (AudioStreamCash.instance && audioYB && !audioYB.isPlaying)
    //  {
    //    //GetComponent<AudioYB>().clip = JumpSoundName;
    //    audioYB.Play(JumpSoundName);
    //  }
    //}

    //public void CheeseSoundPlay()
    //{
    //  if (AudioStreamCash.instance && audioYB && !audioYB.isPlaying)
    //  {
    //    //GetComponent<AudioSource>().clip = CheeseSound[Random.Range(0, CheeseSound.Length - 1)];
    //    //GetComponent<AudioSource>().Play();

    //    //GetComponent<AudioYB>().clip = CheeseSoundName[Random.Range(0, CheeseSound.Length - 1)];
    //    audioYB.Play(CheeseSoundName[Random.Range(0, CheeseSoundName.Length - 1)]);
    //  }
    //}

    //public void EnemySoundPlay()
    //{
    //  if (AudioStreamCash.instance && audioYB && !audioYB.isPlaying)
    //  {
    //    //GetComponent<AudioSource>().clip = EnemySound;
    //    //GetComponent<AudioSource>().Play();

    //    //GetComponent<AudioYB>().clip = EnemySoundName;
    //    audioYB.Play(EnemySoundName);
    //  }
    //}

    //public void TrapSoundPlay()
    //{
    //  if (AudioStreamCash.instance && audioYB && !audioYB.isPlaying)
    //  {
    //    //GetComponent<AudioSource>().clip = TrapSound;
    //    //GetComponent<AudioSource>().Play();

    //    //GetComponent<AudioYB>().clip = TrapSoundName;
    //    audioYB.Play(TrapSoundName);
    //  }
    //}

    void SoundPlay(string soundName)
    {
      if (AudioStreamCash.instance && audioYB && !audioYB.isPlaying)
      {
        audioYB.Play(soundName);
      }
    }

    void Start()
    {
      canJump = true;
      canJump2 = true;
      anim = GetComponent<Animator>();
      audioYB = GetComponent<AudioYB>();
      rb = GetComponent<Rigidbody2D>();
    }

    public void Resume()
    {
      if (YandexGame.Instance && rewarded)
        YandexGame.RewVideoShow(0);
      else
        ResumeGame();
    }

    void ResumeGame()
    {
      if(rewarded)
      {
        YandexGame.RewardVideoEvent -= Rewarded;
        YandexGame.CloseVideoEvent -= CloseVideoEvent;
        rewarded = false;
        rew = false;
      }

      Time.timeScale = 1;

      gameOverPanel.SetActive(false);

      jumpButton.SetActive(true);
      pauseButton.SetActive(true);
      timeBar.SetActive(true);
    }

    bool rew;

    void CloseVideoEvent()
    {
      //close = true;
      if (rew)
      {
        gm.SetTime();
        ResumeGame();
      }
      //print("CloseVideoEvent");

    }

    // Подписанный метод получения награды
    void Rewarded(int id)
    {
      rew = true;

      //ResumeGame();

      //print("Rewarded");

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

    public void Home()
    {
      Time.timeScale = 1;
      SceneManager.LoadScene(0);
    }

    public void Restart()
    {
      Time.timeScale = 1;
      SceneManager.LoadScene(2);
    }

    public void Pause()
    {
      Time.timeScale = 0;

      resumeImage.sprite = resumeSprite;
      rewarded = false;

      jumpButton.SetActive(false);
      pauseButton.SetActive(false);
      timeBar.SetActive(false);

      gameOverPanel.SetActive(true);
      endPanel.SetActive(false);
    }

    public void TimesUp()
    {
      SoundPlay(EnemySoundName);

      GameOver(timeSprite);
    }

    private void OnDisable()
    {
      YandexGame.RewardVideoEvent -= Rewarded;
      YandexGame.CloseVideoEvent -= CloseVideoEvent;
    }

    void GameOver(Sprite sprite)
    {
      Time.timeScale = 0;

      gm.SaveScore();

      if (YandexGame.Instance)
        YandexGame.FullscreenShow();

      resumeImage.sprite = resumeVideoSprite;
      rewarded = true;

      YandexGame.RewardVideoEvent += Rewarded;
      YandexGame.CloseVideoEvent += CloseVideoEvent;

      jumpButton.SetActive(false);
      pauseButton.SetActive(false);
      timeBar.SetActive(false);

      scoreText.text = ((int)gm.score).ToString();

      gameOverPanel.SetActive(true);
      endPanel.SetActive(true);
      endImage.sprite = sprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
      if (col.CompareTag("Enemy"))
      {
        SoundPlay(EnemySoundName);

        col.enabled = false;

        GameOver(enemySprite);
      }
      else if (col.CompareTag("Trap"))
      {
        SoundPlay(TrapSoundName);

        col.enabled = false;

        GameOver(trapSprite);
      }

      //		if (col.tag == "Enemy"
      //		    || col.tag == "Trap")
      //		{
      //PlayerPrefs.SetInt("Score",(int)gm.score);
      //PlayerPrefs.Save();
      //		}

      if (col.CompareTag("Cheese"))
      {
        SoundPlay(CheeseSoundName[Random.Range(0, CheeseSoundName.Length - 1)]);

        gm.SaveScore();

        Destroy(col.gameObject);
        gm.SetTime();
      }
    }

    void OnTriggerStay2D(Collider2D col)
    {
      if (col.CompareTag("Ground"))
      {
        canJump = true;
        canJump2 = true;
        anim.Play("Mouse_Run");
      }
    }

    public void MouseJump()
    {
      if (Time.timeScale < 1)
        return;

      if (canJump)
      {
        SoundPlay(JumpSoundName);
        canJump = false;
        anim.Play("Mouse_Jump", -1, 0);
        Vector3 temp = rb.velocity;
        temp.y = 0;
        rb.velocity = temp;
        rb.AddForce(50 * maxJump * Vector2.up);
      }
      else if (canJump2)
      {
        SoundPlay(JumpSoundName);
        canJump2 = false;
        anim.Play("Mouse_Jump", -1, 0);
        Vector3 temp = rb.velocity;
        temp.y = 0;
        rb.velocity = temp;
        rb.AddForce(50 * maxJump * Vector2.up);
      }
    }
  }
}
