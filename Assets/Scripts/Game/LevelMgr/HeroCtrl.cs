// **********************************************************************
// 
// 文件名(File Name)：             HeroCtrl
// 
// 作者(Author)：                  Circle
// 
// 创建时间(CreateTime):           2016/12/14 17:38:48
//
// 网址：                          http://blog.csdn.net/u013108312
//
// **********************************************************************

using UnityEngine;
using System.Collections;
using System;

public class HeroCtrl : MonoBehaviour ,IEventListener
{
    [Header("角色刚体")]
    public Rigidbody2D playerRigidbody2D;

    [Header("人物动画控制器")]
    public Animator playerAnimator;

    [Header("人物行走速度")]
    public float xSpeed = 40.0f;

    [Header("人物跳跃力的大小")]
    public float ySpeed = 8000;

    /// <summary>
    /// 是否在地面上
    /// </summary>
    private bool isGround = true;

    /// <summary>
    /// 是否跳跃
    /// </summary>
    private bool isJump = false;

    /// <summary>
    /// 人物朝向是不是在左边
    /// </summary>
    private bool isLeft = false;

    /// <summary>
    /// 人物是不是死了
    /// </summary>
    private bool isDie = false;

    [Header("检测人物是否在地上的点")]
    public Transform checkGroudPos;

    [Header("人物踩的层")]
    public LayerMask GroudMask;

    [Header("判断地板的圆形区域 圆的半径")]
    public float radius = 4.5f;

    private bool isGameOver = false;
	void Awake()
	{
        if (AppMgr.Instance.HeroPos == Vector3.zero)
        {
            AppMgr.Instance.HeroPos = transform.position;
        }
        else
        {
            transform.position = AppMgr.Instance.HeroPos;
        }

        if(AppMgr.Instance)
        {
            AppMgr.Instance.AttachEventListener((int)EventDef.LevelEvent.PlayerDie, this);
            AppMgr.Instance.AttachEventListener((int)EventDef.LevelEvent.GameOver, this);
            AppMgr.Instance.AttachEventListener((int)EventDef.LevelEvent.SaveGame, this);
        }
	}
	
    void OnDestroy()
    {
        if (AppMgr.Instance)
        {
            AppMgr.Instance.DetachEventListener((int)EventDef.LevelEvent.PlayerDie, this);
            AppMgr.Instance.DetachEventListener((int)EventDef.LevelEvent.GameOver, this);
            AppMgr.Instance.DetachEventListener((int)EventDef.LevelEvent.SaveGame, this);
        }
    }

	void Start () 
	{
	
	}
	
	void Update () 
	{
        if (isDie || isGameOver)
	    {
            return;
	    }

        if(Input.GetButtonDown("Jump"))
        {
            if (isGround && !isJump)
            {
                isJump = true;
                isGround = false;
                playerRigidbody2D.AddForce(new Vector2(0, ySpeed));
            }
        }
	}

    void FixedUpdate()
    {
        if (isDie || isGameOver)
        {
            return;
        }

        isGround = Physics2D.OverlapCircle(checkGroudPos.position, radius, GroudMask);
        float dir = Input.GetAxis("Horizontal");

        isLeft = false;

        if (dir < -0.01f)
        {
            isLeft = true;
        }

        playerAnimator.SetFloat("Speed", dir);
        playerAnimator.SetBool("Ground", isGround);
        playerAnimator.SetBool("IsLeft", isLeft);
        playerRigidbody2D.velocity = new UnityEngine.Vector2(dir * xSpeed, playerRigidbody2D.velocity.y);
        isJump = false;
    }

    private Vector3 mTragetPos = Vector3.zero;
    void LateUpdate()
    {
        mTragetPos = GetCameraMovePos();
        if (mTragetPos != Camera.main.transform.position)
        {
            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, mTragetPos, 10f);
        }
    }

    [Header("左下角点")]
    public Transform LeftDown;

    [Header("右上角的点")]
    public Transform RightUp;

    Vector3 GetCameraMovePos()
    {
        Vector3 pos = this.transform.position;
        float screenX = SceneToWorldSize(Screen.width * 0.5f, Camera.main,
                                                pos.z);

        pos.y = Camera.main.transform.position.y;
        pos.z = Camera.main.transform.position.z;

        float maxX = RightUp.position.x;
        float minX = LeftDown.position.x;
        if (pos.x - screenX < minX)
        {
            pos.x = minX + screenX;
        }
        else if (pos.x + screenX > maxX)
        {
            pos.x = maxX - screenX;
        }

        return pos;
    }

    /// <summary>
    /// 像素单位转世界单位
    /// </summary>
    /// <param name="size"></param>
    /// <param name="ca"></param>
    /// <returns></returns>
    public float SceneToWorldSize(float size, Camera ca, float Worldz)
    {
        if (ca.orthographic)
        {
            float height = Screen.height / 2;
            float px = (ca.orthographicSize / height);
            return px * size;
        }
        else
        {
            float halfFOV = (ca.fieldOfView * 0.5f);//摄像机夹角 的一半//
            halfFOV *= Mathf.Deg2Rad;//弧度转角度//

            float height = Screen.height / 2;
            float px = height / Mathf.Tan(halfFOV);//得到应该在的Z轴//
            Worldz = Worldz - ca.transform.position.z;
            return (Worldz / px) * size;
        }
    }

    public bool HandleEvent(int id, object param1, object param2)
    {
        EventDef.LevelEvent evid = (EventDef.LevelEvent)id;
        switch(evid)
        {
            case EventDef.LevelEvent.PlayerDie:
                isDie = true;
                playerRigidbody2D.velocity = new UnityEngine.Vector2(0, playerRigidbody2D.velocity.y);
                playerAnimator.SetBool("Die", isDie);
                return false;
            case EventDef.LevelEvent.GameOver:
                isGameOver = true;
                playerRigidbody2D.velocity = new UnityEngine.Vector2(0, playerRigidbody2D.velocity.y);
                playerAnimator.SetFloat("Speed", 0);
                return false;
        }
        return false;
    }

    public int EventPriority()
    {
        return 1000;
    }
}
