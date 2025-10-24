# TextMeshRenderingQuestionDemo

## Solution

Reset AABB and everything work normal.

more information: [Is there a way to make transform in shader effecting Mesh's vertices position in World3d? - Help / Shaders - Godot Forum](https://forum.godotengine.org/t/is-there-a-way-to-make-transform-in-shader-effecting-meshs-vertices-position-in-world3d/125924)

## Godot version

4.5.1 - mono stable / .Net9

## Difference

Project under `./display nice/`  use C# code without shader to transform meshes.

Project under`./not good/`use shader code to transform meshes.

The only different files between projects are `RenderingNode.cs` and `TextShader.gdshader` .

## The Problem

In ***display nice*** project, bunch of texts display good while moving camera anywhere.

In ***not good*** project, when camera excludes the text in (0, 0), all other texts disappear.

<img src=".\readme.png" alt="readme" style="zoom: 50%;" />

*(when "0" has been excluded, all texts will disappear)*

## How to move camera

Move forward: w

Move backward: s

Move left: a

Move right: d

Move up: [space]

Move down: [left shift]

Rotate left: q

Rotate right: e
