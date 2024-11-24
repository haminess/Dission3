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
        // �ִϸ��̼� ���� �� ȣ��Ǵ� �Լ�
        OnAnimationStart();
    }

    private void OnAnimationStart()
    {
        // �ִϸ��̼� ���� �� ó���ؾ� �� ���� ����
        onAnimationStart?.Invoke();
    }
}