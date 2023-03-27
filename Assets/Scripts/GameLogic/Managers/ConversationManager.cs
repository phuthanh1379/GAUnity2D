using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;

public class ConversationManager : MonoBehaviour
{
    public static ConversationManager Instance;
    public event Action<ConversationData> ConversationStart;
    public event Action ConversationEnd;

    [SerializeField] private AudioSource audioSource;

    [Header("Cinematic")]
    [SerializeField] private Image screenBorderTop;
    [SerializeField] private Image screenBorderBottom;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    private const float BaseOrthoSize = 4.5f;
    private const float CineOrthoSize = 2.5f;
    private Sequence _cinematicSequence;
    private bool _isCinematic; // hiệu ứng cinematic có đang bật không?

    [Header("Conversation UI")]
    [SerializeField] private ConversationUI conversationUI;
    private IEnumerator _displaySentenceCoroutine;
    

    private ConversationData _currentData;
    private int _currentSentenceIndex;
    private string _currentSentence;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            NextSentence();

        // Nếu ko bật hiệu ứng
        if (!_isCinematic)
        {
            // Cho camera về lại ortho-size như cũ
            if (mainCamera.m_Lens.OrthographicSize < BaseOrthoSize)
            {
                mainCamera.m_Lens.OrthographicSize += Time.deltaTime * 3f;
            }
        }
        // Bật hiệu ứng
        else
        {
            // Cho camera zoom lên
            if (mainCamera.m_Lens.OrthographicSize > CineOrthoSize)
            {
                mainCamera.m_Lens.OrthographicSize -= Time.deltaTime * 3f;
            }
        }
    }

    private void Init()
    {
        Instance = this;
        conversationUI.gameObject.SetActive(false);
        _currentSentenceIndex = -1;
        _currentSentence = string.Empty;
        _displaySentenceCoroutine = DisplaySentence();

        // Cinematic zoom
        _isCinematic = false;
        _cinematicSequence = DOTween.Sequence();
        var tweenBottom = screenBorderBottom.rectTransform.DOAnchorPos(Vector2.zero, 0.5f);
        var tweenTop = screenBorderTop.rectTransform.DOAnchorPos(Vector2.zero, 0.5f);
        _cinematicSequence
            .Append(tweenBottom)
            .Join(tweenTop)
            .SetAutoKill(false);
    }

    /// <summary>
    /// Bật/Tắt hiệu ứng cinematic
    /// </summary>
    /// <param name="enable"></param>
    private void DoCinematic(bool enable)
    {
        _isCinematic = enable;
        if (enable)
        {
            _cinematicSequence.Restart();
        }
        else
        {
            _cinematicSequence.Rewind();
        }
    }

    /// <summary>
    /// Bắt đầu hội thoại
    /// </summary>
    /// <param name="data"></param>
    public void StartConversation(ConversationData data)
    {
        // Kiểm tra data có đủ câu không
        if (data.data == null) return;
        if (data.data.Count <= 0) return;

        _currentData = data;

        ConversationStart?.Invoke(data);

        // Bắt đầu hiệu ứng
        DoCinematic(true);

        conversationUI.gameObject.SetActive(true);
        // Hiện câu đầu tiên
        StartCoroutine(DisplaySentence(data.data[0]));
        _currentSentenceIndex = 0;
    }

    private IEnumerator DisplaySentence(DialogSentence content = null, float timeLapse = 0.05f)
    {
        if (content == null) yield return null;
        _currentSentence = string.Empty;
        //conversationUI.ClearContent();

        // Hiển thị avatar
        conversationUI.SetAvatar(content.character.sprite, content.position);

        // Hiển thị hiệu ứng chạy từng chữ
        var sentence = content.sentence;
        for (int i = 0; i < sentence.Length; i++)
        {
            _currentSentence = string.Concat(_currentSentence, sentence[i]);
            conversationUI.SetContent(_currentSentence);
            audioSource.Play();
            // Đợi 1 khoảng thời gian = timeLapse rồi chạy tiếp vòng lặp
            yield return new WaitForSeconds(timeLapse);
        }
    }

    // Hiển thị câu tiếp theo
    private void NextSentence()
    {
        StopCoroutine(_displaySentenceCoroutine);

        if (_currentData == null || _currentSentenceIndex < 0) return;

        _currentSentenceIndex++;
        if (_currentSentenceIndex >= _currentData.data.Count)
        {
            EndConversation();
            return;
        }

        StartCoroutine(DisplaySentence(_currentData.data[_currentSentenceIndex]));
    }

    /// <summary>
    /// Kết thúc hội thoại
    /// </summary>
    private void EndConversation()
    {
        ConversationEnd?.Invoke();

        // Clear
        _currentData = null;
        _currentSentenceIndex = -1;
        _currentSentence = string.Empty;
        conversationUI.ClearContent();
        conversationUI.gameObject.SetActive(false);

        // Tắt hiệu ứng 
        DoCinematic(false);
    }
}
