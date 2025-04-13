using UnityEngine;

public class BurmesePose : Pose
{
    private float headOrigin;
    private float forearmLength;
    private float shoulderLength;

    private string poseStat;
    private string poseHint;

    public override void init()
    {
        headOrigin = PlayerPrefs.GetFloat("HeadHeight", Mathf.Infinity);
        if (headOrigin == Mathf.Infinity)
        {
            DebugManager.Log("Please set the head height at first!");
        }

        forearmLength = PlayerPrefs.GetFloat("LeftForearmLength", 0.4f);
        shoulderLength = PlayerPrefs.GetFloat("ShoulderLength", 0.4f);
    }

    public override bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos,
                                  Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        DebugManager.Log("Checking BurmesePose...");

        return IsHeadStraight(_headRot) &&
               AreHandsSymmetry(_leftHandPos, _rightHandPos) &&
               AreHandsOnKnees(_headPos, _leftHandPos, _rightHandPos) &&
               ArePalmsFacingUp(_leftHandRot, _rightHandRot) &&
               IsSitting(_headPos);
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

        bool isHeadStraight = Mathf.Abs(euler.x) <= 10f && Mathf.Abs(euler.z) <= 10f;

        if (!isHeadStraight) {
            poseStat = "The head is not facing forward.";
            poseHint = "Try to face forward and look straight ahead.";
        }

        DebugManager.Log(isHeadStraight ? "HeadStraight" : "NotHeadStraight");

        return isHeadStraight;
    }

    bool AreHandsSymmetry(Vector3 leftPos, Vector3 rightPos)
    {
        Vector3 leftFlat = new Vector3(leftPos.x, 0, leftPos.z);
        Vector3 rightFlat = new Vector3(rightPos.x, 0, rightPos.z);

        float horizontalDistance = Vector3.Distance(leftFlat, rightFlat) - 0.15f;
        float tolerance = 0.2f;

        bool areHandsSymmetric = Mathf.Abs(horizontalDistance - shoulderLength) < tolerance;

        if (!areHandsSymmetric)
        {
            poseStat = "The hand is not placed on the knee.";
            poseHint = "Try to place your hand on your knee.";
        }

        DebugManager.Log(areHandsSymmetric ? "HandsSymmetry" : "NotHandsSymmetry");

        return areHandsSymmetric;
    }

    bool AreHandsOnKnees(Vector3 headPos, Vector3 leftPos, Vector3 rightPos)
    {
        float leftDist = Mathf.Abs(headPos.y - leftPos.y - 0.2f);
        float rightDist = Mathf.Abs(headPos.y - rightPos.y - 0.2f);

        bool areHandsOnKnees = Mathf.Abs(leftDist - forearmLength) < 0.2f &&
                               Mathf.Abs(rightDist - forearmLength) < 0.2f;

        if (!areHandsOnKnees)
        {
            poseStat = "The hand is not placed on the knee.";
            poseHint = "Try to place your hand on your knee.";
        }

        DebugManager.Log(areHandsOnKnees ? "HandsOnKnees" : "NotHandsOnKnees");

        return areHandsOnKnees;
    }

    bool ArePalmsFacingUp(Quaternion leftRot, Quaternion rightRot)
    {
        Vector3 leftUp = leftRot * Vector3.up;
        Vector3 rightUp = rightRot * Vector3.up;

        float leftAngle = Vector3.Angle(leftUp, Vector3.up);
        float rightAngle = Vector3.Angle(rightUp, Vector3.up);

        bool palmsUp = leftAngle < 90f && rightAngle < 90f;

        if (!palmsUp)
        {
            poseStat = "Your palms are not facing upward.";
            poseHint = "Place your palms facing upward on your knees.";
        }

        DebugManager.Log(palmsUp ? "PalmsFacingUp" : $"PalmsNotUp (L:{leftAngle:F1}, R:{rightAngle:F1})");

        return palmsUp;
    }

    bool IsSitting(Vector3 headPos)
    {
        bool isSitting = Mathf.Abs(headOrigin - headPos.y - 1.0f) < 0.2f;

        if (isSitting)
        {
            poseStat = "Great job! Your posture is perfect.";
            poseHint = "Try to hold the pose a little longer.";
            DebugManager.Log("Sitting");
        }
        else
        {
            poseStat = "It seems like you haven't sat down.";
            poseHint = "Find a comfortable spot and take a seat.";
            DebugManager.Log($"NotSitting (headOrigin: {headOrigin}, current: {headPos.y})");
        }

        return isSitting;
    }

}
