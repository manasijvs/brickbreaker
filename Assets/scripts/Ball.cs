using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();//reference to the component attached to the game object
    }

    private void Start()
    {
        ResetBall();

    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;//position of object in 3d space. vector2.zero->same as vector2(0,0)
        this.rigidbody.velocity = Vector2.zero;
        Invoke(nameof(SetRandomTrajectory), 1f);//call the fn byname with a delay of 1sec
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;//creates a vector force and initializes to 0
        force.x = Random.Range(-1f, 1f);//initializes x's value between -1 and 1.
        force.y = -1f;//initializes y's value to -1 to give a downward force
        this.rigidbody.AddForce(force.normalized * this.speed);//applies this force to the rigidbody component of the gameobject.
    }
}