using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InvaderGrid : MonoBehaviour
{
    public Invader[] prefabs;//
    public int amountKilled { get; private set; }//
    public int amountAlive => this.totalInvaders - this.amountKilled;//
    public int totalInvaders => this.rows * this.cols;//
    public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;//

    [SerializeField] private AnimationCurve speed;
    [SerializeField] private Projectile missilePrefab;

    private int rows = 3;
    private int cols = 7;
    private Vector3 direction = Vector2.right;
    private float missileAttackRate = 2f;
    
    public GameManager gameManager;
    public AudioManager _audioManagerClass;

    private void Awake()
    {
        for(int row = 0; row < rows; row++) 
        {
            float width = 2.0f * (this.cols -1);
            float height = 2.0f * (this.rows -1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector2 rowPosition = new Vector3(centering.x , centering.y + (row * 2.0f), 0.0f);

            for(int col = 0; col < cols; col++) 
            {
                Invader invader  = Instantiate(this.prefabs[row], this.transform);
                invader.killed += InvaderKýlled;

                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }

        _audioManagerClass = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissisleAttack), this.missileAttackRate, this.missileAttackRate);//

        gameManager = FindObjectOfType<GameManager>();
    }

    public void Update()
    {
        this.transform.position += direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;//

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);//
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);//

        foreach (Transform invader in this.transform)//
        {
            if (!invader.gameObject.activeInHierarchy) 
            {
                continue;
            }

            if(direction == Vector3.right && invader.position.x >= rightEdge.x - 1.0f) 
            {
                AdvanceRow();
            }

            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + 1.0f) 
            {
                AdvanceRow();
            }
        }
    }
    private void AdvanceRow()// 
    {
        direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissisleAttack() //
    {
        foreach (Transform invader in this.transform) 
        {
            if (!invader.gameObject.activeInHierarchy) 
            {
                continue;
            }

            if(Random.value < (1.0f / (float)this.amountAlive)) 
            {
                Instantiate(this.missilePrefab, invader.position , Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKýlled()
    {
        _audioManagerClass.PlaySFX(_audioManagerClass.deadInvadersSound);
        this.amountKilled++;
        gameManager.SetScore();


        if(this.amountKilled >= this.totalInvaders) 
        {
            gameManager.LevelLoaded();
        }
    }
}
