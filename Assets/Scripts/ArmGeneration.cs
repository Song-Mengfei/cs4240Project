using UnityEngine;
using UnityEngine.InputSystem;

public class ArmGeneration : MonoBehaviour
{
    public GameObject forearm;
    public GameObject upperarm;

    private float leftForearmLength;
    private float rightForearmLength;
    private float shoulderLength;

    private GameObject leftForearm;
    private GameObject rightForearm;
    private GameObject leftUpperArm;
    private GameObject rightUpperArm;
    private GameObject leftElbow;
    private GameObject rightElbow;
    private GameObject leftShoulder;
    private GameObject rightShoulder;

    private Vector3 leftElbowPos;
    private Vector3 rightElbowPos;
    private Vector3 leftShoulderPos;
    private Vector3 rightShoulderPos;

    private void Start()
    {
        leftForearmLength = PlayerPrefs.GetFloat("LeftForearmLength", 0.4f);
        rightForearmLength = PlayerPrefs.GetFloat("RightForearmLength", 0.4f);
        shoulderLength = PlayerPrefs.GetFloat("ShoulderLength", 0.4f);
    }

    public void GenerateArms(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot)
    {
        GetShoulderPos(_headPos, _headRot);
        GenerateLeftForearm(_leftHandPos, _leftHandRot);
        GenerateRightForearm(_rightHandPos, _rightHandRot);
        GenerateLeftUpperArm();
        GenerateRightUpperArm();
    }

    void GetShoulderPos(Vector3 headPos, Quaternion headRot)
    {
        Vector3 baseOffset = new Vector3(0.0f, -0.2f, 0.0f);
        float headYaw = headRot.eulerAngles.y * Mathf.Deg2Rad;

        Vector3 rightVector = new Vector3(Mathf.Cos(headYaw), 0, -Mathf.Sin(headYaw));
        Vector3 shoulderOffset = (shoulderLength / 2) * rightVector;

        rightShoulderPos = headPos + baseOffset + shoulderOffset;
        leftShoulderPos = headPos + baseOffset + (shoulderOffset * -1);

    }

    void GenerateLeftForearm(Vector3 leftHandPos, Quaternion leftHandRot)
    {
        Vector3 downDirection = leftHandRot * Vector3.down;
        Vector3 backDirection = leftHandRot * -Vector3.forward;

        Vector3 forearmPos = leftHandPos + backDirection * (leftForearmLength / 2);

        Quaternion forearmRot = Quaternion.LookRotation(downDirection, leftHandRot * Vector3.forward);

        if (leftForearm == null)
        {
            leftForearm = Instantiate(forearm, forearmPos, forearmRot);
        }

        leftForearm.transform.position = forearmPos;
        leftForearm.transform.rotation = forearmRot;
        leftForearm.transform.localScale = new Vector3(0.05f, leftForearmLength / 2, 0.05f);
        leftElbowPos = forearmPos + forearmRot * Vector3.down * (leftForearmLength / 2);
    }

    void GenerateRightForearm(Vector3 rightHandPos, Quaternion rightHandRot)
    {
        Vector3 downDirection = rightHandRot * Vector3.down;
        Vector3 backDirection = rightHandRot * -Vector3.forward;

        Vector3 forearmPos = rightHandPos + backDirection * (rightForearmLength / 2);

        Quaternion forearmRot = Quaternion.LookRotation(downDirection, rightHandRot * Vector3.forward);

        if (rightForearm == null)
        {
            rightForearm = Instantiate(forearm, forearmPos, forearmRot);
        }

        rightForearm.transform.position = forearmPos;
        rightForearm.transform.rotation = forearmRot;
        rightForearm.transform.localScale = new Vector3(0.05f, rightForearmLength / 2, 0.05f);
        rightElbowPos = forearmPos + forearmRot * Vector3.down * (rightForearmLength / 2);
    }

    void GenerateLeftUpperArm()
    {
        Vector3 direction = leftElbowPos - leftShoulderPos;
        Vector3 upperArmPos = (leftShoulderPos + leftElbowPos) / 2;

        Quaternion upperArmRot = Quaternion.FromToRotation(Vector3.up, direction);

        float upperArmLength = direction.magnitude;

        if (leftUpperArm == null)
        {
            leftUpperArm = Instantiate(upperarm, upperArmPos, upperArmRot);
        }

        if (leftShoulder == null)
        {
            leftShoulder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        if (leftElbow == null)
        {
            leftElbow = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        leftUpperArm.transform.position = upperArmPos;
        leftUpperArm.transform.rotation = upperArmRot;
        leftUpperArm.transform.localScale = new Vector3(0.05f, upperArmLength / 2, 0.05f);

        leftShoulder.transform.position = leftShoulderPos;
        leftShoulder.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        leftElbow.transform.position = leftElbowPos;
        leftElbow.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }

    void GenerateRightUpperArm()
    {
        Vector3 direction = rightElbowPos - rightShoulderPos;
        Vector3 upperArmPos = (rightShoulderPos + rightElbowPos) / 2;

        Quaternion upperArmRot = Quaternion.FromToRotation(Vector3.up, direction);

        float upperArmLength = direction.magnitude;

        if (rightUpperArm == null)
        {
            rightUpperArm = Instantiate(upperarm, upperArmPos, upperArmRot);
        }

        if (rightShoulder == null)
        {
            rightShoulder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        if (rightElbow == null)
        {
            rightElbow = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        rightUpperArm.transform.position = upperArmPos;
        rightUpperArm.transform.rotation = upperArmRot;
        rightUpperArm.transform.localScale = new Vector3(0.05f, upperArmLength / 2, 0.05f);

        rightShoulder.transform.position = leftShoulderPos;
        rightShoulder.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        rightElbow.transform.position = rightElbowPos;
        rightElbow.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }
}
