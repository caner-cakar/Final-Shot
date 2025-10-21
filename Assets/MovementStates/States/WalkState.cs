using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKey(KeyCode.LeftShift)) ExitState(movementStateManager, movementStateManager.Run);
        else if (Input.GetKey(KeyCode.C)) ExitState(movementStateManager, movementStateManager.Crouch);
        else if (movementStateManager.dir.magnitude < 0.1f) ExitState(movementStateManager, movementStateManager.Idle);

        if (movementStateManager.verticalInput < 0) movementStateManager.currentMoveSpeed = movementStateManager.walkBackSpeed;
        else movementStateManager.currentMoveSpeed = movementStateManager.walkSpeed;
    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.anim.SetBool("Walking", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}
