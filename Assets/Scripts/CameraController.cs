using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    Camera _camera;

    void Awake()
    {
        _camera = camera;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        if (camera.orthographic)
        {
            float spread = camera.farClipPlane - camera.nearClipPlane;
            float center = (camera.farClipPlane + camera.nearClipPlane) * 0.5f;
            Gizmos.DrawWireCube(new Vector3(0, 0, center), new Vector3(camera.orthographicSize * 2 * camera.aspect, camera.orthographicSize * 2, spread));
        }
        Gizmos.matrix = temp;
    }
}
