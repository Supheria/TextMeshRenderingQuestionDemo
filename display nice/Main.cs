using System;
using Godot;

namespace TextMeshRenderingQuestionDemo;

public partial class Main : Node3D
{
    public override void _Ready()
    {
        AddChild(new Grid());
    }
}
