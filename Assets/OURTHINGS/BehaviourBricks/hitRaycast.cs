using UnityEngine;

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

    [OutParam("Rotate")]
    [Help("First value to be compared")]
    public bool rotate;
    public override void OnStart()
    {
        lineLenght = 6;
        line = gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        LineRaycast();
        return TaskStatus.COMPLETED;
    }

    void LineRaycast()
    {
        if (line.enabled)
        {
            line.SetPosition(0, gameObject.transform.position);
            line.SetPosition(1, gameObject.transform.position + gameObject.transform.forward * lineLenght);
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hitInfo, lineLenght))
            {
                if (hitInfo.collider.gameObject.tag == "Collision") rotate = true;
            }
            else rotate = false;
        }
    }

 
}
