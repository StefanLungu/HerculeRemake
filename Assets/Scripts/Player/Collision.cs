using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask platformsLayer;
    public LayerMask bridgeLayer;
    public float collisionRadius;

    public bool onGround;
    public bool hitWall;
    public bool wallRight;
    public bool wallLeft;
    public bool isTouchingLedgeRight;
    public bool isTouchingLedgeLeft;
    public bool isGrabbed;
    public Collider2D checkBridge;
    public bool onBridge;

    public Vector2 groundOffset;
    public Vector2 rightWallOffset;
    public Vector2 leftWallOffset;
    public Vector2 rightLedgeOffset;
    public Vector2 leftLedgeOffset;
    public Color gizmoColorGround = Color.red;

    public Vector2 ledgePosition;

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        bool checkGround = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, groundLayer);
        bool checkPlatform = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, platformsLayer);
        checkBridge = Physics2D.OverlapCircle((Vector2)transform.position + groundOffset, collisionRadius, bridgeLayer);

        if (checkGround || checkPlatform)
        {
            onGround = true;
            onBridge = false;
        }
        else
        {
            onGround = false;         
        }

        if(checkBridge)
        {
            onGround = true;
            onBridge = true;
        }
        
        wallLeft = Physics2D.OverlapCircle((Vector2)transform.position + leftWallOffset, collisionRadius, platformsLayer);
        wallRight = Physics2D.OverlapCircle((Vector2)transform.position + rightWallOffset, collisionRadius, platformsLayer);

        if(wallRight || wallLeft)
        {
            hitWall = true;
        }
        else
        {
            hitWall = false;
        }

        if (wallRight && !Physics2D.OverlapCircle((Vector2)transform.position + rightLedgeOffset, collisionRadius, platformsLayer))
        {
            isTouchingLedgeRight = true;
            ledgePosition = (Vector2)transform.position - rightLedgeOffset;
        }
        else
        {
            if (wallLeft && !Physics2D.OverlapCircle((Vector2)transform.position + leftLedgeOffset, collisionRadius, platformsLayer))
            {
                isTouchingLedgeLeft = true;
                ledgePosition = (Vector2)transform.position - leftLedgeOffset;
            }
            else
            {
                isTouchingLedgeRight = false;
                isTouchingLedgeLeft = false;
                ledgePosition = new Vector2(0, 0);
            }
        }
     
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmoColorGround;
        Gizmos.DrawWireSphere((Vector2)transform.position + groundOffset, collisionRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftWallOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightWallOffset, collisionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere((Vector2)transform.position + rightLedgeOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftLedgeOffset, collisionRadius);

    }
}
