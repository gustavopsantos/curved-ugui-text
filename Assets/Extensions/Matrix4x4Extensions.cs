using UnityEngine;

namespace Extensions
{
    public static class Matrix4x4Extensions
    {
        public static Vector3 Right(this Matrix4x4 matrix)
        {
            return new Vector3(matrix.m00, matrix.m10, matrix.m20);
        }
        
        public static Vector3 Up(this Matrix4x4 matrix)
        {
            return new Vector3(matrix.m01, matrix.m11, matrix.m21);
        }
    }
}