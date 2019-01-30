using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMove : MonoBehaviour {

    public GameObject ground;
    public float speed;
    public List<Transform> pos;
    private float Timer;
    public float limit;
    private int b;
    public bool bo;
    private float distance;
    private int c;
	// Use this for initialization
	void Start () {
        b = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if (b == -1)
            c = 0;
        else
            c = 1;

        distance =(pos[c].position - ground.transform.position).magnitude;
        if (distance<0.5)
        {
            Timer = 0;
            b = -b;
            if(b>0)
            limit = Random.Range(4, 10);
        }
        haha(b,bo);
    }
    void haha(int b,bool count)
    {
        if (count == true)
            ground.transform.Translate(speed* b, 0, 0);
        else
            ground.transform.Translate(0, speed * b, 0);
    }
}
