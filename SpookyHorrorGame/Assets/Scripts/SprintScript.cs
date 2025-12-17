using StarterAssets;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SprintScript : MonoBehaviour
{
    FirstPersonController controller;
    float sprintSpeed;

    public float energyPercent = 100f;
    public float energyDrain = 5f;

    [SerializeField] Image sprintFill;
    public Color sprintFillGreen;
    public Color sprintFillRed;

    Coroutine refillCoroutine;
    bool isRefilling = false;
    bool canSprint = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<FirstPersonController>();
        sprintSpeed = controller.SprintSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.shiftKey.isPressed && canSprint)
        {
            if (energyPercent > 0)
            {
                if(!refillCoroutine.IsUnityNull())
                {
                    StopCoroutine(refillCoroutine);
                    isRefilling = false;
                }

                energyPercent -= energyDrain * Time.deltaTime;
                sprintFill.fillAmount = (energyPercent / 100);
            }
            else
            {
                if(!isRefilling)
                {
                    refillCoroutine = StartCoroutine(RefillSprint(true));
                }
            }
            
        }
        else
        {
            if(!(isRefilling) && (energyPercent < 100f))
            {
                bool refillFromZero = !(energyPercent > 0);
                refillCoroutine = StartCoroutine(RefillSprint(refillFromZero));
            }
        }
    }

    IEnumerator RefillSprint(bool refillfromZero)
    {
        isRefilling = true;
        if (refillfromZero) 
        { 
            controller.SprintSpeed = controller.MoveSpeed; 
            canSprint = false; 
            sprintFill.color = sprintFillRed;
        }
        yield return new WaitForSeconds(0.5f);

        while (energyPercent < 100f)
        {
            energyPercent += energyDrain * Time.deltaTime;
            sprintFill.fillAmount = (energyPercent / 100);
            yield return null;
        }

        energyPercent = 100f;
        sprintFill.fillAmount = 1f;
        if (refillfromZero) 
        { 
            controller.SprintSpeed = sprintSpeed; 
            canSprint = true;
            sprintFill.color = sprintFillGreen;
        }
        isRefilling = false;
    }
}
