using UnityEngine;

public class Pranamasana : Pose
{
    private float headOrigin;


    public override void init()
    {
        headOrigin = PlayerPrefs.GetFloat("HeadHeight", Mathf.Infinity);
        if (headOrigin == Mathf.Infinity)
        {
            DebugManager.Log("Please set the head height at first!");
        }
    }

    public override bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        return IsHeadStraight(_headRot) &&
               AreHandsTogether(_leftHandPos, _rightHandPos) &&
               AreArmsStraight(_leftHandRot, _rightHandRot) &&
               IsStanding(_headPos);
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

    bool AreHandsTogether(Vector3 leftPos, Vector3 rightPos)
    {
        float distance = Vector3.Distance(leftPos, rightPos);

        bool areHandsTogether = distance < 0.1f;

        if (areHandsTogether)
        {
            Debug.Log("HandsTogether");
        }
        else
        {
            Debug.Log("notHandsTogether");
        }

        return areHandsTogether;
    }

    bool AreArmsStraight(Quaternion leftRot, Quaternion rightRot)
    {
        float rotationDifference = Quaternion.Angle(leftRot, rightRot);

        Vector3 rightEuler = rightRot.eulerAngles;

        bool leftIsFlat = (Mathf.Abs(rightEuler.x) > 350f || Mathf.Abs(rightEuler.x) < 10f) && (Mathf.Abs(rightEuler.z) < 10f || Mathf.Abs(rightEuler.z) > 350f);
        bool areArmsStraight = leftIsFlat && rotationDifference > 170f && rotationDifference < 190f;

        if (areArmsStraight)
        {
            Debug.Log("ArmsStraight");
        }
        else
        {
            Debug.Log("notArmsStraight");
        }
        return areArmsStraight;
    }

    bool IsStanding(Vector3 headPos)
    {
        bool isStanding = Mathf.Abs(headOrigin - headPos.y -1.0f) < 0.1f;

        if (isStanding)
        {
            Debug.Log("Standing");
        }
        else
        {
            Debug.Log("headOrigin: " + headOrigin + "now: " + headPos.y);
        }

        return isStanding;
    }
}
