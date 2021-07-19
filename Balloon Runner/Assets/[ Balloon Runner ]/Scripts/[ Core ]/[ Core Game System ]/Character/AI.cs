using UnityEngine;
using UnityEngine.Events;

public class AI : MonoBehaviour
{
    private Rigidbody rb;
    public Rigidbody Rigidbody { get { return (rb == null) ? rb = GetComponent<Rigidbody>() : rb; } }

    private Collider coll;
    public Collider Collider { get { return (coll == null) ? coll = GetComponent<Collider>() : coll; } }

    private bool isDead;
    public bool IsDead { get { return (isDead); } set { isDead = value; } }

    #region Events

    [HideInInspector] public UnityEvent OnAIDie = new UnityEvent();
    [HideInInspector] public UnityEvent OnAIHeal = new UnityEvent();
    [HideInInspector] public UnityEvent OnAIHit = new UnityEvent();
    [HideInInspector] public UnityEvent OnAIGetHit = new UnityEvent();

    #endregion

    private void OnEnable()
    {
        IsDead = false;
        CharacterManager.Instance.AddCharacter(this);
    }

    private void OnDisable()
    {
        CharacterManager.Instance.RemoveCharacter(this);
    }

    public void KillCharacter()
    {
        if (IsDead)
            return;

        IsDead = true;
        //IsControlable = false;
        OnAIDie.Invoke();
    }

    public void ReviveCharacter()
    {
        if (!IsDead)
            return;

        IsDead = false;
        //IsControlable = false;
    }
}