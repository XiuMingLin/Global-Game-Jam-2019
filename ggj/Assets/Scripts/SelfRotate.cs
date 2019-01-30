using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotate : MonoBehaviour {

    public List<GameObject> rebornfoods;
    public GameObject food;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        foreach (var item in rebornfoods)
        {
            item.transform.Rotate(new Vector3(0, 1, 0));
            if(item.activeInHierarchy==false)
            {
                StartCoroutine("Reborn",item);
            }
        }
        if(food.activeInHierarchy==false&&Player.Instance.isStonePush)
        {
            StartCoroutine("Reborn", food);
        }
    }

    IEnumerator Reborn(GameObject _item)
    {
        yield return new WaitForSeconds(5);
        _item.SetActive(true);
    }
}
