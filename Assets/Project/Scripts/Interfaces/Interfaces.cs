public interface IDamagable
{
    void ApplyDamage(float damageAmount);
}

public interface IInteractable
{
    void Interact();
}

public interface IShootable
{
    public void Shoot(RaycastHandler raycastHandler);
    public void StopShooting();
}