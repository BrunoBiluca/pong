using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class GoalCheckSystem : JobComponentSystem
{

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
        Entities
            .WithAll<BallTag>()
            .WithoutBurst()
            .ForEach((Entity entity, ref Rotation rotation, in Translation trans) => {
                var pos = trans.Value;

                var bound = GameManager.main.xGameEdge;
                
                if(pos.x >= bound)
                {
                    ScoreGoal(entity, rotation, trans, 1f, in ecb);
                }
                else if(pos.x <= -bound)
                {
                    ScoreGoal(entity, rotation, trans, -1f, in ecb);
                }
            }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();

        return default;
    }

    private static void ScoreGoal(
        Entity entity,
        Rotation rotation,
        Translation trans,
        float direction,
        in EntityCommandBuffer ecb
    )
    {
        GameManager.main.UpdatePlayerScore(0);

        var goalExplosion = ecb
            .Instantiate(GoalExplosionPrefabEntity.prefab);

        var rot = new Rotation() {
            Value = rotation.Value * Quaternion.Euler(0f, 0f, direction * 90f)
        };
        ecb.AddComponent(goalExplosion, rot);

        ecb.AddComponent(goalExplosion, trans);
        ecb.DestroyEntity(entity);
    }
}
