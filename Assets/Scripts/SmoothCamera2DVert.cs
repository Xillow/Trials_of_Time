using UnityEngine;
using System.Collections;

public class SmoothCamera2DVert : MonoBehaviour {

	//originally just for smooth camera behavior, this class now controls both smooth camera movement and bounding
	
	//smooth camera variables
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	public Transform target;



	//bounding variables
	public BoxCollider2D boundBox;
    [SerializeField]
    private Vector3 minBounds; //minimum bounds as defined by the level bounding box
    [SerializeField]
    private Vector3 maxBounds; //maximum bounds as defined by the level bounding box

	private Camera theCamera;
	
	public float halfHeight = 4; //distance from the center to the top / bottom edge of the camera
	
	public float halfWidth = 9; //distance from the center to the right / left edge of the camera

    void Start()
	{
		target = GameObject.Find("Player").transform;
		boundBox = GameObject.Find("Bounds").GetComponent<BoxCollider2D>();

		minBounds = boundBox.bounds.min;
		maxBounds = boundBox.bounds.max;

		theCamera = GetComponent<Camera>();
		halfHeight = theCamera.orthographicSize;
		halfWidth = halfHeight * (Screen.width / Screen.height); //width is decided by aspect ratio- different for every screen
	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		
		if (target)
		{
			//camera smoothing
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            //changed delta from target.position + new Vector3(0, 2.5f,0) 2.5f to target.position + new Vector3(0,1.12f,0) for camera size 3
            Vector3 delta = target.position + new Vector3(0,1.12f,0) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			delta = new Vector3(0f, delta.y, delta.z);
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			//camera bounding
			//float clampedX = Mathf.Clamp(transform.position.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
			float clampedY = Mathf.Clamp(transform.position.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);
			transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
		}

	}
}

