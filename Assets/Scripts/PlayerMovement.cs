using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Projectile laserPrefab;
    [SerializeField] private bool laserActive;

    public GameManager gameManager;
    public AudioManager _audioManagerClass;

    private void Awake()
    {
        _audioManagerClass = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }    
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) 
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        if (!laserActive) 
        {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestoryed;
            laserActive = true;
            _audioManagerClass.PlaySFX(_audioManagerClass.bulletSound);

        }
    }

    private void LaserDestoryed() 
    {
        laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader") ||
           collision.gameObject.layer == LayerMask.NameToLayer("Missile")) 
        {
            _audioManagerClass.PlaySFX(_audioManagerClass.gameOverSound);
            gameManager.LevelLoaded();
        }
           
    }
}
