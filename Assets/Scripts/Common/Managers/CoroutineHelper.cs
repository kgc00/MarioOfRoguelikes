using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour {
    public static CoroutineHelper Instance;
    public static List<Coroutine> allCoroutines = new List<Coroutine> ();

    private void Awake () {
        if (Instance != this && Instance != null) {
            Destroy (gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        }
    }

    ~CoroutineHelper () {
        foreach (var routine in allCoroutines) {
            if (routine != null)
                Stop (routine);
        }
    }

    public void Stop (Coroutine routine) {
        StopCoroutine (routine);
    }

    public Coroutine Countdown (float inital, float precision, System.Action onComplete) {
        var routine = StartCoroutine (countdown (inital, precision, onComplete));
        allCoroutines.Add (routine);
        return routine;
    }

    private IEnumerator countdown (float initial, float precision, System.Action onComplete) {
        float countdown = initial;
        while (countdown > 0) {
            // countdown -= time.deltatime
            // yield return null

            yield return new WaitForSeconds (precision);
            countdown -= precision;
        }
        onComplete ();
    }
}