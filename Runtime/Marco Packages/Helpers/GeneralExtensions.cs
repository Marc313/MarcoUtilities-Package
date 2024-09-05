using System.Collections.Generic;
using UnityEngine;

namespace MarcoUtilities.Extensions
{
    public static class GeneralExtensions
    {
        /// <summary>
        /// Rounds a <see cref="Vector3"/> to a <see cref="Vector3Int"/>.
        /// </summary>
        #region Vector Helpers
        public static Vector3Int ToVector3Int(this Vector3 vector)
        {
            return new Vector3Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
        }

        /// <summary>
        /// Overrides the y value of <paramref name="vector"/>
        /// </summary>
        /// <param name="vector"></param>
        public static Vector3 SetY(this Vector3 vector, float yValue)
        {
            vector.y = yValue;
            return vector;
        }

        /// <summary>
        /// Returns a deviation of the <paramref name="vector"/>, but keeps the original intact.
        /// </summary>
        /// <param name="vector"></param>
        public static Vector3 Where(this Vector3 vector, float? x = null, float? y = null, float? z = null)
        {
            Vector3 newVector = vector;
            if (x != null)
                newVector.x = (float) x;
            if (y != null)
                newVector.y = (float) y;
            if (z != null)
                newVector.z = (float) z;

            return newVector;
        }

        /// <summary>
        /// Converts a <see cref="Vector2"/> into a <see cref="Vector3"/> with z = 0.
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector)
        {
            return new Vector3(vector.x, vector.y);
        }

        /// <summary>
        /// Converts a <see cref="Vector3"/> into a <see cref="Vector2"/>, ignoring the z.
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
        #endregion

        #region Transform Helpers
        /// <summary> Rotates a gameobject clockwise in the specified directions. </summary>
        /// <returns> Returns the rotation of the object </returns>
        public static Quaternion RotateClockwise(this Transform transform, float? angle_x = null, float? angle_y = null, float? angle_z = null)
        {
            Vector3 euler = transform.rotation.eulerAngles;
            if (angle_x != null)
                euler.x += (float)angle_x;
            if (angle_y != null)
                euler.y += (float)angle_y;
            if (angle_z != null)
                euler.z += (float)angle_z;

            transform.rotation = Quaternion.Euler(euler);
            return transform.rotation;
        }

        /// <summary> Sets the euler angles of a transform. </summary>
        /// <returns> Returns the rotation of the object </returns>
        public static Quaternion SetEuler(this Transform transform, float? angle_x = null, float? angle_y = null, float? angle_z = null)
        {
            Vector3 euler = transform.rotation.eulerAngles;
            if (angle_x != null)
                euler.x = (float)angle_x;
            if (angle_y != null)
                euler.y = (float)angle_y;
            if (angle_z != null)
                euler.z = (float)angle_z;

            transform.rotation = Quaternion.Euler(euler);
            return transform.rotation;
        }

        /// <summary>
        /// Sets the x value of _transform.position to _x
        /// </summary>
        public static void SetXPosition(this Transform _transform, float _x)
        {
            Vector3 newPos = _transform.position;
            newPos.x = _x;
            _transform.position = newPos;
        }

        /// <summary>
        /// Sets the y value of _transform.position to _y
        /// </summary>
        public static void SetYPosition(this Transform _transform, float _y)
        {
            Vector3 newPos = _transform.position;
            newPos.y = _y;
            _transform.position = newPos;
        }

        /// <summary>
        /// Sets the z value of _transform.position to _z
        /// </summary>
        public static void SetZPosition(this Transform _transform, float _z)
        {
            Vector3 newPos = _transform.position;
            newPos.z = _z;
            _transform.position = newPos;
        }
        #endregion

        #region GameObject Helpers
        /// <summary> Rotates a gameobject clockwise in the specified directions. </summary>
        /// <returns> Returns the rotation of the object </returns>
        public static Quaternion RotateClockwise(this GameObject gameObject, float? angle_x = null, float? angle_y = null, float? angle_z = null)
        {
            return gameObject.transform.RotateClockwise(angle_x, angle_y, angle_z);
        }
        #endregion

        #region MeshRenderer Helpers
        public static float GetHalfModelHeight(this MeshRenderer meshRenderer)
        {
            return meshRenderer.bounds.size.y / 2;
        }
        #endregion

        #region Collider Helpers
        public static float GetColliderHeight(this Collider collider)
        {
            return collider.bounds.size.y;
        }

        public static float GetHalfColliderHeight(this Collider collider)
        {
            return GetColliderHeight(collider) / 2;
        }
        #endregion

        /// <summary>
        /// Extracts submeshes from a mesh.
        /// </summary>
        /// <remarks>
        /// EXPERIMENTAL! I am not sure if this function works.
        /// </remarks>
        public static Mesh[] SplitSubmeshes(this Mesh _mesh, Material[] _materials, MeshRenderer _meshRenderer)
        {
            List<Vector3>[] submeshVertices = new List<Vector3>[_materials.Length];
            List<int>[] submeshIndices = new List<int>[_materials.Length];
            for (int i = 0; i < _materials.Length; i++)
            {
                submeshVertices[i] = new List<Vector3>();
                submeshIndices[i] = new List<int>();
            }

            // Iterate over the _mesh triangles and assign each triangle to the correct submesh based on its material
            int[] indices = _mesh.GetIndices(0);
            for (int i = 0; i < indices.Length; i += 3)
            {
                int submeshIndex = _mesh.subMeshCount - 1;
                for (int j = 0; j < _materials.Length; j++)
                {
                    int materialIndex = _mesh.GetSubMesh(j).indexStart;
                    int materialIndexEnd = materialIndex + _mesh.GetSubMesh(j).indexCount;

                    if (i >= materialIndex && i < materialIndexEnd)
                    {
                        submeshIndex = j;
                        break;
                    }
                }

                submeshIndices[submeshIndex].Add(indices[i]);
                submeshIndices[submeshIndex].Add(indices[i + 1]);
                submeshIndices[submeshIndex].Add(indices[i + 2]);

                submeshVertices[submeshIndex].Add(_mesh.vertices[indices[i]]);
                submeshVertices[submeshIndex].Add(_mesh.vertices[indices[i + 1]]);
                submeshVertices[submeshIndex].Add(_mesh.vertices[indices[i + 2]]);
            }

            // Create new meshes for each submesh
            Mesh[] submeshes = new Mesh[_materials.Length];
            for (int i = 0; i < _materials.Length; i++)
            {
                submeshes[i] = new Mesh();
                submeshes[i].vertices = submeshVertices[i].ToArray();
                submeshes[i].triangles = submeshIndices[i].ToArray();
                submeshes[i].RecalculateNormals();
                submeshes[i].RecalculateBounds();
            }

            return submeshes;
        }
    }
}
