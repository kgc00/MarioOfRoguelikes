using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour {
    private Text heroEnergyText;

    private void Awake () {
        heroEnergyText = FindObjectOfType<Canvas> ().GetComponentInChildren<Text> ();
        heroEnergyText.text = "";
    }

    private void Start () {
        NotificationCenter.AddListener<StatChange> (OnStatChange);
    }

    private void OnStatChange (StatChange notification) {

        if (notification.Actor.AI.IsHero ()) {
            if (notification.Type == typeof (Energy)) {
                heroEnergyText.text = notification.NewValue.ToString () + " Moves Left";
            }
        }

    }

    public void OnDestroy () {
        NotificationCenter.RemoveListener<StatChange> (OnStatChange);
    }
}