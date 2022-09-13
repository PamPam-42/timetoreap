using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
	[SerializeField] private GameObject tombstonePrefab;
	
	private AudioSource audioSource;
	[SerializeField] AudioClip hitSoundClip;
	
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponentInParent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (!GameManager.TIME_STATE_FROZEN) {
			if (other.gameObject.CompareTag("Ghost")) {
				Vector3 spawnPosition = other.gameObject.transform.position;
				Destroy(other.gameObject);
				audioSource.PlayOneShot(hitSoundClip);
				Instantiate(tombstonePrefab, spawnPosition, Quaternion.identity);
				GameManager.instance.NotifyEnemyReaped();
			}
		}
	}
}
