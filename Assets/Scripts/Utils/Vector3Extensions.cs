using UnityEngine;

namespace Utils
{
    public static class Vector3Extensions
    {
        public static bool Equals(this Vector3 vector, Vector3 otherVector)
        {
            return vector.x == otherVector.x && vector.y == otherVector.y && vector.z == otherVector.z;
        }

        public static bool LessThan(this Vector3 vector, Vector3 otherVector)
        {
            return vector.x < otherVector.x && vector.y < otherVector.y && vector.z < otherVector.z;
        }

        public static bool LessThanOrEquals(this Vector3 vector, Vector3 otherVector)
        {
            return vector.x <= otherVector.x && vector.y <= otherVector.y && vector.z <= otherVector.z;
        }

        public static bool GreaterThan(this Vector3 vector, Vector3 otherVector)
        {
            return vector.x > otherVector.x && vector.y > otherVector.y && vector.z > otherVector.z;
        }

        public static bool GreaterThanOrEquals(this Vector3 vector, Vector3 otherVector)
        {
            return vector.x >= otherVector.x && vector.y >= otherVector.y && vector.z >= otherVector.z;
        }
    }
}