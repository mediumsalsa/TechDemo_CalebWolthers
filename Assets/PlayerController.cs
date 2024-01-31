using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float runSpeed = 5.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float jumpSpeed = 5.0f;
    [SerializeField] private float gravity = 10.0f;
    [SerializeField] private Vector3 teleportPoint = new Vector3(0, 0.95f, 0);


    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 2.0f;
    [SerializeField] private float upDownLimit = 65f;


    [Header("Spawn Settings")]
    [SerializeField] private Vector3 spawnPoint = new Vector3(0, 0.95f, 0);


    float currentSpeed;

    private float verticalRotation;

    [SerializeField] private Camera playerCamera;

    private Vector3 currentMovement = Vector3.zero;

    private CharacterController characterController;

    private Rigidbody rb;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        characterController = this.GetComponent<CharacterController>();

        rb = this.GetComponent<Rigidbody>();

        playerCamera = GetComponentInChildren<Camera>();
    }


    void Update()
    {


        if (characterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            currentMovement.y = jumpSpeed;
        }
        currentMovement.y -= gravity * Time.deltaTime;
        characterController.Move(currentMovement * Time.deltaTime);

        if (Input.GetKey("c"))
        {

            characterController.height = 1f;
        }
        else if (Input.GetKeyUp("c"))
        {
            currentSpeed = walkSpeed;
            characterController.height = 1.8f;
        }



        HandleMovement();

        HandleLook();

       

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killbox")
        {


            characterController.enabled = false;
            gameObject.transform.position = spawnPoint;
            characterController.enabled = true;


        }

        if (other.gameObject.tag == "CheckPoint")
        {
            spawnPoint = gameObject.transform.position;
        }

    }


    void HandleMovement()
    {


        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (!Input.GetKey("c"))
            {
                currentSpeed = runSpeed;
            }
        }
        else if (Input.GetKey("c"))
        {
            currentSpeed = crouchSpeed;
        }

        else
        {
            currentSpeed = walkSpeed;
        }


        Vector3 horizontalMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        horizontalMovement = transform.rotation * horizontalMovement;

        currentMovement.x = horizontalMovement.x * currentSpeed;
        currentMovement.z = horizontalMovement.z * currentSpeed;



        characterController.Move(currentMovement * Time.deltaTime);


        if (Input.GetKeyDown("e"))
        {

            //Vector3 teleportPoint = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);

            characterController.enabled = false;
            gameObject.transform.position = teleportPoint;
            characterController.enabled = true;
        }
    }



    void HandleLook()
    {
        float mouseXrotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        this.transform.Rotate(0, mouseXrotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownLimit, upDownLimit);


        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
