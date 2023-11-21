using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour 
{
	// Ссылка на Canva, на котором отображается кнопка перезапуска и сообщение о проигрыше
	public GameObject gameOverCanvas;

	void Start() 
	{
		// Делаем Canva невидимым в начале игры
		gameOverCanvas.SetActive(false);
	}

	public void ShowGameOverScreen() 
	{
		// Включаем Canva, чтобы показать кнопку перезапуска и сообщение о проигрыше
		gameOverCanvas.SetActive(true);
	}
	
	// Метод, который назначается на кнопку перезапуска
	public void RestartGame() 
	{
		// Загрузка сцены заново
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}