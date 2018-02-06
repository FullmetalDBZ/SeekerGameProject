﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Transform rig;
    private Valve.VR.EVRButtonId touchpad = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
    public float speed; 

    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }

    private Vector2 axis = Vector2.zero;

    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void Update () { 
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        var device = SteamVR_Controller.Input((int)trackedObj.index);
        
        if (controller.GetTouch(touchpad))
        {
            axis = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);

            if (rig != null)
            {
                rig.position += (transform.right * axis.x + transform.forward * axis.y) * Time.deltaTime * speed;
                rig.position = new Vector3(rig.position.x, 0, rig.position.z);
            }
        }
    }
}
