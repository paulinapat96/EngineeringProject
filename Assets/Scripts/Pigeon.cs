using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{

    [SerializeField] float flyMultiplier;
    [SerializeField] float forwardSpeed, leftSpeed, rightSpeed;
    [SerializeField]  bool isGrounded;
    private Rigidbody rigidbody;

    Camera cam;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        cam = Camera.main;
        isGrounded = true;

        Movement.OnWingForce += AddForceToFly;
    }


    void Update()
    {
     //   transform.rotation = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0);
    }

    void AddForceToFly(float force, bool isLeftWing)
    {
        if (isLeftWing)
            rigidbody.velocity = transform.forward * forwardSpeed + transform.right * leftSpeed + Vector3.up * force * flyMultiplier;
        else
            rigidbody.velocity = transform.forward * forwardSpeed + (-transform.right * rightSpeed) + Vector3.up * force * flyMultiplier;

        Debug.Log("ziuuu: " + force);
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8
            && !isGrounded)
        {
            isGrounded = true;
            GetComponentInChildren<Animation>().Play("PigeonShakeAnim 1");
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 8
            && isGrounded)
        {
            isGrounded = false;
        }
    }

    private void OnDestroy()
    {
        Movement.OnWingForce -= AddForceToFly;
    }
}
