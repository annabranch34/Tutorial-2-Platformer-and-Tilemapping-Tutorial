﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public Text win;

    public Text lives;

    public Text lose;
    private int scoreValue = 0;

    private int livesValue = 3;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    Animator anim;

    private bool facingRight = true;







    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        win.text = "";
        lives.text = "Lives: " + livesValue.ToString();
        lose.text = "";
        musicSource.Play();
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))

        {

          anim.SetInteger("State", 0);

         }


         if (Input.GetKeyUp(KeyCode.A))

        {

          anim.SetInteger("State", 0);

         }


        if(Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }


         if (Input.GetKeyUp(KeyCode.W))

        {

          anim.SetInteger("State", 0);

         }     

    }


    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    
    void FixedUpdate()
    {
        {
            float hozMovement = Input.GetAxis("Horizontal");
            float vertMovement = Input.GetAxis("Vertical");
        
            rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));


             if (facingRight == false && hozMovement > 0)
            {
                Flip();
            }
        else if (facingRight == true && hozMovement < 0)
            {
                Flip();
            }
        }


                if (Input.GetKey("escape"))
            {
                Application.Quit();
            }



    }

private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }









    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }


        if (scoreValue == 4)

        {
            transform.position = new Vector2(73.49f, 296.63f);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
        }



         if (scoreValue == 8)
        {   
            win.text = "You Win! Game Created By Anna Branch";
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = false;
            Destroy(this);
        }


         if(collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives:" + livesValue.ToString();
            Destroy(collision.collider.gameObject);
          
        }


        if (livesValue == 0)
        {
            lose.text = "You Lose!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            anim.SetInteger("State", 3);
            Destroy(this);
            
        
        }


    }

}

    




