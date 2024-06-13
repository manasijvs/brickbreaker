using UnityEngine;

public class brick : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }//refer to the spriterender component in the gameobject
    public Sprite[] states;//array of sprites. to represent changes in bricks according to its health
    public int health { get; private set; }
    public int points = 100;
    public bool unbreakable;

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!this.unbreakable)
        {
            this.health = this.states.Length;//health=elements in states array
            this.spriteRenderer.sprite = this.states[this.health - 1]; 
        }
    }

    private void Hit()
    {
        if (this.unbreakable) 
        {
            return;
        }
        this.health --;

        if (this.health <= 0) 
        {
            this.gameObject.SetActive(false);
        }
        else 
        {
            this. spriteRenderer.sprite = this.states[this.health - 1];
        }
        FindAnyObjectByType<GameManager>().Hit(this);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball") 
        {
            Hit();
        }
    }
    

    
}
