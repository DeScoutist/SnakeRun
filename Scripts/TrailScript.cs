using UnityEngine;

namespace DefaultNamespace
{
	[RequireComponent(typeof(TrailRenderer))]
	public class TrailScript : MonoBehaviour 
	{
		private TrailRenderer trail;
		private float initialTrailTime;

		void Start() 
		{
			trail = GetComponent<TrailRenderer>(); // получаем компонент TrailRenderer
			initialTrailTime = trail.time; // сохраняем исходную длину трейла
		}

		void Update()
		{
			var snake = transform.gameObject.GetComponent<SnakeController>();
			// предположим, что currentSpeed - это текущая скорость вашего объекта
			float speedRatio = snake.currentSpeed / snake.maxSpeed; // вычисляем отношение текущей скорости к максимальной
			// Устанавливаем длину трейла в зависимости от скорости. Если скорость максимальная, длина трейла будет в два раза больше исходной.
			trail.time = initialTrailTime + (initialTrailTime * speedRatio);
		}
	}
}