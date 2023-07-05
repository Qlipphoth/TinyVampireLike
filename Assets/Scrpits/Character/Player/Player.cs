using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [Header("Input")]
    [SerializeField] PlayerInput input;

    [Header("Move")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float accelerationTime = 3f;
    [SerializeField] float decelerationTime = 3f;
    
    Vector2 curVelocity;
    float elapsedTime;

    Coroutine moveCoroutine;
    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    protected override void Awake() {
        base.Awake();
    }

    private void Start() {
        input.EnableGameplayInput();
    }

    protected override void OnEnable() {
        base.OnEnable();
        input.OnMoveEvent += Move;
        input.OnStopMoveEvent += StopMove;
    }

    private void OnDisable() {
        input.OnMoveEvent -= Move;
        input.OnStopMoveEvent -= StopMove;
    }

#region Move

    private void Move(Vector2 moveInput) {
        moveDirection = moveInput.normalized;
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveCoroutine(accelerationTime, moveDirection * moveSpeed));
        FlipCharacter();
    }

    private void StopMove() {
        if (moveCoroutine != null) {
            StopCoroutine(moveCoroutine);
        }
        moveDirection = Vector2.zero;
        moveCoroutine = StartCoroutine(MoveCoroutine(decelerationTime, Vector2.zero));
    }

    IEnumerator MoveCoroutine(float time, Vector2 moveVelocity) {
        elapsedTime = 0f;
        curVelocity = rigidbody2D.velocity;

        while (elapsedTime < time) {
            rigidbody2D.velocity = Vector2.Lerp(curVelocity, moveVelocity, elapsedTime / time);
            elapsedTime += Time.fixedDeltaTime;
            animator.SetFloat(String2Num.SPEED, rigidbody2D.velocity.magnitude);
            yield return waitForFixedUpdate;
        }

        rigidbody2D.velocity = moveVelocity;
    }

#endregion

#region Fire



#endregion

}
