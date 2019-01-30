using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillA : MonoBehaviour {

    public float DeadTime;

	// Use this for initialization
	void Start () {
        Player.Instance.CanSkillA = false;
        Destroy(gameObject, DeadTime);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * 0.2f,Space.World);
        transform.Rotate(new Vector3(0, 0, 3));
	}
}
