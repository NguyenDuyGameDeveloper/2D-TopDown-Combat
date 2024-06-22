using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    [SerializeField] private float roamChangeDirFloat = 2f;
    private State state;
    private EnemyPathFinding enemyPathFinding;

    private void Awake()
    {
        enemyPathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }
    private void Start()
    {
        StartCoroutine(RoamingCoroutine());
    }
    private IEnumerator RoamingCoroutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            enemyPathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }
    private Vector2 GetRoamingPosition() => new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
}
