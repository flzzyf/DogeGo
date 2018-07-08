using UnityEngine;
using Vuforia;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class HitTestResultEvent : UnityEvent<HitTestResult> { }

public class VuforiaManager : MonoBehaviour
{
    public HitTestResultEvent clickEvent;

    public bool canTrigger = false;

    public void OnInteractiveHitTest(HitTestResult result)
    {
        if (canTrigger)
            clickEvent.Invoke(result);
    }

    [HideInInspector]
    public bool hit = false;

    public void Hit()
    {
        hit = true;
    }

    private void LateUpdate()
    {
        hit = false;
    }
}
