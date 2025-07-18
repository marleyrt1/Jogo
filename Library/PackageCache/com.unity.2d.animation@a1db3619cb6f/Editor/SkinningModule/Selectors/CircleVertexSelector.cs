using UnityEngine;

namespace UnityEditor.U2D.Animation
{
    internal class CircleVertexSelector : ICircleSelector<int>
    {
        public ISelection<int> selection { get; set; }
        public BaseSpriteMeshData spriteMeshData { get; set; }
        public Vector2 position { get; set; }
        public float radius { get; set; }

        public void Select()
        {
            if (spriteMeshData == null)
                return;

            float sqrRadius = radius * radius;

            for (int i = 0; i < spriteMeshData.vertexCount; i++)
            {
                if ((spriteMeshData.vertices[i] - position).sqrMagnitude <= sqrRadius)
                {
                    selection.Select(i, true);
                }
            }
        }
    }
}
