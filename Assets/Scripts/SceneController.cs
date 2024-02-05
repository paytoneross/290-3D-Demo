using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    private GameObject enemy;

    void Update()
    {
        if(enemy == null)
        {
            enemy = Instantiate(enemyPrefab) as GameObject;

            int height = Random.Range(2, 5);
            enemy.transform.localScale = new Vector3(1, height, 1);
            enemy.GetComponent<Renderer>().material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

            enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
        }
    }
}
