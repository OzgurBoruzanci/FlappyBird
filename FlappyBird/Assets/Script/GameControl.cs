using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public GameObject SkyOne;
    public GameObject SkyTwo;

    Rigidbody2D physicsOne;
    Rigidbody2D physicsTwo;

    float Length = 0;
    public float backgroundSpeed;

    public GameObject Barrier;
    public int HowManyBarriers;
    GameObject[] Barriers;
    float changeTime = 0;
    int Counter = 0;
    bool gameOver = true;

    void Start()
    {
        physicsOne = SkyOne.GetComponent<Rigidbody2D>();
        physicsTwo = SkyTwo.GetComponent<Rigidbody2D>();

        physicsOne.velocity = new Vector2(backgroundSpeed, 0);
        physicsTwo.velocity = new Vector2(backgroundSpeed, 0);

        Length = SkyOne.GetComponent<BoxCollider2D>().size.x;
        Barriers = new GameObject[HowManyBarriers];
        for (int i = 0; i < Barriers.Length; i++)
        {
            Barriers[i] = Instantiate(Barrier, new Vector2(-20, -20), Quaternion.identity);
            Rigidbody2D PhysicsBarier = Barriers[i].AddComponent<Rigidbody2D>();
            PhysicsBarier.gravityScale = 0;
            PhysicsBarier.velocity = new Vector2(backgroundSpeed,0);
        }
        
    }

    
    void Update()
    {
        if (gameOver)
        {
            if (SkyOne.transform.position.x <= -Length)
            {
                SkyOne.transform.position += new Vector3(Length * 2, 0);
            }
            if (SkyTwo.transform.position.x <= -Length)
            {
                SkyTwo.transform.position += new Vector3(Length * 2, 0);
            }
            changeTime += Time.deltaTime;
            if (changeTime > 2f)
            {
                changeTime = 0;
                float yAxis = Random.Range(-0.57f, 1.47f);
                Barriers[Counter].transform.position = new Vector3(20, yAxis);
                Counter++;
                if (Counter >= Barriers.Length)
                {
                    Counter = 0;
                }
            }
        }

    }
    public void GameOverGC()
    {
        for (int i = 0; i < Barriers.Length; i++)
        {
            Barriers[i].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            physicsOne.velocity = Vector2.zero;
            physicsTwo.velocity = Vector2.zero;
        }
        gameOver = false;

    }
}
