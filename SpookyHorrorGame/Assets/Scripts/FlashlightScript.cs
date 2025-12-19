using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FlashlightScript : MonoBehaviour
{
    Light flashlightLight;

    bool flashlightEnabled = false;
    bool flashlightCanBeEnabled = true;

    public float batteryPercent = 100f;
    public float drainSpeed = 5f;

    bool debugNoDrain = false;

    [SerializeField] Slider batterySlider;
    [SerializeField] Image batterySliderImage;

    public Color batterySliderGreen;
    public Color batterySliderRed;

    Coroutine refillCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flashlightLight = GetComponent<Light>();
        
        DisableFlashlight();
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame && flashlightCanBeEnabled)
        {
            if(flashlightEnabled)
            {
                DisableFlashlight();
            }
            else
            {
                EnableFlashlight();
            }
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            debugNoDrain = !debugNoDrain;
        }

        if (flashlightEnabled && !debugNoDrain)
        {
            batteryPercent -= drainSpeed * Time.deltaTime;
            batterySlider.value = batteryPercent;

            if(batteryPercent <= 0f)
            {
                DisableFlashlight();
            }
        }
    }

    public void EnableFlashlight()
    {
        if (batteryPercent > 0)
        {
            flashlightEnabled = true;
            flashlightLight.enabled = true;
            StopCoroutine(refillCoroutine);
        }

    }

    public void DisableFlashlight()
    {
        flashlightEnabled = false;
        flashlightLight.enabled = false;

        bool refillFromZero = !(batteryPercent > 0);
        refillCoroutine = StartCoroutine(RefillFlashlight(refillFromZero));
    }

    IEnumerator RefillFlashlight(bool refillfromZero)
    {
        if (refillfromZero) 
        { 
            flashlightCanBeEnabled = false; 
            batterySliderImage.color = batterySliderRed; 
        }
     
       
        yield return new WaitForSeconds(0.5f);

        while(batteryPercent < 100f)
        {
            batteryPercent += drainSpeed * Time.deltaTime;
            batterySlider.value = batteryPercent;
            yield return null;
        }

        batteryPercent = 100f;
        batterySlider.value = batteryPercent;
        if (refillfromZero) 
        { 
            flashlightCanBeEnabled = true;
            batterySliderImage.color = batterySliderGreen;
        }
    }
}
