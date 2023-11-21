using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	private GameOverController gameOverController;
   
	void Start() 
	{
		// Получение ссылки на контроллер
		gameOverController = FindObjectOfType<GameOverController>();
	}

	void OnCollisionEnter(Collision collision)
	{
		// Проверка столкновения с объектом со стеной
		if (collision.gameObject.name.Equals("Wall(Clone)")) 
		{
			gameOverController.ShowGameOverScreen();
		}
	}
}