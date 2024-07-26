using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab;
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject staminaPrefab;

    public void DropItem()
    {
        int randomNum = Random.Range(1, 11);

        if (randomNum == 1)
        {
            if (GetComponent<EnemyHealth>()) return;
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        if (randomNum > 3)
        {
            int randomGoldAmount = Random.Range(1, 4);
            for (int i = 0; i < randomGoldAmount; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
        }
        if (randomNum > 1 && randomNum <= 3)
        {
            if (GetComponent<EnemyHealth>()) return;
            Instantiate(staminaPrefab, transform.position, Quaternion.identity);
        }
    }
}
