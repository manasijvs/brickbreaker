using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += onlevelloaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        loadlevel(1);
    }
    private void loadlevel(int level)
    {
        this.level = level;
        SceneManager.LoadScene("level" + level);
    }

    private void onlevelloaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindFirstObjectByType<Ball>();
        this.paddle = FindFirstObjectByType<Paddle>();
    }

    private void resetlevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void gameover()
    {
        //SceneManager.LoadScene("gameover");
        NewGame();
    }
    
    public void life()
    {
        this.lives--;
        if(this.lives > 0)
        {
            resetlevel();
        }
        else
        {
            gameover();
        }
    }
    public void Hit(brick brick)
    {
        this.score += brick.points;
    }
}


