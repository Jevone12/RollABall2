using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    Vector2 movement;
    Vector2 mouseMovement;
    float cameraUpRotation = 0.0f;
    CharacterController controller;
    [SerializeField]
    float speed = 2.0f;
    [SerializeField]
    float mouseSensitivity;
    [SerializeField]
    GameObject cam;
    [SerializeField]
    GameObject BulletSpawner;
    [SerializeField]
    GameObject bullet;
    
    bool hasJumped = false;
    float ySpeed = 0;
    [SerializeField]
    float jumpHeight = 1.0f;
    [SerializeField]
    float gravityVal = 9.8f;


    Rigidbody rb;
    //Start is called once before the first execution of Update after 
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

         rb = GetComponent<Rigidbody>();
        print("Welcome!");
    }
    //Update is called once per frame
    void Update()
    {
        float lookX = mouseMovement.x* Time.deltaTime * mouseSensitivity;
        float LookY = mouseMovement.y * Time.deltaTime * mouseSensitivity;

        cameraUpRotation -= LookY;

        cameraUpRotation = Mathf.Clamp(cameraUpRotation, -90,90);

        cam.transform.localRotation = Quaternion.Euler(cameraUpRotation,0,0);

        transform.Rotate(Vector3.up * lookX);

        float moveX = movement.x;
        float moveZ = movement.y;

        Vector3 actual_movement = (transform.forward * moveZ) + (transform.right * moveX);
        
        if (hasJumped)
        {
          hasJumped = false;
          ySpeed = jumpHeight;
        }
        
        ySpeed -= gravityVal * Time.deltaTime;
        actual_movement.y = ySpeed;


        controller.Move(actual_movement * Time.deltaTime * speed);

    }

    void onJump()
    {
      if (controller.isGrounded)
      {
        hasJumped = true;
      }
    }
      void OnMove(InputValue moveVal)
    {
       movement = moveVal.Get<Vector2>();
    }

    void OnLook(InputValue lookVal)
    {
      mouseMovement = lookVal.Get<Vector2>();
    }

    void OnAttack()
    {
        Instantiate(bullet, BulletSpawner.transform.position, BulletSpawner.transform.rotation);
    }
    

}


