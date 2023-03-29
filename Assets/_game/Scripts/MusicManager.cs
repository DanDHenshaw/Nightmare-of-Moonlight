using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _audioClips;
    private AudioSource _audioSource;

    public static MusicManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);

        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = _audioClips[Random.Range(0, _audioClips.Length)];
            _audioSource.Play();
        }
    }
}
