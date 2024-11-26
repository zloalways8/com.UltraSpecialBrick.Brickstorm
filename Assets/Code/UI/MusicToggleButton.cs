using UnityEngine;
using UnityEngine.UI;

public class MusicToggleButton : MonoBehaviour
{
    [SerializeField] private Button toggleButton;   // Ссылка на кнопку
    [SerializeField] private Image buttonImage;    // Ссылка на изображение кнопки
    [SerializeField] private Sprite musicOnSprite; // Спрайт для состояния "Музыка включена"
    [SerializeField] private Sprite musicOffSprite; // Спрайт для состояния "Музыка выключена"

    private void Start()
    {
        UpdateButtonState(); // Обновляем внешний вид кнопки при загрузке
        toggleButton.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        MusicManager.Instance.ToggleMusic(); // Переключаем музыку
        UpdateButtonState(); // Обновляем спрайт кнопки
    }

    private void UpdateButtonState()
    {
        // Определяем текущее состояние музыки
        bool isMusicEnabled = PlayerPrefs.GetInt("MusicEnabled", 1) == 1;
        buttonImage.sprite = isMusicEnabled ? musicOnSprite : musicOffSprite;
    }
}
