using System.Collections;
using UnityEngine;

public class HouseAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSource;
    [SerializeField] private AlarmTrigger _trigger;
    [SerializeField] private float _speed = 0.5f;

    private Coroutine _volumeCoroutine;

    private void Awake()
    {
        _alarmSource.volume = 0f;
        _alarmSource.Stop();
    }

    private void OnEnable()
    {
        if (_trigger != null)
        {
            _trigger.ThiefEnter += OnThiefEntered;
            _trigger.ThiefExit += OnThiefExited;
        }
    }

    private void OnDisable()
    {
        if (_trigger != null)
        {
            _trigger.ThiefEnter -= OnThiefEntered;
            _trigger.ThiefExit -= OnThiefExited;
        }
    }

    private void OnThiefEntered()
    {
        if(_alarmSource.isPlaying == false)
        {
            _alarmSource.volume = 0.01f;
            _alarmSource.Play();
        }

        StartVolumecChange(1f);
    }

    private void OnThiefExited()
    {
        StartVolumecChange(0f);
    }

   private void StartVolumecChange(float targetVolume)
    {
        if (_volumeCoroutine != null)
        {
            StopCoroutine(_volumeCoroutine);
        }

        _volumeCoroutine = StartCoroutine(ChangeVolumeRoutine(targetVolume));
    }

    private IEnumerator ChangeVolumeRoutine(float targetVolume)
    {
        while (!Mathf.Approximately(_alarmSource.volume, targetVolume))
        {
            _alarmSource.volume = Mathf.MoveTowards(_alarmSource.volume, targetVolume, _speed * Time.deltaTime);

            yield return null;
        }

        _alarmSource.volume = targetVolume;

        if(Mathf.Approximately(_alarmSource.volume, 0f) && _alarmSource.isPlaying)
        {
            _alarmSource.Stop();
        }

        _volumeCoroutine = null;
    }
}
