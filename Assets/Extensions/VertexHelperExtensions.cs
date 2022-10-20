using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Extensions
{
    public static class VertexHelperExtensions
    {
        public static UIVertex[] GetVertices(this VertexHelper vertexHelper)
        {
            var vertexes = new UIVertex[vertexHelper.currentVertCount];

            for (int i = 0; i < vertexes.Length; i++)
            {
                vertexHelper.PopulateUIVertex(ref vertexes[i], i);
            }

            return vertexes;
        }
        
        public static void SetVertices(this VertexHelper vertexHelper, IList<UIVertex> vertices)
        {
            for (int i = 0; i < vertexHelper.currentVertCount; i++)
            {
                vertexHelper.SetUIVertex(vertices[0], i);
            }
        }
    }
}