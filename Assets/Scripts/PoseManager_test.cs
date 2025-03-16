using System.Collections.Generic;
using UnityEngine;

public class PoseManager_test : SingletonPatternPersistent<PoseManager>
{
    public Pose currPose;
    public List<Pose> poses;
    public Vector3 offset;

    private float LeftForearmLength, RightForearmLength, LeftUpperArmLength, RightUpperArmLength, ShoulderLength;

    public void Start() 
    {
        LeftForearmLength = PlayerPrefs.GetFloat("LeftForearmLength", 0.0f);
        RightForearmLength = PlayerPrefs.GetFloat("RightForearmLength", 0.0f);
        LeftUpperArmLength = PlayerPrefs.GetFloat("LeftUpperArmLength", 0.0f);
        RightUpperArmLength = PlayerPrefs.GetFloat("RightUpperArmLength", 0.0f);
        ShoulderLength = PlayerPrefs.GetFloat("ShoulderLength", 0.0f);

        BuildPose();
    }

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
        BuildPose();
    }

    void BuildPose()
    {
        Vector3 headPos = currPose.headPos;
        Vector3 LeftHandPos = currPose.leftHandPos;
        Vector3 RightHandPos = currPose.rightHandPos;

        GameObject head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        head.transform.position = offset;
        head.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        GameObject torso = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        torso.transform.position = offset - new Vector3(0, 0.7f, 0);
        torso.transform.localScale = new Vector3(ShoulderLength, 0.5f, ShoulderLength);

        // todo Build the arms
    }
}
