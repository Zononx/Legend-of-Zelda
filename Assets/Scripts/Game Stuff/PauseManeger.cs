using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManeger : MonoBehaviour
{
    private bool isPaused;
    public GameObject pausePanel;
    public string mainMenu;

	public GameObject inventoryPanel;
	public bool usingInventory;

	// Start is called before the first frame update
	void Start()
    {
		isPaused = false;
		pausePanel.SetActive(false);
		inventoryPanel.SetActive(false);
		usingInventory = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !usingInventory)
        {
			ChangePause();
		}
		else if (Input.GetKeyDown(KeyCode.T) && !isPaused)
		{
			OpenInventory();
		}
	}
	public void ChangePause()
    {
		isPaused = !isPaused;
		if (isPaused)
		{
			pausePanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			pausePanel.SetActive(false);
			Time.timeScale = 1f;
		}
	}
    public void QuitToMenu()
    {
        SceneManager.LoadScene(mainMenu);
		Time.timeScale = 1f;
	}
	public void OpenInventory()
	{
		usingInventory = !usingInventory;
		if (usingInventory)
		{
			inventoryPanel.SetActive(true);
			Time.timeScale = 0f;
		}
		else
		{
			inventoryPanel.SetActive(false);
			Time.timeScale = 1f;
		}
	}
}
