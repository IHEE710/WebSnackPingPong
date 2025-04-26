//using UnityEngine;
//using UnityEngine.UI;


//public class AudioManager : MonoBehaviour
//{
//    public static AudioManager Instance;

//    public AudioSource bgmSource;
//    public AudioSource seSource;

//    private void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    public void SetBGMVolume(float volume)
//    {
//        bgmSource.volume = volume;
//    }

//    public void SetSEVolume(float volume)
//    {
//        seSource.volume = volume;
//    }
//}


//public class SoundSlider : MonoBehaviour
//{
//    public Slider bgmSlider;
//    public Slider seSlider;

//    private const string BGM_KEY = "BGM_VOLUME";
//    private const string SE_KEY = "SE_VOLUME";

//    void Start()
//    {
//        // ����� ���� �� �ҷ����� (������ �⺻�� 1.0f)
//        float savedBGM = PlayerPrefs.GetFloat(BGM_KEY, 1f);
//        float savedSE = PlayerPrefs.GetFloat(SE_KEY, 1f);

//        // �����̴� �ʱⰪ ����
//        bgmSlider.value = savedBGM;
//        seSlider.value = savedSE;

//        // ���� ����� ������ �ݿ�
//        AudioManager.Instance.SetBGMVolume(savedBGM);
//        AudioManager.Instance.SetSEVolume(savedSE);

//        // �����̴� ���� �� ȣ��� �Լ� ����
//        bgmSlider.onValueChanged.AddListener(OnBGMVolumeChanged);
//        seSlider.onValueChanged.AddListener(OnSEVolumeChanged);
//    }

//    public void OnBGMVolumeChanged(float value)
//    {
//        AudioManager.Instance.SetBGMVolume(value);
//        PlayerPrefs.SetFloat(BGM_KEY, value); // ����
//    }

//    public void OnSEVolumeChanged(float value)
//    {
//        AudioManager.Instance.SetSEVolume(value);
//        PlayerPrefs.SetFloat(SE_KEY, value); // ����
//    }
//}

//**** �޸�*****
//���� �߰��ϸ鼭 �ڵ� ������ ����. ������ �����̴� ui�۵��� Ȯ�ο