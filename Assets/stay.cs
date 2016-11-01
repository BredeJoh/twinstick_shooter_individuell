using UnityEngine;
using System.Collections;

public class stay : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = gameObject.GetComponentInParent<Transform>().position;
	}
    
}
