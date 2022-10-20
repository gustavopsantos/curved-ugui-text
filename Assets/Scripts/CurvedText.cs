using System;
using Extensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CurvedText : BaseMeshEffect
{
    [SerializeField] private float _arc = 360;
    [SerializeField] private float _radius = 128;
    [SerializeField] private float _rotation = 90;

    private Matrix4x4 _matrix;

    public override void ModifyMesh(VertexHelper vertexHelper)
    {
        var vertices = vertexHelper.GetVertices();

        for (int i = 0; i < vertices.Length; i += 4)
        {
            var charIndex = i / 4;
            var charCount = vertices.Length / 4;
            ModifyCharacter(new Span<UIVertex>(vertices, i, 4), charIndex, charCount);
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            vertexHelper.SetUIVertex(vertices[i], i);
        }
    }

    private void ModifyCharacter(Span<UIVertex> vertices, int charIndex, int charCount)
    {
        // Step1
        var horizontalCenter = (vertices[0].position.x + vertices[2].position.x) / 2;
        Vector3 charMidBaselinePos = new Vector2(horizontalCenter, 0);
        vertices[0].position -= charMidBaselinePos;
        vertices[1].position -= charMidBaselinePos;
        vertices[2].position -= charMidBaselinePos;
        vertices[3].position -= charMidBaselinePos;
        
        // Step2
        
        var angleStep = _arc / charCount;
        var angle = _rotation - charIndex * angleStep;

        var radians = angle * Mathf.Deg2Rad;
        var normal = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));

        
        var translation = normal * _radius;
        var rotation = Quaternion.LookRotation(Vector3.forward, normal);
        
        _matrix = Matrix4x4.TRS(translation, rotation, Vector3.one);
        vertices[0].position = _matrix.MultiplyPoint3x4(vertices[0].position);
        vertices[1].position = _matrix.MultiplyPoint3x4(vertices[1].position);
        vertices[2].position = _matrix.MultiplyPoint3x4(vertices[2].position);
        vertices[3].position = _matrix.MultiplyPoint3x4(vertices[3].position);
    }

    private void OnDrawGizmos()
    {
        var radians = _rotation * Mathf.Deg2Rad;
        var heading = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        Gizmos.DrawRay(transform.position, heading * 1000);
        Handles.DrawWireArc(transform.position, Vector3.back, Vector3.up, _arc, _radius);
        
        Gizmos.DrawSphere(_matrix.GetPosition(), 32);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_matrix.GetPosition(), _matrix.Right() * 100);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(_matrix.GetPosition(), _matrix.Up() * 100);
    }

    // public void Update()
    // {
    //     var graphic = GetComponent<Graphic>();
    //     graphic.SetVerticesDirty();
    // }
}