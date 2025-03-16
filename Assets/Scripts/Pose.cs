using UnityEngine;

public class Pose : MonoBehaviour
{
    public Vector3 headPos;
    public float headRadius;

    public Vector3 leftHandPos;
    public float leftHandRadius;

    public Vector3 rightHandPos;
    public float rightHandRadius;

    public bool IsClose(Pose otherPose)
    {
        return CheckWithinRadius(headPos, otherPose.headPos, headRadius) &&
               CheckWithinRadius(leftHandPos, otherPose.leftHandPos, leftHandRadius) &&
               CheckWithinRadius(rightHandPos, otherPose.rightHandPos, headRadius);
    }
    public bool IsClose(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos)
    {
        return CheckWithinRadius(headPos, _headPos, headRadius) &&
               CheckWithinRadius(leftHandPos, _leftHandPos, leftHandRadius) &&
               CheckWithinRadius(rightHandPos, _rightHandPos, headRadius);
    }

    public bool CheckWithinRadius(Vector3 pos1, Vector3 pos2, float radius)
    {
        return (pos1 - pos2).magnitude < headRadius;
    }

    Pose(Vector3 headPos, float headRadius, Vector3 leftHandPos, float leftHandRadius, Vector3 rightHandPos, float rightHandRadius)
    {
        this.headPos = headPos;
        this.headRadius = headRadius;
        this.leftHandPos = leftHandPos;
        this.leftHandRadius = leftHandRadius;
        this.rightHandPos = rightHandPos;
        this.rightHandRadius = rightHandRadius;
    }

    public override string ToString()
    {
        return "headpos: " + headPos + ", headRadius: " + headRadius +
            "\n leftHandPos: " + leftHandPos + ", leftHandRadius: " + leftHandRadius +
            "\n rightHandPos: " + rightHandPos + ", rightHandRadius: " + rightHandRadius;
    }
}
