using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudio : MonoBehaviour
{
    CharacterControllerRB characterController;
    AudioSource[] audioSource;

    [SerializeField]
    private AudioClip death;
    [SerializeField]
    private AudioClip hit;
   // [SerializeField]
  //  private AudioClip land;

    [SerializeField]
    private GroundType[] grounds;


    // Use this for initialization
    void Start()
    {
        characterController = GetComponent<CharacterControllerRB>();
        audioSource = GetComponents<AudioSource>();
    }

    private void Step()
    {
        if (!audioSource[0].isPlaying)
        {
            var ground = GetGround();
            audioSource[0].volume = ground.volume;
            audioSource[0].pitch = ground.pitch;
            audioSource[0].PlayOneShot(ground.footstepSound[
                    Random.Range(0, ground.footstepSound.Length)]);
        }
    }

    private void QuietStep()
    {
        if (!audioSource[0].isPlaying)
        {
            var ground = GetGround();
            audioSource[0].volume = ground.volume * 0.3f;
            audioSource[0].pitch = ground.pitch * 0.8f;
            audioSource[0].PlayOneShot(ground.footstepSound[
                    Random.Range(0, ground.footstepSound.Length)]);
        }
    }

    private void Jump()
    {
        audioSource[0].volume = 0.8f;
        audioSource[0].pitch = 1.1f;
        audioSource[0].PlayOneShot(hit);

        var ground = GetGround();
        audioSource[1].volume = ground.volume * 0.8f;
        audioSource[1].pitch = ground.pitch *1.3f;
        audioSource[1].PlayOneShot(ground.footstepSound[
                Random.Range(0, ground.footstepSound.Length)]);
    }

    private void GetHit()
    {
        audioSource[0].volume = 1.1f;
        audioSource[0].pitch = 0.9f;
        audioSource[0].PlayOneShot(hit);
    }

    private void DeathSound()
    {
        audioSource[1].volume = 1.1f;
        audioSource[1].pitch = 0.9f;
        audioSource[1].PlayOneShot(death);
    }

    private void GroundFall()
    {
        var ground = GetGround();
        audioSource[0].volume = ground.volume * 1.5f;
        audioSource[0].pitch = ground.pitch * 0.5f;
        audioSource[0].PlayOneShot(ground.footstepSound[
                Random.Range(0, ground.footstepSound.Length)]);
    }

    private void Land()
    {
        var ground = GetGround();
        audioSource[0].volume = ground.volume * 1.5f;
        audioSource[0].pitch = ground.pitch * 0.8f;
        audioSource[0].PlayOneShot(ground.footstepSound[
                Random.Range(0, ground.footstepSound.Length)]);
        audioSource[1].volume = ground.volume*1.5f;
        audioSource[1].pitch = ground.pitch * 0.8f;
        audioSource[1].PlayOneShot(ground.footstepSound[
                Random.Range(0, ground.footstepSound.Length)]);
    }

    private GroundType GetGround()
    {
        foreach (var ground in grounds)
        {
            if (characterController.groundType == ground.name)
            {
                return ground;
            }
        }
        return grounds[0];
    }
}


[System.Serializable]
public class GroundType
{
    public string name;
    public AudioClip[] footstepSound;
    public float volume;
    public float pitch;
}