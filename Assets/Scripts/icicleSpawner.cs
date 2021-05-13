using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class icicleSpawner : MonoBehaviour
{ 
    public GameObject icicle;
    private float minX;
    private float maxX;
    public GameObject player;
    private float diffMod = 1.3f;
    private Color[] rainbowArray;
    

    // Start is called before the first frame update
    void Start()
    {
        float offset = 0.3f;
        minX = (((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) * -1) + offset;
        maxX = ((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) - offset;
        rainbowArray = new Color[7];
        rainbowArray[0] = new Color(1f, 0f, 0f, 1f);
        rainbowArray[1] = new Color(1f, 0.5f, 0f, 1f);
        rainbowArray[2] = new Color(1f, 1f, 0f, 1f);
        rainbowArray[3] = new Color(0f, 1f, 0f, 1f);
        rainbowArray[4] = new Color(0f, 0f, 1f, 1f);
        rainbowArray[5] = new Color(0.29f, 0f, 0.51f, 1f);
        rainbowArray[6] = new Color(0.93f, 0.51f, 0.93f, 1f);
        StartCoroutine(beginSpawn());
    }
    
    IEnumerator beginSpawn()
    {
        playerController pc = player.GetComponent<playerController>();
        yield return new WaitForSeconds(Random.Range(1f/Mathf.Pow(diffMod, pc.level - 1f), 2f/ Mathf.Pow(diffMod, pc.level - 1f)));

        GameObject k = Instantiate(icicle);
        Color c = new Color(1f, 1f, 1f, 1f);
        IEnumerator coroutine = Rainbow(k);
        switch (pc.level)
        {
            case 1:
                c = new Color(1f, 1f, 1f, 1f);
                ChangeColor(k, c);
                break;
            case 2:
                c = new Color(0.24f, 0.78f, 1f, 1f); //Color(0.51f, 0.59f, 1f, 1f);
                ChangeColor(k, c);
                break;
            case 3:
                c = new Color(0.70f, 0.47f, 0.78f, 1f);
                ChangeColor(k, c);
                break;
            case 4:
                c = new Color(0f, 1f, 1f, 1f);
                ChangeColor(k, c);
                break;
            case 5:
                c = new Color(1f, 0.75f, 0.97f, 1f);
                ChangeColor(k, c);
                break;
            default:
                //c = new Color(0f, 1f, 1f, 1f);
                //ChangeColor(k, c);
                StartCoroutine(coroutine);
                break;
        }

        void ChangeColor(GameObject k, Color c)
        {
            k.GetComponent<SpriteRenderer>().color = c;
            var psc = k.transform.GetChild(0).GetComponent<ParticleSystem>().main;
            psc.startColor = c;
        }

        IEnumerator Rainbow(GameObject k)
        {
            int counter = 0;
            while (true)
            {
                try
                {
                    //int r = Random.Range(0, 7);
                    ChangeColor(k, rainbowArray[counter]);
                    counter++;
                    if(counter == rainbowArray.Length)
                    {
                        counter = 0;
                    }
                }
                catch (MissingReferenceException)
                {
                    yield break;
                }
                yield return new WaitForSeconds(0.1f);
            }  
        }
        

        float x = Random.Range(minX, maxX);

        k.transform.position = new Vector2(x, transform.position.y);

        StartCoroutine(beginSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
