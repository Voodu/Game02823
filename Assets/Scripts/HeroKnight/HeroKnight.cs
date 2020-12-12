using System.Collections;
using System.Collections.Generic;
using Statistics;
using UnityEngine;

namespace HeroKnight
{
    public class HeroKnight : MonoBehaviour
    {
        private static readonly int HurtHash      = Animator.StringToHash("Hurt");
        private static readonly int DeathHash     = Animator.StringToHash("Death");
        private static readonly int IdleBlockHash = Animator.StringToHash("IdleBlock");
        private static readonly int BlockHash     = Animator.StringToHash("Block");
        private static readonly int RollHash      = Animator.StringToHash("Roll");
        private static readonly int GroundedHash  = Animator.StringToHash("Grounded");
        private static readonly int JumpHash      = Animator.StringToHash("Jump");
        private static readonly int AnimStateHash = Animator.StringToHash("AnimState");
        private static readonly int AirSpeedYHash = Animator.StringToHash("AirSpeedY");

        private readonly List<GameObject> hearts = new List<GameObject>();

        public Character characterData = new Character("Player");

        private float speed = 4.0f;

        [SerializeField]
        private float jumpForce = 7.5f;

        [SerializeField]
        private float rollForce = 6.0f;

        [SerializeField]
        private int maxHearts = 5;

        private float health;

        [SerializeField]
        private GameObject attackHitBox;

        private Animator          animator;
        private Rigidbody2D       body2d;
        private Sensor_HeroKnight groundSensor;
        private bool              grounded;
        private bool              rolling;
        private int               facingDirection = 1;
        private int               currentAttack;
        private float             timeSinceAttack;
        private float             delayToIdle;
        private int               coins;
        private bool              alive = true;
        private bool              isAttacking;
        public  GameObject        heartPrefab;
        public  GameObject        healthBar;

        // Use this for initialization
        private void Start()
        {
            health = characterData.health.baseValue;
            characterData.health.ValueChanged += (sender, args) =>
                                                 {
                                                     health = Mathf.Min(health, args.newValue);
                                                     UpdateHeartsUi();
                                                 };
            UpdateHeartsUi();
            speed                            =  characterData.speed.baseValue;
            characterData.speed.ValueChanged += (sender, args) => speed = args.newValue;
            animator                         =  GetComponent<Animator>();
            body2d                           =  GetComponent<Rigidbody2D>();
            groundSensor                     =  transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
            attackHitBox                     =  GameObject.Find("AttackHitbox");
            attackHitBox.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (alive)
            {
                // Increase timer that controls attack combo
                timeSinceAttack += Time.deltaTime;

                //Check if character just landed on the ground
                if (!grounded && groundSensor.State())
                {
                    grounded = true;
                    animator.SetBool(GroundedHash, grounded);
                }

                //Check if character just started falling
                if (grounded && !groundSensor.State())
                {
                    grounded = false;
                    animator.SetBool(GroundedHash, grounded);
                }

                // -- Handle input and movement --
                var inputX = Input.GetAxis("Horizontal");

                // Swap direction of sprite depending on walk direction
                if (inputX > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    facingDirection                      = 1;
                }

                else if (inputX < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    facingDirection                      = -1;
                }

                // Move
                if (!rolling)
                {
                    body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);
                }

                //Set AirSpeed in animator
                animator.SetFloat(AirSpeedYHash, body2d.velocity.y);

                if (Input.GetKeyDown(KeyCode.Q) && (timeSinceAttack > 0.25f))
                {
                    Attack();
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    Block();
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    IdleBlock();
                }
                else if (Input.GetKeyDown(KeyCode.LeftShift) && !rolling)
                {
                    Roll();
                }
                else if (Input.GetKeyDown(KeyCode.Space) && grounded)
                {
                    Jump();
                }
                else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                {
                    Run();
                }
                else
                {
                    Idle();
                }
            }
        }

        private void Idle()
        {
            // Prevents flickering transitions to idle
            delayToIdle -= Time.deltaTime;
            if (delayToIdle < 0)
            {
                animator.SetInteger(AnimStateHash, 0);
            }
        }

        private void Run()
        {
            // Reset timer
            delayToIdle = 0.05f;
            animator.SetInteger(AnimStateHash, 1);
        }

        private void Jump()
        {
            animator.SetTrigger(JumpHash);
            grounded = false;
            animator.SetBool(GroundedHash, grounded);
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            groundSensor.Disable(0.2f);
        }

        private void Roll()
        {
            rolling = true;
            animator.SetTrigger(RollHash);
            body2d.velocity = new Vector2(facingDirection * rollForce, body2d.velocity.y);
        }

        private void IdleBlock()
        {
            animator.SetBool(IdleBlockHash, false);
        }

        private void Block()
        {
            animator.SetTrigger(BlockHash);
            animator.SetBool(IdleBlockHash, true);
        }

        private void Attack()
        {
            isAttacking = true;
            StartCoroutine(DoAttack());
            currentAttack++;

            // Loop back to one after third attack
            if (currentAttack > 3)
            {
                currentAttack = 1;
            }

            // Reset Attack combo if time since last attack is too large
            if (timeSinceAttack > 1.0f)
            {
                currentAttack = 1;
            }

            // Call one of three attack animations "Attack1", "Attack2", "Attack3"
            animator.SetTrigger("Attack" + currentAttack);

            // Reset timer
            timeSinceAttack = 0.0f;
        }

        private IEnumerator DoAttack()
        {
            attackHitBox.SetActive(true);
            yield return new WaitForSeconds(.3f);

            isAttacking = false;
            attackHitBox.SetActive(false);
        }

        public bool IsAttacking()
        {
            return isAttacking;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Will we have something like "End" in RPG?
            if (collision.CompareTag("End"))
            {
                Destroy(collision.gameObject);
                body2d.velocity = new Vector2(0, 0);
                StatisticsManager.Instance.AddExperience(5);
                alive = false;
                animator.SetTrigger(BlockHash);
                animator.SetBool(IdleBlockHash, true);
            }
        }

        public void DealDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                animator.SetTrigger(DeathHash);
                alive           = false;
                body2d.velocity = new Vector2(0, 0);
            }
            else
            {
                animator.SetTrigger(HurtHash);
            }

            UpdateHeartsUi();
        }

        private void UpdateHeartsUi()
        {
            var heartsLeft = Mathf.Ceil(health / characterData.health.Value * maxHearts);

            while (hearts.Count > heartsLeft)
            {
                Destroy(hearts[hearts.Count - 1]);
                hearts.RemoveAt(hearts.Count - 1);
            }

            while (hearts.Count < heartsLeft)
            {
                hearts.Add(Instantiate(heartPrefab, healthBar.transform));
            }
        }

        // Animation Events
        // Called in end of roll animation.
        private void AE_ResetRoll()
        {
            rolling = false;
        }
    }
}