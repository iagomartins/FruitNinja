using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
	public Camera cam;
	public GameObject touchObj;
	void Update()
	{
		#if UNITY_EDITOR
		if (Input.GetButton("Fire1"))
        {
            touchObj.SetActive(true);

			Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray))
            {            
                Vector3 p = ray.GetPoint(10);
                touchObj.transform.position = p;
               
            }
        }
        else {
            touchObj.SetActive(false);
        }
        #endif
        #if UNITY_ANDROID
		if (Input.touchCount == 1)
        {
            touchObj.SetActive(true);

			Ray ray = cam.ScreenPointToRay(Input.GetTouch(0).position);

			if (Physics.Raycast(ray))
            {            
				Vector3 p = ray.GetPoint(10);
				touchObj.position = p;
                
            }
        }
        else {
            touchObj.SetActive(false);
        }
        #endif
             
     }
}