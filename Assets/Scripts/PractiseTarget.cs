using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PractiseTarget : MonoBehaviour
{
    public Material damage;

    public float strafeSpeed = 3f;
    public float strafeTime = 1f;
    public float gravityForce = 9.81f;

    private Vector3 moveDirection = Vector3.zero;
    private float verticalVelocity = 0f;
    private CharacterController characterController;

    public float MaxHealth = 100f; 
    public float Health = 100f;

    public TextMeshProUGUI HealthText;

    void Start()
    {
        // Add a CharacterController if there isn't one
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
            characterController = gameObject.AddComponent<CharacterController>();

        StrafeRight();
    }

    void Update()
    {
        HealthText.text = Health + "  ";
        // Apply gravity
        if (!characterController.isGrounded)
            verticalVelocity -= gravityForce * Time.deltaTime;
        else
            verticalVelocity = -1f; // Small downward force to keep grounded

        // Combine horizontal strafe with vertical gravity
        Vector3 finalMove = moveDirection * strafeSpeed;
        finalMove.y = verticalVelocity;

        characterController.Move(finalMove * Time.deltaTime);

        float healthPercent = Health / MaxHealth;
          
        //damage.SetFloat("_MaxHeight", MaxHealth - Health );
        damage.SetFloat("_MinHeight", healthPercent);

    }

    public void StrafeLeft()
    {
        moveDirection = -transform.right;
        Invoke("StrafeRight", strafeTime);
    }

    public void StrafeRight()
    {
        moveDirection = transform.right;
        Invoke("StrafeLeft", strafeTime);
    }
}