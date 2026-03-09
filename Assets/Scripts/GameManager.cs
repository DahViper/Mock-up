using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int exp = 0;
    public int lives = 3;
    public Text scoreText;
    public Text livesText;
    public GameObject gameOverPanel;

    private bool isGameOver = false;
    public int RewardThreshold = 20;

    public BallChoiceUI choiceUI;

    public ParticleSystem deathParticle;

    public void GiveReward()
    {
        Debug.Log("Giving Ball Reward!");
        choiceUI.Show(OnBallPicked);
    }

    void OnBallPicked(BallData ball)
    {
        BallLauncher.Instance.OnBallReturned(ball);
    }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();
    }

    public void AddExp(int points)
    {
        exp += points;
        checkThreshold();
        UpdateUI();
    }

    public void LoseLife()
    {
        if (isGameOver) return;

        lives--;
        UpdateUI();

        if (lives <= 0)
        {
            //GameOver();
        }
    }

    public void checkThreshold()
    {
        if (exp == RewardThreshold)
        { 
            GiveReward();
        RewardThreshold *= 2;
        }
    }
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + exp;
        if (livesText != null)
            livesText.text = "Lives: " + lives;
    }

    public void PlayDeath(Vector3 position)
    {
        if (deathParticle != null)
        {
            ParticleSystem particle = Instantiate(deathParticle, position, Quaternion.identity);
            particle.Play();
            Destroy(particle.gameObject, particle.main.duration);
        }
    }
}