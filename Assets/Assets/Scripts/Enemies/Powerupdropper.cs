using UnityEngine;

public class Powerupdropper : MonoBehaviour
{
    [SerializeField] private PowerupEffect[] possiblePowerUps;
    [SerializeField] private float dropChance = 0.2f;

    public void DropPowerUp()
    {
        if (Random.value > dropChance)
            return;

        if (possiblePowerUps == null || possiblePowerUps.Length == 0)
            return;

        PowerupEffect powerupEffect = possiblePowerUps[
            Random.Range(0, possiblePowerUps.Length)
        ];

        GameObject droppedPowerUp = Instantiate(
            powerupEffect.powerupPrefab,
            transform.position,
            Quaternion.identity
        );

        droppedPowerUp.GetComponent<Powerup>().powerup = powerupEffect;
    }
}