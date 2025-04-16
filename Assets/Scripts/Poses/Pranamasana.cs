using UnityEngine;
using UnityEngine.InputSystem.XR;

public class Pranamasana : Pose
{
    private float headOrigin;

    private string poseStat;
    private string poseHint;

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
        if (IsDebugCheatOn)
        {
            poseStat = "Great job! Your posture is perfect.";
            poseHint = "Try to hold the pose a little longer.";
            DebugManager.Log("DEBUG");
            return true;
        }

        return IsHeadStraight(_headRot) &&
               AreHandsTogether(_leftHandPos, _rightHandPos) &&
               AreArmsStraight(_leftHandRot, _rightHandRot) &&
               IsStanding(_headPos);
    }

    public override string GetStat()
    {
        return poseStat;
    }

    public override string GetHint()
    {
        return poseHint;
    }

    bool IsHeadStraight(Quaternion headRot)
    {
        Vector3 euler = headRot.eulerAngles;
        euler.x = (euler.x > 180) ? euler.x - 360 : euler.x;
        euler.z = (euler.z > 180) ? euler.z - 360 : euler.z;

        bool isHeadStraight = Mathf.Abs(euler.x) <= 10f && Mathf.Abs(euler.z) <= 10f;

        if (!isHeadStraight)
        {
            poseStat = "The head is not facing forward.";
            poseHint = "Try to face forward and look straight ahead.";
            Debug.Log("HeadStraight");
        }

        return isHeadStraight;
    }

    bool AreHandsTogether(Vector3 leftPos, Vector3 rightPos)
    {
        float distance = Vector3.Distance(leftPos, rightPos);

        bool areHandsTogether = distance < 0.1f;

        if (!areHandsTogether)
        {
            poseStat = "Your hands are not placed together.";
            poseHint = "Try to bring your hands together.";
            Debug.Log("HandsTogether");
        }

        return areHandsTogether;
    }

    bool AreArmsStraight(Quaternion leftRot, Quaternion rightRot)
    {
        Vector3 leftEuler = leftRot.eulerAngles;
        Vector3 rightEuler = rightRot.eulerAngles;

        bool leftIsFlat = (Mathf.Abs(leftEuler.x) > 350f || Mathf.Abs(leftEuler.x) < 10f) && (Mathf.Abs(leftEuler.z) < 10f || Mathf.Abs(leftEuler.z) > 350f);
        bool rightIsFlat = (Mathf.Abs(rightEuler.x) > 350f || Mathf.Abs(rightEuler.x) < 10f) && (Mathf.Abs(rightEuler.z) < 10f || Mathf.Abs(rightEuler.z) > 350f);

        Debug.Log("leftIsFlat: " + leftIsFlat + ", rightIsFlat" + rightIsFlat);
        Debug.Log("leftEuler: " + leftEuler + ", rightEuler" + rightEuler);
        if (leftIsFlat && rightIsFlat)
        {
            //Debug.Log("leftIsFlat: " + leftIsFlat + ", rightIsFlat" + rightIsFlat);
            //Debug.Log("leftEuler: " + leftEuler + ", rightEuler" + rightEuler);
            poseStat = "It looks like your arms aren't level.";
            poseHint = "Try to align both arms so they're on the same horizontal line.";
            Debug.Log("ArmsStraight");
            return false;
        }

        return true;
    }

    bool IsStanding(Vector3 headPos)
    {
        bool isStanding = Mathf.Abs(headOrigin - headPos.y) < 0.1f;

        if (isStanding)
        {
            poseStat = "Great job! Your posture is perfect.";
            poseHint = "Try to hold the pose a little longer.";
            DebugManager.Log("Sitting");
        }
        else
        {
            poseStat = "It seems like you haven't stand up.";
            poseHint = "Find an open space and stand up.";
            DebugManager.Log($"NotSitting (headOrigin: {headOrigin}, current: {headPos.y})");
        }

        return isStanding;
    }
}
