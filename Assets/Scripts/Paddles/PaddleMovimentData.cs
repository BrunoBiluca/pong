using Unity.Entities;

[GenerateAuthoringComponent]
public class PaddleMovimentData : IComponentData {
    public int direction;
    public float speed;
    public float speedMultiplier = 1f;
}