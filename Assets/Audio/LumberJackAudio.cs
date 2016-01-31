using UnityEngine;
using System.Collections;

public class LumberJackAudio : MonoBehaviour {
	public AudioSource output;

	public AudioClip[] Chop = new AudioClip[3];
	public AudioClip[] Hello = new AudioClip[3];
	public AudioClip[] Happy = new AudioClip[3];

	void Start () {
		output = gameObject.GetComponent<AudioSource> ();

		// lets get guigly!
		output.pitch = Random.Range(0.5f, 1.5f);
	}

	public void playChop() {
		int i = (int)Mathf.Floor(Random.Range (0.0f, 2.9f));
		output.clip = Chop [i];
		output.Play ();
	}
		
	public void playHello() {
		int i = (int)Mathf.Floor(Random.Range (0.0f, 2.9f));
		output.clip = Hello [i];
		output.Play ();
	}

	public void playHappy() {
		int i = (int)Mathf.Floor(Random.Range (0.0f, 2.9f));
		output.clip = Happy [i];
		output.Play ();
	}
}
