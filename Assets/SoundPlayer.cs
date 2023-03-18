using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    AudioSource _source;
    public AudioClip[] _clipsCrash;
    public AudioClip[] _clipsJump;
    PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        _playerController= GetComponentInParent<PlayerController>();
        _source= GetComponent<AudioSource>();   
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            AudioClip clip = _clipsJump[Random.Range(0, _clipsJump.Length)];
            _source.pitch = Random.Range(0.95f, 1.05f); 
            if (!_playerController.playerGrounded) _source.PlayOneShot(clip);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioClip clip = _clipsCrash[Random.Range(0, _clipsCrash.Length)];
            _source.pitch = Random.Range(0.95f, 1.05f);
            _source.PlayOneShot(clip);
        }
    }
}
