using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    private static Player _instance;

    public static Player Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (Instance == null)
            _instance = this;
    }

    public bool isStonePush = false;

    public Transform FallWallTran;
    public GameObject FallWallobj;

    public float fallMultiplier = 2.5f;
    public float lowMultiplier = 2f;

    public float jumpValue;
    private Rigidbody2D rd2d;
    private Animator anim;

    bool isJump = false;
    bool isGround = true;

    //SkilllA
    public bool CanSkillA = false;
    public GameObject Ball;
    public GameObject skillball;

    //RestartPos
    public Transform RestartPos;

    public GameObject stone;

    public int Health;
    public Text healthText;

    public Text Save;

    public bool CanFood = false;
    public GameObject Food;

    public GameObject stoneQ;
    public Transform stoneQObj;

    public Image GameEnd;
    public Text GameEndText;


    public bool isDead = false;

    public Image haveFood;
    //public SpriteRenderer SpriteRendererrender;
    public Material skillIconMat;
    // Use this for initialization
    void Start () {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        skillIconMat = haveFood.material;
        stoneQObj = stoneQ.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead)
            return;
		if(Input.GetKey(KeyCode.RightArrow))
        {

            transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(0.1f, 0, 0);
            anim.SetFloat("Run", 1.0f);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.Translate(-0.1f, 0, 0);
            anim.SetFloat("Run", 1.0f);
        }
        else
        {
            anim.SetFloat("Run", 0);
        }

        if(Input.GetKeyDown(KeyCode.Space)&&!isJump&&isGround)
        {
            rd2d.velocity = Vector2.up * jumpValue;
            isJump = true;
        }
        BestJump();
        SkillA();
        if (!CanSkillA)
        {
            //haveFood.SetActive(false);
            skillIconMat.shader = Shader.Find("Sprites/Gray");
            //Debug.Log(GetComponent<SpriteRenderer>().material.GetInt("_DualGrid"));
            //GetComponent<SpriteRenderer>().material.SetInt("_DualGrid", 0);
            //GetComponent<SpriteRenderer>().material.DisableKeyword("_ShowOutline_ON");
        }
        if (Health <= 0)
            GameOver();
	}

    void BestJump()
    {
        if (rd2d.velocity.y < 0) 
        {
            rd2d.velocity += Vector2.up * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rd2d.velocity.y>0&&!Input.GetKey(KeyCode.Space))
        {
            rd2d.velocity += Vector2.up * (lowMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag=="Ground")
        {
            isJump = false;
            isGround = true;
        }
        if(collision.transform.name=="Stone")
        {
            Health--;
            healthText.text = "X " + Health.ToString();
            transform.position = RestartPos.position;
            stoneQ.transform.position = stoneQObj.transform.position;
            stoneQ.SetActive(false);
        }
        if(collision.transform.tag=="FallWall")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isJump = false;
            isGround = true;
        }
    }

    void SkillA()
    {
        if(Input.GetKeyDown(KeyCode.Z)&&CanSkillA)
        {
            
            Instantiate(Ball, new Vector3(transform.position.x + 0.1f, transform.position.y,0), Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            skillball = GameObject.Find("SkillA(Clone)");
            if(skillball!=null)
                gameObject.transform.position = skillball.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Restart")
        {
            Save.gameObject.SetActive(true);
            Save.text = "Press E To Save";
        }
        if (collision.tag == "Enemy")
        {
            Health--;
            healthText.text = "X " + Health.ToString();
            transform.position = RestartPos.position;
            stone.SetActive(true);
            FallWallobj.transform.position = FallWallTran.position;
            FallWallobj.gameObject.GetComponent<Rigidbody2D>().gravityScale=0;
        }
        if (collision.name == "End")
        {
            isDead = true;
            GameEndText.text = "Wellcome back";
            GameEnd.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Restart")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                RestartPos = collision.transform;
                Save.text = "Save Successful ! ! !";
            }
        }
        if(collision.tag=="Skill")
        {
            Save.gameObject.SetActive(true);
            Save.text = "Press Z and X Use Skill";
        }
        if (collision.name == "OtherHome")
        {
            Save.gameObject.SetActive(true);
            Save.text = "This is not your home";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag=="Restart")
        {
            Save.gameObject.SetActive(false);
        }
        if(collision.tag=="Skill")
        {
            Save.gameObject.SetActive(false);
        }
        if (collision.name == "OtherHome")
        {
            Save.gameObject.SetActive(false);
        }
        if (collision.name == "StoneTrigger")
            StartCoroutine("StoneStart");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            isGround = false;
        }
    }

    IEnumerator StoneStart()
    {
        yield return new WaitForSeconds(1);
        stoneQ.SetActive(true);
    }

    void GameOver()
    {
        isDead = true;
        GameEndText.text = "Game Over";
        GameEnd.gameObject.SetActive(true);
    }
}
