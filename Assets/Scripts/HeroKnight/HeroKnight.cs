using System.Collections;
using Common;
using Dialogues;
using Inventory;
using Other;
using Statistics;
using UnityEngine;

namespace HeroKnight
{
    public class HeroKnight : Singleton<HeroKnight>
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

        public Character characterData = new Character("Player");

        [SerializeField]
        private float speed = 4.0f;

        [SerializeField]
        private float jumpForce = 7.5f;

        [SerializeField]
        private float rollForce = 6.0f;

        private float health;

        [SerializeField]
        private GameObject attackHitBox;

        private Animator          animator;
        private Rigidbody2D       body2d;
        private Sensor_HeroKnight groundSensor;
        private bool              grounded;
        private bool              rolling;
        private bool              airJump;
        private int               facingDirection = 1;
        private int               currentAttack;
        private float             timeSinceAttack;
        private float             delayToIdle;
        private int               coins;
        private bool              alive = true;
        private bool              frozen;
        private bool              isAttacking;

        // Use this for initialization
        private void Start()
        {
            airJump = false;
            health  = characterData.health.baseValue;
            characterData.health.ValueChanged += (sender, args) =>
                                                 {
                                                     health = Mathf.Min(health, args.newValue);
                                                     HealthUiManager.Instance.UpdateHeartsUi(health, characterData.health.Value);
                                                 };
            HealthUiManager.Instance.UpdateHeartsUi(health, characterData.health.Value);
            speed                            =  characterData.speed.baseValue;
            characterData.speed.ValueChanged += (sender, args) => speed = args.newValue;
            animator                         =  GetComponent<Animator>();
            body2d                           =  GetComponent<Rigidbody2D>();
            groundSensor                     =  transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
            attackHitBox                     =  GameObject.Find("AttackHitbox");
            attackHitBox.SetActive(false);
            SetupEquipment();
        }

        private void SetupEquipment()
        {
            foreach (var slot in InventoryManager.Instance.equipmentSlots)
            {
                var eqName = slot.name.Replace("Slot", string.Empty).ToLower();
                typeof(Inventory.Inventory).GetField(eqName).SetValue(characterData.inventory, slot);
            }
        }

        public int GetDirection()
        {
            return facingDirection;
        }

        public int GetStrength()
        {
            return (int) characterData.strength.Value;
        }

        // Update is called once per frame
        private void Update()
        {
            if (alive && !frozen && !rolling)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    PrintState();
                }

                // Increase timer that controls attack combo
                timeSinceAttack += Time.deltaTime;

                // Check if character just landed on the ground
                if (!grounded && groundSensor.State())
                {
                    grounded = true;
                    airJump  = false;
                    animator.SetBool(GroundedHash, grounded);
                }

                // Check if character just started falling
                if (grounded && !groundSensor.State())
                {
                    grounded = false;
                    animator.SetBool(GroundedHash, grounded);
                }

                // Get user input left and right
                var inputX = Input.GetAxis("Horizontal");

                // Swap direction of sprite depending on walk direction
                if (inputX > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    attackHitBox.transform.localScale    = new Vector3(1, 1, 1);
                    facingDirection                      = 1;
                }

                else if (inputX < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    attackHitBox.transform.localScale    = new Vector3(-1, 1, 1);
                    facingDirection                      = -1;
                }

                // Move
                if (!rolling)
                {
                    body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);
                }

                //Set AirSpeed in animator
                animator.SetFloat(AirSpeedYHash, body2d.velocity.y);
                if (characterData.inventory.accessory.Occupied)
                {
                    if (characterData.inventory.accessory.Item.itemName == "Feather")
                    {
                        body2d.gravityScale = 0.7f;
                    }

                    if (Input.GetKeyDown(KeyCode.E) && (characterData.inventory.accessory.Item.itemName == "Shield"))
                    {
                        Block();
                    }
                    else if (Input.GetKeyUp(KeyCode.E))
                    {
                        IdleBlock();
                    }
                    else if (Input.GetKeyDown(KeyCode.LeftShift) && (characterData.inventory.accessory.Item.itemName == "Beer") && !rolling)
                    {
                        Roll();
                    }
                }
                else
                {
                    body2d.gravityScale = 1f;
                }

                if (Input.GetKeyDown(KeyCode.Space) && (airJump == false) && characterData.inventory.boots.Occupied && (characterData.inventory.boots.Item.itemName == "BouncyBoots"))
                {
                    Jump(jumpForce * 0.95f);
                }
                else if (Input.GetKeyDown(KeyCode.Space) && grounded)
                {
                    Jump(jumpForce);
                }
                else if (Input.GetKeyDown(KeyCode.Q) && (timeSinceAttack > 0.5f / characterData.strength.Value) && characterData.inventory.weapon.Occupied)
                {
                    Attack();
                }
                else if (Input.GetKeyUp(KeyCode.E))
                {
                    IdleBlock();
                }
                else if (Mathf.Abs(inputX) > Mathf.Epsilon)
                {
                    Run();
                }
                else if (Input.GetKeyDown(KeyCode.T))
                {
                    Interact();
                }
                else
                {
                    Idle();
                }
            }
        }

        private void PrintState()
        {
            print($"Health: {health}/{characterData.health.Value}");
            print($"Strength (damage dealt): {characterData.strength.Value}");
            print($"Speed: {characterData.speed.Value}");
            print($"Exp: {StatisticsManager.Instance.GetExperience()}");

        }

        public void Freeze(bool freeze)
        {
            frozen = freeze;
        }

        private void Interact()
        {
            var hit = Physics2D.Raycast(body2d.position + Vector2.up * 0.2f,
                                        facingDirection == 1 ? Vector2.right : Vector2.left,
                                        1,
                                        LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                var go         = hit.collider.gameObject;
                var talkingNpc = go.GetComponent<TalkingNPC>();
                if (talkingNpc != null)
                {
                    talkingNpc.Talk();
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

        private void Jump(float jumpForce)
        {
            animator.SetTrigger(JumpHash);
            if (grounded == false)
            {
                airJump = true;
            }

            grounded = false;
            animator.SetBool(GroundedHash, grounded);
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            groundSensor.Disable(0.4f);
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
                Freeze(true);
                Invoke(nameof(CleanSceneReload), 1);
            }
            else
            {
                animator.SetTrigger(HurtHash);
                rolling = false;
            }

            HealthUiManager.Instance.UpdateHeartsUi(health, characterData.health.Value);
        }

        private void CleanSceneReload()
        {
            SceneManager.Instance.ReloadScene();
            InventoryManager.Instance.ClearAllInventory();
            Destroy(gameObject);
        }

        // Animation Events
        // Called in end of roll animation.
        private void AE_ResetRoll()
        {
            rolling = false;
        }
    }
}