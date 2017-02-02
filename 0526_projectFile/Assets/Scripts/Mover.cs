using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {


    public float speed;
    public float x_axis;
    public float y_axis;
    public float z_axis;
    public float reverse_time;

	float timer;
	int waitingTime;
    float elapsedTime;

    int dir;// 이동방향 +-

	// Use this for initialization
	void Start () {
		timer = 0.0f;
		waitingTime = 1;
        elapsedTime = 0.0f;
        dir = 1;
	}


	// Update is called once per frame
	void Update () 
	{	
		timer += Time.deltaTime;
        elapsedTime += Time.deltaTime;

		if(timer > waitingTime)
		{
			timer = 0;
		}
        
        transform.Translate(new Vector3(x_axis, y_axis, z_axis) * Time.deltaTime * speed * dir);

        if (elapsedTime >= reverse_time)
        { 
            dir = dir * (-1);
            elapsedTime = 0.0f;
        }
       
	}


}

