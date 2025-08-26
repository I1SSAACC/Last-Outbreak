using UnityEngine;
using UnityEngine.AI;

namespace JU.AI
{
    /// <summary>
    /// Contains utility methods for AI.
    /// </summary>
    public static class JU_Ai
    {
        /// <summary>
        /// Calculate a path inside navmesh.
        /// </summary>
        /// <param name="start">Start point.</param>
        /// <param name="end">Destination.</param>
        /// <param name="path">The navmesh data that will receive the new path.</param>
        public static void CalculatePath(Vector3 start, Vector3 end, NavMeshPath path)
        {
            if ((start - end).magnitude < 0.5f)
                return;

            Debug.Assert(path != null, "Path data can't be null.");
            Debug.Assert((start - end).magnitude > 0.5f, "The start position can't be equal to end position.");

            NavMesh.CalculatePath(start, end, NavMesh.AllAreas, path);
        }

        /// <summary>
        /// Try get the point inside the navmesh. Will return the same position if couldn't get the point inside navmesh.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static Vector3 ClosestToNavMesh(Vector3 point)
        {
            if (NavMesh.SamplePosition(point, out NavMeshHit hit, float.MaxValue, NavMesh.AllAreas))
                point = hit.position;

            return point;
        }

        /// <summary>
        /// Try get the point inside the navmesh, return true and the point as result if success. 
        /// Will return false the same position if couldn't get the point inside navmesh.
        /// </summary>
        /// <param name="point">The source point.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static bool ClosestToNavMesh(Vector3 point, out Vector3 result)
        {
            result = point;
            if (NavMesh.SamplePosition(point, out NavMeshHit hit, float.MaxValue, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }

            return false;
        }
    }
}