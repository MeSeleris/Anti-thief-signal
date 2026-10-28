using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action ThiefEnter;
    public event Action ThiefExit;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[Detector] Вошел объект: {other.name}");
        if (other.TryGetComponent(out Thief _))
        {
            Debug.Log("[Detector] На объекте найден Rogue! Отправляю событие RogueEntered.");
            ThiefEnter?.Invoke();
        }
        else
        {
            Debug.LogWarning($"[Detector] На объекте {other.name} нет компонента Rogue!");
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Thief _))
        {
            ThiefExit?.Invoke();
        }
    }
}
