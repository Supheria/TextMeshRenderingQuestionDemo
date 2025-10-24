using Godot;

namespace TextMeshRenderingQuestionDemo;

public sealed class Cell
{
    public Vector3 Center { get; set; }

    public int Index { get; set; }

    public override string ToString()
    {
        return $"{Index}";
    }
}
