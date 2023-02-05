using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    private float currentDuration;

    private void Start()
    {
        originalPos = transform.localPosition;

        TriggerShake();
    }

    private void Update()
    {
        
        if (currentDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            currentDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            currentDuration = 0f;
            transform.localPosition = originalPos;
        }
    }

    public void TriggerShake()
    {
        currentDuration = shakeDuration;
    }
}