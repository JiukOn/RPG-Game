using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public Interactable focus;

	public LayerMask movementMask;

	Camera cam;

	void Start () {
		cam = Camera.main;
	}
	
	void Update () {

		if(EventSystem.current.IsPointerOverGameObject()){
			return;
		}

		if(focus != null){
		FaceTarget();
		if(Input.GetButtonDown("Horizontal")){
			RemoveFocus();
		}
		if(Input.GetButtonDown("Vertical")){
			RemoveFocus();
		}
		}

		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100))
			{
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				if (interactable != null)
				{
					SetFocus(interactable);
				}
			}
		}
	}

	void SetFocus (Interactable newFocus)
	{
		if (newFocus != focus)
		{
			if (focus != null)
				focus.OnDefocused();

			focus = newFocus;
		}
		
		newFocus.OnFocused(transform);
	}

	void RemoveFocus ()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;
	}

	void FaceTarget ()
	{
		Vector3 direction = (focus.transform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

	
}