using UnityEngine;
using System.Collections;

public class AudioPlayOnce : MonoBehaviour {

    public AudioClip clip;
    AudioSource source;
    
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clip, 0.5f);
        StartCoroutine(destroy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
