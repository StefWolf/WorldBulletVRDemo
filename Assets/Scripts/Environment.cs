using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private AudioClip sound;

    void Start() {
        AudioManager.Instance.PlayAudio(sound, true);
    }
}
