using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float minSize = 5f;
    public float maxSize = 10f;
    public float padding = 2f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2;
        float distance = Vector3.Distance(player1.position, player2.position);

        cam.transform.position = new Vector3(
            midpoint.x,
            midpoint.y,
            cam.transform.position.z
        );

        cam.orthographicSize = Mathf.Clamp(distance / 2 + padding, minSize, maxSize);
    }
}
