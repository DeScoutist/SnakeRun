using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour {

	public void PlayGame()
	{
		SceneManager.LoadScene("Game"); // Замените "GameScene" именем вашей игровой сцены
	}
	
	public void ExitGame()
	{
		Application.Quit();
	}
}