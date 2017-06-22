using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRecallable : MonoBehaviour
{
    public delegate void TimeRevertDel(TimeRecallable timeRevertable);
    public TimeRevertDel AddOrRemoveTimeRevertable;

    private const int listSize = 120;
    // 이게 느리다면 직접 자료구조를 만들어야 할 듯
	private LinkedList<TimeRecallNode> nodes = new LinkedList<TimeRecallNode>();
    private LinkedListNode<TimeRecallNode> lastNode;
    // 지금 시간이 되돌아가고 있는 상황인가
	public bool isReverting { private set; get; }
    // 지금 시간을 되돌릴 수 있는 상황인가(여러가지 이유로 인해 불가능할 수도 있음)
    private bool isRevertable;

    private TimeRecallNode lastFrameNode;
    private new Rigidbody rigidbody;

    private BoxCollider boxcollider;

    private Renderer mRenderer;
    private Animator animator;

    private Material originMaterial;
    private Material outlineMaterial;

    public bool isAnimationBased { private set; get; }
    public string animationName;

    private void Awake()
    {
        isAnimationBased = animationName.Length > 0;
        outlineMaterial = Resources.Load("ExtraAssets/Selected Effect --- Outline/Material/OutlineSprite") as Material;

        lastFrameNode = TimeRecallNode.CreateNode(Vector3.zero, Vector3.zero, Vector3.zero);

        rigidbody = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider>();

        mRenderer = GetComponent<Renderer>();
        originMaterial = mRenderer.material;

        animator = GetComponent<Animator>();

        isReverting = false;
        isRevertable = true;
    }

    private void FixedUpdate()
    {
        // 지금이 돌아가는 상태인지 아닌지 판단
        if (isReverting)
        {
            // 리스트에 노드가 없으면 reverting이 끝남
            if (nodes.Count == 0)
            {
                FinishRevert();
                return;
            }

            lastNode = nodes.Last;
            TimeRecallNode lastTimeRecallNode = lastNode.Value;
            transform.position = lastTimeRecallNode.position;
            transform.eulerAngles = lastTimeRecallNode.eularAngles;
            rigidbody.velocity = lastTimeRecallNode.velocity;
            nodes.RemoveLast();
        }
        else if (lastFrameNode.position != transform.position &&
            lastFrameNode.eularAngles != transform.eulerAngles)
        {
            nodes.AddLast(TimeRecallNode.CreateNode(transform.position, transform.eulerAngles, rigidbody.velocity));
            lastFrameNode = nodes.Last.Value;
        }
    }

    private void FinishRevert()
    {
        isReverting = false;
        if (rigidbody)
            rigidbody.isKinematic = false;
    }

    public void StartRevert()
    {
        isReverting = true;
        if (rigidbody)
            rigidbody.isKinematic = true;
        NotIndicateRevert();
    }

    public void StartRevertAni()
    {
        if (animator != null)
        {
            animator.Play(animationName, 0, 1.0f);
            animator.SetFloat("rewindMult", -1f);
            
        }

        NotIndicateRevert();
    }


    private bool isSelected = false;
    private void OnMouseEnter()
    {
        mRenderer.material = outlineMaterial;
        if (isSelected)
            mRenderer.material.color = new Color(0.5f, 0.9f, 1.0f);
        else
            mRenderer.material.color = Color.white;
    }

    private void OnMouseExit()
    {
        mRenderer.material = originMaterial;
        if (isSelected)
            mRenderer.material.color = new Color(0.5f, 0.9f, 1.0f);
        else
            mRenderer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        AddOrRemoveTimeRevertable(this);
    }

    // 따로 renderer를 관리하는 클래스에 넣든가
    // Revert 만을 위한 이펙트가 구현되든가
    public void IndicateRevert()
    {
        mRenderer.material.color = new Color(0.5f, 0.9f, 1.0f);
        isSelected = true;
    }

    public void NotIndicateRevert()
    {
        mRenderer.material.color = Color.white;
        isSelected = false;
    }
}
