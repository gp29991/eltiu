using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerupSpawner : MonoBehaviour
{

    public GameObject bomb;
    public GameObject inv;
    private float minX;
    private float maxX;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        float offset = 0.3f;
        minX = (((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) * -1) + offset;
        maxX = ((Camera.main.aspect * (Camera.main.orthographicSize * 2)) / 2) - offset;
        StartCoroutine(beginSpawn());
    }

    IEnumerator beginSpawn()
    {
        yield return new WaitForSeconds(Random.Range(10f, 20f));
        playerController pc = player.GetComponent<playerController>();
        if (!pc.hasBomb || !pc.hasInv)
        {
            int random = RandomPower(pc);
            GameObject k = null;

            switch (random)
            {
                case 1:
                    k = Instantiate(bomb);
                    break;
                case 2:
                    k = Instantiate(inv);
                    break;
            }

            float x = Random.Range(minX, maxX);

            k.transform.position = new Vector2(x, transform.position.y);
        }

        StartCoroutine(beginSpawn());
    }

    int RandomPower(playerController pc)
    {
        bool validPower = false;
        int random = 0;
        while (!validPower)
        {
            random = Random.Range(1, 3);
            switch (random)
            {
                case 1:
                    if (!pc.hasBomb)
                    {
                        validPower = true;
                    }
                    break;
                case 2:
                    if (!pc.hasInv)
                    {
                        validPower = true;
                    }
                    break;
            }
        }
        return random;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
