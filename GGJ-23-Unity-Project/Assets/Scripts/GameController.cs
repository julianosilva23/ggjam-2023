using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public Vector3 initialPosition1;
    public Vector3 initialPosition2;
    public float scoreGainRate;
    public float roleChangeTime;
    public int scoreCap;
    public Text winText;
    public Camera mainCamera;

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
        float distance = Vector3.Distance(player1.position, player2.position);

        if (player1Scores)
        {
            player1.GetComponent<PlayerController>().score += scoreGainRate * (1 / distance) * Time.deltaTime;

            if (player1.GetComponent<PlayerController>().score >= scoreCap)
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
            player2.GetComponent<PlayerController>().score += scoreGainRate * (1 / distance) * Time.deltaTime;

            if (player2.GetComponent<PlayerController>().score >= scoreCap)
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
        player1.position = initialPosition1;
        player2.position = initialPosition2;
        player1Scores = !player1Scores;
        currentTime = 0;
    }
}