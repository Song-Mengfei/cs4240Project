using System.Runtime.CompilerServices;
using UnityEngine;

public class Warrior : Pose
{
    private float headOrigin;
    private float leftForearmLength;
    private float rightForearmLength;
    private float shoulderLength;

    public override void init()
    {
        headOrigin = PlayerPrefs.GetFloat("HeadHeight", Mathf.Infinity);
        if (headOrigin == Mathf.Infinity)
        {
            DebugManager.Log("Please set the head height at first!");
        }

        leftForearmLength = PlayerPrefs.GetFloat("LeftForearmLength", 0.4f);
        rightForearmLength = PlayerPrefs.GetFloat("RightForearmLength", 0.4f);
        shoulderLength = PlayerPrefs.GetFloat("ShoulderLength", 0.4f);
    }

    public override bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos,
                                  Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        DebugManager.Log("Checking BurmesePose...");

        return IsHeadStraight(_headRot) &&
               AreArmsStraight(_leftHandPos, _rightHandPos) &&
               AreArmsSameLevel(_leftHandPos, _rightHandPos) &&
               IsLeftHandForward(_leftHandPos, _headPos, _headRot) &&
               IsLunge(_headPos);
    }

    bool IsHeadStraight(Quaternion headRot)
    {
        Vector3 euler = headRot.eulerAngles;
        euler.x = NormalizeAngle(euler.x);
        euler.z = NormalizeAngle(euler.z);

        bool isHeadStraight = Mathf.Abs(euler.x) <= 10f && Mathf.Abs(euler.z) <= 10f;

        DebugManager.Log(isHeadStraight ? "HeadStraight" : "NotHeadStraight");

        return isHeadStraight;
    }

    bool AreArmsStraight(Vector3 leftPos, Vector3 rightPos)
    {
        float distance = Vector3.Distance(leftPos, rightPos);

        bool areArmsStraight = Mathf.Abs(distance - 2 * leftForearmLength - 2 * rightForearmLength - shoulderLength + 0.2f) < 0.25f;

        DebugManager.Log(areArmsStraight ? "ArmsStraight" : "notArmsStraight");

        return areArmsStraight;
    }

    bool AreArmsSameLevel(Vector3 leftPos, Vector3 rightPos)
    {
        bool areArmsSameLevel = Mathf.Abs(leftPos.y - rightPos.y) < 0.15f;

        DebugManager.Log(areArmsSameLevel ? "ArmsSameLevel" : "notArmsSameLevel");
        return areArmsSameLevel;
    }

    bool IsLeftHandForward(Vector3 leftPos, Vector3 headPos, Quaternion headRot) {
        Vector3 headForward = headRot * Vector3.forward;

        Vector3 toLeftController = (leftPos - headPos).normalized;

        float dot = Vector3.Dot(headForward.normalized, toLeftController);

        bool isLeftHandForward = dot > 0.9f;

        if (isLeftHandForward)
        {
            Debug.Log("LeftHandForward");
        }
        else
        {
            Debug.Log("notLeftHandForward");
        }

        return isLeftHandForward;
    }

    bool IsLunge(Vector3 headPos)
    {
        bool isLunge = headOrigin - headPos.y > 0.25f;

        DebugManager.Log(isLunge ? "Lunge" : "notLunge");

        return isLunge;
    }


}
