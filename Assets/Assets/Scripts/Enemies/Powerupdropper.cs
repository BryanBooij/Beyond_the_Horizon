using UnityEngine;

public class Powerupdropper : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private PowerupEffect[] possiblePowerUps;
    [SerializeField] private float dropChance = 0.2f;

    public void DropPowerUp()
    {
        if (Random.value > dropChance)
            return;

        PowerupEffect powerupEffect = possiblePowerUps[
            Random.Range(0, possiblePowerUps.Length)
        ];

        GameObject droppedPowerUp = Instantiate(
            powerUpPrefab,
            transform.position,
            Quaternion.identity
        );

        droppedPowerUp.GetComponent<Powerup>().powerup = powerupEffect;
    }
}