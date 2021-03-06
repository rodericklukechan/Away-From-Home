﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public AudioSource AS;

    public static PlayerController instance;
	public GameObject Winning;
    Animator anim;

    void setAnimation(string animationName) {
        string[] names = new string[4];
        names[0] = "WalkFront";
        names[1] = "WalkBack";
        names[2] = "WalkLeft";
        names[3] = "WalkRight";
        foreach (string animation in names) {
            if (animation == animationName) {
                anim.SetBool(animation, true);
            } else {
                anim.SetBool(animation, false);
            }
            
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        Debug.Log(col.name);
		if (col.name == "EndPoint") {

            GameManager.instance.Win();
//			Winning.SetActive (true);
			Debug.Log ("Win");
        }else if (col.name == "CatRange"){
            col.transform.parent.GetComponent<CatController>().Alarm();

        } else if (col.name == "Line of Sight") {
            //	Debug.Log ("Parent Triggered");

        } else if (col.name == "Parent") {//it's a trigger of parent trigger area
        }
        if (col.tag == "Wood")
        {
            if (onGrass) return;
            playGrass();
            onGrass = true;
        }
        else if (col.tag == "Path")
        {
            if (!onGrass) return;
            playWood();
            onGrass = false;
        }
        else {  }

	}
    public bool onGrass = true;
	// Use this for initialization
	void Start () {
        AS = GetComponent<AudioSource>();
        instance = this;
        WoodSound = Resources.Load("Wood", typeof(AudioClip)) as AudioClip;
        GrassSound = Resources.Load("Grass", typeof(AudioClip)) as AudioClip;
        anim = GetComponent<Animator>();

    }
    AudioClip WoodSound;
    AudioClip GrassSound;
    void playWood(){
        //trigger enter
        AS.clip = WoodSound;
        AS.volume = 1f;
        AS.Play();

    }
    void playGrass(){ AS.clip = GrassSound;AS.Play();
        AS.volume = 0.35f;}
    void stop(){
     
        AS.Stop();

    }

    public bool isActive = true;
    // Update is called once per frame
    void Update()
    {
        if (!isActive) { return; }

        MoveControlByTranslate();
    }
	public float m_speed = 5f;
	//Translate移动控制函数
	bool MoveControlByTranslate()
	{
        bool clicked = false;

        float TranslateAmount = m_speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W)|| Input.GetKey((KeyCode.S))){
            if (Input.GetKey((KeyCode.A))|| Input.GetKey((KeyCode.D))){
                TranslateAmount /= 1.41f;

            }
        }

        string animationName = "";

		if (Input.GetKey(KeyCode.W)|Input.GetKey(KeyCode.UpArrow)) //前
		{
            animationName = "WalkBack";
            this.transform.Translate(Vector3.up*TranslateAmount); 
            clicked = true;
		}
		if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow)) //后
		{
            animationName = "WalkFront";
            this.transform.Translate(Vector3.up *- TranslateAmount); 
            clicked = true;
		}
		if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) //左
		{
            animationName = "WalkLeft";
            this.transform.Translate(Vector3.right *-TranslateAmount); 
            clicked = true;
		}
		if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) //右
		{
            animationName = "WalkRight";
            this.transform.Translate(Vector3.right * TranslateAmount);
            clicked = true;
        }

        setAnimation(animationName);

        if (clicked) return true;
        return false;
    }
}
