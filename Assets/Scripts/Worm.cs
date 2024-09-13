using TMPro;
using UnityEngine;

public class Worm : MonoBehaviour
{
    public static Worm instance;
    public static Worm Instance
    { get { return instance; } }

    public GameObject WormPrefab;
    public GameObject WormTail;

    [SerializeField] float sec;
    [SerializeField] GameObject gameover = null;
    [SerializeField] TMP_Text Keycounts = null;

    private float basicSec;
    private float cooltime = 0f;    // 
    private int keycount = 0;       // 0 �� 1�� ������ ���� �ȵ��� ���� ���� ����
    private float keytime = 0;      // keycount �ð��� ������� ����
    Vector3 FirstTailPos = Vector3.zero;    // ù��° ������ ���� ��ġ

    public int count = 0;      // �� = ���� ����
    public Vector3 presentPos = Vector2.zero; // ���� Worm��ġ
    public Vector3 afterPos = Vector2.zero;   // �����̰� �� �� Worm ��ġ

    Tail tail = null;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        basicSec = sec;
    }

    public void Update()
    {
        if (keycount == 0)
        {
            Keycounts.text = "dirMove :  X";
        }
        else if (keycount == 1)
        {
            Keycounts.text = "dirMove : O";
        }

        cooltime += Time.deltaTime;
        keytime += Time.deltaTime;

        if (keytime > 1)
        {
            keycount = 1;
            keytime = 0;
        }

        WormMove();
        keyDown();
    }

    public void WormMove()
    {
        if (cooltime > sec)
        {
            if (tail != null)
            {
                tail.ReceivePosition(WormPrefab.transform.rotation, WormPrefab.transform.position);
                tail.TailMove();
            }

            WormPrefab.transform.Translate(new Vector3(0, 0, 1));

            cooltime = 0;
        }
    }

    private void keyDown()
    {
        if (Input.GetKeyDown(KeyCode.A) && keycount >= 1)
        {
            WormPrefab.transform.Rotate(new Vector3(0f, -90f, 0f));
            keycount--;
        }
        else if (Input.GetKeyDown(KeyCode.D) && keycount >= 1)
        {
            WormPrefab.transform.Rotate(new Vector3(0f, 90f, 0f));
            keycount--;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            sec /= 2;
            Time.timeScale = 2;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            sec = basicSec;
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Time.timeScale = 0;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Time.timeScale = 1;
        }
    }

    public void FirstTailAdd()
    {
        FirstTailPos = WormPrefab.transform.position - WormPrefab.transform.forward;
        Tail firsttail = WormTail.GetComponent<Tail>();
        tail = Instantiate(firsttail, FirstTailPos , Quaternion.identity);
        tail.transform.rotation = WormPrefab.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.SetActive(false);

        if (count < 1)
        {
            FirstTailAdd();
        }
        else
        {
            tail.Tailadd();
        }

        count++;
        Debug.Log(count);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || LayerMask.LayerToName(collision.gameObject.layer) == "Tail")
        {
            gameover.SetActive(true);
            Time.timeScale = 0;
        }

    }

}
