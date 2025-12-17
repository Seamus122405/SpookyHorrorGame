using DG.Tweening;
using UnityEngine;

public class DoorScript : MonoBehaviour, IInteractable
{
    [Tooltip("Check players position for opening against x or z axis")]
    public bool checkXAngle = true;

    [Header("Z Only")]
    [Tooltip("For z, where both directions will be the same")]
    public float openAngle = 90f;

    [Header("X Only")]
    [Tooltip("For x, where pos might be 180 and neg might be 0")]
    public float openAnglePos = 180f;
    [Tooltip("For x, where pos might be 180 and neg might be 0")]
    public float openAngleNeg = 0f;

    [Header("Both")]
    public float closedAngle = 0f;
    public float timeToMove = 0.5f;
    public Transform player;
    public bool doorOpen = false;

    public void Interact()
    {
        if(doorOpen)
        {
            transform.parent.transform.DORotate(new Vector3(transform.parent.transform.rotation.x, closedAngle, transform.parent.transform.rotation.z), timeToMove, RotateMode.Fast);
            doorOpen = false;
        }
        else
        {
            if(checkXAngle) //check door x against player x to determine opening direction
            {
                if(player.position.x > transform.parent.position.x) //player x > door x. rotate positive
                {
                    print("a");
                    transform.parent.transform.DORotate(new Vector3(transform.parent.transform.rotation.x, openAnglePos, transform.parent.transform.rotation.z), timeToMove, RotateMode.Fast);
                }
                else // player x <= door x. rotate negative
                {
                    transform.parent.transform.DORotate(new Vector3(transform.parent.transform.rotation.x, openAngleNeg, transform.parent.transform.rotation.z), timeToMove, RotateMode.Fast);

                }
            }
            else //check door z against player z to determine opening direction
            {
                if (player.position.z > transform.parent.position.z) //player z > door z. rotate positive
                {
                    transform.parent.transform.DORotate(new Vector3(transform.parent.transform.rotation.x, openAngle, transform.parent.transform.rotation.z), timeToMove, RotateMode.Fast);
                }
                else // player x <= door z. rotate negative
                {
                    transform.parent.transform.DORotate(new Vector3(transform.parent.transform.rotation.x, -openAngle, transform.parent.transform.rotation.z), timeToMove, RotateMode.Fast);

                }
            }

            doorOpen = true;
        }
    }
}
