using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{    
    public Image HealthBar;
    public Sprite[] HealthSprites;
    public GameObject pauseMenuUI;
    private bool gamePaused;

    void Start()
    {
        gamePaused = false;
        pauseMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.sprite = HealthSprites[GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().health];
        if (Input.GetButtonDown("Exit"))
        {
            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
