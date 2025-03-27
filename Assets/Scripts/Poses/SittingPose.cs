using UnityEngine;

public class SittingPose : Pose
{
    public override bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        return IsHeadStraight(_headRot) &&
               AreHandsTogether(_leftHandPos, _rightHandPos) &&
               AreArmsStraight(_leftHandRot, _rightHandRot) &&
               IsSitting(_headPos);
    }

    bool IsHeadStraight(Quaternion headRot)
    {
        Vector3 euler = headRot.eulerAngles;
        euler.x = (euler.x > 180) ? euler.x - 360 : euler.x;
        euler.z = (euler.z > 180) ? euler.z - 360 : euler.z;

        return Mathf.Abs(euler.x) <= 10f && Mathf.Abs(euler.z) <= 10f;
    }

    bool AreHandsTogether(Vector3 leftPos, Vector3 rightPos)
    {
        float distance = Vector3.Distance(leftPos, rightPos);

        return distance < 0.1f;
    }

    bool AreArmsStraight(Quaternion leftRot, Quaternion rightRot)
    {
        float rotationDifference = Quaternion.Angle(leftRot, rightRot);

        Vector3 rightEuler = rightRot.eulerAngles;
        
        bool leftIsFlat = (Mathf.Abs(rightEuler.x) > 350f || Mathf.Abs(rightEuler.x) < 10f) && (Mathf.Abs(rightEuler.z) < 10f || Mathf.Abs(rightEuler.z) > 350f);

        return leftIsFlat && rotationDifference > 170f && rotationDifference < 190f;
    }

    bool IsSitting(Vector3 headPos)
    {
        return headPos.y < 1.7f;
    }
}
