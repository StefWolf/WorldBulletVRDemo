using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private AudioSource sound;

    void Start() {
        AudioClip clip = sound.clip;
        AudioManager.Instance.PlayAudio(clip, true);
    }
}
