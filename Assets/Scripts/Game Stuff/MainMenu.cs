using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene("OpeningCutscene");

    }
	public void Quit()
	{
        Application.Quit();
	}
    // for gameOver scene
	public void QuitToMenu()
	{
		SceneManager.LoadScene("StartMenu");
		Time.timeScale = 1f;
	}
    public void GameOver()
    {
		SceneManager.LoadScene("GameOverScene");
		Time.timeScale = 1f;
	}
    public void EndGame()
    {
		SceneManager.LoadScene("EndScene");
		Time.timeScale = 1f;
	}
    public void LoadTestScene()
    {
        SceneManager.LoadScene("TestingTimeLine");
    }
}
