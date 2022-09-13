using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	
	private Text enemiesText;
	private Text timeText;
	
	public static GameManager instance = null;
	public static bool TIME_STATE_FROZEN = false;
	
	public const int menuLevelNumber = 0;
	public const int maxLevelNumber = 3;


	private static int[] enemiesPerLevel = new int[3] {3,5,5};
	private static int[] timePerLevel = new int[3] {10, 15, 20};
	private static int[] clocksPerLevel = new int[3] {3, 3, 4};

	private static int currentLevel = 0;
	private static int levelToRestart = 0;
	private static int enemiesReaped = 0;
	private static int clocksPickedUp = 0;
	private static float timeRemaining = 0;
	
	private GhostController[] enemies;
	private static int enemiesCoupled = 0;
	
	void Awake () {
			if (instance == null)
				instance = this;
			else
				Destroy(gameObject);
			
			DontDestroyOnLoad(gameObject);
			
			FirstInit();
	}
	
	public void HourglassPickup() {
		clocksPickedUp++;
		if (TIME_STATE_FROZEN) {
			TIME_STATE_FROZEN = false;
			BGSoundManager.instance.toggleClockSound();
			for (int i = 0; i < enemies.Length; i++) {
				enemies[i].notifyAnimChange(false);
			}
		}
		else {
			TIME_STATE_FROZEN = true;
			if (clocksPickedUp == clocksPerLevel[currentLevel-1]) {
				levelToRestart = currentLevel;
				currentLevel = menuLevelNumber;
				BGSoundManager.instance.pauseMusic();
				SceneManager.LoadScene("Level failed via Clocks");
			}
			else {
				BGSoundManager.instance.toggleClockSound();
				for (int i = 0; i < enemies.Length; i++) {
					enemies[i].notifyAnimChange(true);
				}
			}
		}
	}
	
	public void NotifyEnemyReaped() {
			enemiesReaped++;
			enemiesText.text = "Enemies reaped: " + enemiesReaped + "/" + enemiesPerLevel[currentLevel-1];
			if (enemiesReaped == enemiesPerLevel[currentLevel-1]) {
				//LOAD NEXT LEVEL
				currentLevel++;
				if (currentLevel <= maxLevelNumber) {	
					InitLevel();
					BGSoundManager.instance.stopClockSound();
		
					SceneManager.LoadScene(currentLevel);
					
				}
				else {
					BGSoundManager.instance.stopPlaying();
					SceneManager.LoadScene(currentLevel);
				}
			}
	}
	
	void FirstInit() {
		enemiesReaped = 0;
		clocksPickedUp = 0;
		TIME_STATE_FROZEN = true;
		timeRemaining = timePerLevel[0];
		enemies = new GhostController[enemiesPerLevel[0]];
		enemiesCoupled = 0;		
	}
	
	
	void InitLevel() {
		enemiesReaped = 0;
		TIME_STATE_FROZEN = true;
		clocksPickedUp = 0;
		timeRemaining = timePerLevel[currentLevel-1];
		enemies = new GhostController[enemiesPerLevel[currentLevel-1]];
		enemiesCoupled = 0;
		//BGSoundManager.instance.startPlaying();
	}
	
	public void InitMenu() {
		currentLevel = 0;
		
		BGSoundManager.instance.stopPlaying();
		SceneManager.LoadScene(currentLevel);
	}
	
	public void startGame() {
		currentLevel++;
		InitLevel();
		BGSoundManager.instance.startPlaying();
		SceneManager.LoadScene(currentLevel);
	}
	
	public void backToMainMenu() {
		SceneManager.LoadScene(currentLevel);
	}
	
	public void howToPlay() {
		SceneManager.LoadScene("How To Play");
	}
	
	public void retryLevel() {
		currentLevel = levelToRestart;
		InitLevel();
		
		BGSoundManager.instance.startPlaying();
		
		SceneManager.LoadScene(levelToRestart);	
	}
	
	public void pauseGame() {
			Time.timeScale = 0;
			Debug.Log("timeScale = 0");

	}
	
	public void resumeGame() {
			Time.timeScale = 1;
			Debug.Log("timeScale = 1");

	}
	
	public void quitGame() {
		Application.Quit();
	}
	
	public void SetEnemiesText(Text reference) {
		enemiesText = reference;
	}
	
	public void SetTimeText(Text reference) {
		timeText = reference;
	}
	
	public void CoupleEnemy(GhostController enemy) {
		enemies[enemiesCoupled++] = enemy;
	}
	
    // Start is called before the first frame update
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
		//PROBABLY SHOULD TRANSFER THIS LOGIC TO PLAYER GAME OBJECT?
		if (currentLevel > menuLevelNumber && currentLevel <= maxLevelNumber) {
			if (!TIME_STATE_FROZEN) {
				timeRemaining -= Time.deltaTime;
				timeText.text = "Time Remaining: " + string.Format("{0:00}",  Mathf.FloorToInt(timeRemaining));
				if (timeRemaining <= .1f) {
					//end level
					levelToRestart = currentLevel;
					currentLevel = menuLevelNumber;
					BGSoundManager.instance.pauseMusic();
					SceneManager.LoadScene("Level Failed via Time");
				}
			}
		}
    }
}
