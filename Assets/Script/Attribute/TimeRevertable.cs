﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRevertable : MonoBehaviour
{
    private const int listSize = 120;
    // 이게 느리다면 직접 자료구조를 만들어야 할 듯
    private LinkedList<Vector3> positions = new LinkedList<Vector3>();
    LinkedListNode<Vector3> positionsLastNode;

    private bool isReverting;
    private int revertingCount;

    private Vector3 beforeVelocity;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        isReverting = false;
    }

    private void Update()
    {
        // 지금이 돌아가는 상태인지 아닌지 판단
        if (isReverting)
        {
            // 리스트에 노드가 없거나 애초에 의도한 프레임이 되면 reverting이 끝남
            if (revertingCount <= 0 || positions.Count == 0)
            {
                FinishRevert();
                return;
            }

            revertingCount--;
            positionsLastNode = positions.Last;
            transform.position = positionsLastNode.Value;
            positions.RemoveLast();
        }
        else
        {
            if (positions.Count >= listSize)
                positions.RemoveFirst();
            positions.AddLast(transform.position);
        }
    }

    private void FinishRevert()
    {
        isReverting = false;
        revertingCount = 0;
        rigidBody.velocity = beforeVelocity;
    }

    public void StartRevertFor(int revertingCnt)
    {
        beforeVelocity = rigidBody.velocity;
        revertingCount = revertingCnt;
        isReverting = true;
    }
}