using UnityEngine;
using System.Collections;

public class BatteryLife : MonoBehaviour {
    private float battery;
	// Use this for initialization
	void Start () {
        battery = 100;
	}
	
	// Update is called once per frame
	void Update () {
        battery -= Time.deltaTime * 3.5f;//bloody inefficient!
        transform.Find("R1").gameObject.SetActive(true);
        transform.Find("R2").gameObject.SetActive(true);
        transform.Find("R3").gameObject.SetActive(true);
        transform.Find("L1").gameObject.SetActive(true);
        transform.Find("L2").gameObject.SetActive(true);
        transform.Find("L3").gameObject.SetActive(true);
        if (battery <= 60)
        {
            transform.Find("R1").gameObject.SetActive(false);
            transform.Find("L1").gameObject.SetActive(true);
        }
        if (battery <= 30)
        {
            transform.Find("R2").gameObject.SetActive(false);
            transform.Find("L2").gameObject.SetActive(true);

        }
        if (battery == 0)
        {
            transform.Find("R3").gameObject.SetActive(false);
            transform.Find("L3").gameObject.SetActive(true);
        }
    }
}
