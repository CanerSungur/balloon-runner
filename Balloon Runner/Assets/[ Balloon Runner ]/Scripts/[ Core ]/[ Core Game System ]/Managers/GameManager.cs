using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver, GameIsStarted, LevelIsWon, PlatformIsOver;

    private void OnEnable()
    {
        #region Base Events

        EventManager.OnGameStart.AddListener(GameStart);
        EventManager.OnGameEnd.AddListener(GameOver);
        EventManager.OnPlatformOver.AddListener(PlatformOver);
        EventManager.OnLevelContine.AddListener(LevelContinue);
        EventManager.OnLevelFinish.AddListener(LevelFinish);
        EventManager.OnLevelFail.AddListener(LevelFail);
        EventManager.OnLevelStart.AddListener(LevelStart);
        EventManager.OnLevelSuccess.AddListener(LevelSuccess);

        #endregion
    }

    private void OnDisable()
    {
        #region Base Events

        EventManager.OnGameStart.RemoveListener(GameStart);
        EventManager.OnGameEnd.RemoveListener(GameOver);
        EventManager.OnPlatformOver.RemoveListener(PlatformOver);
        EventManager.OnLevelContine.RemoveListener(LevelContinue);
        EventManager.OnLevelFinish.RemoveListener(LevelFinish);
        EventManager.OnLevelFail.RemoveListener(LevelFail);
        EventManager.OnLevelStart.RemoveListener(LevelStart);
        EventManager.OnLevelSuccess.RemoveListener(LevelSuccess);

        #endregion
    }

    private void Awake()
    {
        GameIsOver = GameIsStarted = LevelIsWon = PlatformIsOver = false;
    }

    private void GameStart() 
    {
        GameIsStarted = true;
        Debug.Log("Game is Started!");
    }
    private void GameOver() 
    {
        GameIsOver = true;
        Debug.Log("Game is Over!");
    }

    private void PlatformOver()
    {
        PlatformIsOver = true;
        Debug.Log("Platform is Over!");
    }

    #region Level Functions

    private void LevelSuccess()
    {
        LevelIsWon = true;
    }

    private void LevelStart() { }

    private void LevelFail() { }

    private void LevelContinue() { }

    private void LevelFinish() { }

    #endregion
}
