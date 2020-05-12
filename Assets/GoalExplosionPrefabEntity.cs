using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GoalExplosionPrefabEntity : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

    public static Entity prefab;

    public GameObject goalExplosionPrefab;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
        referencedPrefabs.Add(goalExplosionPrefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        prefab = conversionSystem.GetPrimaryEntity(goalExplosionPrefab);
    }
}
