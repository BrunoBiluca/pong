using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PaddleMovimentSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps) {

        float deltaTime = Time.DeltaTime;
        float yBound = GameManager.main.yBound;

        Entities
            .WithoutBurst()
            .ForEach((ref Translation trans, in PaddleMovimentData movimentData) => {
                trans.Value.y = math.clamp(trans.Value.y + (movimentData.speed * movimentData.direction * deltaTime), -yBound, yBound);
            }).Run();

        return default;
    }
}
