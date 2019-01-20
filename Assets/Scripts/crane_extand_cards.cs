using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class crane_extand_cards: MonoBehaviour
{
	[Header("Set the Handle")]
	public GameObject handle;
	public Transform my_hand;
	public Transform colider_box;
	[Header("Set Board Info")]
	//public GameObject[] FoldBoards;
	public List<GameObject> FoldBoards;
	public List<Vector3> Fold_intpos;
	public float long_B =1;
	public float short_B =1;
	public float board_thick = 0.3f;
	[Range(0.0f, 100.0f)]

	public float common_border = 1;
	int board_count;

	[Space(10)]
	[Header("Set Draw Mode")]
	public bool cube_mode = true;
	public enum draw_Selections // custom enumeration for different draw
	{
		flat_draw,
		pop_up,
		right_draw,
		album_draw
	};
    public draw_Selections draw_mode;
	[Header("only works in pop mode")]
	public float mult_h = 0.5f;
	public float mult_d = 0.5f;

	Vector3 start_pos;
	[Space(10)]
    [Header("Draw it!")]
	[Range(0.0f, 1.0f)]
	public float anim_time = 0;
	public float lerp_perct = 0.1f;
    public float lerp_album = 0.1f;
	[Space(10)]
    [Header("Rotate it")]
	[Range(0.0f, 180.0f)]
	public float all_rotate_x = 0;
	[Range(0.0f, 180.0f)]
	public float all_rotate_y = 0;
	[Range(0.0f, 180.0f)]
	public float all_rotate_z = 0;

	float max_length =1;

	int mode_index = 0;


	public float back_dis=0;
	public float front_dis = -1;
	Vector3 handle_maxpos, handle_min;
    
	public bool if_handin =false;



	float min_hand_a_dis = 0f;
	int min_hand_a_index = 0;
	public float select_a_y;
	// Use this for initialization
	void Start()
	{


		min_hand_a_dis = mult_d;



		if (draw_mode==draw_Selections.flat_draw){
			flat_setup();
		}
		else if(draw_mode == draw_Selections.right_draw){
			right_setup();
		}
		else if (draw_mode == draw_Selections.pop_up)
        {
            pop_setup();
		}else if (draw_mode == draw_Selections.album_draw)
        {
            album_setup();
        }

		//Debug.Log(max_length);
		// back_dis = transform.position.z;
		// front_dis = -max_length - 0.2f;

		select_a_y = FoldBoards[0].transform.position.y;
		//handle_maxpos = new Vector3(transform.position.x, transform.position.y - max_length, transform.position.z);
        //handle_min = transform.position;
    }

	// Update is called once per frame
	void Update()
	{

		// Debug.Log(handle.transform.position.z);
		if(handle.transform.position.z> back_dis){
			anim_time = anim_time * (1 - lerp_perct) + 0 * lerp_perct;
           
		}else if (handle.transform.position.z <front_dis){         
			anim_time = anim_time * (1 - lerp_perct) + 1 * lerp_perct;
		}else{         
			// float handle_tran = Math_maplimit(transform.position.z-handle.transform.position.z, front_dis, back_dis, 0, 1);
			float handle_tran = (transform.position.z-handle.transform.position.z)/ (back_dis-front_dis);
            
            //Debug.Log(handle_tran);
			anim_time = anim_time * (1 - lerp_perct) + handle_tran * lerp_perct;

		}



		if(anim_time > 0){
        if (draw_mode == draw_Selections.flat_draw)
            {
                flat();
            }
            else if (draw_mode == draw_Selections.right_draw)
            {
                right();
			}else if (draw_mode == draw_Selections.pop_up)
            {
                pop();
			} else if (draw_mode == draw_Selections.album_draw)
            {
                album();
            }         
			}
		transform.rotation = Quaternion.Euler(-all_rotate_x, all_rotate_y, all_rotate_z);
	}

	void album_setup(){


		board_count = 0;
        foreach (GameObject child in FoldBoards)
        {

            if (cube_mode)
            {
				child.transform.localScale = new Vector3(common_border, long_B,board_thick);

            }
            //else if (cube_mode)
            //{
            child.transform.localPosition = new Vector3(0, 0, - board_count * 0.001f);
            child.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //    child.transform.localScale = new Vector3(common_border, short_B, 0.01f);
            //}
            board_count++;
        }
        start_pos = FoldBoards[0].transform.localPosition;

		colider_box.localScale = new Vector3(common_border, long_B, board_thick);
		max_length = board_count*(0.1f + mult_d);
	}

	void album()
	{ 
//float elev = mult_h * anim_time * anim_time;
        float elevd = mult_d * anim_time * anim_time;
      
		for (int n = 0; n < board_count; n++)

		{ if (if_handin)
            {

				find_album(FoldBoards[0].transform.position.z - elevd * n, n);
            }
		}

	Debug.Log(min_hand_a_index);

        for (int i = 0; i < board_count; i++)
        {

           
			//float single_lerp = (i + 1) / board_count*0.1f;
			//if (i > 0)
            //{
                Vector3 cenpos;

			   float des_y;
                
              
			if (min_hand_a_index== i && if_handin)
            {
				des_y = long_B;
                cenpos = new Vector3(FoldBoards[i].transform.localPosition.x,des_y* (lerp_album)+FoldBoards[i].transform.localPosition.y*(1 - lerp_album), FoldBoards[0].transform.localPosition.z - elevd*i);
             
			}else{
				des_y =0;
                cenpos = new Vector3(FoldBoards[i].transform.localPosition.x,des_y* (lerp_album)+FoldBoards[i].transform.localPosition.y*(1-lerp_album), FoldBoards[0].transform.localPosition.z - elevd*i);
                            

			}
			FoldBoards[i].transform.localPosition = cenpos;

         
        }
        
		float coli_width = FoldBoards[board_count - 1].transform.localPosition.z - FoldBoards[0].transform.localPosition.z- board_thick;
		//Debug.Log(coli_width);
        

		colider_box.localPosition = new Vector3(colider_box.localPosition.x,  colider_box.localPosition.y,coli_width / 2);
		colider_box.localScale=new Vector3(common_border,long_B, -coli_width);
		min_hand_a_dis = long_B;
		min_hand_a_index = 0;
		//if_handin = false;
	}
 

	void find_album(float thisa_z, int this_index){
		int ori_in =min_hand_a_index;
		if (Mathf.Abs(thisa_z - my_hand.position.z) < min_hand_a_dis){
			min_hand_a_dis = Mathf.Abs(thisa_z - my_hand.position.z);
			min_hand_a_index = this_index;
		}
		if(ori_in !=min_hand_a_index)select_a_y = FoldBoards[0].transform.position.y;
	}   
    
	void pop_setup(){

		board_count = 0;
        foreach (GameObject child in FoldBoards)
        {

            if (cube_mode)
			{ child.transform.localScale = new Vector3(common_border, long_B, board_thick);

            }
            //else if (cube_mode)
            //{
			child.transform.localPosition = new Vector3(0, 0, board_count * 0.001f);
			child.transform.localRotation = Quaternion.Euler(90, 0, 0);
            //    child.transform.localScale = new Vector3(common_border, short_B, 0.01f);
            //}
            board_count++;
        }
        start_pos = FoldBoards[0].transform.localPosition;
		//max_length = board_count * long_B;
    }   

	void pop(){
		float elev = mult_h * anim_time*anim_time;
		float elevd = mult_d * anim_time * anim_time;
		//Debug.Log("e:" + elev +"   a:" +anim_time);
		//Debug.Log(anim_time);
		for (int i = 0; i < board_count; i++)
		{
			
			if(i>0){
				Vector3 cenpos;
			cenpos = new Vector3(FoldBoards[i].transform.localPosition.x, FoldBoards[i - 1].transform.localPosition.y + elev, FoldBoards[i - 1].transform.localPosition.z + elevd);
			FoldBoards[i].transform.localPosition = cenpos;
			}
		}

	}

	void flat_setup(){

		board_count = 0;

        foreach (GameObject child in FoldBoards)
        {

            if (board_count % 2 == 0 && cube_mode)
            {

				child.transform.localScale = new Vector3(common_border, long_B, 0.01f);

            }
            else if (cube_mode)
            {

				child.transform.localScale = new Vector3(common_border, short_B, 0.01f);
            }
            board_count++;
            }

        for (int i = 0; i < board_count; i++)
        {
            float m = Mathf.Floor(board_count / 2) - i;
            //Debug.Log(m);
            FoldBoards[i].transform.localPosition = new Vector3(0, (long_B - short_B) / 2 * m, m * 0.001f);
        }
        start_pos = FoldBoards[0].transform.localPosition;

		max_length = Mathf.Floor(board_count / 2) * short_B + Mathf.CeilToInt(board_count / 2) * long_B;
	}


	void flat(){

        Vector3 cenpos;
	
        for (int i = 0; i < board_count; i++)
        {
            Quaternion rotation;
            float ang = Mathf.PI * 0.5f * anim_time;
            if (i % 2 == 0)
            {


				rotation = Quaternion.Euler(90 * anim_time, 0, 0);



          
                
                //rotation = Quaternion.Euler(90 * anim_time, 0, 0);
                //Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);
                if (i > 0)
                {
                    cenpos = new Vector3(FoldBoards[i].transform.localPosition.x, FoldBoards[i - 1].transform.localPosition.y - Mathf.Cos(ang) * (long_B - short_B) * 0.5f, FoldBoards[i - 1].transform.localPosition.z - Mathf.Sin(ang) * (long_B + short_B) * 0.5f);
                   



					FoldBoards[i].transform.localPosition = cenpos;
                }
            }
            else
            {


				rotation = Quaternion.Euler(180 - 90 * anim_time, 0, 0);


 
                if (i > 0)
                {
                    cenpos = new Vector3(FoldBoards[i].transform.localPosition.x, FoldBoards[i - 1].transform.localPosition.y + Mathf.Cos(ang) * (short_B - long_B) * 0.5f, FoldBoards[i - 1].transform.localPosition.z - Mathf.Sin(ang) * (short_B + long_B) * 0.5f);
					FoldBoards[i].transform.localPosition = cenpos;

                }
            }

			FoldBoards[i].transform.localRotation = rotation;

        }
	}
	void right_setup(){

		board_count = 0;
        foreach (GameObject child in FoldBoards)
        {
            if (board_count % 2 == 0 && cube_mode)
            {
				child.transform.localScale = new Vector3( long_B,common_border, 0.01f);
            }
            else if (cube_mode)
            {
				child.transform.localScale = new Vector3( short_B,common_border, 0.01f);
            }
            board_count++;
         //Fold_intpos.Add(child.transform.localPosition);
        }

        for (int i = 0; i < board_count; i++)
        {
            float m = Mathf.Floor(board_count / 2) - i;
            //Debug.Log(m);
            FoldBoards[i].transform.localPosition = new Vector3((-long_B + short_B) / 2 * m,0, m * 0.001f);
        }
        start_pos = FoldBoards[0].transform.localPosition;
		max_length = Mathf.Floor(board_count / 2) * short_B + Mathf.CeilToInt(board_count / 2) * long_B;
	}

	void right()
    {
        Vector3 cenpos;
        for (int i = 0; i < board_count; i++)
        {
            Quaternion rotation;
            float ang = Mathf.PI * 0.5f * anim_time;
            if (i % 2 == 0)
            {
                rotation = Quaternion.Euler(0,90 * anim_time,  0);
                //Quaternion target = Quaternion.Euler(tiltAroundX, 0, 0);
                if (i > 0)
                {
					cenpos = new Vector3(FoldBoards[i-1].transform.localPosition.x+ Mathf.Cos(ang) * (long_B - short_B) * 0.5f, FoldBoards[i ].transform.localPosition.y , FoldBoards[i - 1].transform.localPosition.z - Mathf.Sin(ang) * (long_B + short_B) * 0.5f);
                    FoldBoards[i].transform.localPosition = cenpos;
                }
            }

            else
            {
                rotation = Quaternion.Euler(0,180 - 90 * anim_time,  0);
                if (i > 0)
                {

					cenpos = new Vector3(FoldBoards[i-1].transform.localPosition.x- Mathf.Cos(ang) * (short_B-long_B) * 0.5f, FoldBoards[i ].transform.localPosition.y , FoldBoards[i - 1].transform.localPosition.z - Mathf.Sin(ang) * (short_B + long_B) * 0.5f);
                    FoldBoards[i].transform.localPosition = cenpos;
                }
            }

            FoldBoards[i].transform.localRotation = rotation;
            //FoldBoards[0].transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5f);   
        }
    }


	float Math_maplimit(float va,float minva, float maxva,float min, float max){

		float old_Range = (maxva - minva);
        float new_Range = (max - min);
		float new_value = (((va - old_Range) * new_Range) / old_Range) + min;
		//Debug.Log(new_value);
		return Mathf.Clamp(new_value, min, max);      
        }


}




