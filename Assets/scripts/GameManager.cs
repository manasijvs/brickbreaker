using UnityEngine; //allows us to use core functionalities such as gameobject, component, transform etc...
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //allows gamemanager to use game(), awake() etc...
{
    public Ball ball { get; private set; }//class classname get->ball's value can be read outside the class, private set-> its's value can be modified only within the class.
    public Paddle paddle { get; private set; }
    public int level = 1;
    public int score = 0;
    public int lives = 3;
    
    private void Awake() //when script is first initialized, it will be called. initializing variables or states before the game starts.
    {
        DontDestroyOnLoad(this.gameObject);//don't destroy the gameobject this gamemanager is attached to while loading a new scene. here the game object we created is game manager.
        SceneManager.sceneLoaded += onlevelloaded;//onlevelloaded is called every time a new scene is loaded.
    }

    private void Start()//when the game object becomes active and before the first frame starts, call NewGame() 
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        loadlevel("New Scene1");//load the first game scene
    }

    private void loadlevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);//loading the scene using this function. passing tha parameter scenename
    }

    private void onlevelloaded(Scene scene, LoadSceneMode mode)
    {
        this.ball = FindFirstObjectByType<Ball>();//find the object of type ball and assign it to ball
        this.paddle = FindFirstObjectByType<Paddle>();
    }

    private void resetlevel()
    {
        this.ball.ResetBall();
        this.paddle.ResetPaddle();
    }

    private void gameover()
    {
        NewGame();
    }
    
    public void life()
    {
        this.lives--;
        if (this.lives > 0)
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
