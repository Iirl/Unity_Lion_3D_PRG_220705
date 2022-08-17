using UnityEngine;
using Valve.VR.InteractionSystem;

public class SpawnBasketball : MonoBehaviour
{
    [SerializeField]
    GameObject basketBall, groundBall;
    UIElement btnBskt, btnUnsom;
    Transform playerTrns;
    [SerializeField, Header("位置資訊")]
    private Vector3 offset= new Vector3(1,2,1);

    private void Awake()
    {
        btnBskt = GameObject.Find("召喚籃球").GetComponent<UIElement>();
        btnUnsom = GameObject.Find("解除召喚").GetComponent<UIElement>();
        playerTrns = GameObject.Find("Player").GetComponent<Transform>();
        btnBskt.onHandClick.AddListener((a) => { if (!groundBall) groundBall = Instantiate(basketBall,playerTrns.position+ offset, playerTrns.rotation); });
        btnUnsom.onHandClick.AddListener((a) => { if (groundBall) Destroy(groundBall); });

    }
}
