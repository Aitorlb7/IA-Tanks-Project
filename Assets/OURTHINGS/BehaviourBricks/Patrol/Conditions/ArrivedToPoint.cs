
using Pada1.BBCore;
using Pada1.BBCore.Framework;


[Condition("MyConditions/Has arrived to point?")]
[Help("Checks whether Cop is near the Treasure.")]
public class ArrivedToPoint : ConditionBase
{
    [InParam("DistanceFromPoint")]
    public float DistancePoint;

    [InParam("MinDistance")]
    public float MinDistance;

    public override bool Check()
    {   
        return DistancePoint < MinDistance;
    }
}