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
    public float distanceCap;
    public Text winText;

    public Text scoreText1;
    public Text scoreText2;

    public bool player1Scores;

    private float currentTime;
    private bool gameStart;
    private bool gameOver;

    void Start()
    {
        gameOver = false;
        gameStart = false;
        winText.gameObject.SetActive(false);
    }

    public void buildPlayers()
    {
        currentTime = 0;
        player1Scores = true;
        player1.tag = "Hunter";        
        gameStart = true;

        //colors
        player1.aoe.GetComponent<SpriteRenderer>().color = Color.blue;
        //player1.arrow.GetComponent<SpriteRenderer>().color = Color.blue;
        //player1.flag.GetComponent<SpriteRenderer>().color = Color.blue;
        player1.GetComponent<PlayerAction>().shield.GetComponent<SpriteRenderer>().color = Color.blue;

        player2.aoe.GetComponent<SpriteRenderer>().color = Color.red;
        //player2.arrow.GetComponent<SpriteRenderer>().color = Color.red;
        //player2.flag.GetComponent<SpriteRenderer>().color = Color.red;
        player2.GetComponent<PlayerAction>().shield.GetComponent<SpriteRenderer>().color = Color.red;
        
    }

    void Update()
    {
        if (gameOver || !gameStart) return;
        ScoreScreenUpdate();

        currentTime += Time.deltaTime;
        float distance = Vector3.Distance(player1.transform.position, player2.transform.position);

        if (player1Scores)
        {
            if (distance < distanceCap)
            {
                player1.score += scoreGainRate * (1 / distance) * Time.deltaTime;
            }
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
            if (distance < distanceCap)
            {
                player2.score += scoreGainRate * (1 / distance) * Time.deltaTime;
            }

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

    private void ScoreScreenUpdate()
    {
        scoreText1.text = player1.score + "/" + scoreCap;
        scoreText2.text = player2.score + "/" + scoreCap;
    }

    public void ChangeRole()
    {
        player1.SetVulnerability(false);
        player2.SetVulnerability(false);
        player1.transform.position = initialPosition1;
        player2.transform.position = initialPosition2;
        player1Scores = !player1Scores;
        player1.SetFlag(!player1Scores);
        player2.SetFlag(player1Scores);
        currentTime = 0;
        player1.SetVulnerability(true);
        player2.SetVulnerability(true);
    }
}