public interface IDestructable
{
    void Destroy();
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