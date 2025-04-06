using UnityEngine;

public class BurmesePose : Pose
{
    private float headOrigin;
    private float upperArmLength;

    public override void init()
    {
        headOrigin = PlayerPrefs.GetFloat("HeadHeight", Mathf.Infinity);
        if (headOrigin == Mathf.Infinity)
        {
            DebugManager.Log("Please set the head height at first!");
        }

        upperArmLength = PlayerPrefs.GetFloat("LeftUpperArmLength", 0.4f);
    }
    public override bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        return IsHeadStraight(_headRot) &&
               AreHandsSymmetry(_leftHandPos, _rightHandPos) &&
               AreHandsOnKnees(_headPos, _rightHandPos) &&
               IsSitting(_headPos);
    }

    bool IsHeadStraight(Quaternion headRot)
    {
        Vector3 euler = headRot.eulerAngles;
        euler.x = (euler.x > 180) ? euler.x - 360 : euler.x;
        euler.z = (euler.z > 180) ? euler.z - 360 : euler.z;

        bool isHeadStraight = Mathf.Abs(euler.x) <= 10f && Mathf.Abs(euler.z) <= 10f;

        if (isHeadStraight)
        {
            Debug.Log("HeadStraight");
        }
        else
        {
            Debug.Log("notHeadStraight");
        }
        return isHeadStraight;
    }

    bool AreHandsSymmetry(Vector3 leftPos, Vector3 rightPos)
    {
        float distance = Mathf.Abs(leftPos.y - rightPos.y);

        bool areHandsSymmetry = distance < 0.1f;

        if (areHandsSymmetry)
        {
            Debug.Log("HandsSymmetry");
        }
        else
        {
            Debug.Log("notHandsSymmetry");
        }

        return areHandsSymmetry;
    }

    bool AreHandsOnKnees(Vector3 headPos, Vector3 rightPos)
    {
        float distance = headPos.y - rightPos.y;

        bool areHandsSymmetry = distance < upperArmLength + 0.05f || distance > upperArmLength - 0.05f;

        if (areHandsSymmetry)
        {
            Debug.Log("HandsOnKnees");
        }
        else
        {
            Debug.Log("notHandsOnKnees");
        }

        return areHandsSymmetry;
    }

    bool IsSitting(Vector3 headPos)
    {
        bool isSitting = headPos.y < headOrigin - 0.8f;

        if (isSitting)
        {
            Debug.Log("Sitting");
        }
        else
        {
            Debug.Log("headOrigin: " + headOrigin + "now: " + headPos.y);
        }

        return isSitting;
    }
}
