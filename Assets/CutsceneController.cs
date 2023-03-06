using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;
    [SerializeField] private SpriteRenderer sky;
    [SerializeField] private Camera mainCamera;

    public Color _color;
    private Sequence seq;

    private void Awake()
    {
        _color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        seq = DOTween.Sequence();
        seq.Append(sky.DOBlendableColor(_color, 0.5f))
            .OnComplete(() =>
            {
                _color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            })
            ;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            director.Play();
    }

    public void OnCameraLive()
    {
        Debug.Log("IsPlaying");
        seq.Restart();
    }
}
