using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BirdControl : MonoBehaviour
{
    public Sprite[] BirdSprite;
    SpriteRenderer spriteRenderer;
    bool backAndForthControl = true;
    int birdCounter = 0;
    float birdAnimationTime = 0;
    Rigidbody2D physics;
    public int Score;
    public Text pointText;
    public Text GameOverText;

    GameControl GmCntrl;

    bool GameOver = true;
    AudioSource[] Sounds;
    int HighScore = 0;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        physics = GetComponent<Rigidbody2D>();
        GmCntrl = GameObject.FindGameObjectWithTag("GameControlTag").GetComponent<GameControl>();
        Sounds = GetComponents<AudioSource>();
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameOver)
        {
            physics.velocity = new Vector2(0, 0);
            physics.AddForce(new Vector2(0, 200));
            Sounds[0].Play();
        }
        if (physics.velocity.y>0)
        {
            transform.eulerAngles = new Vector3(0, 0, 45);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -45);
        }

        Animation();
        
    }
    void Animation()
    {
        birdAnimationTime += Time.deltaTime;
        if (birdAnimationTime > 0.2f)
        {
            birdAnimationTime = 0;
            if (backAndForthControl)
            {
                spriteRenderer.sprite = BirdSprite[birdCounter];
                birdCounter++;
                if (birdCounter == BirdSprite.Length)
                {
                    birdCounter--;
                    backAndForthControl = false;
                }
            }
            else
            {
                birdCounter--;
                spriteRenderer.sprite = BirdSprite[birdCounter];
                if (birdCounter == 0)
                {
                    birdCounter++;
                    backAndForthControl = true;
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag=="PointTag")
        {
            Score++;
            pointText.text = "SCORE : " + Score;
            Sounds[1].Play();
        }
        if (col.gameObject.tag=="BarrierTag")
        {
            GameOverText.text = "GAME OVER";
            Sounds[2].Play();
            GmCntrl.GameOverGC();
            GameOver = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Invoke("ReturnToMainMenu", 1);
        }
        if (Score>HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScoreRecord", HighScore);
        }
        
    }
    void ReturnToMainMenu()
    {
        PlayerPrefs.SetInt("ScoreRecord", Score);
        SceneManager.LoadScene("MainMenu");
    }
}
