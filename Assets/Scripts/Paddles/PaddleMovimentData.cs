using Unity.Entities;

[GenerateAuthoringComponent]
public class PaddleMovimentData : IComponentData {
    public int gameplaySide;
    public int direction;
    public int xDirection;

    public float speed;
    public float speedMultiplier = 1f;
}