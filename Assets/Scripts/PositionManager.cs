using UnityEngine;
using UnityEngine.InputSystem;

public class PositionManager : MonoBehaviour
{
    public InputActionReference recordAction;

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject cylinderPrefab;

    private Vector3 headPos, leftControllerPos, rightControllerPos;
    private Quaternion headRot, leftControllerRot, rightControllerRot;

    private ArmGeneration armGeneration;
    private PoseManager poseManager;

    void Start()
    {
        recordAction.action.Enable();
        recordAction.action.performed += OnRecordPormed;

        armGeneration = GetComponent<ArmGeneration>();
        poseManager = PoseManager.Instance;
    }

    void Update()
    {
        headPos = head.transform.position;
        leftControllerPos = leftHand.transform.position;
        rightControllerPos = rightHand.transform.position;

        headRot = head.transform.rotation;
        leftControllerRot = leftHand.transform.rotation;
        rightControllerRot = rightHand.transform.rotation;

        armGeneration.GenerateArms(headPos, leftControllerPos, rightControllerPos, headRot, leftControllerRot, rightControllerRot);
        poseManager.CheckPose(headPos, leftControllerPos, rightControllerPos, headRot, leftControllerRot, rightControllerRot);
    }

    void OnRecordPormed(InputAction.CallbackContext ctx)
    {
        DebugManager.Log("headPos: " + headPos);
        DebugManager.Log("headRot: " + headRot);
        DebugManager.Log("leftPos: " + leftControllerPos);
        DebugManager.Log("leftRot: " + leftControllerRot);
        DebugManager.Log("rightPos: " + rightControllerPos);
        DebugManager.Log("rightRot: " + rightControllerRot);
    }
}
