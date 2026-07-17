using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour 
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    }

    private SpawnState state = SpawnState.COUNTING;  //TODO: Maybe Need be public

    [System.Serializable]
    public class Wave
    {
        public string name;
        public int enemyNumInPool;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    private int waveCounter;
    private int wavesLenght;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 1.5f;
    private float waveCountdown;
    
    private float searchCountDown = 1.0f;
    //-------------  
    private int spawnCountDown;


    public GameObject Ajir;

    public float rewardRate = 10.0f;
    float timeToReward;

    bool enemyWithReward;
   
    // Use this for initialization
    void Start () 
    {
        waveCounter = 0;
        wavesLenght = 2;
        waveCountdown = timeBetweenWaves;
        spawnCountDown = 0;
        enemyWithReward = false;

        if(spawnPoints.Length == 0)
        {
            Debug.LogError("No Spawn Point Refrenced");
        }
    }

    void OnEnable()
    {
        waveCountdown = timeBetweenWaves;
        spawnCountDown = 0;
        timeToReward = Time.time + rewardRate;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (state == SpawnState.WAITING)
        {
            if(!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

	    if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }


        //reward
        if (Time.time > timeToReward)
        {
            timeToReward = Time.time + rewardRate;
            enemyWithReward = true;
            
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        
        if (nextWave + 1 > wavesLenght - 1)
        {
            nextWave = 0;
            waveCounter++;

            for (int i = 0; i < waves.Length; i++)
            {
                waves[i].count++;
                waves[i].rate += 0.1f;
            }
            if (timeBetweenWaves > 0.09f)
            {
                timeBetweenWaves -= 0.1f;
            }

            //Make Game Harder
            if (waveCounter % 2 == 0)
            {

                if (wavesLenght < waves.Length)
                    wavesLenght++;
                Debug.Log(wavesLenght + "  " + waves.Length);
            }
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;
        
        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyNumInPool);
            yield return new WaitForSeconds(1.0f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(int _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //Spawn enemy
        GameObject obj = EnemyObjectPoolingScript.currentEn.GetPooledObjectEn(_enemy);
        if (obj == null) return;


        if(enemyWithReward)
        {
            obj.GetComponent<Enemy>().enemyStats.killReward = true;
            
            enemyWithReward = false;
        }

        //Make Game Harder
        /*
        if (waveCounter % 2 == 0)
        {
            obj.GetComponent<EnemiesSimpleMovement>().speed += 0.04f;
        }
        */

        //Pool Shoot
        obj.transform.position = _sp.position;
        obj.transform.rotation = _sp.rotation;
        obj.SetActive(true);
        
        Ajir.transform.position = _sp.position;
        //Ajir.transform.rotation = _sp.rotation;
        Ajir.SetActive(true);
        spawnCountDown++;
        //Play Enemy Spawn Sound
    }
}
