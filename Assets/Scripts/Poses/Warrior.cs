using System.Runtime.CompilerServices;
using UnityEngine;

public class Warrior : Pose
{
    private float headOrigin;
    private float armSpan;
    private float thighLength;

    private string poseStat;
    private string poseHint;

    public override void init()
    {
        headOrigin = PlayerPrefs.GetFloat("HeadHeight", Mathf.Infinity);
        if (headOrigin == Mathf.Infinity)
        {
            DebugManager.Log("Please set the head height at first!");
        }

        armSpan = PlayerPrefs.GetFloat("ArmSpan", 1.6f);
        thighLength = PlayerPrefs.GetFloat("ThighLength", 0.4f);
    }

    public override bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos,
                                  Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        if (IsDebugCheatOn)
        {
            poseStat = "Great job! Your posture is perfect.";
            poseHint = "Try to hold the pose a little longer.";
            DebugManager.Log("DEBUG");
            return true;
        }
        DebugManager.Log("Checking BurmesePose...");

        return IsHeadStraight(_headRot) &&
               AreArmsStraight(_leftHandPos, _rightHandPos) &&
               AreArmsSameLevel(_leftHandPos, _rightHandPos) &&
               IsLeftHandForward(_leftHandPos, _headPos, _headRot) &&
               IsLunge(_headPos);
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
        euler.x = NormalizeAngle(euler.x);
        euler.z = NormalizeAngle(euler.z);

        bool isHeadStraight = Mathf.Abs(euler.x) <= 10f && Mathf.Abs(euler.z) <= 20f;

        if (!isHeadStraight)
        {
            poseStat = "The head is not facing forward.";
            poseHint = "Try to face forward and look straight ahead.";
        }

        DebugManager.Log(isHeadStraight ? "HeadStraight" : "NotHeadStraight");

        return isHeadStraight;
    }

    bool AreArmsStraight(Vector3 leftPos, Vector3 rightPos)
    {
        float distance = Vector3.Distance(leftPos, rightPos);

        bool areArmsStraight = Mathf.Abs(distance - armSpan) < 1f;

        if (!areArmsStraight)
        {
            poseStat = "It looks like your arms aren't fully extended.";
            poseHint = "Try to fully extend your arms.";
            Debug.Log("distance " + distance + "armSpan " + armSpan);
            Debug.Log("ArmsStraight");
        }

        DebugManager.Log(areArmsStraight ? "ArmsStraight" : "notArmsStraight");

        return areArmsStraight;
    }

    bool AreArmsSameLevel(Vector3 leftPos, Vector3 rightPos)
    {
        bool areArmsSameLevel = Mathf.Abs(leftPos.y - rightPos.y) < 0.3f;
        Debug.Log("left: " + leftPos.y + " right: " + rightPos.y);

        if (!areArmsSameLevel)
        {
            poseStat = "It looks like your arms aren't level.";
            poseHint = "Try to align both arms so they're on the same horizontal line.";
            Debug.Log("ArmsStraight");
        }

        DebugManager.Log(areArmsSameLevel ? "ArmsSameLevel" : "notArmsSameLevel");
        return areArmsSameLevel;
    }

    bool IsLeftHandForward(Vector3 leftPos, Vector3 headPos, Quaternion headRot) {
        Vector3 headForward = headRot * Vector3.forward;

        Vector3 toLeftController = (leftPos - headPos).normalized;

        float dot = Vector3.Dot(headForward.normalized, toLeftController);

        bool isLeftHandForward = dot > 0.9f;

        if (!isLeftHandForward)
        {
            poseStat = "It seems like you haven't extended your left hand forward.";
            poseHint = "Try to extend your left hand forward.";
            
            Debug.Log("LeftHandForward");
        }

        return isLeftHandForward;
    }

    bool IsLunge(Vector3 headPos)
    {
        bool isLunge = Mathf.Abs(headOrigin - headPos.y - thighLength) < 0.2f;

        if (isLunge)
        {
            poseStat = "Great job! Your posture is perfect.";
            poseHint = "Try to hold the pose a little longer.";
            DebugManager.Log("Sitting");
        }
        else
        {
            poseStat = "It looks like you're not in a lunge position.";
            poseHint = "Try getting into a lunge position.";
        }

        DebugManager.Log(isLunge ? "Lunge" : "notLunge");

        return isLunge;
    }


}
