using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float cameraSpeed;
	private CameraTargetController target;

	// Use this for initialization
	void Start () {
	
		target = FindObjectOfType<CameraTargetController>();

	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.Lerp (transform.position, target.transform.position, cameraSpeed);

	}
}
