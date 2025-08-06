using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float WASD_SPEED = 5f;    //镜头水平移动速度
    public float UD_SPEED = 1f;      //镜头垂直移动速度

    public float MIN_Y = 0.5f;       //镜头最低y坐标

    void Update()
    {
        ////////////////////移动//////////////////
        Vector3 wasdVector = (
            transform.forward * Input.GetAxis("Vertical")
            + transform.right * Input.GetAxis("Horizontal")
        ).normalized;   //计算前后左右位移矢量

        float upDown = 0f;
        if (Input.GetKey(KeyCode.Space)) upDown += UD_SPEED;
        else if (Input.GetKey(KeyCode.C)) upDown -= UD_SPEED;
        Vector3 upDownVector = Vector3.up * upDown;   //计算上下位移矢量

        Vector3 wasdMove = wasdVector * WASD_SPEED * Time.deltaTime;
        Vector3 upDownMove = upDownVector * UD_SPEED * Time.deltaTime;  //位移

        Vector3 nextPosition = transform.position + wasdMove + upDownMove;  //位移后坐标

        if (nextPosition.y < MIN_Y)        //限制y轴移动
        {
            nextPosition.y = MIN_Y;
        }

        transform.position = nextPosition;   //执行移动
    }
}
