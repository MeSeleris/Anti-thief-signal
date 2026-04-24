using UnityEngine;

public class HouseAlarm : MonoBehaviour
{
    [SerializeField] private AudioSource alarmSource;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _targetVolume = 0f;

    private void Start()
    {
        alarmSource.volume = 0f;
        alarmSource.Stop();
    }

    private void Update()
    {
        alarmSource.volume = Mathf.MoveTowards(alarmSource.volume, _targetVolume, _speed * Time.deltaTime);

        if (alarmSource.volume <= 0 && _targetVolume == 0 && alarmSource.isPlaying)
        {
            alarmSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetVolume = 1f;

            if(alarmSource.isPlaying == false)
            {
                alarmSource.Play();
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetVolume = 0f;
        }
    }
}
