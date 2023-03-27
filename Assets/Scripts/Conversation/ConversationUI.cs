using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script để quản lí việc hiển thị hội thoại
/// </summary>
public class ConversationUI : MonoBehaviour
{
    [SerializeField] private TMP_Text contentLabel;
    [SerializeField] private Image leftAvatar;
    [SerializeField] private Image rightAvatar;

    private void Awake()
    {
        leftAvatar.gameObject.SetActive(false);
        rightAvatar.gameObject.SetActive(false);
    }

    public void SetContent(string content)
    {
        contentLabel.text = content;
    }

    public void ClearContent()
    {
        contentLabel.text = string.Empty;
    }

    public void SetAvatar(Sprite sprite, CharacterAvatarPosition position)
    {
        // Nếu vị trí ở bên trái, thì set bên trái
        if (position == CharacterAvatarPosition.Left)
        {
            leftAvatar.gameObject.SetActive(true);
            rightAvatar.gameObject.SetActive(false);
            leftAvatar.sprite = sprite;
        }

        // Nếu vị trí ở bên phải, thì set bên phải
        else
        {
            rightAvatar.gameObject.SetActive(true);
            leftAvatar.gameObject.SetActive(false);
            rightAvatar.sprite = sprite;
        }
    }
}
