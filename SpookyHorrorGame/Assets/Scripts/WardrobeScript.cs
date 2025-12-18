using Cinemachine;
using StarterAssets;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WardrobeScript : MonoBehaviour, IInteractable
{
    public FirstPersonController controller;
    public CinemachineVirtualCamera playerFollowCamera;
    public CinemachineVirtualCamera centerCam;
    public CinemachineVirtualCamera leftCam;
    public CinemachineVirtualCamera rightCam;

    public GameObject door1Closed;
    public GameObject door2Closed;
    public GameObject door1Open;
    public GameObject door2Open;

    public Transform flashlight;
    public Transform playerCapsule;
    public Transform exitPos;

    bool playerInWardrobe = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        centerCam.enabled = false;
        leftCam.enabled = false;
        rightCam.enabled = false;

        door1Closed.SetActive(true);
        door2Closed.SetActive(true);
        door1Open.SetActive(false);
        door2Open.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInWardrobe)
        {
            if (Keyboard.current.wKey.wasPressedThisFrame) //exit wardrobe
            {
                centerCam.enabled = false;
                leftCam.enabled = false;
                rightCam.enabled = false;
                playerFollowCamera.enabled = true;
                controller.disableMovement = false;
                playerInWardrobe = false;

                StartCoroutine(CloseDoor());

                flashlight.localPosition = new Vector3(flashlight.localPosition.x, 0, flashlight.localPosition.z);
            }

            if (Keyboard.current.aKey.wasPressedThisFrame && !leftCam.enabled) //shift left
            {
                if (!centerCam.enabled) //right cma -> center cam
                {
                    rightCam.enabled = false;
                    centerCam.enabled = true;
                }
                else if (!leftCam.enabled) //center cam -> left cam
                {
                    centerCam.enabled = false;
                    leftCam.enabled = true;
                }
            }
            else if (Keyboard.current.dKey.wasPressedThisFrame && !rightCam.enabled) //shift right
            {
                if (!centerCam.enabled) // left cam -> center cam
                {
                    leftCam.enabled = false;
                    centerCam.enabled = true;
                }
                else if (!rightCam.enabled) //center cam -> right cam
                {
                    centerCam.enabled = false;
                    rightCam.enabled = true;
                }
            }
        }
       
    }

    public void Interact()
    {
        centerCam.enabled = true;
        playerFollowCamera.enabled = false;
        controller.disableMovement = true;
        playerInWardrobe = true;

        door1Closed.SetActive(false);
        door2Closed.SetActive(false);
        door1Open.SetActive(true);
        door2Open.SetActive(true);

        StartCoroutine(SetPlayerExitPos());

        //offset flashlight y by -0.1 or else the light flickers when rotating camera in wardrobe. i don't know why that happens or why this fixes it, but it does
        flashlight.localPosition = new Vector3(flashlight.localPosition.x, -0.1f, flashlight.localPosition.z);
    }

    IEnumerator SetPlayerExitPos()
    {
        yield return new WaitForSeconds(0.5f);
        playerCapsule.SetPositionAndRotation(exitPos.position, exitPos.rotation);
    }

    IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(0.5f);
        door1Closed.SetActive(true);
        door2Closed.SetActive(true);
        door1Open.SetActive(false);
        door2Open.SetActive(false);
    }

}
