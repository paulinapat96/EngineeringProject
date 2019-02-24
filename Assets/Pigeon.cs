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

    void AddForceToFly(float force)
    {
        rigidbody.velocity = transform.forward + Vector3.up * force * flyMultiplier;
        Debug.Log("ziuuu: " + force);
    }

    private void OnDestroy()
    {
        Movement.OnWingForce -= AddForceToFly;
    }
}
