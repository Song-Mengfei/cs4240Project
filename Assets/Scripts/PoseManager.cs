using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoseManager : SingletonPatternPersistent<PoseManager>
{    
    public Pose currPose;
    public List<Pose> poses;

    public bool CheckPose(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        currPose.init();
        bool isCorrect = currPose.IsCorrct(_headPos, _leftHandPos, _rightHandPos, _headRot, _leftHandRot, _rightHandRot);
        //if (isCorrect)
        //{
        //    DebugManager.Log("good job!");
        //}
        //else
        //{
        //    DebugManager.Log("bad boy!");
        //}
        return isCorrect;
    }

    public void SetCurrPose(int index)
    {
        currPose = poses[index];
    }
}
