using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

[AlwaysSynchronizeSystem]
public class PaddleInputSystem : JobComponentSystem {
    protected override JobHandle OnUpdate(JobHandle inputDeps) {

        var timeDelta = Time.DeltaTime;

        Entities
            .WithoutBurst()
            .ForEach((PaddleMovimentData moveData, DashActionData dashData, in PaddleInputData inputData) => {
                moveData.direction = 0;

                moveData.speedMultiplier = 1f;
                dashData.dashCoolDown += timeDelta;
                if(Input.GetKeyDown(inputData.dashKey)) {
                    if(dashData.dashCoolDown >= dashData.dashCoolDownTotal) {
                        moveData.speedMultiplier = 20f;
                        dashData.dashCoolDown = 0f;
                    }
                }
                moveData.direction += Input.GetKey(inputData.upKey) ? 1 : 0;
                moveData.direction -= Input.GetKey(inputData.downKey) ? 1 : 0;


                if(Input.GetKeyDown(inputData.spawnNewBall)) {
                    GameManager.main.LaunchNewBall(inputData.playerId);
                }
            }).Run();

        return default;
    }
}
