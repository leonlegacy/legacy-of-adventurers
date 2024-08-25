using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using ZhengHua;

public class AudioManager : SingtonMono<AudioManager>
{

    private AudioSource currPlaySounndSource;

    [SerializeField]
    private AudioClip clickButton;

    [SerializeField]
    private AudioClip encounterSuccess;

    [SerializeField]
    private AudioClip encounterFail;

    /**
    * 播放點擊按鈕音效
    */
    public void PlayClickButton()
    {
        PlayAudio(clickButton);
    }

    /**
    * 遭遇事件成功音效
    */
    public void PlayEncounterSuccess()
    {
        PlayAudio(encounterSuccess);
    }

    /**
    * 遭遇事件失敗音效
    */
    public void PlayEncounterFail()
    {
        PlayAudio(encounterFail);
    }

    /**
    * 播放指定的音效
    * @param clip 音效片段
    */
    public void PlayAudio(AudioClip clip)
    {
        GameObject obj = new GameObject("Audio", typeof(AudioManager));
        obj.transform.SetParent(transform);
        AudioSource currPlaySounndSource = obj.AddComponent<AudioSource>();
        currPlaySounndSource.playOnAwake = false;
        currPlaySounndSource.loop = true;
        currPlaySounndSource.volume = 0.5f;
        currPlaySounndSource.PlayOneShot(clip);
        StartCoroutine(PlayedAudio(obj, clip));
    }

    private IEnumerator PlayedAudio(GameObject obj, AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);
        Destroy(obj);
    }

}
