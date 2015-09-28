using UnityEngine;
using System.Collections;

public class HeartMonitorAnim : MonoBehaviour {
    private Vector3 currentPos;
    private float health_slope;
    private float health_amp;
    private const float maxHeight = -0.2f;
    private const float minHeight = -0.5f;
    private bool beat = false;
    //boundaries are:
    //TOP = -0.25f
    //BOTTOM = -0.5f
    //LEFT = -1.0f
    //RIGHT = -0.5f
    private const float maxWidth = -0.5f;
    private const float minWidth = -1.0f;
    private const float initialX = -1f;
    private const float initialY = -0.4f;
    private const float initialZ = 1;
    private float[] health_slopes = {0.3f,1.0f,0.2f};//the different stages of the beat
    private int stage;
	void Start () {
        //camera range = -0.5 to 0.5 -1 to 1
        health_slope = 3f;//slope of the heart monitor's line
        health_amp = 0.25f*0.6f;//amplitude
        currentPos = new Vector3(initialX, initialY, initialZ);//camera.ScreenToWorldPoint(new Vector3(initialX, initialY, initialZ));
        stage = 0;
        //if (gameObject.name == "pulse2")
          //  GameObject.Find("pulse2").active = false;
    }

    void Update () {
        //goes from point A.x to point B.x
        //everytime it hits point B.x, it goes back to point A.x
        //it's position in y could fluctuate, but it's speed in x is always constant
        //if y goes up, it has to go down in the negative direction until it hits the bottom
        //currentPos = camera.WorldToScreenPoint(currentPos);
        //print(currentPos);
        //if(!GetComponent<TrailRenderer>().enabled)
       /// if (currentPos.x >= initialX+0.1f)
           // GetComponent<TrailRenderer>().enabled = true;
        currentPos.x += (Time.deltaTime*0.4f);
        if (currentPos.x >= maxWidth)//so it goes back from the left
        {
            //string otherPulse = gameObject.name.Substring(0,5);
            /*if (gameObject.name.Equals("pulse1"))
            {
                Debug.Log("pulse1 true");
                GameObject.Find("pulse2").active = true;
            }
            if (gameObject.name == "pulse2")
                GameObject.Find("pulse1").active = true;*/
            //print(otherPulse);
            //GameObject.Find(gameObject.name).active = true;
            //gameObject.active = false;
            //GetComponent<TrailRenderer>().enabled = false;
            //GetComponent<TrailRenderer>().enabled = !GetComponent<TrailRenderer>().enabled;
            currentPos.x = initialX;
            currentPos.y = initialY;
        }
        if(currentPos.x >= maxWidth - 0.1f)
        {
            currentPos.y = initialY;
        }
        if (currentPos.y >= maxHeight || currentPos.y <= minHeight)
            currentPos.y = initialY;

        //ypos
        if (Mathf.Abs(initialY-currentPos.y) >= (health_amp*health_slopes[stage]))//so it goes up and down
            health_slope = -health_slope;

        currentPos.y += (Time.deltaTime * health_slope*health_slopes[stage]);
        stage = (stage + 1) % health_slopes.Length;

        gameObject.transform.localPosition = currentPos;//camera.ScreenToWorldPoint(currentPos);
	}
}
