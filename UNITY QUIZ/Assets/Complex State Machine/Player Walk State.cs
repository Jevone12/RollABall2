using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
   public override void EnterState(PlayerStateManager player)
   {
      Debug.Log("I'm walking!");
   }
   
   public override void UpdateState(PlayerStateManager player)
   {
    // What are doing during state?
    player.MovePlayer(player.default_speed);

    // On what conditions do we leave the state?
    if (player.movement.magnitude < 0.1)
    {
        player.SwitchState(player.idleState);
    }else if (player.isSneaking)
    {
        player.SwitchState(player.sneakState);
    }
   }
}
