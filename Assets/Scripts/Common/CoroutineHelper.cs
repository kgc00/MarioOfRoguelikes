using UnityEngine;
using System.Collections;

public class CoroutineHelper : MonoBehaviour
{
    public static CoroutineHelper Instance;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Stop(Coroutine routine)
    {
        StopCoroutine(routine);
    }

    public Coroutine Countdown(float inital, float precision, System.Action onComplete)
    {
        return StartCoroutine(countdown(inital, precision, onComplete));
    }

    private IEnumerator countdown(float initial, float precision, System.Action onComplete)
    {
        float countdown = initial;
        while (countdown > 0)
        {
            yield return new WaitForSeconds(precision);
            countdown -= precision;
        }
        onComplete();
    }
}