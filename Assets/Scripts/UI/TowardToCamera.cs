using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowardToCamera : MonoBehaviour
{
    
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
