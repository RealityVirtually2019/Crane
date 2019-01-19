using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class crane_extand_cards: MonoBehaviour
{
	//public List <GameObject> FoldBoards;
	//public GameObject Parent_Box;

	[Header("Set Board Info")]
	public GameObject[] FoldBoards;
	public List<Vector3> Fold_intpos;
	public float long_B =1;
	public float short_B =1;
	[Range(0.0f, 100.0f)]

	public float common_border = 1;
	int board_count;

	[Space(10)]
	[Header("Set Draw Mode")]
	public bool cube_mode = false;
	public enum draw_Selections // custom enumeration for different draw
	{
		flat_draw,
		pop_up,
		right_draw,
		album_unfinished
	};
    public draw_Selections draw_mode;
	[Header("only works in pop mode")]
	public float mult_h = 5f;
	public float mult_d = 5f;
	Vector3 start_pos;
	[Space(10)]
    [Header("Draw it!")]
	[Range(0.0f, 1.0f)]
	public float anim_time = 0;

	[Space(10)]
    [Header("Rotate it")]
	[Range(0.0f, 180.0f)]
	public float all_rotate_x = 0;
	[Range(0.0f, 180.0f)]
	public float all_rotate_y = 0;
	[Range(0.0f, 180.0f)]
	public float all_rotate_z = 0;

    

	int mode_index = 0;
    
	// Use this for initialization
	void Start()
	{
      
		if (draw_mode==draw_Selections.flat_draw){
			flat_setup();
		}
		else if(draw_mode == draw_Selections.right_draw){
			right_setup();
		}
		else if (draw_mode == draw_Selections.pop_up)
        {
            pop_setup();
        }
    }

	// Update is called once per frame
	void Update()
	{
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
            }         
			}
		transform.rotation = Quaternion.Euler(-all_rotate_x, all_rotate_y, all_rotate_z);
	}
	void pop_setup(){

		board_count = 0;
        foreach (GameObject child in FoldBoards)
        {

            if (cube_mode)
            { child.transform.localScale = new Vector3(common_border, long_B, 0.01f);

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
    }   

	void pop(){
		float elev = mult_h * anim_time*anim_time;
		float elevd = mult_d * anim_time * anim_time;
		Debug.Log("e:" + elev +"   a:" +anim_time);
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

}




