using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainBall : MonoBehaviour
{

    public static MainBall Instance;

    [SerializeField] GameObject arrow;
    [SerializeField] GameObject ballPrefab;
    private bool isGrounded = true;

    public int startingBallsAmount;
    [HideInInspector] public int ballsCount;

    [SerializeField] TMP_Text ballsCountText;
    
    public float dashForce;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private CircleCollider2D plrCollider;
    private Camera mainCamera;

    //public UnityEvent onLanding;

    List<GameObject> clonedBalls = new List<GameObject>();

    private int ballsInAir; // Счетчик активных шариков

    public EndlessLevelController endlessLevelController;
    public LevelController levelController;
    private void Awake()
    {
        Instance = this;
        Input.simulateMouseWithTouches = true;
        ballsCount = startingBallsAmount;
        InstantiateBalls();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        plrCollider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;

    }

    private void Update()
    {


       

        if ((Input.GetMouseButton(0) && isGrounded && ballsCount == startingBallsAmount))
        {
            if((mainCamera.ScreenToWorldPoint(Input.mousePosition - transform.position).y >= transform.position.y))
            {
                arrow.SetActive(false);
                
            }
            else
            {
                arrow.SetActive(true);
                Aim();
            }

            
        } 
        if(Input.GetMouseButtonUp(0) && isGrounded && ballsCount == startingBallsAmount && !(mainCamera.ScreenToWorldPoint(Input.mousePosition - transform.position).y >= transform.position.y))
        {
            
            
            
            StartCoroutine(ShootBalls());
            
        }




    }

    private void Aim()
    {
        
        Vector3 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        
        
        direction.z = 0;


        transform.up = direction;
    }

    private void OnGUI()
    {
        ballsCountText.text = "x" + ballsCount;
    }

    private IEnumerator ShootBalls()
    {
        isGrounded = false;
        arrow.SetActive(false);
        ballsInAir = startingBallsAmount; // Устанавливаем количество шариков, отправленных в полет

        for (int i = 0; i < clonedBalls.Count; i++)
        {
            clonedBalls[i].transform.localPosition = transform.localPosition;
        }

        rb.AddForce(transform.up * dashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.05f);

        for (int i = 0; i < clonedBalls.Count; i++)
        {
            clonedBalls[i].gameObject.SetActive(true);
            clonedBalls[i].GetComponent<Rigidbody2D>().AddForce(transform.up * dashForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            ballsCount--;

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void InstantiateBalls()
    {
        for (int i = 0; i < startingBallsAmount; i++)
        {

            clonedBalls.Add(Instantiate(ballPrefab, transform.parent, false));
            clonedBalls[clonedBalls.Count - 1].transform.localPosition = transform.localPosition;
            clonedBalls[clonedBalls.Count - 1].transform.localScale = transform.localScale;
            clonedBalls[clonedBalls.Count - 1].gameObject.SetActive(false);





        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("TopBound") && !isGrounded)
        {
            //onLanding.Invoke();
            isGrounded = true;
            rb.velocity = Vector3.zero;
            CheckAllBallsReturned(); // Проверяем, вернулись ли все шарики
        }
    }

    public void CheckAllBallsReturned()
    {
        if (ballsInAir <= 0) return; // Если шариков в полете нет, игнорируем
        ballsInAir--; // Уменьшаем количество активных шариков
        if (ballsInAir == 0) // Если все вернулись
        {
            try
            {
                endlessLevelController.OnPlayerLand();
            }
            catch
            {
                levelController.OnPlayerLand();
            }
        }
    }




}
