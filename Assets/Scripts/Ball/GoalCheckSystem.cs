using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

public class GoalCheckSystem : JobComponentSystem {

    protected override JobHandle OnUpdate(JobHandle inputDeps) {
        EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);

        Entities
            .WithAll<BallTag>()
            .WithoutBurst()
            .ForEach((Entity entity, in Translation trans) => {
                var pos = trans.Value;
                float bound = GameManager.main.xBound;

                if(pos.x >= bound) {
                    GameManager.main.UpdatePlayerScore(0);
                    var goalExplosion = ecb.Instantiate(GoalExplosionPrefabEntity.prefab);
                    ecb.AddComponent(goalExplosion, trans);
                    ecb.DestroyEntity(entity);
                } else if(pos.x <= -bound) {
                    GameManager.main.UpdatePlayerScore(1);
                    var goalExplosion = ecb.Instantiate(GoalExplosionPrefabEntity.prefab);
                    ecb.AddComponent(goalExplosion, trans);
                    ecb.DestroyEntity(entity);
                }
            }).Run();

        ecb.Playback(EntityManager);
        ecb.Dispose();

        return default;
    }
}
