
//an interface that will be referenced on all type of weapons
public interface IShootable
{
    void Shoot();
    void Reload();
}

//an interface that will be referenced on all type of objects that will take damage and die or destroyed 
public interface IDamageable
{
    void TakeDamage(int damageAmount);
    void Death();
}

public interface ICollectable
{
    void Collect();
}