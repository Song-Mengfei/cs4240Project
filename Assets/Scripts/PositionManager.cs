using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class PositionManager : SingletonPattern<PositionManager>
{
    public InputActionReference recordAction;

    public GameObject head;
    public GameObject leftHand;
    public GameObject rightHand;
    public GameObject cylinderPrefab;

    private bool isCorrect;
    private Vector3 headPos, leftControllerPos, rightControllerPos;
    private Quaternion headRot, leftControllerRot, rightControllerRot;

    private ArmGeneration armGeneration;
    private PoseManager poseManager;
    //private ProgressBarFiller progressBarFiller;

    void Start()
    {
        // Init
        head = Camera.main.gameObject;
        GameObject goRef = head.GetComponentInParent<XROrigin>().gameObject;
        leftHand = goRef.GetComponentInChildren<LeftHandRef>().gameObject;
        rightHand = goRef.GetComponentInChildren<RightHandRef>().gameObject;

        recordAction.action.Enable();
        recordAction.action.performed += OnRecordPormed;

        armGeneration = GetComponent<ArmGeneration>();
        poseManager = PoseManager.Instance;

        //progressBarFiller = GetComponent<ProgressBarFiller>();
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
        isCorrect = poseManager.CheckPose(headPos, leftControllerPos, rightControllerPos, headRot, leftControllerRot, rightControllerRot);



        //progressBarFiller.FillAmount(isCorrect);
    }

    public bool IsPoseCorrect()
    {
        return isCorrect;
    }

    

    void OnRecordPormed(InputAction.CallbackContext ctx)
    {
        DebugManager.Log("headPos: " + headPos);
        //DebugManager.Log("headRot: " + headRot);
        //DebugManager.Log("leftPos: " + leftControllerPos);
        //DebugManager.Log("leftRot: " + leftControllerRot);
        //DebugManager.Log("rightPos: " + rightControllerPos);
        //DebugManager.Log("rightRot: " + rightControllerRot);
    }
}
