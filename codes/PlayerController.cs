using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

//https://youtu.be/sLqXFF8mlEU?si=e20nPpNwOiJYlpyV
public class PlayerController : MonoBehaviour
{

    private const string PLAYER_IDLE = "PLAYER_IDLE";
    private const string PLAYER_WALK = "PLAYER_WALK";
    private const string PLAYER_RUN = "PLAYER_RUN";
    private string currAnimationState;
    public float moveSpeed;
    public float runningSpeed;
    public Vector2 moveDirection;
    public Animator animator;
    public Rigidbody2D rb;
    public bool facingRight = true;
    public bool isRunning;

    public bool isMoving;

    void Start()
    {
    }

    //Behavior

    void Update()
    {
        ProccessInputs();
        if (isMoving)
        {
            if (isRunning)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_WALK);
            }
        }
        else
        {
            ChangeAnimationState(PLAYER_IDLE);
        }

        if (moveDirection.x != 0)
        {
            handleFacingDirection();
        }
    }

    //call Physics Calcs

    void FixedUpdate()
    {
        Move();
    }

    //Inputs Processing

    void ProccessInputs()
    {
        //move inputs
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);
        isMoving = moveDirection != new Vector2(0, 0);

        //shift running
        isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);
    }

    //Movement Calcs

    void Move()
    {
        float speed = isRunning ? runningSpeed : moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y).normalized * speed;
    }


    //Animation/Sprite Methods

    void handleFacingDirection()
    {
        transform.localScale = new Vector2(moveDirection.x, transform.localScale.y);
    }

    void ChangeAnimationState(string newAnimationState)
    {
        if (newAnimationState != currAnimationState)
        {
            animator.Play(newAnimationState);
            currAnimationState = newAnimationState;
        }
    }

    bool IsAnimationPlaying(string stateName)
    {
        AnimatorStateInfo currStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return currStateInfo.IsName(stateName) &&
         currStateInfo.normalizedTime < 1.0f;
    }

    //Utils

    bool oppositeSigns(int a, int b)
    {
        return (a > 0 && 0 <= b) || (b > 0 && a <= 0);
    }
}