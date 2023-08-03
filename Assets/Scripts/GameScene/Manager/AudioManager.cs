using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Observer
{
    [SerializeField]
    private AudioSource bgm;
    [SerializeField]
    private AudioSource sfx;

    [SerializeField]
    private AudioClip[] bgmClips;
    [SerializeField]
    private AudioClip[] sfxClips;

    [SerializeField]
    int index = 0;
    public override void EnemyHitNotify()
    {
        sfx.PlayOneShot(sfxClips[index]);
        index++;
        if (index >= 3)
        {
            index = 0;
        }
    }

    public override void EnemyDeadNotify()
    {
        sfx.PlayOneShot(sfxClips[index]);
        sfx.PlayOneShot(sfxClips[3]);
    }

    public override void PlayerHitNotify()
    {
        sfx.PlayOneShot(sfxClips[4]);
    }

    public override void EndNotify()
    {
        bgm.Stop();
        bgm.PlayOneShot(bgmClips[1]);
        sfx.PlayOneShot(sfxClips[5]);
    }
}
