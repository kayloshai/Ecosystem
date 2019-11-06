using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {

    Vector3 velocity;
    Rigidbody myRigidBody;

	void Start () {
        myRigidBody = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    public void FixedUpdate()
    {
        myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);
    }
}

