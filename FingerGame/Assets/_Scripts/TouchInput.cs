using UnityEngine;
using System.Collections;

using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TouchInput : MonoBehaviour {

	public LayerMask touchInputMask;

	private List<GameObject> touchList=new List<GameObject>();
	private GameObject[] touchesOld;
	private RaycastHit hit;

	public Text tCount;
	public Canvas gOver,btRst;
	public Text pt;
	public GameObject tgt;

	float  timeCount=6;
	int pnt=0;

	void Start(){
		gOver.enabled = false;
		btRst.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		
		if (timeCount > 0) {
			timeCount -= Time.deltaTime;
			tCount.text = ((int)timeCount).ToString ();
		} else { 			
			tgt.SetActive(false);
			tCount.enabled=false;
			btRst.enabled = true;
			gOver.enabled = true;
			pt.rectTransform.localPosition = new Vector3 (0, -40, 0);
			pt.resizeTextMaxSize = 40;
		}

		if(Input.touchCount>0){

			touchesOld = new GameObject[touchList.Count];
			touchList.CopyTo (touchesOld);
			touchList.Clear ();

			foreach(Touch touch in Input.touches){

				Ray ray =  Camera.main.ScreenPointToRay (touch.position);

				if(Physics.Raycast(ray,out hit,touchInputMask)){

					GameObject recipient=hit.transform.gameObject;
					touchList.Add (recipient);

					if(touch.phase==TouchPhase.Began){
						Debug.Log( recipient.gameObject.name );

						recipient.SendMessage("OnTouchDown",hit.point,SendMessageOptions.DontRequireReceiver);
					}					

					if(touch.phase==TouchPhase.Ended){
						recipient.SendMessage("OnTouchUp",hit.point,SendMessageOptions.DontRequireReceiver);
						pnt = pnt + 1;
						pt.text = "Pontos - " + pnt;
//						esfera.SetActive (false);
					}					

					if(touch.phase==TouchPhase.Stationary){
						recipient.SendMessage("OnTouchStay",hit.point,SendMessageOptions.DontRequireReceiver);
					}					

					if(touch.phase==TouchPhase.Canceled){
						recipient.SendMessage("OnTouchExit",hit.point,SendMessageOptions.DontRequireReceiver);
					}					


				}				
			}	

			foreach (GameObject g in touchesOld) {
				if (!touchList.Contains (g)) {
					g.SendMessage ("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
				}

			}
		}

	}


	public void OnClick(){
		pt.enabled = false;
		SceneManager.LoadScene ("Splash");
	}
}
