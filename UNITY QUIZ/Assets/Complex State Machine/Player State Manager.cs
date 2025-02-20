using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStateManager : MonoBehaviour
{
    
    public PlayerBaseState currentState;

    [HideInInspector]
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkState walkState = new PlayerWalkState();
    
    [HideInInspector]
    public Vector2 movement;
    CharacterController controller;
    public float default_speed = 1;
    [HideInInspector]
    public PlayerSneakState sneakState = new PlayerSneakState();
    public bool isSneaking = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

       controller = GetComponent<CharacterController>();

       SwitchState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    //Handle Input //

    void OnMove(InputValue moveVal)
    {
        movement = moveVal.Get<Vector2>();
    }

    void OnSprint()
    {
      if (isSneaking == false)
      {
        isSneaking = true;
      }else
      {
        isSneaking = false;
      }
    }

    // Helper Function //
    public void MovePlayer(float speed)
    {
        float moveX = movement.x;
        float moveZ = movement.y;

        Vector3 actual_movement = new Vector3(moveX, 0, moveZ);
        controller.Move(actual_movement * Time.deltaTime);

    }
      
      public void SwitchState(PlayerBaseState newState)
      {
        currentState = newState;
        currentState.EnterState(this);
      }



}
