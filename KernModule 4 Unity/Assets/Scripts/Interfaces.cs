public interface IPickupable {
    void Pickup(Player player);
}

public interface IInteractable {
    void Use(Player player);
}

public interface IDamageable {
    void Hit(DamageInfo damage);
}

public interface IPlayerOwnedObject {
    Player Owner { get; }
    void InitOwnership(Player player);
}