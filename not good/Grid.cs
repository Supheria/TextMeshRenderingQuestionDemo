using Godot;

namespace TextMeshRenderingQuestionDemo;

public sealed partial class Grid : Node3D
{
    public const int CellCountX = 15;
    public const int CellCountZ = 20;
    private const int CellEdgeLength = 10;

    private Cell[] Cells { get; } = new Cell[CellCountX * CellCountZ];

    private RenderingNode RenderingNode { get; } = new();

    public Grid()
    {
        CreateCells();
        AddChild(RenderingNode);
    }

    public override void _Ready()
    {
        RenderingNode.GenerateTexts(Cells);
    }

    private void CreateCells()
    {
        for (int x = 0, index = 0; x < CellCountZ; x++)
        {
            for (var z = 0; z < CellCountX; z++)
            {
                var cell = CreateCell(z, x, index);
                Cells[index++] = cell;
            }
        }
    }

    private static Cell CreateCell(int x, int z, int index)
    {
        x *= CellEdgeLength;
        z *= CellEdgeLength;
        var center = new Vector3(x, 0, z);
        var cell = new Cell { Center = center, Index = index };
        return cell;
    }
}
