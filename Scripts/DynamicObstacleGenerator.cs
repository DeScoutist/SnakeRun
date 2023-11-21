using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DynamicObstacleGenerator : MonoBehaviour
{
	// Очередь для хранения текущих активных объектов на поле.
	private Queue<GameObject> gameObjects = new Queue<GameObject>();

	// Текущий размер поля.
	private int currentSize = 0;

	// Максимальный размер поля.
	public int maxSize = 35;
	private float travelledDistance = 0;
	private Vector3 previousPosition;
	public int distanceInFront = -80;
	public int distanceBetween = 40;
	public GameObject obstaclePrefab;
	public GameObject wallSectionPrefab;
	public GameObject floorSectionPrefab;
	public int minWallSize = 4; // Минимальный размер стены
	public int gapSize = 4;
	public int numberOfGaps = 2;

	private void Start()
	{
		// Инициализация начальной позиции.
		previousPosition = transform.position;
    
		// Изначальное создание трех секций препятствий.
		for (int i = 1; i < 3; i++)
		{
			GenerateObstacleRow(distanceBetween * i);
			GenerateFloor(distanceBetween * i);
		}
		
	}

	private void Update()
	{
		travelledDistance += Vector3.Distance(previousPosition, transform.position);

		// Если пройдено расстояние более или равно 10
		if (travelledDistance >= distanceBetween)
		{
			UpdateField();
			travelledDistance -= distanceBetween;
		}

		previousPosition = transform.position;
	}

	// Функция для обновления поля.
	public void UpdateField()
	{
		// Создайте новую строку или столбец объектов.
		GenerateObstacleRow(transform.position.z + distanceBetween * 2);
		GenerateFloor(transform.position.z + 60);

		// Если размер поля превышает максимальный, удаляем старые объекты.
		if (currentSize > maxSize)
		{
			var oldObject = gameObjects.Dequeue();
			Destroy(oldObject);
			currentSize--;
		}
	}

	// Функция для создания новой строки или столбца объектов.
	private void GenerateObstacleRow(float z)
	{
		gapSize = Random.Range(5, 10);
		numberOfGaps = Random.Range(2, 3);
		// Генериуем позиции дырок + 2 на краях
		List<int> gapPositions = new List<int> { -25, 25 };
		while (gapPositions.Count < numberOfGaps + 2)
		{
			int gapPos = Random.Range(-25 + gapSize, 25 - gapSize);
			if (!gapPositions.Exists(pos => Math.Abs(pos - gapPos) < gapSize))
			{
				gapPositions.Add(gapPos);
			}
		}

		// Сортируем
		gapPositions.Sort();

		// Создаем сегменты стены между дырами
		for (int i = 0; i < gapPositions.Count - 1; i++)
		{
			int start = gapPositions[i] + gapSize / 2;
			int end = gapPositions[i + 1] - gapSize / 2;

			if(Math.Abs(end - start) < minWallSize)
			{
				continue; // Если пространства недостаточно, пропустить итерацию
			}

			float sizeX = end - start;
			float posX = start + sizeX / 2;

			Vector3 spawnPosition = new Vector3(posX, transform.position.y, z + distanceInFront);
			GameObject wallSection = Instantiate(wallSectionPrefab, spawnPosition, Quaternion.identity);
			wallSection.transform.localScale = new Vector3(sizeX, wallSection.transform.localScale.y,
				wallSection.transform.localScale.z);

			gameObjects.Enqueue(wallSection);
			currentSize++;
		}
	}

	private void GenerateFloor(float z)
	{
		Vector3 spawnPosition = new Vector3(0, transform.position.y - 0.5f, z + distanceInFront);
		GameObject floorSection = Instantiate(floorSectionPrefab, spawnPosition, Quaternion.identity);
		
		gameObjects.Enqueue(floorSection);
		currentSize++;
	}
}