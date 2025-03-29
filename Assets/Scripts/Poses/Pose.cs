using UnityEngine;

public abstract class Pose : MonoBehaviour
{
    public abstract void init();
    public abstract bool IsCorrct(Vector3 _headPos, Vector3 _leftHandPos, Vector3 _rightHandPos, Quaternion _headRot, Quaternion _leftHandRot, Quaternion _rightHandRot);

}
