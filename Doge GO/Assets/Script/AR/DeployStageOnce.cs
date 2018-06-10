using UnityEngine;
using System;
using Vuforia;

public class DeployStageOnce : MonoBehaviour
{
    public GameObject AnchorStage;
    public GameObject cube;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

	void Start()
	{
        AnchorStage.SetActive(false);
	}

	private void Awake()
	{
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
	}

	private void OnDestroy()
	{
        VuforiaARController.Instance.UnregisterVuforiaStartedCallback(OnVuforiaStarted);
	}


    void OnVuforiaStarted()
    {
        _deviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();

    }

    public void OnInteractiveHitTest(HitTestResult result)
    {
        GameManager.instance.SetText("pos", result.Position.ToString());
        GameManager.instance.SetText("rot", result.Rotation.ToString());

        var anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

        // but now the anchor doesn't create a GameObject, so we will have to with the HitTestResult position and rotation values
        GameObject anchorParent = Instantiate(cube, result.Position, result.Rotation);
        GameObject anchorGO = Instantiate(AnchorStage, result.Position, result.Rotation);

        AnchorStage.transform.parent = anchorParent.transform;
        AnchorStage.transform.localScale = Vector3.zero;
        AnchorStage.transform.rotation = Quaternion.identity;

        AnchorStage.SetActive(true);

        /*
        if (_previousAnchor != null)
        {

            Destroy(_previousAnchor);

        }

        // Save it
        _previousAnchor = AnchorStage;
        */

    }
}
