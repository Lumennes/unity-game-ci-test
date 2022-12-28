using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using YG;

namespace Completed
{
  using System.Collections.Generic;   //Allows us to use Lists. 
  using UnityEngine.UI;         //Allows us to use UI.

  public class GameManager : MonoBehaviour
  {
    public float levelStartDelay = 2f;            //Time to wait before starting level, in seconds.
    public float turnDelay = 0.1f;              //Delay between each Player turn.
    public int playerFoodPoints = 100;            //Starting value for Player food points.
    public static GameManager instance = null;        //Static instance of GameManager which allows it to be accessed by any other script.
    [HideInInspector] public bool playersTurn = true;   //Boolean to check if it's players turn, hidden in inspector but public.


    private Text levelText;                 //Text to display current level number.
    private Text maxLevelText;
    private GameObject maxLevel;
    private GameObject levelImage;              //Image to block out level as levels are being set up, background for levelText.
    private BoardManager boardScript;           //Store a reference to our BoardManager which will set up the level.
    private int level = 0;                  //Current level number, expressed in game as "Day 1".
    private List<Enemy> enemies;              //List of all Enemy units, used to issue them move commands.
    private bool enemiesMoving;               //Boolean to check if enemies are moving.
    private bool doingSetup = true;             //Boolean to check if we're setting up board, prevent Player from moving during setup.

    public float restartGameDelay;
    bool isGameOver;

    // Подписываемся на событие GetDataEvent в OnEnable
    private void OnEnable() => YandexGame.GetDataEvent += GetLoad;

    // Отписываемся от события GetDataEvent в OnDisable
    private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

    public string lang;

    //Awake is always called before any Start functions
    void Awake()
    {
      //if (!string.IsNullOrEmpty(lang))
      //  YandexGame.savesData.language = lang;

      //if (!YandexGame.Instance)
      //  GetLoad();

      //print(YandexGame.savesData.language);

      //Check if instance already exists
      if (instance == null)

        //if not, set instance to this
        instance = this;

      //If instance already exists and it's not this:
      else if (instance != this)

        //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
        Destroy(gameObject);

      //Sets this to not be destroyed when reloading scene
      DontDestroyOnLoad(gameObject);

      //Assign enemies to a new List of Enemy objects.
      enemies = new List<Enemy>();

      //Get a component reference to the attached BoardManager script
      boardScript = GetComponent<BoardManager>();

      level++;      

      //Call the InitGame function to initialize the first level 
      InitGame();
    }

    // Ваш метод для загрузки, который будет запускаться в старте
    public void GetLoad()
    {
      // Получаем данные из плагина и делаем с ними что хотим
      // Например, мы хотил записать в компонент UI.Text сколько у игрока монет:
      //textMoney.text = YandexGame.savesData.money.ToString();

      if (!string.IsNullOrEmpty(lang))
        YandexGame.savesData.language = lang;

      /* if(!maxLevel) */
      maxLevel = GameObject.Find("MaxLevel");
     /* if(maxLevel && !maxLevelText) */maxLevelText = maxLevel.GetComponent<Text>();

      maxLevelText.text = YandexGame.savesData.language switch
      {
        "ru" => "твой рекорд " + YandexGame.savesData.maxLevel + " дней",//Set the foodText to reflect the current player food total.
        "tr" => "rekorun " + YandexGame.savesData.maxLevel + " gündür",//Set the foodText to reflect the current player food total.
        _ => "your record is " + YandexGame.savesData.maxLevel + " days",//Set the foodText to reflect the current player food total.
      };
    }

    //this is called only once, and the paramter tell it to be called only after the scene was loaded
    //(otherwise, our Scene Load callback would be called the very first load, and we don't want that)
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
      //register the callback to be called everytime the scene is loaded
      SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //private void OnEnable()
    //{
    //  SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //private void OnDisable()
    //{
    //  SceneManager.sceneLoaded -= OnSceneLoaded;
    //}

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
      if (instance)
      {
        if (instance.isGameOver)
        {
          //print("isGameOver: " + instance.isGameOver);
          instance.level = 0;
          instance.playerFoodPoints = 100;
          instance.isGameOver = false;
          instance.enabled = true;
        }

        instance.level++;
        instance.InitGame();
      }
    }


