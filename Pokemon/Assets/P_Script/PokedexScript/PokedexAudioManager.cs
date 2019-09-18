using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokedexAudioManager : MonoBehaviour {

    public AudioClip[] audio_;
    [SerializeField]
    AudioSource pokedexAudio;

    public void PlayAudio(int pokemonNo)
    {
        pokedexAudio.clip = audio_[pokemonNo];
        pokedexAudio.Play();
    }
}
