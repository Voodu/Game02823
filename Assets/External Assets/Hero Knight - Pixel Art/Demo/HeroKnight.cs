using UnityEngine;
using System.Collections;

public class HeroKnight : MonoBehaviour {

    [SerializeField] float      m_speed = 4.0f;
    [SerializeField] float      m_jumpForce = 7.5f;
    [SerializeField] float      m_rollForce = 6.0f;
    [SerializeField] bool       m_noBlood = false;
    [SerializeField] GameObject heart1;
    [SerializeField] GameObject heart2;
    [SerializeField] GameObject attackHitBox;

    private Animator            m_animator;
    private Rigidbody2D         m_body2d;
    private Sensor_HeroKnight   m_groundSensor;
    private Collider2D          m_enemy;
    private bool                m_grounded = false;
    private bool                m_rolling = false;
    private int                 m_facingDirection = 1;
    private int                 m_currentAttack = 0;
    private float               m_timeSinceAttack = 0.0f;
    private float               m_delayToIdle = 0.0f;

    private string items = "";
    private int coins = 0;
    private int lives = 2;
    private int xp = 0;
    private bool alive = true;
    private bool isAttacking = false;

    // Use this for initialization
    void Start ()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        attackHitBox = GameObject.Find("AttackHitbox");
        attackHitBox.SetActive(false);
        heart1 = GameObject.Find("Heart1");
        heart2 = GameObject.Find("Heart2");
    }



    // Update is called once per frame
    void Update ()
    {
        if (alive == true)
        {
            // Increase timer that controls attack combo
            m_timeSinceAttack += Time.deltaTime;

            //Check if character just landed on the ground
            if (!m_grounded && m_groundSensor.State())
            {
                m_grounded = true;
                m_animator.SetBool("Grounded", m_grounded);
            }

            //Check if character just started falling
            if (m_grounded && !m_groundSensor.State())
            {
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
            }

            // -- Handle input and movement --
            float inputX = Input.GetAxis("Horizontal");

            // Swap direction of sprite depending on walk direction
            if (inputX > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                m_facingDirection = 1;
            }
                
            else if (inputX < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                m_facingDirection = -1;
            }

            // Move
            if (!m_rolling )
                m_body2d.velocity = new Vector2(inputX * m_speed, m_body2d.velocity.y);

            //Set AirSpeed in animator
            m_animator.SetFloat("AirSpeedY", m_body2d.velocity.y);


            //Death
            // if (Input.GetKeyDown("e"))
            // {
            //     m_animator.SetBool("noBlood", m_noBlood);
            //     m_animator.SetTrigger("Death");
            // }
                
            //Hurt
            // else if (Input.GetKeyDown("q"))
            //     m_animator.SetTrigger("Hurt");

            //Attack
            if (Input.GetKeyDown("q") && m_timeSinceAttack > 0.25f)
            {
                isAttacking = true;
                StartCoroutine(DoAttack());
                m_currentAttack++;

                // Loop back to one after third attack
                if (m_currentAttack > 3)
                    m_currentAttack = 1;

                // Reset Attack combo if time since last attack is too large
                if (m_timeSinceAttack > 1.0f)
                    m_currentAttack = 1;

                // Call one of three attack animations "Attack1", "Attack2", "Attack3"
                m_animator.SetTrigger("Attack" + m_currentAttack);

                // Reset timer
                m_timeSinceAttack = 0.0f;
            }

            // Block
            else if (Input.GetKeyDown("e"))
            {
                m_animator.SetTrigger("Block");
                m_animator.SetBool("IdleBlock", true);
            }

            else if (Input.GetKeyUp("e"))
                m_animator.SetBool("IdleBlock", false);

            // Roll
            else if (Input.GetKeyDown("left shift") && !m_rolling)
            {
                m_rolling = true;
                m_animator.SetTrigger("Roll");
                m_body2d.velocity = new Vector2(m_facingDirection * m_rollForce, m_body2d.velocity.y);
            }
                

            //Jump
            else if (Input.GetKeyDown("space") && m_grounded)
            {
                m_animator.SetTrigger("Jump");
                m_grounded = false;
                m_animator.SetBool("Grounded", m_grounded);
                m_body2d.velocity = new Vector2(m_body2d.velocity.x, m_jumpForce);
                m_groundSensor.Disable(0.2f);
            }

            //Run
            else if (Mathf.Abs(inputX) > Mathf.Epsilon)
            {
                // Reset timer
                m_delayToIdle = 0.05f;
                m_animator.SetInteger("AnimState", 1);
            }

            //Idle
            else
            {
                // Prevents flickering transitions to idle
                m_delayToIdle -= Time.deltaTime;
                    if(m_delayToIdle < 0)
                        m_animator.SetInteger("AnimState", 0);
            }
        }
    }

    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.3f);
        isAttacking = false;
        attackHitBox.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            Destroy(collision.gameObject);
            items += "helm,";
        }

        if (collision.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coins++;
        }

        if (collision.tag == "Enemy" & isAttacking == true)
        {
            Destroy(collision.gameObject);
            xp++;
        }
        if (collision.tag == "End")
        {
            Destroy(collision.gameObject);
            m_body2d.velocity = new Vector2(0, 0);
            xp+=5;
            alive = false;
            m_animator.SetTrigger("Block");
            m_animator.SetBool("IdleBlock", true);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy"){
            lives--;
            
            if (lives == 0){
                m_animator.SetBool("noBlood", m_noBlood);
                m_animator.SetTrigger("Death");
                alive = false;
                Destroy(heart1.gameObject);
                m_body2d.velocity = new Vector2(0, 0);
            }
            else
            {
                m_animator.SetTrigger("Hurt");
                Destroy(heart2.gameObject);
            }

        }
    }


    // Animation Events
    // Called in end of roll animation.
    void AE_ResetRoll()
    {
        m_rolling = false;
    }
}