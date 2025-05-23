using UnityEngine;

public class RevealManager : MonoBehaviour
{
    public int requiredReveals = 3;
    private int currentReveals = 0;

    public GameObject enemyToSpawn; // l’ennemi désactivé au début
    public Transform spawnPoint;    // l’endroit où le faire apparaître (facultatif)

    public void ObjectRevealed()
    {
        currentReveals++;

        if (currentReveals >= requiredReveals)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyToSpawn != null)
        {
            if (spawnPoint != null)
            {
                enemyToSpawn.transform.position = spawnPoint.position;
            }

            enemyToSpawn.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Enemy to spawn non assigné dans RevealManager.");
        }
    }
}
