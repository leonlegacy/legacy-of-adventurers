using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZhengHua;

public class BGMManager : SingtonMono<BGMManager>
{

    private AudioSource audioSource;
    public AudioClip VillageBgm;
    public AudioClip EncounterBgm;

    public override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    /**
    * 播放村莊(選擇任務或角色)的背景音樂
    */
    public void PlayVillageBgm()
    {
        audioSource.clip = VillageBgm;
        audioSource.Play();
    }

    /**
    * 播放遭遇事件(選擇任務或角色)的背景音樂
    */
    public void PlayAdventureBgm()
    {
        audioSource.clip = EncounterBgm;
        audioSource.Play();
    }


}
