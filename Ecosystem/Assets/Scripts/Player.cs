using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
    
	void Start () {
		
	}
	
	void Update () {
        //Playter movement input
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
	}
}
