using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject gameHUD;

    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("LVLMNG"));
        Destroy(GameObject.FindGameObjectWithTag("Letters"));
        gameHUD = GameObject.FindGameObjectWithTag("HUD");
        gameHUD.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
