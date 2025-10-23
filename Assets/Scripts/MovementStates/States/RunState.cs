using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movementStateManager)
    {
        movementStateManager.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movementStateManager)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movementStateManager, movementStateManager.Walk);
        else if (movementStateManager.dir.magnitude < 0.1f) ExitState(movementStateManager, movementStateManager.Idle);

        if (movementStateManager.verticalInput < 0) movementStateManager.currentMoveSpeed = movementStateManager.runBackSpeed;
        else movementStateManager.currentMoveSpeed = movementStateManager.runSpeed;
    }

    void ExitState(MovementStateManager movementStateManager, MovementBaseState movementBaseState)
    {
        movementStateManager.anim.SetBool("Running", false);
        movementStateManager.SwitchState(movementBaseState);
    }
}
