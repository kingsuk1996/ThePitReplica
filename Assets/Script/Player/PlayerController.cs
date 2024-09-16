using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RedApple.ThePit
{
    public class PlayerController : MonoBehaviour
    {
        [Space(10)]
        [SerializeField] private Rigidbody playerRB;
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private Animator playerAnimation;
        [SerializeField] private SpriteRenderer playerShadow;
        [SerializeField] private UIManager uIManager;

        [Space(20)]
        [SerializeField] private float playerCrouchHeight = .63f;
        [SerializeField] private float playerCrouchY = .284f;
        [SerializeField] private float upForce = 7;
        [SerializeField] private float downForce = 1.5f;
        [SerializeField] private float maxMouseButtonDownTime = 0.1f;

        private float timer = 0f;
        private float playerStandHeight;
        private bool grounded = false;
        private bool doubleJumpUsed = false;
        private Vector3 centerOfCapsuleCollider;
        private Coroutine coroutine;

        public static Action OnRoadSpawn;

        internal GameObject Spike;
        internal GameObject Door;
        internal GameObject floorSpike;

        internal void Init()
        {
            playerStandHeight = playerCollider.height;
            centerOfCapsuleCollider = playerCollider.center;
        }
      
        private void Update()
        {
            if (GameManager.Instance.CurrentGameState == GameState.Start)
            {
                MouseInput();
                PlayerDownForce();
                //TouchInput();
            }
        }

        private void TouchInput()
        {
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    StartCoroutine(StartTimer());
                }
                if (touch.phase == TouchPhase.Stationary && timer>maxMouseButtonDownTime)
                {
                    PlayerSlide();
                    Debug.Log("Sliding");
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    playerAnimation.SetBool("Slide", false);
                    Debug.Log("Jump");
                    PlayerJump();
                    timer = 0;
                    StopAllCoroutines();
                }
            }
        }

        private void MouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                coroutine = StartCoroutine(StartTimer());
            }

            if (Input.GetMouseButtonUp(0))
            {
                playerAnimation.SetBool("Slide", false);
                PlayerJump();
                timer = 0;
                StopCoroutine(coroutine);
            }

            if (Input.GetMouseButton(0) && timer > maxMouseButtonDownTime)
            {
                PlayerSlide();
            }
        }

        IEnumerator StartTimer()
        {
            while (true)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void PlayerJump()
        {
            playerCollider.center = centerOfCapsuleCollider;
            playerCollider.height = playerStandHeight;

            if (grounded || !doubleJumpUsed)
            {
                playerRB.velocity = Vector3.up * upForce;
            }

            if (!grounded && !doubleJumpUsed)
            {
                playerAnimation.SetTrigger("VaultJump");
                doubleJumpUsed = true;
            }

            if (grounded)
            {
                playerAnimation.SetBool("Jump", true);
            }
        }

        private void PlayerSlide()
        {
            playerAnimation.SetBool("Slide", true);
            playerCollider.center = new Vector3(centerOfCapsuleCollider.x, playerCrouchY, centerOfCapsuleCollider.z);
            playerCollider.height = playerCrouchHeight;
        }

        private void PlayerDownForce()
        {
            if (!grounded)
            {
                playerRB.AddForce(Vector3.down * downForce * playerRB.mass, ForceMode.Force);
                playerShadow.color = new Color(0, 0, 0, .8f);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    grounded = true;
                    playerAnimation.SetBool("Jump", false);
                    playerShadow.color = new Color(0, 0, 0, 1f);
                    break;
                case "Bridge":
                    grounded = true;
                    break;
                case "Obstacle":
                    GameManager.Instance.ChangeGameState(GameState.Finish);
                    break;
            }
        }

        private void OnCollisionStay(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    grounded = true;
                    break;
                case "Bridge":
                    grounded = true;
                    break;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Ground":
                    grounded = false;
                    doubleJumpUsed = false;
                    break;

                case "Bridge":
                    grounded = false;
                    doubleJumpUsed = false;
                    collision.gameObject.transform.DOMoveY(-10, 1);
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "SpawnTrig")
            {
                OnRoadSpawn?.Invoke();
            }
        }

        //Used for instruction page in future
        private void Instruction()
        {
            if (Spike != null)
            {
                if (Vector3.Distance(transform.position, Spike.transform.position) <= 5)
                {
                    Debug.Log("Jump");
                    uIManager.GameplayInstructionState(InstructionState.jump);
                }
            }
            if (Door != null)
            {
                if (Vector3.Distance(transform.position, Door.transform.position) <= 5)
                {
                    Debug.Log("Slide");
                    uIManager.GameplayInstructionState(InstructionState.slide);
                }
            }
            if (floorSpike != null)
            {
                if (Vector3.Distance(transform.position, floorSpike.transform.position) <= 5)
                {
                    Debug.Log("DoubleJump");
                    uIManager.GameplayInstructionState(InstructionState.doublejump);
                }
            }
        }
    }
}
