using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public class PaddleInputData : IComponentData {
    public int playerId;
    public KeyCode upKey;
    public KeyCode downKey;

    public KeyCode dashKey;

    public KeyCode spawnNewBall;
}
