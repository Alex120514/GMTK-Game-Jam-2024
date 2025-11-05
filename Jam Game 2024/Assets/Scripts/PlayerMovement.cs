using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float walkSpeed = 1f;
    public float jumpSpeed = 5f;
    public Collider2D collider;
    private Rigidbody2D body;
    private bool isGrounded;

    public Transform groundCheck; // 用于检测玩家与地面的接触
    public float groundCheckRadius = 0.1f; // 检测半径
    public LayerMask groundLayer; // 用于确定哪些对象是地面

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * walkSpeed, body.velocity.y);
        
        // 更新动画速度参数
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        
        // 根据输入控制角色朝向
        if (horizontalInput > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true; // 朝右
        }
        else if (horizontalInput < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false; // 朝左
        }
        
        // 检测是否与地面重叠
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded) 
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            isGrounded = false; // 允许跳跃后，更新状态
        }

        // 计算 y 方向的速度和绝对值
        float verticalVelocity = body.velocity.y;
        float verticalVelocityAbs = Mathf.Abs(verticalVelocity);

        // 调试输出
        animator.SetFloat("VSpeed", verticalVelocity);
        animator.SetFloat("VSpeedAbs", verticalVelocityAbs);

        // 你可以使用 verticalVelocity 和 verticalVelocityAbs 进行其他处理，如更新动画参数等
    }
}
