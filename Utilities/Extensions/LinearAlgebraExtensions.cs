﻿using UnityEngine;

namespace PJL.Utilities.Extensions
{
    public static class LinearAlgebraExtensions
    {
        public static void Deconstruct(this Vector4 vector, out float x, out float y, out float z, out float w)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
            w = vector.w;
        }

        public static void Deconstruct(this Vector3 vector, out float x, out float y, out float z)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static void Deconstruct(this Vector3Int vector, out int x, out int y, out int z)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static void Deconstruct(this Vector2 vector, out float x, out float y)
        {
            x = vector.x;
            y = vector.y;
        }

        public static void Deconstruct(this Vector2Int vector, out int x, out int y)
        {
            x = vector.x;
            y = vector.y;
        }

        public static void Deconstruct(this Quaternion quaternion, out float x, out float y, out float z, out float w)
        {
            x = quaternion.x;
            y = quaternion.y;
            z = quaternion.z;
            w = quaternion.w;
        }

        public static Quaternion Quaternion(this Vector4 vector) => new(vector.x, vector.y, vector.z, vector.w);

        public static Vector4 Vector4(this Quaternion quaternion) =>
            new(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

        public static Vector2 ClampedToNormalized(this Vector2 vector) =>
            vector.sqrMagnitude > 1 ? vector.normalized : vector;

        public static Vector3 ClampedToNormalized(this Vector3 vector) =>
            vector.sqrMagnitude > 1 ? vector.normalized : vector;

        public static Vector4 ClampedToNormalized(this Vector4 vector) =>
            vector.sqrMagnitude > 1 ? vector.normalized : vector;
    }
}