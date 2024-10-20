﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;

public class PlayerMotor : MonoBehaviour
{
       private CharacterController controller;
       private Vector3 moveVector;

       private float speed = 5.0f;
       private float verticalVelocity = 0.0f;
       private float gravity = 12.0f;

       private float animationDuration = 3.0f;
       private float startTime = 0.0f;

       private bool isDead = false;

       // Use this for initialization
       void Start ()
       {
           controller = GetComponent<CharacterController>();
           startTime = Time.time;
    }

    // Update is called once per frame
    void Update ()
       {
           if (isDead)
           {
               return;
           }

           if (Time.time - startTime < animationDuration)
           {
               controller.Move(Vector3.forward * speed * Time.deltaTime);
               return;
           }

           moveVector = Vector3.zero;

           if (controller.isGrounded)
           {
               verticalVelocity = -0.5f;
           }
           else
           {
               verticalVelocity -= gravity * Time.deltaTime;
           }

           // X - Left and Right
           moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
           if (Input.GetMouseButton(0))
           {
               //Are we holding touch on the right side?
               if (Input.mousePosition.x > Screen.width / 2)
                   moveVector.x = speed;
               else
                   moveVector.x = -speed;
           }

           // Y - Up and Down
           moveVector.y = verticalVelocity;

           // Z - Forward and Backward
           moveVector.z = speed;

           controller.Move(moveVector* Time.deltaTime);
    }

       public void SetSpeed(float modifier)
       {
           speed = 10.0f + modifier;
       }

       // It is beign called every time our capsule hits something
       private void OnControllerColliderHit(ControllerColliderHit hit)
       {
           if (hit.gameObject.tag == "Enemy")
               Death();
       }

       void Death()
       {
           isDead = true;
           GetComponent<Score>().OnDeath();
       }

    
}
