using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLogic : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 1f;
    [SerializeField] float maxX = 15f;

    // cache variables
    GameSessionLogic gameSession;
    BallLogic ball;

    // Start is called before the first frame update
    void Start()
    {
        gameSession = FindObjectOfType<GameSessionLogic>();
        ball = FindObjectOfType<BallLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(GetXPositionInUnits(), minX, maxX);
        transform.position = paddlePosition;
    }

    private float GetXPositionInUnits()
    {
        return (gameSession.IsAutoPlayEnabled() ? ball.transform.position.x : Input.mousePosition.x / Screen.width * screenWidthInUnits);
    }
}
