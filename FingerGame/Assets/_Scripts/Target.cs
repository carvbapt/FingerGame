using UnityEngine;
using System.Collections;
using UnityEngine.UI;



public class Target : MonoBehaviour {


	private int thrust;
//	public float thrust;
	public Rigidbody rb;

	void Start() 
	{
		thrust =(int)Random.Range(0.1f,9.9f);
			rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() 
	{
		rb.AddForce(transform.forward * thrust);
	}
		
}
