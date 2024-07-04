using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    #region Info
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilePerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = .1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [Tooltip("Stagger has to be enabled for oscillate to work properly")]
    [SerializeField] private bool oscillate;

    private bool isShooting = false;
    #endregion
    private void OnValidate()
    {
        if (oscillate) stagger = true;
        if (!oscillate) stagger = false;
        if (projectilePerBurst < 1) projectilePerBurst = 1;
        if (burstCount < 1) burstCount = 1;
        if (timeBetweenBursts < .1f) timeBetweenBursts = .1f;
        if (restTime < .1f) restTime = .1f;
        if (startingDistance < .1f) startingDistance = .1f;
        if (angleSpread == 0) projectilePerBurst = 1;
        if (bulletMoveSpeed <= 0) bulletMoveSpeed = .1f;
    }
    public void Attack()
    {
        if (!isShooting)
        {
            StartCoroutine(BurstCoroutine());
        }
    }
    private IEnumerator BurstCoroutine()
    {
        isShooting = true;
        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger)
            timeBetweenProjectiles = timeBetweenBursts / projectilePerBurst;

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

            if (oscillate && i % 2 != 1)
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            else if (oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < projectilePerBurst; j++)
            {
                Vector2 pos = FindBulletSpawnPos(currentAngle);
                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent(out Projectile projectile))
                {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }
                currentAngle += angleStep;

                if (stagger)
                    yield return new WaitForSeconds(timeBetweenProjectiles);
            }
            currentAngle = startAngle;

            if (!stagger)
                yield return new WaitForSeconds(timeBetweenBursts);
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float _startAngle, out float _currentAngle, out float _angleStep, out float _endAngle)
    {
        Vector2 targetDir = PlayerController.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        _startAngle = targetAngle;
        _endAngle = targetAngle;
        _currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        _angleStep = 0;
        if (angleSpread != 0)
        {
            _angleStep = angleSpread / (projectilePerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            _startAngle = targetAngle - halfAngleSpread;
            _endAngle = targetAngle + halfAngleSpread;
            _currentAngle = _startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float _currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(_currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(_currentAngle * Mathf.Deg2Rad);
        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
