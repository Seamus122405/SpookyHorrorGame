using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{

    public float maxInteractDistance = 10f;
    public LayerMask layerMask;

    [SerializeField] Transform playerCameraRoot;
   
    [SerializeField] RawImage cursor;
    [SerializeField] Texture2D cursorOutline;
    [SerializeField] Texture2D cursorFill;

    private void Update()
    {
        RaycastHit hit;

        //doing physics raycasting in update because of waspressedthisframe. hopefully this doesn't cause issues?
        if (Physics.Raycast(playerCameraRoot.transform.position, playerCameraRoot.transform.forward, out hit, maxInteractDistance, layerMask))
        {
            cursor.texture = cursorFill;
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                hit.transform.GetComponent<IInteractable>().Interact();
            }
            Debug.DrawRay(playerCameraRoot.transform.position, playerCameraRoot.transform.forward * hit.distance, Color.red);
        }
        else
        {
            cursor.texture = cursorOutline;
            Debug.DrawRay(playerCameraRoot.transform.position, playerCameraRoot.transform.forward * maxInteractDistance, Color.white);
        }
    }
}
