using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    private float minX;
    private float maxX;
    private Animator anim;
    private SpriteRenderer sr;

    public float speed = 3f;

    public Text timer_Text;
    public Text pointText;
    private int timer;
    public int points = 0;
    public GameObject restartPanel;
    public int level = 1;
    public Text levelText;
    private static int levelThreshold = 5;
    private int targetScore = levelThreshold;
    public bool hasBomb = false;
    public GameObject bombButton;
    public GameObject flash;
    public bool hasInv = false;
    public bool isInv = false;
    public GameObject invButton;
    public Material flashEffect;
    private Material defaultMaterial;
    public GameObject uiStar;
    public bool hasSpeed = false;
    public GameObject speedButton;
    public GameObject music;
    private sounds soundScript;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        float offset = 0.3f;
        minX = (((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) * -1) + offset;
        maxX = ((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) - offset;
        defaultMaterial = this.gameObject.GetComponent<SpriteRenderer>().material;
        soundScript = music.GetComponent<sounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(CountTime());
        timer = 0;
        restartPanel.gameObject.SetActive(false);
        bombButton.gameObject.SetActive(false);
        invButton.gameObject.SetActive(false);
        speedButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move();
        PlayerBounce();
        pointText.text = "×" + points.ToString();
        if (points >= targetScore)
        {
            level++;
            levelText.text = "POZIOM: " + level.ToString();
            targetScore += levelThreshold;
        }
    }

    void PlayerBounce()
    {
        Vector3 temp = transform.position;

        if( temp.x > maxX)
        {
            temp.x = maxX;
        }
        else if (temp.x < minX)
        {
            temp.x = minX;
        }

        transform.position = temp;

    }

    public void move()
    {
        //float h = Input.GetAxis("Horizontal");
        if (Input.touchCount > 0)
        {
            var h = Input.GetTouch(0);

            Vector3 temp = transform.position;

            if (h.position.x > Screen.width / 2)
            {
                temp.x += speed * Time.deltaTime;
                sr.flipX = false;
                anim.SetBool("walking", true);
            }
            else if (h.position.x < Screen.width / 2)
            {
                temp.x -= speed * Time.deltaTime;
                sr.flipX = true;
                anim.SetBool("walking", true);
            }
            /*else if (h == 0)
            {
                anim.SetBool("walking", false);
            }*/

            transform.position = temp;
        }
        else if (Input.touchCount == 0)
        {
            anim.SetBool("walking", false);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(1f);
        timer++;

        timer_Text.text = timer.ToString();
        StartCoroutine(CountTime());
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Icicle")
        {
            if (!isInv)
            {
                restartPanel.gameObject.SetActive(true);
                Time.timeScale = 0f;
                bombButton.gameObject.SetActive(false);
                invButton.gameObject.SetActive(false);
                speedButton.gameObject.SetActive(false);
            }
        }

        if (collider.tag == "Star")
        {
            points++;
            Destroy(collider.gameObject);
            music.GetComponent<AudioSource>().PlayOneShot(soundScript.starPickup, 1.5f);
            uiStar.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }

        if (collider.tag == "Bomb")
        {
            hasBomb = true;
            bombButton.gameObject.SetActive(true);
            music.GetComponent<AudioSource>().PlayOneShot(soundScript.powerPickup, 1f);
            bombButton.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
            Destroy(collider.gameObject);
        }

        if (collider.tag == "Inv")
        {
            hasInv = true;
            invButton.gameObject.SetActive(true);
            music.GetComponent<AudioSource>().PlayOneShot(soundScript.powerPickup, 1f);
            invButton.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
            Destroy(collider.gameObject);
        }

        if (collider.tag == "Speed")
        {
            hasSpeed = true;
            speedButton.gameObject.SetActive(true);
            music.GetComponent<AudioSource>().PlayOneShot(soundScript.powerPickup, 1f);
            speedButton.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().Play();
            Destroy(collider.gameObject);
        }

    }

    public void Boom()
    {
        hasBomb = false;
        bombButton.gameObject.SetActive(false);
        music.GetComponent<AudioSource>().PlayOneShot(soundScript.bombActivate, 4f);
        StartCoroutine("Flash");
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Icicle");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
    }

    IEnumerator Flash()
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }

    public void Inv()
    {
        hasInv = false;
        invButton.gameObject.SetActive(false);
        music.GetComponent<AudioSource>().PlayOneShot(soundScript.invActivate, 15f);
        isInv = true;
        StartCoroutine("SetInv");
        StartCoroutine("SpriteFlash");
    }

    IEnumerator SetInv()
    {
        //isInv = true;
        yield return new WaitForSeconds(5f);
        isInv = false;
    }

    IEnumerator SpriteFlash()
    {
        //float timeLeft = 5.0f;
        while(isInv) //(timeLeft > 0.0f)
        {
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().material = flashEffect;
            //timeLeft -= 0.1f;
            yield return new WaitForSeconds(0.1f);
            this.gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
            //timeLeft -= 0.1f;
        }
    }

    public void Speed()
    {
        hasSpeed = false;
        speedButton.gameObject.SetActive(false);
        music.GetComponent<AudioSource>().PlayOneShot(soundScript.speedActivate, 10f);
        StartCoroutine("SetSpeed");
    }

    IEnumerator SetSpeed()
    {
        float currentSpeed = speed;
        speed = currentSpeed * 2;
        music.gameObject.GetComponent<AudioSource>().pitch = 1.5f;
        yield return new WaitForSeconds(10f);
        speed = currentSpeed;
        music.gameObject.GetComponent<AudioSource>().pitch = 1f;
    }
}
