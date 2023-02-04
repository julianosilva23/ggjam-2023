using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController player2;
    public Vector3 initialPosition1;
    public Vector3 initialPosition2;
    public float scoreGainRate;
    public float roleChangeTime;
    public int scoreCap;
    public Text winText;
    //public Camera mainCamera;

    public Text scoreText1;
    public Text scoreText2;

    private float currentTime;
    private bool player1Scores;
    private bool gameOver;

    void Start()
    {
        currentTime = 0;
        player1Scores = true;
        gameOver = false;
        winText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameOver) return;

        currentTime += Time.deltaTime;
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        if (player1Scores)
        {
            player1.score += scoreGainRate * (1 / distance) * Time.deltaTime;
            scoreText1.text = player1.score + "/" + scoreCap;

            if (player1.score >= scoreCap)
            {
                gameOver = true;
                winText.gameObject.SetActive(true);
                winText.text = "Player 1 Wins!";
                return;
            }

            if (currentTime >= roleChangeTime || distance <= 0.5f)
            {
                ChangeRole();
            }
        }
        else
        {
            player2.score += scoreGainRate * (1 / distance) * Time.deltaTime;
            scoreText1.text = player1.score + "/" + scoreCap;

            if (player2.score >= scoreCap)
            {
                gameOver = true;
                winText.gameObject.SetActive(true);
                winText.text = "Player 2 Wins!";
                return;
            }

            if (currentTime >= roleChangeTime || distance <= 0.5f)
            {
                ChangeRole();
            }
        }
    }

    public void ChangeRole()
    {
        player1.transform.position = initialPosition1;
        player2.transform.position = initialPosition2;
        player1Scores = !player1Scores;
        currentTime = 0;
    }
}