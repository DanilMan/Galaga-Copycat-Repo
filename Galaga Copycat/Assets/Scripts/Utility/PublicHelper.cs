using UnityEngine;

public class Pair<T, U>
{
    public Pair()
    {
    }

    public Pair(T first, U second)
    {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
}

public class AngleBetween
{
    public AngleBetween() { }
    public Quaternion getAngleBetween(GameObject gameObject1, GameObject gameObject2)
    {
        Vector3 direction = gameObject2.transform.position - gameObject1.transform.position;
        Vector3 direction2D = new Vector3(direction.x, direction.y, 0f);
        Quaternion rotation = Quaternion.LookRotation(direction2D, Vector3.forward);
        return rotation;
    }
}
