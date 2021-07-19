using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator == null ? animator = GetComponent<Animator>() : animator; } }

    private Character player;
    public Character Player { get { return player == null ? player = GetComponent<Character>() : player; } }

    #region Animation ID Setup

    private int sizeID = Animator.StringToHash("Size");

    #endregion

    private void OnEnable()
    {
        Player.OnBubbleGumGetBigger.AddListener(GetBigger);
        Player.OnBubbleGumGetSmaller.AddListener(GetSmaller);
        Player.OnBubbleGumPop.AddListener(Pop);
    }

    private void OnDisable()
    {
        Player.OnBubbleGumGetBigger.RemoveListener(GetBigger);
        Player.OnBubbleGumGetSmaller.RemoveListener(GetSmaller);
        Player.OnBubbleGumPop.RemoveListener(Pop);
    }

    private void GetBigger() => Animator.SetInteger(sizeID, Animator.GetInteger(sizeID) + 1);
    private void GetSmaller() => Animator.SetInteger(sizeID, Animator.GetInteger(sizeID) - 1);
    private void Pop() => Animator.SetInteger(sizeID, 0);
}
