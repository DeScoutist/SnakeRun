using UnityEngine;

public class FollowCamera : MonoBehaviour
{
	public Transform target;
	public Vector3 offset; 
	public Vector3 rotation; 

	void Update()
	{
		transform.position = target.position + offset; // позиция камеры равна позиции цели + смещение
		transform.LookAt(target); // камера смотрит на цель
	}
}