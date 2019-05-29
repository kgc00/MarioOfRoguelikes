using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private Text heroEnergyText;

    private void Awake()
    {
        heroEnergyText.text = "";
    }

    private void Start()
    {
        NotificationCenter.AddListener<StatChange>(OnStatChange);
    }

    private void OnStatChange(StatChange notification)
    {

        if (notification.Actor.AI.IsHero())
        {
            if (notification.Type == typeof(Energy))
            {
                heroEnergyText.text = notification.NewValue.ToString();
            }
        }

    }

    public void OnDestroy()
    {
        NotificationCenter.RemoveListener<StatChange>(OnStatChange);
    }
}
