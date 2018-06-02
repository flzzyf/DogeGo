using UnityEngine;
using System;
using Vuforia;

public class DeployStageOnce : MonoBehaviour
{
    public GameObject AnchorStage;
    private PositionalDeviceTracker _deviceTracker;
    private GameObject _previousAnchor;

	void Start()
	{
        //AnchorStage.SetActive(false);
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
        // same anchor code from before
        //GameManager.instance.SetText("2", result.Position.ToString());

        Anchor anchor = _deviceTracker.CreatePlaneAnchor(Guid.NewGuid().ToString(), result);

        // but now the anchor doesn't create a GameObject, so we will have to with the HitTestResult position and rotation values

        //if (anchor != null)
        //{

        AnchorStage.transform.position = result.Position;
        AnchorStage.transform.rotation = result.Rotation;

            AnchorStage.SetActive(true);

        //}

        // Clean up

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
