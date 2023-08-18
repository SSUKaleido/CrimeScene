using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 사운드 생성을 관리하는 매니저
* play() 메서드 이용해서 관리합니다
*/
public class SoundManager
{
    // Define.cs의 Sound enum에 따라, 배경음악과 효과음을 재생할 오디오클립 2개 담을 배열
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    // 효과음은 자주 재생되므로 매번 로드하면 비효율적. 미리 로드해놓을 딕셔너리
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    /**
    * AudioClip을 붙일 오브젝트를 만들고 컴포넌트를 붙임
    */
    public void Init()
    {
        GameObject root = GameObject.Find("Sound");
        
        if (root == null) 
        {
            root = new GameObject { name = "Sound" };
            Object.DontDestroyOnLoad(root); // 사운드는 로딩 중에도 재생되도록

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] }; 
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true; // bgm 재생기는 무한 반복 재생
        }
    }

    /**
    * 게임이 아주 오래 지속될 경우 효과음을 담는 딕셔너리가 너무 커질 수 있음. 오디오를 멈추고 딕셔너리를 청소.
    */
    public void Clear()
    {
        // 재생기 전부 재생 스탑, 음반 빼기
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음 Dictionary 비우기
        _audioClips.Clear();
    }

    /**
    * AudioClip을 받아서 실행하는 메서드
    * @param audioClip 실행할 AudioClip
    * @param type 오디오 타입(BGM, Effect). 기본은 효과음
    * @param pitch 피치. 기본은 1.0f
    */
    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
        if (audioClip == null)
            return;

		if (type == Define.Sound.Bgm) // BGM 배경음악 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else // Effect 효과음 재생
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
	}

    /**
    * 위 Play() 메서드의 경로를 대신 받는 버전
    */
    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);
    }

    /**
    * 경로로부터 AudioClip을 가져옴
    * 효과음은 한번만 가져오고 딕셔너리에 넣음
    * 저장 경로는 resources/Sounds/(파일 경로)
    */
    AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
		if (path.Contains("Sounds/") == false)
			path = $"Sounds/{path}"; // Sound 폴더 안에 저장될 수 있도록

		AudioClip audioClip = null;

		if (type == Define.Sound.Bgm) // BGM 배경음악 클립 붙이기
		{
			audioClip = GameManager.Resource.Load<AudioClip>(path);
		}
		else // Effect 효과음 클립 붙이기
		{
			if (_audioClips.TryGetValue(path, out audioClip) == false)
			{
				audioClip = GameManager.Resource.Load<AudioClip>(path);
				_audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.Log($"AudioClip Missing ! {path}");

		return audioClip;
    }
}