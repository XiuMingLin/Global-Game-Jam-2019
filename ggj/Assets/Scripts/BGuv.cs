using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGuv : MonoBehaviour {

    public Renderer m_Renderer;

    public Vector2 m_Offset;
    public float m_Speed = 5;

	void Start () {
        m_Renderer = GetComponent<Renderer>();
        m_Offset = Vector2.left;
	}
	

	void Update () {

        m_Renderer.material.SetTextureOffset("_MainTex", m_Offset * Time.time * m_Speed);
	}
}
