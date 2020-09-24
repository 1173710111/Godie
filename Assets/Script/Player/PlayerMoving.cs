using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    [Tooltip("水平移动速度")]
    public float m_speed;

    private bool is_lookingRight = true;
    //是否与楼梯接触，0表示未接触，1表示上楼梯，2表示下楼梯;
    private int is_onStairs = 0;
    //是否与狗子接触，0表示未接触，1表示要摸狗子，2表示要喂罐头；
    private int is_touchDog = 0;
    //切换场景,0表示无动作，1表示可以切换场景；
    private bool is_changeScene = false;
    //开开关/关开关，0表示无动作，1表示开开关，2表示关开关；
    private int is_switch = 0;
    //捡起字条，0表示无动作，1表示可以捡起字条；
    private bool is_pickUpPaper = false;
    //查看墙上刻字，0表示无动作，1表示可以查看；
    private bool is_checkWall = false;


    void Start()
    {

    }

    private void MoveLeft()
    {
        is_lookingRight = false;
        Transform spriteTransform = transform.GetChild(0);
        if (is_lookingRight)
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 0.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        else
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 180.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        transform.Translate(Vector3.left * m_speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        is_lookingRight = true;
        Transform spriteTransform = transform.GetChild(0);
        if (is_lookingRight)
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 0.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        else
        {
            Quaternion look = new Quaternion(spriteTransform.rotation.x, 180.0f, spriteTransform.rotation.z, spriteTransform.rotation.w);
            spriteTransform.rotation = look;
        }
        transform.Translate(Vector3.right * m_speed * Time.deltaTime);
    }

    /// <summary>
    /// 当玩家按下交互键时调用此方法执行相应操作
    /// </summary>
    private void Interaction()
    {
        #region 上下楼梯
        switch (is_onStairs)
        {
            case 0:
                break;
            case 1:
                #region 上楼梯
                GameController.LoadScene(1);
                #endregion
                break;
            case 2:
                #region 下楼梯
                #endregion
                break;
            default:
                break;
        }
        #endregion

        #region 摸狗头/喂罐头
        switch (is_touchDog)
        {
            case 0:
                break;
            case 1:
                #region 摸摸狗头
                #endregion
                break;
            case 2:
                #region 给狗子吃罐头
                #endregion
                break;
            default:
                break;
        }
        #endregion

        #region 切换场景
        if (is_changeScene)
        {

        }
        #endregion

        #region 开开关、关开关
        switch (is_switch)
        {
            case 0:
                break;
            case 1:
                #region 开开关
                #endregion
                break;
            case 2:
                #region 关开关
                #endregion
                break;
        }
        #endregion

        #region 捡起字条
        if (is_pickUpPaper)
        {

        }
        #endregion

        #region 查看墙上刻字
        if (is_checkWall)
        {

        }
        #endregion
    }
    /// <summary>
    /// 当player走到楼梯口时，调用该方法传递信息
    /// </summary>
    /// <param name='is_onStair'>
    /// 是否与楼梯接触，0表示未接触，1表示上楼梯，2表示下楼梯;
    /// </param>
    public void PI_ChangeOnStairs(int is_onStair)
    {
        this.is_onStairs = is_onStair;
    }

    /// <summary>
    /// 当player走到小狗旁边时，调用该方法传递信息
    /// </summary>
    /// <param name='is_touchDog'>
    /// 是否与狗子接触，0表示未接触，1表示要摸狗子，2表示要喂罐头
    /// </param>
    public void PI_ChangeTouchDog(int is_touchDog)
    {
        this.is_touchDog = is_touchDog;
    }

    /// <summary>
    /// 当player走到门前时，调用该方法传递信息
    /// </summary>
    public void PI_ChangeScene(bool flag)
    {
        is_changeScene = flag;
    }

    /// <summary>
    /// 当player走到开关时，调用该方法传递信息
    /// </summary>
    public void PI_Switch(int on_off)
    {
        is_switch = on_off;
    }

    /// <summary>
    /// 当player走到字条时，调用该方法传递信息
    /// </summary>
    public void PI_PickUpPaper(bool flag)
    {
        is_pickUpPaper = flag;
    }

    /// <summary>
    /// 当player走到墙上刻字的地方时，调用该方法传递信息
    /// </summary>
    public void PI_CheckWall(bool flag)
    {
        is_checkWall = flag;
    }
}