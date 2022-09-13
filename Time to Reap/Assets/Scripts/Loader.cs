using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
	public GameObject gameManager;
	public GameObject bgSoundManager;

	[SerializeField] private Text enemiesText;
	[SerializeField] private Text timeText;
	
	void Awake () {
			if (GameManager.instance == null)
				Instantiate(gameManager);
			if (BGSoundManager.instance == null)
				Instantiate(bgSoundManager);
			
			GameManager.instance.SetEnemiesText(enemiesText);
			GameManager.instance.SetTimeText(timeText);
	}
    
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
