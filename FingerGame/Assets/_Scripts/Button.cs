using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Button : MonoBehaviour {



	public Color dft;
	public Color slt;
	private Material mat;

	void Start(){
		mat = gameObject.GetComponent<Renderer>().material;	
	}

	void OnTouchDown(){
		mat.color = slt;
	}

	void OnTouchUp(){
		mat.color = dft;
	}

	void OnTouchStay(){
		mat.color = slt;	
	}

	void OnTouchExit(){
		mat.color = dft;	
	}
}
