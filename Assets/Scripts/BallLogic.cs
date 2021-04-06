using UnityEngine;

public class BallLogic : MonoBehaviour
{
    // configuration parameters
    [SerializeField] PaddleLogic paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballAudioClips;
    [SerializeField] float randomFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    bool hasLaunched = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLaunched)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            hasLaunched = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(RandomRangeExcept(-1 * randomFactor, randomFactor, 0f), RandomRangeExcept(-1 * randomFactor, randomFactor, 0f));

        if (hasLaunched)
        {
            AudioClip clip = ballAudioClips[Random.Range(0, ballAudioClips.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody2D.velocity += velocityTweak;
        }
    }

    private float RandomRangeExcept(float min, float max, float except)
    {
        float returnedRandom = except;
        while (returnedRandom == except)
        {
            returnedRandom = Random.Range(min, max);
        }
        return returnedRandom;
    }
}
