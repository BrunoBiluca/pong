using UnityEngine;
using Unity.Entities;

[GenerateAuthoringComponent]
public class PaddleInputData : IComponentData {
    public KeyCode upKey;
    public KeyCode downKey;
}
