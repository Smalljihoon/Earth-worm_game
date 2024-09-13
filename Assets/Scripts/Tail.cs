using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tail : MonoBehaviour
{
    Quaternion rotate = Quaternion.identity;
    Vector3 movePos = Vector3.zero;

    Tail nextTail = null;

    public void Tailadd()
    {
        if (nextTail == null)
        {
            Vector3 addtailPos = transform.position - transform.forward;
            Tail tails = Worm.instance.WormTail.GetComponent<Tail>();
            nextTail = Instantiate(tails, addtailPos, Quaternion.identity).GetComponent<Tail>();
        }
        else
        {
            nextTail.Tailadd();
        }
    }

    public void ReceivePosition(Quaternion rot, Vector3 pos) // 포지션 받을때 회전값도 같이 받기
    {
        rotate = rot;
        movePos = pos;

        if(nextTail != null)
        {
            nextTail.ReceivePosition(transform.rotation, transform.position);
        }
    }

    public void TailMove()
    {
        transform.position = movePos;
        transform.rotation = rotate;

        if (nextTail != null)
        {
            nextTail.TailMove();
        }
    }
}
