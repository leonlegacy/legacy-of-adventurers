using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZhengHua;

public class BGMManager : SingtonMono<BGMManager>
{

    private AudioSource AudioSource;
    public AudioClip VillageBgm;
    public AudioClip EncounterBgm;

    public override void Awake()
    {
        base.Awake();
        AudioSource = gameObject.AddComponent<AudioSource>();
        AudioSource.playOnAwake = false;
        AudioSource.volume = 0.5f;
    }

    /**
    * 播放村莊(選擇任務或角色)的背景音樂
    */
    public void PlayVillageBgm()
    {
        PlayBgm(VillageBgm);
    }

    /**
    * 播放遭遇事件(選擇任務或角色)的背景音樂
    */
    public void PlayAdventureBgm()
    {
        PlayBgm(EncounterBgm);
    }

    /**
    * 播放指定的背景音樂
    * @param clip 音效片段
    */
    public void PlayBgm(AudioClip clip)
    {
        AudioSource.clip = clip;
        AudioSource.Play();
        AudioSource.loop = true;
    }


}
