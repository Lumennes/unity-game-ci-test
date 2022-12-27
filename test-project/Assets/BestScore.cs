using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

public class BestScore : MonoBehaviour
{
  [SerializeField] GameObject bestScoreText;
  [SerializeField] TMP_Text scoreText;

  private void OnEnable()
  {
    YandexGame.GetDataEvent += GetLoad;
  }

  private void OnDisable()
  {
    YandexGame.GetDataEvent -= GetLoad;
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
    scoreText.text = YandexGame.savesData.maxScore.ToString();
  }

    // Update is called once per frame
    //void Update()
    //{
        
    //}
}
