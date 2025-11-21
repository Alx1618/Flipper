using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BehaviourScript : MonoBehaviour
{
    public float launchForce = 500f;
    


    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.forward * launchForce);
        }
     
    }

   
   
}