using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pigeon : MonoBehaviour
{

    [SerializeField] float flyMultiplier;
    [SerializeField] float forwardSpeed, leftSpeed, rightSpeed;
    [SerializeField] bool isGrounded;
    private Rigidbody rb;

    private Vector3 _movement;

    private Vector3 _drawPosition = new Vector3(-10, 0, -10);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        _movement = Vector3.zero;

        Movement.OnWingForce += AddForceToFly;
    }

    void Update()
    {
    }

    void AddForceToFly(float force, bool isLeftWing)
    {
        if (isLeftWing)
            _movement += transform.right * leftSpeed * force;
        else
            _movement -= transform.right * rightSpeed * force;

        _movement.z = forwardSpeed;
        _movement = Vector3.ClampMagnitude(_movement, forwardSpeed);
        _movement.y = flyMultiplier * force;

        rb.velocity = _movement;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_drawPosition, _drawPosition + _movement);
    }
}