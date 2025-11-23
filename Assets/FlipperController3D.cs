using UnityEngine;

public class Flipper : MonoBehaviour
{
    public HingeJoint hinge;

    void Update()
    {
        JointSpring s = hinge.spring;

        if (Input.GetMouseButton(0))    // clic souris
            s.targetPosition = 20;      // flipper monte
        else
            s.targetPosition = -20;     // flipper redescend

        hinge.spring = s;
    }
}
