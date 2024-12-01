using UnityEngine;

public class AnimationStartEventHandler : StateMachineBehaviour
{
    private System.Action onAnimationStart;

    public void Initialize(System.Action callback)
    {
        onAnimationStart = callback;
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 애니메이션 시작 시 호출되는 함수
        OnAnimationStart();
    }

    private void OnAnimationStart()
    {
        // 애니메이션 시작 시 처리해야 할 로직 구현
        onAnimationStart?.Invoke();
    }
}