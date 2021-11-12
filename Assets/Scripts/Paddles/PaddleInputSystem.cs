using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PaddleInputSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var timeDelta = Time.DeltaTime;

        Entities
            .WithoutBurst()
            .ForEach((
                PaddleMovimentData moveData,
                DashActionData dashData,
                in PaddleInputData inputData
            ) => {
                PaddleHandle(moveData, dashData, inputData, timeDelta);
            }).Run();

        return default;
    }

    private void PaddleHandle(
        PaddleMovimentData moveData,
        DashActionData dashData,
        in PaddleInputData inputData,
        float timeDelta
    )
    {
        moveData.direction = 0;
        moveData.xDirection = 0;

        moveData.speedMultiplier = 1f;
        dashData.dashCoolDown += timeDelta;
        if(Input.GetKeyDown(inputData.dashKey) && dashData.CanDash)
        {
            moveData.speedMultiplier = 20f;
            dashData.dashCoolDown = 0f;
        }

        moveData.direction += Input.GetKey(inputData.upKey) ? 1 : 0;
        moveData.direction -= Input.GetKey(inputData.downKey) ? 1 : 0;

        moveData.xDirection += Input.GetKey(inputData.fowardKey) ? 1 : 0;
        moveData.xDirection -= Input.GetKey(inputData.backwardKey) ? 1 : 0;

        moveData.gameplaySide = inputData.playerId % 2 == 0 ? -1 : 1;

        if(Input.GetKeyDown(inputData.spawnNewBall))
        {
            GameManager.main.LaunchNewBall(inputData.playerId);
        }
    }
}
