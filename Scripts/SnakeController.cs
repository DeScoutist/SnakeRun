using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeController : MonoBehaviour 
{
	private GameOverController gameOverController;
	private float speed = 10.0f;
	private float rotationSpeed = 90.0f;
	private float currentAngle = 0.0f;
	private bool isTurningRight = false;
	
	public float score = 0;
	public GameObject Score;
	public float speedIncrease = 0.01f; // значение, на которое скорость будет увеличиваться
	public float maxSpeed = 1.5f; // максимальная скорость
	public float currentSpeed = 1f; // текущая скорость
    
	void Start() 
	{
		// Получение ссылки на контроллер
		gameOverController = FindObjectOfType<GameOverController>();
	}

	void Update()
	{
		if (speed != 0)
		{
			score += 0.01f; // увеличиваем счет
			// Увеличиваем скорость
			currentSpeed += speedIncrease * Time.deltaTime;
			currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
			speed = currentSpeed * 10;
		}

		if (Input.GetMouseButtonDown(0) && speed != 0)
		{
			isTurningRight = !isTurningRight;

			// Переключаемся между 45 и -45 градусами
			currentAngle = isTurningRight ? 45.0f : -45.0f;
		}

		Quaternion targetRotation = Quaternion.Euler(0, currentAngle, 0);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

		// Продолжаем движение вперед
		transform.position += transform.forward * speed * Time.deltaTime;
		
		Score.GetComponent<TextMeshProUGUI>().text = MathF.Round(score).ToString();
	}

	void OnCollisionEnter(Collision collision)
	{
		// Проверка столкновения с объектом стеной или дочерним объектом Floor
		if (collision.gameObject.name.Equals("Wall(Clone)") 
		    || (collision.gameObject.transform.parent != null 
		        && collision.gameObject.transform.parent.name.Equals("Floor")))
		{
			speed = 0;
			gameOverController.ShowGameOverScreen();
		}
		
		// Проверяем столкновение, начинаем с самого объекта и поднимаемся по иерархии до корня
		Transform t = collision.gameObject.transform;
		while (t != null) 
		{
			if (t.name.Equals("Wall"))
			{
				speed = 0;
				gameOverController.ShowGameOverScreen();
				break;
			}
			t = t.parent;
		}
	}
}