using UnityEngine;

using System.Collections.Generic;
using System.Linq;

using Pada1.BBCore;
using Pada1.BBCore.Framework;
using Pada1.BBCore.Tasks;     // TaskStatus
using BBUnity.Actions;        // GOAction

[Action("MyActions/hitRaycast")]
[Help("If the raycast casted by the tank hits an Object from the scene returns true.")]
public class hitRaycast : GOAction
{
    public LineRenderer line;
    public float lineLenght ;
    public RaycastHit hitInfo;
    public RaycastHit surrInfo;

    [OutParam("Rotate")]
    [Help("First value to be compared")]
    public bool rotate;

    [OutParam("newDirection")]
    public Vector3 newDirection;
    public override void OnStart()
    {
        lineLenght = 6;
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        newDirection = LineRaycast();
        return TaskStatus.COMPLETED;
    }

    Vector3 LineRaycast()
    {
        if (line.enabled)
        {
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, gameObject.transform.position + gameObject.transform.forward * lineLenght);
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitInfo, lineLenght))
            {
                if (hitInfo.collider.gameObject.tag == "Collision")
                {
                    foreach (Vector3 direction in GetCircleDirections(30, gameObject.transform.position.x, gameObject.transform.position.y))
                    {
                        Debug.DrawRay(gameObject.transform.position, direction, Color.blue);
                        if (!Physics.Raycast(gameObject.transform.position, direction, out surrInfo, 10))
                        {
                            Debug.Log("Third Cast");
                            rotate = true;
                            return direction;
                        }
                    }
                    //rotate = true;
                }
            }
            else
            {
                rotate = false;
            }
        }
        return gameObject.transform.forward;
    }
    private Vector3[] GetSphereDirections(int numDirections)  //Doing it in 3D it's unefficient and return weird directions
    {
        var pts = new Vector3[numDirections];
        var inc = Mathf.PI * (3 - Mathf.Sqrt(5));
        var off = 2f / numDirections;

        foreach (var k in Enumerable.Range(0, numDirections))
        {
            var y = k * off - 1 + (off / 2);
            var r = Mathf.Sqrt(1 - y * y);
            var phi = k * inc;
            var x = (float)(Mathf.Cos(phi) * r);
            var z = (float)(Mathf.Sin(phi) * r);
            pts[k] = new Vector3(x, y, z);
        }

        return pts;
    }
    private Vector3[] GetCircleDirections(int radius, float x, float y) //2D Looks like it works!!
    {
        var points = new Vector3[12];
        int i = 0;
        for (int angle = 0 ; angle < 360; angle = angle + 30)
        {
            var newX = Mathf.Cos(angle + Mathf.Acos(x / radius));
            var newZ = Mathf.Sin(angle + Mathf.Asin(y / radius));
            points[i] = new Vector3(newX, 0, newZ);

            Debug.Log(points[i]);
            i++;
        }
        return points;
    }


}
