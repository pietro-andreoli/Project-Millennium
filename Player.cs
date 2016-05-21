using UnityEngine;
using System.Collections;

[RequireComponent(typeof (Controller2D))]
public class Player : MonoBehaviour {

    float moveSpeed = 6;

    public float jumpHeight = 4;
    public  float timeToJumpApex = 0.4f;

    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    float gravity;
    float jumpVelocity;
    float velocityXSmooting;

    Vector3 velocity;

    Controller2D controller;

    void Start() {
        controller =  GetComponent<Controller2D>();
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + " Jump Velocity: " + jumpVelocity); 
    }

    void Update()
    {

        if(controller.collisions.above || controller.collisions.below) //if on ground or touching ceiling 
        {
            velocity.y = 0;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below) //if on ground and jump key is pressed
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmooting, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
