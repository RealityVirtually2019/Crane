using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class select_album : MonoBehaviour {

	// Use this for initialization

	public crane_extand_cards albums;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//void OnCollisionEnter(Collision col)
 //   {
	//	if (col.gameObject.name == "collider_tracker")
 //       {
	//		albums.if_handin = true; 
	//	}
 //   }

	//void OnCollisionExit(Collision col)
    //{
    //    if (col.gameObject.name == "collider_tracker")
    //    {
    //        albums.if_handin = false;
    //    }
    //}

	 void OnTriggerEnter(Collider col)
    {
		if (col.gameObject.name == "collider_tracker")
        {
      albums.if_handin = true; 
  }
    }
	void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "collider_tracker")
        {
            albums.if_handin = false;
        }
    }
}
