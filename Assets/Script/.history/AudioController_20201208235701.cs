using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioCollectDiamond;
    public AudioSource audioAttack;
    public AudioSource audioJump;
    public void PlayCollectDiamond()
    {
        audioCollectDiamond.Play();
    }
    public void PlayAttack()
    {
        audioAttack.Play();
    }
    public void PlayJump()
    {
        audioJump.Play();
    }
}
