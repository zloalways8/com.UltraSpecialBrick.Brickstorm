using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource backgroundMusic;

    private const string MusicPrefKey = "MusicEnabled"; // ���� ��� ���������� ��������� ������

    private void Awake()
    {
        // Singleton ����������
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ��������� ���������� ��������� ������
        if (!PlayerPrefs.HasKey(MusicPrefKey))
        {
            PlayerPrefs.SetInt(MusicPrefKey, 1); // 1 = ��������, 0 = ���������
            PlayerPrefs.Save();
        }

        UpdateMusicState();
    }

    public void ToggleMusic()
    {
        // ������������ ��������� ������
        int musicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1);
        musicEnabled = 1 - musicEnabled; // ������ 1 �� 0 ��� 0 �� 1
        PlayerPrefs.SetInt(MusicPrefKey, musicEnabled);
        PlayerPrefs.Save();

        UpdateMusicState();
    }

    private void UpdateMusicState()
    {
        // ��������� ��������� ������
        bool isMusicEnabled = PlayerPrefs.GetInt(MusicPrefKey, 1) == 1;
        backgroundMusic.mute = !isMusicEnabled;
    }
}
