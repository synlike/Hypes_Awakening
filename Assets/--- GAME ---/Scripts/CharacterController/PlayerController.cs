using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;

    public float playerSpeed = 0;

    [SerializeField]
    private float playerMaxSpeed;

    [SerializeField]
    private float timeToHitMaxSpeed = 1f;

    // Player Speed Lerp
    private float timeElapsed;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, 0, moveY);

        if(movement != Vector3.zero)
        {
            transform.forward = movement;
            controller.Move(movement * PlayerSpeed() * Time.deltaTime);

            // walk animation
        }
        else
        {
            ResetPlayerSpeed();
            // idle animation
        }
    }

    private void ResetPlayerSpeed()
    {
        timeElapsed = 0f;
    }

    // Lerp PlayerSpeed to add acceleration
    private float PlayerSpeed()
    {
        if(timeElapsed < timeToHitMaxSpeed)
        {
            playerSpeed = Mathf.Lerp(0, playerMaxSpeed, timeElapsed / timeToHitMaxSpeed);
            timeElapsed += Time.deltaTime;
        }
        else
        {
            playerSpeed = playerMaxSpeed;
        }

        return playerSpeed;
    }
}
