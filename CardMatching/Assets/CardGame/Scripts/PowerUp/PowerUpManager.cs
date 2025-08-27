using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    private List<PowerUp> activePowerUps = new List<PowerUp>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void ActivatePowerUp(PowerUp powerUp)
    {
        activePowerUps.Add(powerUp);
        powerUp.Activate();
    }

    public void DeactivatePowerUp(PowerUp powerUp)
    {
        if (activePowerUps.Contains(powerUp))
        {
            powerUp.Deactivate();
            activePowerUps.Remove(powerUp);
        }
    }
}