using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {


    void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player.Instance.CanSkillA = true;
            //Player.Instance.haveFood.SetActive(true);
            Player.Instance.skillIconMat.shader = Shader.Find("UI/Default");
            //StartCoroutine(WaiFaGuang());
            //Player.Instance.GetComponent<SpriteRenderer>().material.SetInt("_DualGrid", 1);
            //Player.Instance.GetComponent<SpriteRenderer>().material.EnableKeyword("_ShowOutline_ON");
            gameObject.SetActive(false);
        }
    }

    //IEnumerator WaiFaGuang()
    //{

    //    yield return new WaitForSeconds(0.5f);
    //    Player.Instance.GetComponent<SpriteRenderer>().material.SetFloat("_DualGrid", 1);
    //    Player.Instance.GetComponent<SpriteRenderer>().material.EnableKeyword("_DualGrid_ON");
    //    yield return new WaitForSeconds(0.5f);
    //    Player.Instance.GetComponent<SpriteRenderer>().material.SetInt("_DualGrid", 0);
    //    Player.Instance.GetComponent<SpriteRenderer>().material.DisableKeyword("_DualGrid_ON");
    //}


}
