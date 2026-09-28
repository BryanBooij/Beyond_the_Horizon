using UnityEngine;

public class LevelEnemy : MonoBehaviour
{
    public LevelManager levelManager;

    private bool registered = false;

    private void Start()
    {
        if (levelManager != null)
        {
            levelManager.EnemySpawned();
            registered = true;
        }
    }

    private void OnDestroy()
    {
        if (registered && levelManager != null)
        {
            levelManager.EnemyDestroyed();
        }
    }
}