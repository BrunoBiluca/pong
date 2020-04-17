using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BallPrefabEntity : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity {

    public static Entity prefab;

    public GameObject ballPrefab;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs) {
        referencedPrefabs.Add(ballPrefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem) {
        prefab = conversionSystem.GetPrimaryEntity(ballPrefab);
    }

}
