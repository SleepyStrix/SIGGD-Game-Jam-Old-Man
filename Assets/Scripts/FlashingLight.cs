using UnityEngine;
using System.Collections;

public class FlashingLight : MonoBehaviour {
	private Light vLight;
	public AudioClip[] clipList;
	public AudioSource source;
	private int current = -1;
	private float[] samples = new float[64];

	void Start () {
		source = GetComponent<AudioSource>();
		vLight = GetComponent<Light>();
	}
	
	void Update () {
		source.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
		vLight.intensity = samples[0] * 7 + 1;
	}

	public void PlayAttackingAudio () {
		//Debug.Log ("playingaudio");
		int rand = Random.Range (0, 22);
		source.clip = clipList [rand];
		source.Play();
		current = 0;
	}

	public void StopAttackingAudio () {
		current = -1;
	}
}
