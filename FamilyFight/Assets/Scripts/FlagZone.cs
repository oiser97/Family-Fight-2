
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagZone : 
    MonoBehaviour
{

    private Timer pointTimer;

    public int pointsPerTick;

    public float delay;

    // Start is called before the first frame update.

    void Start()
    {
        pointTimer = new Timer(delay);
    }

    // Update is called once per frame.

    void Update()
    {
        pointTimer.Update();
    }

    public bool CheckTimer()
    {
        return pointTimer.Check();
    }

}
