using UnityEngine;

static public class PlayerAnimatorData
{
    static public class Params
    {
        static public readonly int IsMove = Animator.StringToHash(nameof(IsMove));
        static public readonly int IsJump = Animator.StringToHash(nameof(IsJump));
        static public readonly int IsDeath = Animator.StringToHash(nameof(IsDeath));
    }    
}
