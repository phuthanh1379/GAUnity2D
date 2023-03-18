using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Enemy enemyProperties;
    [SerializeField] private EnemyPatrol enemyPatrol;

    private void OnEnable()
    {
        enemyProperties.EnemyDead += OnEnemyDead;
    }

    private void OnDisable()
    {
        enemyProperties.EnemyDead -= OnEnemyDead;
    }

    private void OnEnemyDead()
    {
        StopPatrol();
    }

    private void StopPatrol()
    {
        enemyPatrol.StopPatrol();
    }

    public void EnablePatrol(bool isEnable)
    {
        enemyPatrol.enabled = isEnable;
    }
}
