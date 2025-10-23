using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool("Crouching", true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movementStateManager, movementStateManager.Run);
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (movementStateManager.dir.magnitude < 0.1f) ExitState(movementStateManager, movementStateManager.Idle);
            else ExitState(movementStateManager, movementStateManager.Walk);
        }
        
        if (movementStateManager.verticalInput < 0) movementStateManager.currentMoveSpeed = movementStateManager.crouchBackSpeed;
        else movementStateManager.currentMoveSpeed = movementStateManager.crouchSpeed;
    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.anim.SetBool("Crouching", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}
