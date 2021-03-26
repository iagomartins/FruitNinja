using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitInstancer : MonoBehaviour
{
    public static FruitInstancer instance;
    public int playerPoints = 0;
    public int playerHealth = 10;
    public bool isGameOver = false;
    public GameObject gameOverScreen;
    public TMP_Text playerPointsHUD;
    public TMP_Text playerHealthHUD;
    [System.Serializable]
    public class Pool {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    public List<Pool> myPools;
    public Dictionary<string, Queue<GameObject>> pools;
    public float averageForce;
    public float spawnTime;
    public GameObject fruitPrefab;
    //public GameObject bombPrefab;
    public Transform spawnPoint; //Fix collisions
    
    private float countdown = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        pools = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in myPools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            pools.Add(pool.tag, objectPool);
        }
        //InvokeRepeating("SpawnObject", 3.0f, spawnTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameOver) {
            gameOverScreen.SetActive(true);
            Time.timeScale = 0;
            return;
        }
        if(countdown < spawnTime) {
            countdown += Time.fixedDeltaTime / spawnTime;
        }
        else {
            int _nextObject = Random.Range(0, 2); //Sorteia bomba ou fruta.
            SpawnFromPool(myPools[_nextObject].tag, spawnPoint.position, Quaternion.identity);
            countdown = 0;
        }
    }

    private void Update()
    {
        playerPointsHUD.text = "Score: " + playerPoints.ToString();
        playerHealthHUD.text = "Health: " + playerHealth.ToString();
        if(playerHealth <= 0) {
            isGameOver = true;
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation) {
        if(pools.ContainsKey(tag)) {
            float xForce = Random.Range(-2.0f, 2.0f);
            float yForce = Random.Range(averageForce - 5.0f, averageForce + 5.0f);
            GameObject objectToSpawn = pools[tag].Dequeue();
            Rigidbody rb = objectToSpawn.GetComponent<Rigidbody>();
            Vector3 force = new Vector3(xForce, yForce, 0);
            objectToSpawn.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            rb.AddForce(force, ForceMode.Impulse);

            pools[tag].Enqueue(objectToSpawn);

            return objectToSpawn;
        }

        return null;
    }

    // void SpawnObject() {
    //     float xForce = Random.Range(-2.0f, 2.0f);
    //     float yForce = Random.Range(averageForce - 5.0f, averageForce + 5.0f);
    //     GameObject nextItem = Instantiate(fruitPrefab, transform.position, Quaternion.identity);
    //     Rigidbody rb = nextItem.GetComponent<Rigidbody>();
    //     Vector3 force = new Vector3(xForce, yForce, 0);
    //     rb.AddForce(force, ForceMode.Impulse);
    // }
}
