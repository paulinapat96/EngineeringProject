using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour {


    [SerializeField] float flyMultiplier;

    Rigidbody rigidbody;

    void Start () {
        rigidbody = GetComponent<Rigidbody>();

        Movement.OnWingForce += AddForceToFly;

    }
	
	void Update () {  

    }

    void AddForceToFly(float force, bool isLeftWing)
    {
        if(isLeftWing) rigidbody.velocity = transform.forward + transform.right + Vector3.up * force * flyMultiplier;
        else rigidbody.velocity = transform.forward + (-transform.right) + Vector3.up * force * flyMultiplier;

        Debug.Log("ziuuu: " + force);
    }

    private void OnDestroy()
    {
        Movement.OnWingForce -= AddForceToFly;
    }
}
