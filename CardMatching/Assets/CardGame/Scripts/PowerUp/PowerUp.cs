
public abstract class PowerUp
{
    public string PowerUpName { get; protected set; }
    // Execute powerup logic
    public abstract void Activate();

    // Optional cleanup (if needed after duration ends)
    public virtual void Deactivate() { }
}