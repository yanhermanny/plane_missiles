using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour {

	public GameObject player;
	public GameObject playerExplosion;
	public TextMeshProUGUI starCountText;
	public Animator bonusAlertAnimator;

	public static bool isGameOver;
	public static bool isPaused;
	public static bool isSoundOn;

	private static AudioSource backgroundMusic;
	private static AudioSource playerSF;

	private static bool addBonusText;

	private static int starsCount;
	private static int missilesDestroyed;

	private int timerPoints;
	private int starPoints;
	private int bonusPoints;
	private int totalPoints;

	private static bool startPlayerSF;

	private void Start() {
		Time.timeScale = 1;

		addBonusText = false;
		isGameOver = false;
		isPaused = true;
		isSoundOn = true;

		startPlayerSF = false;

		starsCount = 0;
		missilesDestroyed = 0;

		backgroundMusic = this.GetComponent<AudioSource>();
		playerSF = GameObject.Find("Player").GetComponent<AudioSource>();
	}

	private void Update() {
		starCountText.text = string.Format("x {0:00}", starsCount);

		if (!isGameOver) {
			timerPoints = (int) TimerScript.GetTimer();

			if (addBonusText) {
				bonusAlertAnimator.SetTrigger("addBonus");
				addBonusText = false;
			}

			if (startPlayerSF) {
				playerSF.volume = 0;
				playerSF.Play();
				StartCoroutine(StartPlayerSound());
			}
		} else {
			if (player != null) {
				Instantiate(playerExplosion, player.transform.position, playerExplosion.transform.rotation);
				Destroy(player, 0.2f);
			}

			CalculatePoints();
			if (TimerScript.GetTimer() - timerPoints >= 2.5f) {
				CanvasScript.ShowGameOver(timerPoints, starPoints, bonusPoints, totalPoints);
			}
		}
	}

	public static void AddMissilesDestroyed() {
		missilesDestroyed++;
		addBonusText = true;
	}

	public static void AddStar() {
		starsCount++;
	}

	private void CalculatePoints() {
		starPoints = starsCount * 10;
		bonusPoints = (int) (missilesDestroyed * 2.5f);
		totalPoints = timerPoints + starPoints + bonusPoints;
	}

	public static void StartGame() {
		isPaused = false;

		if (isSoundOn) {
			startPlayerSF = true;
		}
		Time.timeScale = 1;
	}

	public static void PauseGame() {
		isPaused = true;
		playerSF.Pause();
		Time.timeScale = 0;
	}

	public static void GoHome() {
		SceneManager.LoadScene("GameScene");
	}

	public static void MuteSound() {
		backgroundMusic.Pause();
		playerSF.Pause();
		isSoundOn = false;
	}

	public static void SoundOn() {
		backgroundMusic.UnPause();
		playerSF.UnPause();
		isSoundOn = true;
	}

	private IEnumerator StartPlayerSound() {
		startPlayerSF = false;
		while (playerSF.volume < 0.01f) {
			playerSF.volume += 0.001f;
			yield return new WaitForSeconds(0.5f);
		}
	}
}