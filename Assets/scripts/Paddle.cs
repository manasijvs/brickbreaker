using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Rigidbody2D rb;
    public Vector2 direction { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);//resets the horizonatal while maintaing the vertical position
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    private void Update()//to continuously check the player input to determine the movement direction of the object
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))//a or left arrow key is pressed
        {
            this.direction = Vector2.left;//set direction to left
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate()//for physics based calculations
    {
        this.rb.velocity = this.direction * this.speed;//to move the paddle in the specified direction and speed
    }

    private void OnCollisionEnter2D(Collision2D collision)//called when a gameobject(paddle) collides with another gameobject(ball)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();//to make sure the other thing colliding with the paddle is ball. to get the reference to the ball component from gameobject involved in the collision

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;//get the paddle position
            Vector2 contactPoint = collision.GetContact(0).point;//where the ball hits the paddle. 0= the first point of conntact.

            float offset = paddlePosition.x - contactPoint.x;//offset=calc where on the paddle the ball hits relative to the middle point
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity);//current angle of the ball relative to y and direction of the ball
            float bounceAngle = (offset / width) * this.maxBounceAngle;//the bounce angle won't exceed the maxbounceangle(75)
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);//clamp=to make sure the new angale doesn't exceed the limit (75).

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);//to make the ball rotate in while moving in forward direction.
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }
}