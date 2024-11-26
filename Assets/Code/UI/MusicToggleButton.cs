using UnityEngine;
using UnityEngine.UI;

public class MusicToggleButton : MonoBehaviour
{
    [SerializeField] private Button toggleButton;   // ������ �� ������
    [SerializeField] private Image buttonImage;    // ������ �� ����������� ������
    [SerializeField] private Sprite musicOnSprite; // ������ ��� ��������� "������ ��������"
    [SerializeField] private Sprite musicOffSprite; // ������ ��� ��������� "������ ���������"

    private void Start()
    {
        UpdateButtonState(); // ��������� ������� ��� ������ ��� ��������
        toggleButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        MusicManager.Instance.ToggleMusic(); // ����������� ������
        UpdateButtonState(); // ��������� ������ ������
    }

    private void UpdateButtonState()
    {
        // ���������� ������� ��������� ������
        bool isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        buttonImage.sprite = isMusicEnabled ? musicOnSprite : musicOffSprite;
    }
}
