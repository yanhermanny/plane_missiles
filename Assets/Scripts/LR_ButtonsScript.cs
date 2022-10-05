using UnityEngine;
using UnityEngine.EventSystems;

public class LR_ButtonsScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public static bool lButtonPressed;
	public static bool rButtonPressed;

	public void OnPointerDown(PointerEventData eventData) {
		if (this.gameObject.CompareTag("LeftButton")) {
			lButtonPressed = true;
		}
		else if (this.gameObject.CompareTag("RightButton")) {
			rButtonPressed = true;
		}
	}

	public void OnPointerUp(PointerEventData eventData) {
		if (this.gameObject.CompareTag("LeftButton")) {
			lButtonPressed = false;
		}
		else if (this.gameObject.CompareTag("RightButton")) {
			rButtonPressed = false;
		}
	}
}