    //Initializes the game for each level.
    void InitGame()
    {
      //While doingSetup is true the player can't move, prevent player from moving while title card is up.
      doingSetup = true;

      //Get a reference to our image LevelImage by finding it by name.
      levelImage = GameObject.Find("LevelImage");

      //Get a reference to our text LevelText's text component by finding it by name and calling GetComponent.
      levelText = GameObject.Find("LevelText").GetComponent<Text>();

      //YandexGame.savesData.language = "tr";

      //Set the text of levelText to the string "Day" and append the current level number.
      //levelText.text = "Day " + level;
      levelText.text = YandexGame.savesData.language switch
      {
        "ru" => "День " + level,//Set the foodText to reflect the current player food total.
        "tr" => "Gün " + level,//Set the foodText to reflect the current player food total.
        _ => "Day " + level,//Set the foodText to reflect the current player food total.
      };

      //Set levelImage to active blocking player's view of the game board during setup.
      levelImage.SetActive(true);

      //Call the HideLevelImage function with a delay in seconds of levelStartDelay.
      Invoke(nameof(HideLevelImage), levelStartDelay);

      //Clear any Enemy objects in our List to prepare for next level.
      enemies.Clear();

      //Call the SetupScene function of the BoardManager script, pass it current level number.
      boardScript.SetupScene(level);

      // Проверяем запустился ли плагин
      if (YandexGame.SDKEnabled == true)
      {
        // Если запустился, то запускаем Ваш метод для загрузки
        GetLoad();

        // Если плагин еще не прогрузился, то метод не запуститься в методе Start,
        // но он запустится при вызове события GetDataEvent, после прогрузки плагина
      }
    }


    //Hides black image used between levels
    void HideLevelImage()
    {
      //Disable the levelImage gameObject.
      levelImage.SetActive(false);

      //Set doingSetup to false allowing player to move again.
      doingSetup = false;
    }

    //Update is called every frame.
    void Update()
    {
      //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
      if (playersTurn || enemiesMoving || doingSetup)

        //If any of these are true, return and do not start MoveEnemies.
        return;

      //Start moving enemies.
      StartCoroutine(MoveEnemies());
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
      //Add Enemy to List enemies.
      enemies.Add(script);
    }


    public void SaveScore(bool isGameOver)
    {
      var tempLevel = isGameOver ? level - 1 : level;

      // если уровень больше сохранённого максимального уровня
      if (YandexGame.Instance && tempLevel > YandexGame.savesData.maxLevel)
      {
        //print("maxLevel: " + YandexGame.savesData.maxLevel + " | level: " + level);
        YandexGame.NewLeaderboardScores("LeaderBoard", tempLevel); // сохраняем текущий уровень в лидерборде
        YandexGame.savesData.maxLevel = tempLevel; // сохраняем текущий уровень в сохранении как максимальный
        YandexGame.SaveProgress(); // сохраняем прогресс
        //GetLoad();
      }
    }

    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
      SaveScore(true); 

      YandexGame.FullscreenShow(); // показываем рекламу

      //Set levelText to display number of levels passed and game over message
      //levelText.text = "After " + level + " days, you starved.";
      levelText.text = YandexGame.savesData.language switch
      {
        "ru" => "Через " + level + " дней\nвы проголодались.",//Set the foodText to reflect the current player food total.
        "tr" => level + " Gün sonra açlıktan\nöldün.",//Set the foodText to reflect the current player food total.
        _ => "After " + level + " days,\nyou starved.",//Set the foodText to reflect the current player food total.
      };

      //Enable black background image gameObject.
      levelImage.SetActive(true);

      //Disable this GameManager.
      enabled = false;

      isGameOver = true;

      Invoke(nameof(Restart), restartGameDelay);
    }

    


    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies()
    {
      //While enemiesMoving is true player is unable to move.
      enemiesMoving = true;

      //Wait for turnDelay seconds, defaults to .1 (100 ms).
      yield return new WaitForSeconds(turnDelay);

      //If there are no enemies spawned (IE in first level):
      if (enemies.Count == 0)
      {
        //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
        yield return new WaitForSeconds(turnDelay);
      }

      //Loop through List of Enemy objects.
      for (int i = 0; i < enemies.Count; i++)
      {
        //Call the MoveEnemy function of Enemy at index i in the enemies List.
        enemies[i].MoveEnemy();

        //Wait for Enemy's moveTime before moving next Enemy, 
        yield return new WaitForSeconds(enemies[i].moveTime);
      }
      //Once Enemies are done moving, set playersTurn to true so player can move.
      playersTurn = true;

      //Enemies are done moving, set enemiesMoving to false.
      enemiesMoving = false;
    }


    //Restart reloads the scene when called.
    public void Restart()
    {
      

      //Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
      //and not load all the scene object in the current scene.
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);


      //enabled = true;
      //if (isGameOver)
      //  Destroy(gameObject);
    }
  }
}

