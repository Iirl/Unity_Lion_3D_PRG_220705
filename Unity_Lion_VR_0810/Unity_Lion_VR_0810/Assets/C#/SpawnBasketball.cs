using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpawnBasketball : MonoBehaviour
{
    [SerializeField]
    GameObject basketBall, groundBall;
    UIElement btnBskt, btnUnsom;
    Transform playerTrns;
    [SerializeField, Header("��m��T")]
    private Vector3 offset= new Vector3(1,2,1);

    private void Awake()
    {
        btnBskt = GameObject.Find("�l���x�y").GetComponent<UIElement>();
        btnUnsom = GameObject.Find("�Ѱ��l��").GetComponent<UIElement>();
        playerTrns = GameObject.Find("Player").GetComponent<Transform>();
        btnBskt.onHandClick.AddListener((a) => { if (!groundBall) groundBall = Instantiate(basketBall,playerTrns.position+ offset, playerTrns.rotation); });
        btnUnsom.onHandClick.AddListener((a) => { if (groundBall) Destroy(groundBall); });

    }
}
