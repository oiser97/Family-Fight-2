﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : 
    MonoBehaviour
{

    public Vector3      offset;

    public Camera       targetCamera;

    public GameObject   player1,

                        player2;

    // Start is called before the first frame update.

    void Start()
    {
        
    }

    // Update is called once per frame.

    void Update()
    {
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);
        targetCamera.transform.position = new Vector3(offset.x + distance, offset.y + distance, transform.position.z);
    }

}
