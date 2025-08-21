using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JUTPS.AI
{
    [System.Serializable]
    public class FieldView
    {
        [Range(0, 500)] public float Radious;
        [Range(0,360)] public float Angle;

        public FieldView(float radious, float angle)
        {
            Radious = radious;
            Angle = angle;
        }

        public Collider[] CheckViewCollider(Vector3 position, Vector3 forward, LayerMask targetMask, GameObject viewerToIgnore = null)
        {
            List<Collider> colliders = Physics.OverlapSphere(position, Radious, targetMask).ToList();

            foreach (Collider col in colliders.ToArray())
            {
                Transform target = col.transform;
                
                Vector3 targetposition = target.position; targetposition.y = position.y;
                Vector3 directionToTarget = (targetposition - position).normalized;

                if (Vector3.Angle(forward, directionToTarget) > Angle / 2 || col.gameObject == viewerToIgnore)
                    colliders.Remove(col);
            }

            return colliders.ToArray();
        }

        public bool IsVisibleToThisFieldOfView(Transform LookedTarget, Vector3 ViewPosition, Vector3 ViewForward, LayerMask LayerMask, float threshold = 0.6f, string[] TagsToConsiderVisible = default(string[]))
        {
            if (LookedTarget == null) 
                return false;

            bool CanSeeTarget = true;
            Vector3 directionToTarget = (LookedTarget.position - ViewPosition).normalized;

            if (Vector3.Angle(ViewForward, directionToTarget) > Angle / 2)
            {
                CanSeeTarget = false;
            }
            else
            {
                float normalDistance = Vector3.Distance(ViewPosition, LookedTarget.position);
                Vector3 lineCastEndPosition = ViewPosition + directionToTarget * normalDistance;
                Physics.Linecast(ViewPosition, lineCastEndPosition, out RaycastHit hit, LayerMask);

                if (hit.collider != null)
                {
                    if (JUCharacterArtificialInteligenceBrain.TagMatches(hit.collider.tag, TagsToConsiderVisible) == false)
                    {
                        Debug.DrawLine(hit.point, ViewPosition, Color.cyan);
                        CanSeeTarget = false;
                    }
                    else
                    {
                        CanSeeTarget = true;
                    }
                }
            }

            return CanSeeTarget;
        }

        public static void DrawFieldOfView(Vector3 position, Vector3 forward, FieldView view)
        {
#if UNITY_EDITOR
            UnityEditor.Handles.color = Color.green;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.up, view.Radious);

            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireArc(position, Vector3.up, forward, view.Angle/2, view.Radious - 0.1f);
            UnityEditor.Handles.DrawWireArc(position, Vector3.up, forward, -view.Angle/2, view.Radious - 0.1f);

            UnityEditor.Handles.color = new Color(1, 0, 0, 0.1f);
            UnityEditor.Handles.DrawSolidArc(position, Vector3.up, forward, view.Angle/2, view.Radious - 0.2f);
            UnityEditor.Handles.DrawSolidArc(position, Vector3.up, forward, -view.Angle/2, view.Radious - 0.2f);
#endif
        }
    }
}