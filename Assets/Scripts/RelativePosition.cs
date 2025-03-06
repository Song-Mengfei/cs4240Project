using UnityEngine;
using UnityEngine.InputSystem;

public class RelativePosition : MonoBehaviour
{
    public InputActionReference headPositionAction;
    public InputActionReference leftControllerPositionAction;
    public InputActionReference rightControllerPositionAction;

    public Vector3 targetLeftRelative = new Vector3(0f, 0f, 0f);
    public Vector3 targetRightRelative = new Vector3(0f, 0f, 0f);

    void OnEnable()
    {
        headPositionAction.action.Enable();
        leftControllerPositionAction.action.Enable();
        rightControllerPositionAction.action.Enable();
    }

    void OnDisable()
    {
        headPositionAction.action.Disable();
        leftControllerPositionAction.action.Disable();
        rightControllerPositionAction.action.Disable();
    }

    void Update()
    {
        Vector3 headPosition = headPositionAction.action.ReadValue<Vector3>();
        Vector3 leftControllerPosition = leftControllerPositionAction.action.ReadValue<Vector3>();
        Vector3 rightControllerPosition = rightControllerPositionAction.action.ReadValue<Vector3>();

        Vector3 leftRelativePosition = leftControllerPosition - headPosition;
        Vector3 rightRelativePosition = rightControllerPosition - headPosition;

        Debug.Log($"Left hand relative to the headset: {leftRelativePosition}");
        Debug.Log($"Right hand relative to the headset: {rightRelativePosition}");

        float leftDist = (leftRelativePosition - targetLeftRelative).magnitude;
        float rightDist = (rightRelativePosition - targetRightRelative).magnitude;

        //Debug.Log($"Left hand distance: {leftDist}");
        //Debug.Log($"Right hand distance: {rightDist}");

        float tolerance = 0.1f;

        bool leftInPlace = leftDist < tolerance;
        bool rightInPlace = rightDist < tolerance;

        Debug.Log($"LeftInplace: {leftInPlace}");
        Debug.Log($"RightInplace: {rightInPlace}");

        if (leftInPlace && rightInPlace)
        {
            Debug.Log("Hands in place!");
        }
    }
}
