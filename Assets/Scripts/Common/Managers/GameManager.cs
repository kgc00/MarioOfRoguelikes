using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string[] levels;
    public int currentLevelIndex;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Reload()
    {
        Load(levels[currentLevelIndex]);
    }

    public void NextLevel()
    {
        currentLevelIndex += 1;
        if (currentLevelIndex >= levels.Length)
        {
            currentLevelIndex = 0;
        }

        Load(levels[currentLevelIndex]);
    }

    public void Load(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public LevelData SetCurrentLevel()
    {
        LevelData request = (LevelData)Resources.Load("Levels/" + levels[currentLevelIndex], typeof(LevelData));
        if (request != null)
        {
            return request;
        }
        else
        {
            Debug.LogError("couldn't load data");
            return null;
        }

    }
}