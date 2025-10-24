using System.Collections.Generic;
using Godot;
using Godot.Collections;

namespace TextMeshRenderingQuestionDemo;

public sealed partial class RenderingNode : Node3D
{
    // private static ShaderMaterial Material { get; } = new();
    //
    // private static Rid MaterialRid { get; } = Material.GetRid();

    private Shader Shader { get; } = ResourceLoader.Load<Shader>("res://TextShader.gdshader");

    private Rid[] TextInstances { get; } = new Rid[Grid.CellCountX * Grid.CellCountZ];

    private Rid[] TextMeshes { get; } = new Rid[Grid.CellCountX * Grid.CellCountZ];

    private ShaderMaterial[] TextMaterials { get; } =
        new ShaderMaterial[Grid.CellCountX * Grid.CellCountZ];

    private static Transform3D TransformBase { get; }

    static RenderingNode()
    {
        var transform = Transform3D.Identity;
        transform = transform.Scaled(new Vector3(20, 20, 1));
        transform = transform.Rotated(Vector3.Right, float.DegreesToRadians(90));
        transform = transform.Rotated(Vector3.Forward, float.DegreesToRadians(180));
        transform = transform.Translated(Vector3.Up);
        TransformBase = transform;
    }

    public override void _ExitTree()
    {
        foreach (var instance in TextInstances)
        {
            RenderingServer.FreeRid(instance);
        }
        foreach (var mesh in TextMeshes)
        {
            RenderingServer.FreeRid(mesh);
        }
    }

    public void GenerateTexts(IList<Cell> cells)
    {
        var textMesh = new TextMesh();
        var scenario = GetWorld3D().Scenario;
        for (var index = 0; index < cells.Count; index++)
        {
            var cell = cells[index];
            textMesh.Text = cell.ToString();
            var array = textMesh.GetMeshArrays();

            RenderingServer.FreeRid(TextMeshes[index]);
            if (!TextInstances[index].IsValid)
                TextInstances[index] = RenderingServer.InstanceCreate();
            TextMeshes[index] = RenderingServer.MeshCreate();
            var mesh = TextMeshes[index];
            RenderingServer.MeshAddSurfaceFromArrays(
                mesh,
                RenderingServer.PrimitiveType.Triangles,
                array
            );
            var meterial = GetMaterial(index, cell);
            RenderingServer.MeshSurfaceSetMaterial(mesh, 0, meterial);
            var instance = TextInstances[index];
            RenderingServer.InstanceSetBase(instance, mesh);
            RenderingServer.InstanceSetScenario(instance, scenario);
            var aabb = new Aabb()
            {
                Position = cell.Center,
                Size = new Vector3(10, 10, 10)
            };
            RenderingServer.InstanceSetCustomAabb(instance, aabb);
            // var transform = Transform3D.Identity.Translated(cell.Center);
            // RenderingServer.InstanceSetTransform(instance, transform);
        }
    }

    private Rid GetMaterial(int index, Cell cell)
    {
        if (TextMaterials[index] != null)
            return TextMaterials[index].GetRid();
        var material = TextMaterials[index] = new ShaderMaterial { Shader = Shader };
        material.SetShaderParameter("color", Colors.SkyBlue);
        material.SetShaderParameter("transform", TransformBase.Basis);
        material.SetShaderParameter("cell_center", cell.Center);
        return material.GetRid();
    }

    // private static Array GetMeshArray(TextMesh textMesh, Cell cell)
    // {
    //     textMesh.Text = cell.ToString();
    //     var array = textMesh.GetMeshArrays();
    //     return array;
    //     var vertices = (Vector3[])array[(int)Mesh.ArrayType.Vertex];
    //     var normals = (Vector3[])array[(int)Mesh.ArrayType.Normal];
    //
    //     for (var i = 0; i < vertices.Length; i++)
    //     {
    //         var vertex = vertices[i];
    //         vertex = TransformBase * vertex;
    //         vertices[i] = vertex;
    //
    //         var normal = normals[i];
    //         normal = TransformBase * normal;
    //         normals[i] = normal;
    //     }
    //     array[(int)Mesh.ArrayType.Vertex] = vertices;
    //     array[(int)Mesh.ArrayType.Normal] = normals;
    //     return array;
    // }
}
