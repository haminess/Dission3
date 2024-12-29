using UnityEngine;

public class FaceExpression : MonoBehaviour
{
    public enum FACE_TYPE
    {
        Face_1,
        Face_2,
        Face_3,
        Face_4,
        Face_5,
        Face_6,
        None,
    }

    private Animator animator;
    public FACE_TYPE type;

    private void Start() 
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            gameObject.AddComponent<Animator>();
        }
    }

    [ContextMenu("ChangeFace")]
    public void ChangeFace()
    {
        string name = type.ToString();  // enum을 문자열로 변환
        animator.Play(name);
    }
}