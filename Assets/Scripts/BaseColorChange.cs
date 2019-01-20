using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseColorChange : MonoBehaviour {


    public GameObject buttonSurface;
    public Vector3 buttonSurfaceOriPos;
    float buttonSurOriDist;
    float buttonCurrentPosY;
    float buttonOriPosY;
    public float buttonPressHeight;

    MeshRenderer buttonBaseMat;
    float buttonBaseOriAlpha;
    float buttonBaseCurAlpha;
    float buttonBaseOriPosY;
    public Color currentBaseColor;

    public float speed;

    void Start () {
        buttonSurfaceOriPos = buttonSurface.transform.position;
        buttonOriPosY = buttonSurface.transform.position.y;

        buttonBaseMat = GetComponent<MeshRenderer>();
        buttonBaseOriPosY = this.transform.position.y;

        buttonSurOriDist = buttonSurface.transform.position.y - this.transform.position.y;
        buttonBaseOriAlpha = GetComponent<MeshRenderer>().material.color.a;
	}
	
	// Update is called once per frame
	void Update () {
        buttonPressHeight = buttonSurface.transform.position.y - this.transform.position.y;
        // buttonSurface.transform.position = new Vector3 (buttonSurface.transform.position.x, 
        //         Mathf.Clamp(buttonCurrentPosY,buttonBaseOriPosY, buttonOriPosY), 
        //         buttonSurface.transform.position.z);

        //float normal = Mathf.Lerp(0, buttonSurOriDist, buttonPressHeight);

        float normal = 1 - (buttonPressHeight / buttonSurOriDist);
        //buttonBaseCurAlpha = Mathf.Lerp(buttonBaseOriAlpha, 1, normal * speed);
        buttonBaseCurAlpha =  normal * (0.8f - buttonBaseOriAlpha);
        //if(buttonPressHeight < 0 )
        //{
        //    buttonBaseCurAlpha = 1;
        //}

        currentBaseColor = buttonBaseMat.material.color;
        currentBaseColor.a = buttonBaseCurAlpha;
        buttonBaseMat.material.color = currentBaseColor;



        //Debug.Log(currentBaseColor.a);


        //if(buttonPressHeight < 0.01)
        //{
        //    this.transform.SetParent(buttonSurface.transform);
        //}
	}

   

}
