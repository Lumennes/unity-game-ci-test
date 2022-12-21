using UnityEngine;
using System.Collections;
using YG;

namespace CheesyRun
{
  public class AudioController : MonoBehaviour
  {
    public Sprite on;
    public Sprite off;

    SpriteRenderer sp;

    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    void Start()
    {
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
      sp = GetComponent<SpriteRenderer>();

      //if (PlayerPrefs.GetInt("Mute", 0) == 1)
      if (YandexGame.savesData.mute)
      {
        AudioListener.volume = 0;
        sp.sprite = off;
      }
      else
      {
        AudioListener.volume = 1;
        sp.sprite = on;
      }
    }

    void OnMouseDown()
    {
      transform.localScale = new Vector3(0.9f, 0.9f, 1);
    }

    void OnMouseUp()
    {
      transform.localScale = new Vector3(1, 1, 1);

      //if (PlayerPrefs.GetInt("Mute", 0) == 0)
      if(!YandexGame.savesData.mute)
      {
        AudioListener.volume = 0;
        //PlayerPrefs.SetInt("Mute", 1);
        YandexGame.savesData.mute = true;
        sp.sprite = off;
      }
      else
      {
        AudioListener.volume = 1;
        //PlayerPrefs.SetInt("Mute", 0);
        YandexGame.savesData.mute = false;
        sp.sprite = on;
      }

      YandexGame.SaveProgress();
      //PlayerPrefs.Save();
    }
  }
}
