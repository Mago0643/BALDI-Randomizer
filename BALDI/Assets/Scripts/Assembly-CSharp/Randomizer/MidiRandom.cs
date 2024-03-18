using FluidMidi;
using UnityEngine;

public class MidiRandom : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SongPlayer>().Tempo = Random.Range(0.5f, 3f);
    }
}