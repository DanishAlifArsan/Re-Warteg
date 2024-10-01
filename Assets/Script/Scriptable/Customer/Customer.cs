using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable/Customer")]
public class Customer : ScriptableObject
{
    public float walkSpeed;
    public float acceleration;
    public float eatDuration;
    public int maxNumberOfFoods = 1;
    public CustomerAI prefab;
    public AudioClip happySound;
    public AudioClip angrySound;
}
