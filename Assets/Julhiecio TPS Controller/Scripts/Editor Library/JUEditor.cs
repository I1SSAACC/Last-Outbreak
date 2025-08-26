#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace JU.Editor
{
    /// <summary>
    /// Editor utilities for JU systems.
    /// </summary>
    public class JUEditor
    {
        private static Camera _sceneViewCamera;

        /// <summary>
        /// The editor viewport camera.
        /// </summary>
        public static Camera SceneViewCamera
        {
            get
            {
                if (!_sceneViewCamera)
                    _sceneViewCamera = SceneView.lastActiveSceneView.camera;

                return _sceneViewCamera;
            }
        }

        /// <summary>
        /// The editor viewport position that can be used to spawn new gameObjects on user position.
        /// </summary>
        /// <returns></returns>
        public static Vector3 SceneViewSpawnPosition()
        {
            if (!SceneViewCamera)
                return Vector3.zero;

            Ray ray = SceneViewCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out RaycastHit hit))
                return hit.point;

            return SceneViewCamera.transform.position + (SceneViewCamera.transform.forward * 10);
        }

        /// <summary>
        /// The editor viewport rotation that can be used to spawn new gameObjects on user position.
        /// </summary>
        /// <returns></returns>
        public static Quaternion SceneViewSpawnRotation()
        {
            if (!SceneViewCamera)
                return Quaternion.identity;

            return Quaternion.Euler(Vector3.up * SceneViewCamera.transform.eulerAngles.y);
        }
    }
}

#endif