using UnityEngine;

public class Pointer : MonoBehaviour
{
	[HideInInspector]
	public Camera virtualCamera;

	private Ray ray;

	private RaycastHit[] hitInfo;

	[SerializeField]
	private LayerMask groundLayer;

	private void Start()
	{
		Cursor.visible = false;
		hitInfo = new RaycastHit[1];
	}

	private void Update()
	{
		if (!float.IsInfinity(Input.mousePosition.x) && !float.IsInfinity(Input.mousePosition.y))
		{
			ray = virtualCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.RaycastNonAlloc(ray, hitInfo, 100f, groundLayer) > 0)
			{
				base.transform.position = hitInfo[0].point;
			}
		}		
	}
}
