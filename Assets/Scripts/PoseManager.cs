using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PoseManager : SingletonPatternPersistent<PoseManager>
{    
    public Pose currPose;
    public List<Pose> poses;

    public bool CheckPose(Pose playerPos)
    {
        return currPose.IsClose(playerPos);
    }
    public bool CheckPose(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos)
    {
        return currPose.IsClose(_headPos, _leftHandPos, _rightHandPos);
    }

    public void SetCurrPose(int index)
    {
        currPose = poses[index];
    }
}
