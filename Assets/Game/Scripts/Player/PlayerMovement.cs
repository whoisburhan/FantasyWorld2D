using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS.FanstayWorld2D.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Collider2D collider;
        [SerializeField] private Rigidbody2D rb2d;
        [SerializeField] private float speedOffset = 2f;
        [SerializeField] private float sprintOffset = 5f;

        [SerializeField] private float climbSpeedOffset = 1.2f;
        [SerializeField] private float jumpForce = 50f;

        [SerializeField] private LayerMask groundMask;

        private float finalSpeedOffset;
        bool isGrounded, landTouch = true;

        private bool isDirectionRight = true;

        // Jump
        private float jumpPressedRemember, jumpPressedRememberTime = 0.2f, groundRemember, groundRememberTime = 0.15f;
        private void Update()
        {
            SwitchCharacter();  // Switch Character

            isGrounded = IsGrounded();

            jumpPressedRemember -= Time.deltaTime;
            groundRemember -= Time.deltaTime;

            PlayerDirection();


            if (!PlayerConstant.Instance.CanClimb)
            {
                PlayerAnimation.Instance.CanClimb(false);
                // Horizontal Movement
                if (Mathf.Abs(PlayerController.Instance.MoveX) >= 1f)
                {
                    PlayerAnimation.Instance.Run();
                    if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                    {
                        PlayerConstant.Instance.MermaidColliderRun.SetActive(true);
                        PlayerConstant.Instance.MermaidColliderIdle.SetActive(false);
                    }
                }
                else
                {
                    PlayerAnimation.Instance.StopRun();
                    if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                    {
                        PlayerConstant.Instance.MermaidColliderIdle.SetActive(true);
                        PlayerConstant.Instance.MermaidColliderRun.SetActive(false);
                    }
                }

                if (PlayerController.Instance.Sprint)
                {
                    finalSpeedOffset = sprintOffset;
                    PlayerAnimation.Instance.Sprint();
                    if (Mathf.Abs(PlayerController.Instance.MoveX) >= 1f && PlayerConstant.Instance.MoveParticles.isStopped && isGrounded)
                    {
                        PlayerConstant.Instance.MoveParticles.Play();
                    }
                    if (PlayerConstant.Instance.MoveParticles.isPlaying && !isGrounded)
                    {
                        PlayerConstant.Instance.MoveParticles.Stop();
                    }
                }
                else
                {
                    finalSpeedOffset = speedOffset;
                    PlayerAnimation.Instance.StopSprint();
                    if (PlayerConstant.Instance.MoveParticles.isPlaying)
                    {
                        PlayerConstant.Instance.MoveParticles.Stop();
                    }
                }

            }

            else
            {
                if (PlayerController.Instance.MoveY >= .1f)
                {
                    PlayerAnimation.Instance.CanClimb(true);
                    PlayerAnimation.Instance.CanClimbUp(true);
                    PlayerAnimation.Instance.CanClimbDown(false);
                }
                else if (PlayerController.Instance.MoveY <= -.1f)
                {
                    PlayerAnimation.Instance.CanClimb(true);
                    PlayerAnimation.Instance.CanClimbUp(false);
                    PlayerAnimation.Instance.CanClimbDown(true);
                }
                else
                {
                    PlayerAnimation.Instance.CanClimb(true);
                    PlayerAnimation.Instance.CanClimbUp(false);
                    PlayerAnimation.Instance.CanClimbDown(false);
                }
            }

            //Jump
            if (isGrounded)
            {
                groundRemember = groundRememberTime;
            }

            if (PlayerController.Instance.Jump)
            {
                if (!PlayerConstant.Instance.CanClimb)
                    jumpPressedRemember = jumpPressedRememberTime;
                else
                    PlayerConstant.Instance.CanClimb = false;
            }

            if ((jumpPressedRemember > 0) && (groundRemember > 0))
            {
                jumpPressedRemember = 0f;
                groundRemember = 0f;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                PlayerAnimation.Instance.Jump();
                PlayerConstant.Instance.JumpParticles.Play();
            }

            if (PlayerController.Instance.ReleaseJump && rb2d.velocity.y > 0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, rb2d.velocity.y * 0.5f);
            }

            // Landing
            if (!isGrounded /*&& rb2d.velocity.y < 0f*/)
            {
                PlayerAnimation.Instance.NotGrounded();
                landTouch = false;
            }
            if (isGrounded && !landTouch)
            {
                PlayerAnimation.Instance.Grounded();
                PlayerAnimation.Instance.StopRun();
                landTouch = true;
                if (!PlayerConstant.Instance.CanClimb)
                    PlayerConstant.Instance.LandParticles.Play();
            }

        }


        private void FixedUpdate()
        {
            if (!PlayerConstant.Instance.CanClimb)
            {
                rb2d.velocity = new Vector2(PlayerController.Instance.MoveX * finalSpeedOffset, rb2d.velocity.y);
                if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                {
                    rb2d.velocity = new Vector2(rb2d.velocity.x, PlayerController.Instance.MoveY * climbSpeedOffset);
                }
            }
            else
                rb2d.velocity = new Vector2(rb2d.velocity.x, PlayerController.Instance.MoveY * climbSpeedOffset);
        }

        private void SwitchCharacter()
        {
            if (PlayerConstant.Instance.CanSwime && PlayerController.Instance.Switch_Character)
            {
                if (PlayerConstant.Instance.GamePlayerType == PlayerType.Human)
                {
                    PlayerConstant.Instance.GamePlayerType = PlayerType.Mermaid;
                    rb2d.gravityScale = 0f;
                    // rb2d.isKinematic = true;
                    rb2d.velocity = Vector2.zero;

                }
                else if (PlayerConstant.Instance.GamePlayerType == PlayerType.Mermaid)
                {
                    PlayerConstant.Instance.GamePlayerType = PlayerType.Human;
                    rb2d.gravityScale = 10f;
                    // rb2d.isKinematic = false;
                    rb2d.velocity = Vector2.zero;
                }
            }
        }

        private void PlayerDirection()
        {
            // if (PlayerController.Instance.MoveX > 0f && transform.localScale.x < 0f || PlayerController.Instance.MoveX < 0f && transform.localScale.x > 0f)
            // {
            //     Vector3 dir = transform.localScale;
            //     transform.localScale = new Vector3(dir.x * -1f, dir.y, dir.z);
            // }

            if (PlayerController.Instance.MoveX > 0f && !isDirectionRight || PlayerController.Instance.MoveX < 0f && isDirectionRight)
            {
                isDirectionRight = !isDirectionRight;
                transform.Rotate(0f,180f,0f);
            }
        }

        private bool IsGrounded()
        {
            float extraHeight = .5f;
            Vector2 origin = new Vector2(collider.bounds.center.x, collider.bounds.center.y);
            RaycastHit2D raycastHit = Physics2D.Raycast(origin, Vector2.down, collider.bounds.extents.y + extraHeight, groundMask);

            Vector2 origin1 = new Vector2(collider.bounds.center.x + collider.bounds.extents.x, collider.bounds.center.y);
            RaycastHit2D raycastHit1 = Physics2D.Raycast(origin1, new Vector2(0.5f, -1f), collider.bounds.extents.y + extraHeight, groundMask);

            Vector2 origin2 = new Vector2(collider.bounds.center.x - collider.bounds.extents.x, collider.bounds.center.y);
            RaycastHit2D raycastHit2 = Physics2D.Raycast(origin2, new Vector2(-0.5f, -1f), collider.bounds.extents.y + extraHeight, groundMask);


            Color rayColor = raycastHit.collider != null ? Color.blue : Color.red;

            Debug.DrawRay(origin, Vector2.down * (collider.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(origin1, new Vector2(0.5f, -1f) * (collider.bounds.extents.y + extraHeight), rayColor);
            Debug.DrawRay(origin2, new Vector2(-0.5f, -1f) * (collider.bounds.extents.y + extraHeight), rayColor);


            return raycastHit.collider != null || raycastHit1.collider != null || raycastHit2.collider != null;

        }

        private void OnDrawGizmos()
        {
            // Bounds
            // Gizmos.color = Color.yellow;
            // Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(0, -0.01f), transform.localScale);

        }


    }
}