using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
    //Constants
    //maximum values for your player's stats
    private const int MaxEnergy = 100;
    private const int MaxLeftEarBattery = 100;
    private const int MaxRightEarBattery = 100;

    //player's stats
    private int leftEarBattery;//battery life of left ear
    private int rightEarBattery;//battery life of right ear
    private int heartStat;//status of heart, higher = heart attack and shizz, lower = heart dying and shizz
    //heartStat caps at 100, if it goes above 100, it goes back to 0 to signify death. (mod it)
    //private int heartRate_amplitude;//amplitude of heart monitor
    //private int heartRate_period;//period of the heart monitor
    private int energy;//amount of energy, 0 = no more energy, 100 = full energy

    // Use this for initialization
    void Start () {
        leftEarBattery = MaxLeftEarBattery;
        rightEarBattery = MaxRightEarBattery;
        energy = MaxEnergy;
        heartStat = 50;//50 = initial = healthy; 100 = crazy, 0 = death. going past 100 = goes back to 0	
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getEnergy()
    {
        return energy;
    }
    public int getHeartStat()
    {
        return heartStat;
    }
    public int getLeftEarBattery()
    {
        return leftEarBattery;
    }
    public int getRightEarBattery()
    {
        return rightEarBattery;
    }

    //updates heart status based on..??
    //needs to add fixedUpdate for battery life based on actual time instead of frames
}
