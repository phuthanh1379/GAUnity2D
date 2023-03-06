using UnityEngine;

public class FxController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private bool _isPlay;

    private void Start()
    {
        _isPlay = false;
    }

    private void PlayParticle()
    {
        _isPlay = !_isPlay;
        if (_isPlay)
            particles.Play();
        else
            particles.Pause();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            PlayParticle();
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            particles.Stop();
            _isPlay = false;
        }
    }
}
