using Cinemachine;
using DG.Tweening;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class CrouchScript : MonoBehaviour
{

    private FirstPersonController controller;
    float defaultWalkSpeed;
    float defaultSprintSpeed;
    float crouchingWalkSpeed = 2f;
    float crouchingSprintSpeed = 4f;


    public Transform playerCameraRoot;

    bool isMoveDown = false;
    bool isCrouched = false;
    
    bool isMoveUp = false;
    bool isUncrouched = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<FirstPersonController>();
        defaultWalkSpeed = controller.MoveSpeed;
        defaultSprintSpeed = controller.SprintSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.ctrlKey.IsPressed())
        {
            isUncrouched = false;
            isMoveUp = false;

            controller.MoveSpeed = crouchingWalkSpeed;
            controller.SprintSpeed = crouchingSprintSpeed;

            if (!isMoveDown && !isCrouched)
            {
                isMoveDown = true;
                playerCameraRoot.DOLocalMoveY(1f, 0.5f)
                    .OnComplete(() => { isMoveDown = false; isCrouched = true; });
            }
        }
        else
        {
            isCrouched = false;
            isMoveDown = false;

            controller.MoveSpeed = defaultWalkSpeed;
            controller.SprintSpeed = defaultSprintSpeed;

            if (!isMoveUp && !isUncrouched)
            {
                isMoveUp = true;
                playerCameraRoot.DOLocalMoveY(1.9f, 0.5f)
                    .OnComplete(() => { isMoveUp = false; isUncrouched = true; });
            }
        }
    }
}
