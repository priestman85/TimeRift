using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public int currentChapter = 0;
    public bool isGamePaused = false;

    [Header("Player")]
    public GameObject player;
    public Transform spawnPoint;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.position;
            player.transform.rotation = spawnPoint.rotation;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0f : 1f;
        Cursor.lockState = isGamePaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isGamePaused;
    }

    public void ChangeChapter(int newChapter)
    {
        currentChapter = newChapter;
        Debug.Log($"Chapter changed to {currentChapter}");
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("CurrentChapter", currentChapter);
        PlayerPrefs.Save();
        Debug.Log("Game saved!");
    }

    public void LoadGame()
    {
        currentChapter = PlayerPrefs.GetInt("CurrentChapter", 0);
        Debug.Log($"Game loaded! Chapter: {currentChapter}");
    }
}
