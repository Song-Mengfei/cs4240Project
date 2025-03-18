using UnityEngine;
using UnityEngine.InputSystem;

public class ArmGeneration : MonoBehaviour
{
    public GameObject cylinderPrefab;
    public InputActionReference headPositionAction;

    private string armName;
    private float forearmLength;
    private float shoulderLength;
    private GameObject forearm;
    private GameObject upperArm;
    private GameObject elbow;
    private GameObject shoulder;
    private Vector3 elbowPos;

    private void Start()
    {
        armName = gameObject.name;
        string forearmName = armName + "ForearmLength";
        string upperArmName = armName + "UpperArmLength";

        forearmLength = PlayerPrefs.GetFloat(forearmName, 0.4f);
        shoulderLength = PlayerPrefs.GetFloat("ShoulderLength", 0.4f);
    }

    void Update()
    {
        GenerateForearm();
        GenerateUpperArm();
    }

    void GenerateForearm()
    {
        Vector3 controllerPos = transform.position;
        Quaternion controllerRot = transform.rotation;

        Vector3 downDirection = controllerRot * Vector3.down;
        Vector3 backDirection = controllerRot * -Vector3.forward;

        Vector3 forearmPos = controllerPos + backDirection * (forearmLength / 2);

        Quaternion forearmRot = Quaternion.LookRotation(downDirection, controllerRot * Vector3.forward);

        if (forearm == null)
        {
            forearm = Instantiate(cylinderPrefab, forearmPos, forearmRot);
        }

        forearm.transform.position = forearmPos;
        forearm.transform.rotation = forearmRot;
        forearm.transform.localScale = new Vector3(0.05f, forearmLength / 2, 0.05f);
        elbowPos = forearmPos + forearmRot * Vector3.down * (forearmLength / 2);
    }

    void GenerateUpperArm()
    {
        Vector3 headPos = headPositionAction.action.ReadValue<Vector3>();
        Vector3 offset = new Vector3(0.0f, 1.95f, -0.05f);
        if (armName == "Left")
        {
            offset -= new Vector3(shoulderLength / 2, 0.0f, 0.0f);
        }
        else
        {
            offset += new Vector3(shoulderLength / 2, 0.0f, 0.0f);
        }

        Vector3 shoulderPos = headPos + offset;
        Vector3 direction = elbowPos - shoulderPos;
        Vector3 upperArmPos = (shoulderPos + elbowPos) / 2;

        Quaternion upperArmRot = Quaternion.FromToRotation(Vector3.up, direction);

        float upperArmLength = direction.magnitude;

        if (upperArm == null)
        {
            upperArm = Instantiate(cylinderPrefab, upperArmPos, upperArmRot);
        }

        if (shoulder == null)
        {
            shoulder = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        if (elbow == null)
        {
            elbow = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        }

        upperArm.transform.position = upperArmPos;
        upperArm.transform.rotation = upperArmRot;
        upperArm.transform.localScale = new Vector3(0.05f, upperArmLength / 2, 0.05f);

        shoulder.transform.position = shoulderPos;
        shoulder.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        elbow.transform.position = elbowPos;
        elbow.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
    }


}
