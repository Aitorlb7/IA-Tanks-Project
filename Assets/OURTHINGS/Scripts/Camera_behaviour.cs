using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Camera_behaviour : MonoBehaviour
{
    private float ZoomSpeed = 2, DampTime = 0.2f, ScreenEdgeBuffer = 4f, MinSize = 6.5f;
    private Vector3 MoveSpeed, DesiredPosition;
    public List<GameObject> Tanks;
    public Camera main_cam;


    void Start()
    {
        main_cam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (Tanks[1] && Tanks[0] != null)
        {
            Move();
            Zoom();
        }
    }

    private void Move()
    {
        FindAveragePos();

        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref MoveSpeed, DampTime);
    }

    private void FindAveragePos()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        for (int i = 0; i < 2; i++)
        {
            averagePos += Tanks[i].transform.position;
            numTargets++;
        }

        if(numTargets > 0)
        {
            averagePos /= numTargets;
        }

        averagePos.y = transform.position.y;

        DesiredPosition = averagePos;
    }

    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        main_cam.orthographicSize = Mathf.SmoothDamp(main_cam.orthographicSize, requiredSize, ref ZoomSpeed, DampTime);
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(DesiredPosition);

        float size = 0f;

        for (int i = 0; i < 2; i++)
        {
            if (!Tanks[i].gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(Tanks[i].transform.position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / main_cam.aspect);
        }

        size += ScreenEdgeBuffer;

        size = Mathf.Max(size, MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        FindAveragePos();

        transform.position = DesiredPosition;

        main_cam.orthographicSize = FindRequiredSize();
    }
}

