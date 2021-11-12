using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class PaddleMovimentSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {

        float deltaTime = Time.DeltaTime;
        float yBound = GameManager.main.yBound;
        float2 xBound = GameManager.main.xBound;

        Entities
            .WithoutBurst()
            .ForEach(
                (ref Translation trans, in PaddleMovimentData movimentData)
                    => {
                        MovementHandle(
                            ref trans, 
                            movimentData, 
                            deltaTime, 
                            yBound, 
                            xBound
                        );
                    }
                ).Run();

        return default;
    }

    private void MovementHandle(
        ref Translation trans,
        in PaddleMovimentData movimentData,
        float deltaTime,
        float yBound,
        float2 xBound
    )
    {
        var speed = movimentData.speed * movimentData.speedMultiplier;

        var boundXFirst = xBound.x * movimentData.gameplaySide;
        var boundXSecond = xBound.y * movimentData.gameplaySide;

        var minX = math.min(boundXFirst, boundXSecond);
        var maxX = math.max(boundXFirst, boundXSecond);

        trans.Value.x = math.clamp(
            trans.Value.x + (speed * movimentData.xDirection * deltaTime),
            minX,
            maxX
        );

        trans.Value.y = math.clamp(
            trans.Value.y + (speed * movimentData.direction * deltaTime), 
            -yBound, 
            yBound
        );
    }
}
