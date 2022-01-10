using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement1 : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb;

    public Animator anim;

    public float jumpBoost = 20f;
    public float jumpForce = 20f;
    public Transform feet;
    public Transform arms;
    public Transform head;
    public LayerMask groundLayers;
    public GameObject Died;

    public static bool drowning = false;

    public float graceTime = 2f;
    float mx;
    private void Update()
    {
        mx = Input.GetAxis("Horizontal");

        //Jos pelaaja painaa "jump" nappulaa ja on maassa, hypätään
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
            AudioManager.instance.Play("jumpSFX");                             // Hyppyääni
        }


        if (Mathf.Abs(mx) > 0.05)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (mx > 0f)                                             //Jos pelaaja liikkuu oikealle
        {
            transform.localScale = new Vector3(1f, 1f, 1f);     //käännetään pelaajan sprite oikealle
        }
        else if (mx < 0f)                                        //Jos pelaaja liikkuu vasemmalle
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);    //Käännetään pelaajan sprite vasemmalle
        }

        //Tarkastetaan onko pelaaja jääseinän lähellä
        if (NearIcewall())
        {
            anim.SetBool("isIcewall", false);           //Jos pelaaja on jääseinän lähellä, mutta ei paina nappulaa hajottaakseen sitä, ei näytetä animaatiota

            if (Input.GetKeyDown("q"))
            {
                anim.SetBool("isIcewall", true);        //Jos pelaaja on jääseinän lähellä ja painaa "q" näppäintä, toistetaan animaatio
            }
        }


        if(!NearIcewall())
        {
            anim.SetBool("isIcewall", false);           //Jos pelaaja ei ole enää jääseinän lähellä, poistutaan animaatiosta      
        }

        if (inWater())
        {
            rb.velocity = new Vector2(0,0);             //Jos pelaaja on hukkumassa, estetään hänen liikkuminen.
            anim.SetBool("isDrowning", true);
            graceTime -= Time.deltaTime;                //Aika, mitä lasketaan alaspäin, kunnes 0
            drowning = true;                            //Käytetään WaterRising scriptissä

            if (graceTime <= 0)
            {
                Died.SetActive(true);                   //Näytetään kuolemaruutu  
            }
        }

        anim.SetBool("isGrounded", IsGrounded());
        anim.SetBool("isPushing", IsPushing());

       if (OnLadder())
        {
            anim.SetBool("OnLadder", true);

            if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("up") 
                || Input.GetKey("down") || Input.GetKey("left") || Input.GetKey("right"))
            {
                anim.speed = 1;                 //Jos pelaaja on tikkaissa ja painaa suunta nappulaa, animaatio pyörii, mutta vain silloin kun pelaaja painaa nappulaa
            }
            else
            {
                anim.speed = 0;                 //Jos pelaaja ei paina suuntanäppäintä, pysäytetään animaatio
            }
        }

       //Jos pelaaja ei ole tikapuissa, asetetaan animaationopeus takaisin normaaliksi.
        if (!OnLadder())
        {
            anim.SetBool("OnLadder", false);
            anim.speed = 1;
            
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(mx * movementSpeed, rb.velocity.y);

        rb.velocity = movement;
    }

    private void Start()
    {
        drowning = false;           //Kun peli käynnistyy asetetaan kerrotaan että pelaaja ei ole hukkumassa nyt
        Time.timeScale = 1f;        //Aika asetetaan kulkemaan normaalilla nopeudella
    }

    void Jump()
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        //Trampoliinin toiminta, pomppii jatkuvasti jos pelaaja on siinä päällä.
        if (collision.gameObject.CompareTag("Trampoline")) 
        {
            if (IsTrampJumping())
            {
                Vector2 movement = new Vector2(rb.velocity.x, jumpBoost);

                rb.velocity = movement;

            }
        }

        if (collision.gameObject.CompareTag("Snowball") || collision.gameObject.CompareTag("Trampoline"))
        {
            if (IsPushing())
            {
                AudioManager.instance.Play("pushSFX");                             // Hyppyääni
            }
            
        }

      

    }


    //Tarkastetaan onko pelaaja lähellä jääseinää
    public bool NearIcewall()
    {
        Collider2D iceCheck = Physics2D.OverlapCircle(arms.position, 0.5f, LayerMask.GetMask("Icewall"));

        if (iceCheck != null)
        {
            return true;
        }
        return false;
    }

    //Tarkasatetaan onko pelaajan jalat maassa
    public bool IsGrounded()
    {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        if (groundCheck != null)
        {
            return true;
        }

        return false;
    }

    //Tarkastetaan työntääkö pelaaja esineitä
    public bool IsPushing()
    {
        Collider2D ballCheck = Physics2D.OverlapCircle(arms.position, 0.5f, LayerMask.GetMask("Snowball"));
        Collider2D trampCheck = Physics2D.OverlapCircle(arms.position, 0.5f, LayerMask.GetMask("Trampoline"));

        if (ballCheck != null)
        { 
            return true; 
        }

        else if (trampCheck != null)
        {
            return true;
        }

        return false;

    }

    //Tarkastetaan onko pelaajan trampoliinin päällä.
    public bool IsTrampJumping()
    {
        Collider2D TrampJump = Physics2D.OverlapCircle(feet.position, 0.2f, LayerMask.GetMask("Trampoline"));
        
        if (TrampJump != null)
        {
            AudioManager.instance.Play("trampoSFX");                             // BOING -ääni
            return true;
        }
        return false;
    }


    //Tarkastetaan onko pelaaja tikapuilla
    public bool OnLadder()
    {
        Collider2D ladderCheck = Physics2D.OverlapCircle(head.position, 0.4f, LayerMask.GetMask("Ladder"));

        if(ladderCheck != null)
        {
            return true;
        }
        return false;
    }
   
    //Tarkastetaan onko pelaajan pää vedessä
    public bool inWater()
    {
        Collider2D waterCheck = Physics2D.OverlapCircle(head.position, 0.1f, LayerMask.GetMask("water"));

        if(waterCheck != null)
        {
            return true;
        }
        return false;
    }
}
