using System.Collections;
using System.Collections.Generic;
using GS.AudioAsset;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class ConsumableTile : MonoBehaviour
{
    private Tilemap tilemap;
    private AudioSourceScript audioSourceScript;
    [SerializeField] private List<AudioName> audioName;
    //public Tile tile;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        audioSourceScript = GetComponent<AudioSourceScript>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            var temp = Vector3Int.FloorToInt(col.contacts[0].point);
            tilemap.SetTile(temp, null);

            if (audioName.Count > 0)
            {
                int chosenAudio = Random.Range(0, audioName.Count);
                audioSourceScript.Play(AudioManager.Instance.GetAudioClip(audioName[chosenAudio]));
            }
        }
    }
}
