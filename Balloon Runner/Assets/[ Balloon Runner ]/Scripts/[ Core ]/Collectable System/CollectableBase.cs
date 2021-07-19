using UnityEngine;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    #region Effect Time & Type If Needed

    public enum EffectType { Positive, Negative }
    private EffectType effectType;
    public enum EffectTime { Instant, LongLasting, Delayed }
    public EffectTime effectTime;

    public void SetEffectType(EffectType type) => effectType = type;
    public EffectType GetEffectType() => effectType;
    public void SetEffectTime(EffectTime time) => effectTime = time;
    public EffectTime GetEffectTime() => effectTime;

    #endregion

    // You can add sound & general collectable effects here.
    //public GameObject CollectParticlePrefab;

    /// <summary>
    /// Do something that all collectables does when collected.
    /// </summary>
    public virtual void Collect()
    {
        // You can add sound here.

        //if (CollectParticlePrefab != null)
        //{
        //    ParticleSystem ps = Instantiate(CollectParticlePrefab, new Vector3(transform.position.x, -1f, transform.position.z), Quaternion.Euler(-90f, 0f, 0f)).GetComponent<ParticleSystem>();
        //    ps.Play();
        //}
    }

    public abstract void Use();

    public void Dispose()
    {
        Destroy(gameObject);
    }

    #region Self Destruct If Needed

    private bool isOnGround = false;
    private float timer;
    private float countdown;
    private void Awake()
    {
        isOnGround = false;
        countdown = 5f;
        timer = countdown;
    }
    public virtual void SelfDestruct()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f && !isOnGround)
            Dispose();
    }

    #endregion
}
