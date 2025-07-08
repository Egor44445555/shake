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

		if (FindObjectOfType<GameManager>().isMobile)
		{
			GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private void Update()
	{
		if (!float.IsInfinity(Input.mousePosition.x) && !float.IsInfinity(Input.mousePosition.y))
		{
			if (FindObjectOfType<GameManager>().isMobile)
			{
				Vector3 targetPosition = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * 100f;
				base.transform.position = Vector3.Lerp(transform.position, targetPosition, 5f * Time.deltaTime);
			}
			else
			{
				ray = virtualCamera.ScreenPointToRay(Input.mousePosition);

				if (Physics.RaycastNonAlloc(ray, hitInfo, 100f, groundLayer) > 0)
				{
					base.transform.position = hitInfo[0].point;
				}
			}
		}		
	}
}
