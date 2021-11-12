using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;

[AlwaysSynchronizeSystem]
public class BallMovimentSystem : JobComponentSystem {
    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        var deltaTime = Time.DeltaTime;

        Entities
            .WithAll<BallTag>()
            .WithoutBurst()
            .ForEach((ref PhysicsVelocity phyVelocity, in BallMovimentData movimentData) => {
                var speedModifier = new float2(movimentData.speedIncreasedPerSecond * deltaTime);

                var newVel = phyVelocity.Linear.xy;
                newVel += math.lerp(-speedModifier, speedModifier, math.sign(newVel));
                phyVelocity.Linear.xy = newVel;
            }).Run();

        return default;
    }
}
