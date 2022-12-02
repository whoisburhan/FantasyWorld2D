using System.Collections;
using System.Collections.Generic;
using GS.AudioAsset;
using GS.FanstayWorld2D.Projectile;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class ConsumableTile : MonoBehaviour
{
    private Tilemap tilemap;
    private AudioSourceScript audioSourceScript;
    [SerializeField] private List<AudioName> audioName;
    [SerializeField] private List<ParticleType> particles;
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
            Vector2 hitPoint = col.contacts[0].point;
            var temp = Vector3Int.FloorToInt(hitPoint);
            tilemap.SetTile(temp, null);

            if (audioName.Count > 0)
            {
                int chosenAudio = Random.Range(0, audioName.Count);
                audioSourceScript.Play(AudioManager.Instance.GetAudioClip(audioName[chosenAudio]));
            }

            if (particles.Count > 0)
            {
                int chosenParticle = Random.Range(0, particles.Count);
                GameObject go = ProjectileController.Instance.GetParticle(particles[chosenParticle]);
                go.transform.position = hitPoint;
                go.SetActive(false);
                go.SetActive(true);
            }
        }
    }
}
