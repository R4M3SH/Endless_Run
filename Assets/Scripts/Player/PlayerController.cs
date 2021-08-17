﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float Gravity = -20;

    public Animator[] animator;
    private bool isSliding = false;
    private int Index=0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
       //Index = CharacterSelection.index;
        
    }

    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        //Increase Speed 
        if(forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;


        animator[Index].SetBool("isGameStarted", true);
        //animator[1].SetBool("isGameStarted", true);
        direction.z = forwardSpeed;


        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator[Index].SetBool("isGrounded", isGrounded);
        //animator[1].SetBool("isGrounded", isGrounded);
        if (isGrounded)
        {
            if (SwipeManager.swipeUp)
                Jump();

            if (SwipeManager.swipeDown && !isSliding)
                StartCoroutine(Slide());
        }
        else 
        {
            direction.y += Gravity * Time.deltaTime;
            if (SwipeManager.swipeDown && !isSliding)
            {
                StartCoroutine(Slide());
                direction.y = -8;
            }
                

        }

        //Gather the inputs on which lane we should be
        if (SwipeManager.swipeRight)
        {
            animator[Index].SetTrigger("right");
            //animator[1].SetTrigger("right");
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeManager.swipeLeft)
        {
            animator[Index].SetTrigger("left");
            //animator[1].SetTrigger("left");

            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;


        //transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        //Move Player
        controller.Move(direction * Time.deltaTime);


    }

    private void Jump()
    {
        Debug.Log(jumpForce.ToString());
        //Debug.Log("jumped");
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }

    private IEnumerator Slide()
    {
        //Debug.Log("Slide");

        isSliding = true;
        animator[Index].SetBool("isSliding", true);
        //animator[1].SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;

        yield return new WaitForSeconds(1.3f);

        controller.center = Vector3.zero;
        controller.height = 2;
        animator[Index].SetBool("isSliding", false);
        //animator[1].SetBool("isSliding", false);
        isSliding = false;
    }
}
