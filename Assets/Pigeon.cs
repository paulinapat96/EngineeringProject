using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{

    [SerializeField] float flyMultiplier;
    [SerializeField] float forwardSpeed, leftSpeed, rightSpeed;

    Rigidbody rigidbody;
    Camera cam;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;

        Movement.OnWingForce += AddForceToFly;
    }

    void Update()
    {
        //Debug.Log(cam.transform.eulerAngles.t)
        transform.rotation = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0);
    }

    void AddForceToFly(float force, bool isLeftWing)
    {
        if (isLeftWing)
            rigidbody.velocity = transform.forward * forwardSpeed + transform.right * leftSpeed + Vector3.up * force * flyMultiplier;
        else
            rigidbody.velocity = transform.forward * forwardSpeed + (-transform.right * rightSpeed) + Vector3.up * force * flyMultiplier;

        Debug.Log("ziuuu: " + force);
    }

    private void OnDestroy()
    {
        Movement.OnWingForce -= AddForceToFly;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + rigidbody.velocity);
    }
}
