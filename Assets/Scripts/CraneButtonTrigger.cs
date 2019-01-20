using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneButtonTrigger : MonoBehaviour {

    bool buttonIsThouched = false;
    bool buttonIsTriggered = false;
    public bool buttonIsBouncingBack = false;
    public float bounceSmooth = 1f;

    public BaseColorChange buttonBaseData;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (buttonBaseData.buttonPressHeight <= 0)
        {
            buttonIsTriggered = true;
        }
        else
        {
            buttonIsTriggered = false;
        }

        Debug.Log(buttonIsTriggered);


        if(buttonIsBouncingBack)
        {
            buttonBaseData.buttonSurface.transform.position = Vector3.Lerp(buttonBaseData.buttonSurface.transform.position,
                                                                           buttonBaseData.buttonSurfaceOriPos,
                                                                           Time.deltaTime * bounceSmooth);
        }
	}


    public void ButtonBounceBack()
    {
        buttonIsBouncingBack = true;
    }

    public void ButtonContact()
    {
        buttonIsBouncingBack = false;
    }
}
