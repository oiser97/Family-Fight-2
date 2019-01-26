
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
        pointTimer = new Timer(false, delay);
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

    public void ResetTimer()
    {
        pointTimer.Reset();
    }

}
