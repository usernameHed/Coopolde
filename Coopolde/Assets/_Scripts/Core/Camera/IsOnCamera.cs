using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Check object is on camera
/// <summary>
public class IsOnCamera : MonoBehaviour
{
    [SerializeField]
	private FrequencyTimer updateTimer = new FrequencyTimer(1.0f);

	[SerializeField]
	private float xMargin;

	[SerializeField]
	private float yMargin;

    
	public bool isOnScreen = false;
    [ReadOnly]
    public bool IsTooMuchInside = false;


    public float interneBorder = 0.2f;

    [ShowInInspector, ReadOnly]
    private Camera cam;

    private Vector3 bounds;

    #region Initialization
    private void Start()
	{
        TryToGetCam();
    }
	#endregion

    #region Core

    private void TryToGetCam()
    {
        cam = GameManager.Instance.CameraMain;
    }

	/// <summary>
	/// Check object is on screen
	/// <summary>
	private void CheckOnCamera()
	{
		if (!cam)
		{
            TryToGetCam();
		}

		Vector3 bottomCorner = cam.WorldToViewportPoint(gameObject.transform.position - bounds);
		Vector3 topCorner = cam.WorldToViewportPoint(gameObject.transform.position + bounds);

        
        isOnScreen = (topCorner.x >= -xMargin && bottomCorner.x <= 1 + xMargin && topCorner.y >= -yMargin && bottomCorner.y <= 1 + yMargin);
        IsTooMuchInside = (topCorner.x >= -interneBorder && bottomCorner.x <= 1 + interneBorder && topCorner.y >= -interneBorder && bottomCorner.y <= 1 + interneBorder);

    }

    // Unity functions
    private void Update()
    {
		if (updateTimer.Ready())
        {
			CheckOnCamera();
        }
    }
	#endregion
}
