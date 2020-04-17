using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager main;

    EntityManager manager;

    public float xBound = 15f;
    public float yBound = 3f;

    public Text getReady;
    WaitForSeconds delayInitial;
    WaitForSeconds delayBetween;

    public Text[] playerScoreTexts;
    int[] playerScores;

    private void Awake() {
        if(main != null && main != this) {
            Destroy(gameObject);
            return;
        }
        main = this;

        delayInitial = new WaitForSeconds(2f);
        delayBetween = new WaitForSeconds(1f);

        manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        playerScores = new int[2];

        StartCoroutine(CountdownGetReadyText());
        // TODO: verificar uma forma de saber se a coroutine terminou e então chamar o spawn da bola
    }

    public void UpdatePlayerScore(int playerID) {
        playerScoreTexts[playerID].text = (++playerScores[playerID]).ToString();

        StartCoroutine(CountdownGetReadyText());
    }

    IEnumerator CountdownGetReadyText() {
        getReady.text = "Get Ready!";
        yield return delayInitial;

        getReady.text = "3";
        yield return delayBetween;

        getReady.text = "2";
        yield return delayBetween;

        getReady.text = "1";
        yield return delayBetween;

        getReady.text = "0";
        yield return delayBetween;

        getReady.text = "";

        SpawnBall();
    }

    void SpawnBall() {
        var ball = manager.Instantiate(BallPrefabEntity.prefab);

        var dir = new Vector3(UnityEngine.Random.Range(0, 2) == 0 ? -1 : 1, UnityEngine.Random.Range(-.5f, .5f), 0f).normalized;
        var speed = dir * 4;

        var velocity = new PhysicsVelocity() {
            Linear = speed,
            Angular = float3.zero
        };

        manager.AddComponentData(ball, velocity);
    }
}
