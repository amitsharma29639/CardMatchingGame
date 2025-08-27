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

    public void Init(List<PowerUp> powerUps)
    {
        this.activePowerUps = powerUps;
    }

    public void ActivatePowerUp()
    {
        if (activePowerUps.Count <= 0)
        {
            return;
        }

        PowerUp powerUp = activePowerUps[0];
        powerUp.Activate();
        DeactivatePowerUp(powerUp);
    }

    private void DeactivatePowerUp(PowerUp powerUp)
    {
        if (activePowerUps.Contains(powerUp))
        {
            powerUp.Deactivate();
            activePowerUps.Remove(powerUp);
        }
    }
    
    public int PowerUpCount => activePowerUps.Count;
}