using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
private Rigidbody2D rd2d;

public float speed;

public Text score;
public Text lives;
public Text winText;
public Text loseText;

public AudioClip musicClipOne;
public AudioClip musicClipTwo;
public AudioSource musicSource;

Animator anim;

public GameObject winTextObject;
public GameObject loseTextObject;

private float movementX;
private float movementY;


private int scoreValue = 0;
private int livesValue = 3;

private bool facingRight = true;


// Start is called before the first frame update
void Start()
{
rd2d = GetComponent<Rigidbody2D>();
score.text = "Score: " + scoreValue.ToString();
lives.text = "Lives: " + livesValue.ToString();

winTextObject.SetActive(false);
loseTextObject.SetActive(false);

musicSource.clip = musicClipOne;
musicSource.Play();
musicSource.loop = true;

anim = GetComponent<Animator>();

}

void Update()
{
    if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
    {
        anim.SetInteger("State", 1);
    }

    else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
    {
        anim.SetInteger("State", 0);
    }
    
    

}
// Update is called once per frame
void FixedUpdate()
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

if (vertMovement > 0)
{
    anim.SetInteger("State", 2);
}

if (Input.GetKey("escape"))
        {

        Application.Quit();
        
        }
}

private void OnCollisionEnter2D(Collision2D collision)
{
if (collision.collider.tag == "Coin")
{
scoreValue += 1;
score.text = "Score: " + scoreValue.ToString();
Destroy(collision.collider.gameObject);

    if (scoreValue == 8)
    {
    winTextObject.SetActive(true);
    winText.text = "You Win!                       Game created by Rilie Scott";

    musicSource.Pause();
    musicSource.clip = musicClipTwo;
    musicSource.Play();
    musicSource.loop = false;


    }

    if (scoreValue == 4)
    {
        rd2d.transform.position = new Vector3(-117f, 0.4f, 0.0f);
        livesValue = 3;
        lives.text = "Lives: " + livesValue.ToString();

    }
}

if (collision.collider.tag == "Enemy" && scoreValue <= 7)
{
livesValue -= 1;
lives.text = "Lives: " + livesValue.ToString();
Destroy (collision.collider.gameObject);


    if (livesValue == 0)
    {
    loseTextObject.SetActive(true);
    loseText.text = "You Lose!";
    Destroy(gameObject);
    }
}
}
private void OnCollisionStay2D(Collision2D collision)
{
    if (collision.collider.tag == "Ground")
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rd2d.AddForce(new Vector2(0, 7), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors. You can also create a public variable for it and then edit it in the inspector.
            
        }
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

}
