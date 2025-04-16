using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoseManager : SingletonPatternPersistent<PoseManager>
{    
    public Pose currPose;
    public bool isPoseCorrect;

    public bool CheckPose(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        currPose.init();
        isPoseCorrect = currPose.IsCorrct(_headPos, _leftHandPos, _rightHandPos, _headRot, _leftHandRot, _rightHandRot);
        return isPoseCorrect;
    }

    public bool IsPoseCorrect()
    {
        return isPoseCorrect;
    }

    public void SetCurrPose(Pose pose)
    {
        currPose = pose;
    }

    public string GetPoseStat()
    {
        return currPose.GetStat();
    }

    public string GetPoseHint()
    {
        return currPose.GetHint();
    }
}
