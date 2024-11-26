using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource backgroundMusic;

    private const string MusicPrefKey = "MusicEnabled"; // Ключ для сохранения состояния музыки

    private void Awake()
    {
        // Singleton реализация
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Установка начального состояния музыки
        if (!PlayerPrefs.HasKey(MusicPrefKey))
        {
            PlayerPrefs.SetInt(MusicPrefKey, 1); // 1 = включено, 0 = выключено
            PlayerPrefs.Save();
        }

        UpdateMusicState();
    }

    public void ToggleMusic()
    {
        // Переключение состояния музыки
        int musicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1);
        musicEnabled = 1 - musicEnabled; // Меняем 1 на 0 или 0 на 1
        PlayerPrefs.SetInt(MusicPrefKey, musicEnabled);
        PlayerPrefs.Save();

        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        // Обновляем состояние музыки
        bool isMusicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
        backgroundMusic.mute = !isMusicEnabled;
    }
}
