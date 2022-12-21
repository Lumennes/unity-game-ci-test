using UnityEngine;
using System.Collections;
using YG;

namespace CheesyRun
{
  public class Mouse : MonoBehaviour
  {
    private int maxJump = 10;
    private bool canJump;
    private bool canJump2;

    private Animator anim;

    public GameManager gm;

    public AudioClip JumpSound;
    public AudioClip EnemySound;
    public AudioClip TrapSound;
    public AudioClip[] CheeseSound;

    public GameObject TrapMenu;
    public GameObject EnemyMenu;
    public GameObject TimeMenu;
    public GameObject GameOverMenu;

    public GameObject chestsMenu;

    public string JumpSoundName;
    public string EnemySoundName;
    public string TrapSoundName;
    public string[] CheeseSoundName;

    AudioYB audioYB;

    Rigidbody2D rb;

    public void JumpSoundPlay()
    {
      //if (!GetComponent<AudioSource>().isPlaying)
      //{
      //  GetComponent<AudioSource>().clip = JumpSound;
      //  GetComponent<AudioSource>().Play();
      //}

      //if (!GetComponent<AudioYB>().isPlaying)
      {
        //GetComponent<AudioYB>().clip = JumpSoundName;
        audioYB.Play(JumpSoundName);
      }
    }

    public void CheeseSoundPlay()
    {
      //if (!audio.isPlaying)
      {
        //GetComponent<AudioSource>().clip = CheeseSound[Random.Range(0, CheeseSound.Length - 1)];
        //GetComponent<AudioSource>().Play();

        //GetComponent<AudioYB>().clip = CheeseSoundName[Random.Range(0, CheeseSound.Length - 1)];
        audioYB.Play(CheeseSoundName[Random.Range(0, CheeseSound.Length - 1)]);
      }
    }

    public void EnemySoundPlay()
    {
      //if (!audio.isPlaying)
      {
        //GetComponent<AudioSource>().clip = EnemySound;
        //GetComponent<AudioSource>().Play();

        //GetComponent<AudioYB>().clip = EnemySoundName;
        audioYB.Play(EnemySoundName);
      }
    }

    public void TrapSoundPlay()
    {
      //if (!audio.isPlaying)
      {
        //GetComponent<AudioSource>().clip = TrapSound;
        //GetComponent<AudioSource>().Play();

        //GetComponent<AudioYB>().clip = TrapSoundName;
        audioYB.Play(TrapSoundName);
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

    public void TimesUp()
    {
      Time.timeScale = 0;
      EnemySoundPlay();

      TrapMenu.SetActive(false);
      TimeMenu.SetActive(true);
      EnemyMenu.SetActive(false);
      chestsMenu.SetActive(true);
      GameOverMenu.SetActive(true);
    }

    void SaveScore()
    {
      if (gm.score > YandexGame.savesData.maxScore)
      {
        YandexGame.NewLeaderboardScores("score", (int)gm.score);
        YandexGame.savesData.maxScore = (int)gm.score;
        YandexGame.SaveProgress();
      }
    }

    void OnTriggerEnter2D(Collider2D col)
    {     
      if (col.CompareTag("Enemy"))
      {
        SaveScore();
        YandexGame.FullscreenShow();
        Time.timeScale = 0;

        //YandexGame.FullscreenShow();

        EnemySoundPlay();
        col.enabled = false;

        TrapMenu.SetActive(false);
        TimeMenu.SetActive(false);
        EnemyMenu.SetActive(true);
        chestsMenu.SetActive(true);
        GameOverMenu.SetActive(true);
      }
      else if (col.CompareTag("Trap"))
      {
        SaveScore();
        YandexGame.FullscreenShow();
        Time.timeScale = 0;

        //YandexGame.FullscreenShow();

        TrapSoundPlay();
        col.enabled = false;

        TrapMenu.SetActive(true);
        TimeMenu.SetActive(false);
        EnemyMenu.SetActive(false);
        chestsMenu.SetActive(true);
        GameOverMenu.SetActive(true);
      }

      //		if (col.tag == "Enemy"
      //		    || col.tag == "Trap")
      //		{
      //PlayerPrefs.SetInt("Score",(int)gm.score);
      //PlayerPrefs.Save();
      //		}

      if (col.CompareTag("Cheese"))
      {
        SaveScore();
        CheeseSoundPlay();
        Destroy(col.gameObject);
        gm.SetTime();
      }
    }

    //private void OnApplicationPause(bool pause)
    //{
    //  if (audioYB)
    //  {
    //    if (pause)
    //    {
    //      audioYB.Pause();
    //    }
    //    else
    //    {
    //      audioYB.UnPause();
    //    }
    //  }
    //}

    void OnTriggerStay2D(Collider2D col)
    {
      if (col.CompareTag("Ground"))
      {
        SaveScore();
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
        JumpSoundPlay();
        canJump = false;
        anim.Play("Mouse_Jump", -1, 0);
        Vector3 temp = rb.velocity;
        temp.y = 0;
        rb.velocity = temp;
        rb.AddForce(50 * maxJump * Vector2.up);
      }
      else if (canJump2)
      {
        JumpSoundPlay();
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
