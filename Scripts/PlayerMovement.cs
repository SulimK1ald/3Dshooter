using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public Transform obstacleCheck;
    public LayerMask groundMask;
    public float speed = 7f;
    public float shiftSpeed = 14f;
    public float cSpeed = 3f;
    public bool slowMove = false;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public float groundDistance = 0.4f;
    public float obstacleDistance = 0.4f;
    [SerializeField] private Vector3 velosity;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool cantGetUp = false;

    void Update()
    {
        //Гравитация.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        cantGetUp = Physics.CheckSphere(obstacleCheck.position, obstacleDistance, groundMask);
        velosity.y += gravity * Time.deltaTime;
        controller.Move(velosity * Time.deltaTime);

        //Движение и прыжок.
        slowMoveC();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounded)
        {
            velosity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded && velosity.y < 0)
        {
            velosity.y = -2f;
        }
        
        if (Input.GetKey("c"))
        {
            slowMove = true;
            controller.height = 1f;
        }
        else if (!cantGetUp)
        {
            controller.height = 2f;
            slowMove = false;
            speed = 7f;
        }

        if (Input.GetKey("left shift") && slowMove == false)
        {
            speed = shiftSpeed;
        }
        else if (!cantGetUp && slowMove == false)
        {
            speed = 7f;
        }
    }
    public void slowMoveC()
    {
        if (slowMove)
        {
            speed = cSpeed;
        }
    }
}
