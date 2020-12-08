using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioCollectDiamond;
    public AudioSource audioAttack;
    public AudioSource audioJump;
    void PlayCollectDiamond()
    {
        audioCollectDiamond.Play();
    }
    void PlayAttack()
    {
        audioAttack.Play();
    }
    void PlayJump()
    {
        audioJump.Play();
    }
}
