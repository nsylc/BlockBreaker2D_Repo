using UnityEngine;

public class Ball : MonoBehaviour
{
    //config params
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSouns;
    [SerializeField] float randomFactor = 0.2f;

    //state
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    AudioSource mySounds;
    Rigidbody2D myRigidBody2D;

    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        mySounds = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouse();
        }
    }

    private void LaunchOnMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0.5f, randomFactor), Random.Range(0.5f, randomFactor)); 
        if(hasStarted)
        {
            AudioClip clip = ballSouns[Random.Range(0, ballSouns.Length)];
            mySounds.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        } 
    }

}
