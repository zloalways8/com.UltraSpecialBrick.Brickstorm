using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClonedBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private MainBall mainBall;

    private void Awake()
    {
        mainBall = MainBall.Instance;
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("TopBound"))
        {
            mainBall.ballsCount++;
            rb.velocity = Vector3.zero;
            gameObject.SetActive(false);
            mainBall.CheckAllBallsReturned(); // Вызываем проверку при каждом возвращении шарика
        }
    }

}
