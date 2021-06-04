using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.3f;
    [SerializeField] int health = 1000;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0,1)] float deathSoundVolume = 0.5f;
    [SerializeField] bool invincibility = false;

    [Header("Projectile")]
    [SerializeField] ObjectPooler pooler;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileDelay = 0.1f;
    [SerializeField] AudioClip shotSound;
    [SerializeField] [Range(0, 1)] float shotSoundVolume = 0.5f;

    [Header("Triple Projectile")]
    [SerializeField] bool isTripleShootOn = false;
    [SerializeField] float shotAngle = 10f;

    [SerializeField]
    private bool isAutoFiring = true;

    [Header("Death")]
    [SerializeField] Transform spawnPosition;
    [SerializeField] float messageDuration = 4f;
    [SerializeField] float invincibilityDuration = 10f;

    DeathText deathText;
    Animator myAnimator;
    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    bool isDead = false;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        deathText = FindObjectOfType<DeathText>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        SetUpMoveBoundaries();

        yield return new WaitForSeconds(2f);
        myAnimator.enabled = false;

        if (isAutoFiring)
        {
            StartCoroutine(FireContinuously());
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        // if (!isAutoFiring)
        // {
        //     Fire();
        // }
    }

    private void Fire()
    {
        // Remove one of the alt inputs so we don't get two coroutines

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }

        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            Firing();
            if (isTripleShootOn)
            {
                FiringLeft(shotAngle);
                FiringRight(shotAngle);
            }
            PlayAudio(shotSound, shotSoundVolume);
            yield return new WaitForSeconds(projectileDelay);
        }
    }

    private void FiringRight(float shotAngle)
    {
        float xVelocity = (projectileSpeed / Mathf.Tan(90 - shotAngle));

        //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, -shotAngle)) as GameObject;

        GameObject laser = pooler.GetPooledObject();
        if (laser)
        {
            // laser.transform.position = transform.position;
            laser.transform.position = new Vector3(transform.position.x, transform.position.y + 1f) ;
            laser.transform.rotation = Quaternion.Euler(0, 0, -shotAngle);
            laser.SetActive(true);
        }

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, projectileSpeed);
    }

    private void Firing()
    {
        //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;

        GameObject laser = pooler.GetPooledObject();
        if (laser)
        {
            laser.transform.position = new Vector3(transform.position.x, transform.position.y + 1f) ;
            laser.transform.rotation = Quaternion.identity;
            laser.SetActive(true);
        }
        
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
    }

    private void FiringLeft(float shotAngle)
    {
        float xVelocity = (projectileSpeed / Mathf.Tan(90 - Mathf.Abs(shotAngle))) * (shotAngle / -shotAngle) ;
        //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, shotAngle)) as GameObject;

        GameObject laser = pooler.GetPooledObject();
        if (laser)
        {
            laser.transform.position = new Vector3(transform.position.x, transform.position.y + 1f) ;
            laser.transform.rotation = Quaternion.Euler(0, 0, shotAngle);
            laser.SetActive(true);
        }

        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, projectileSpeed);
    }

    private void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;


        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if ((other.CompareTag("Enemy Laser") || other.CompareTag("Enemy")) && !invincibility)
        {
            ProcessHit(damageDealer);
        }
        else if (other.CompareTag("Triple Shoot"))
        {
            
            Debug.Log("Triple Shoot");
            
            if (isTripleShootOn)
                return;
            SetTripleShoot();
            Invoke(nameof(SetTripleShoot), other.GetComponent<TripleShoot>().GetBuff());
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Heal Pill"))
        {
            Debug.Log("Heal Pill");

            health += other.GetComponent<HealPill>().GetHeal();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Invincibility"))
        {
            Debug.Log("Invincibility");

            if (invincibility)
                return;
            SetInvincibility();
            Invoke(nameof(SetInvincibility), other.GetComponent<Invincibility>().GetBuff());
            Destroy(other.gameObject);
        }
    }

    private void SetTripleShoot()
    {
        isTripleShootOn = !isTripleShootOn;
    }

    private void SetInvincibility()
    {
        invincibility = !invincibility;
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0 && !isDead)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        isDead = true;
        PlayAudio(deathSound, deathSoundVolume);

        Fader fader = FindObjectOfType<Fader>();

        yield return fader.FadeOut(1f);
        transform.position = spawnPosition.position;

        if (!deathText.HasMessage())
        {
            deathText.ShowMessage();
        }

        yield return new WaitForSeconds(messageDuration);

        deathText.HideMessage();

        yield return fader.FadeIn(1f);

        health = 1000;
        invincibility = true;
        yield return new WaitForSeconds(invincibilityDuration);
        invincibility = false;
        isDead = false;
    }

    public int GetHealth()
    {
        return health;
    }

    private void PlayAudio(AudioClip audioClip, float volume)
    {
        AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position, volume);
    }


}
