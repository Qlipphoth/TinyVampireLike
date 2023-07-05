using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletShell : MonoBehaviour
{
    [SerializeField] float raiseSpeed;
    [SerializeField] float raiseTime;
    [SerializeField] float fadeTime;

    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    
    Color defaultColor, fadeColor;
    WaitForSeconds waitForRaiseTime;
    float elapsedTime;

    private void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        defaultColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        fadeColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        waitForRaiseTime = new WaitForSeconds(raiseTime);
    }

    private void OnEnable() {
        float angle = Random.Range(-30f, 30f);
        // rigidbody2D.velocity = Quaternion.AngleAxis(angle, Vector3.forward) * Vector2.up * raiseSpeed;
        rigidbody2D.velocity = Quaternion.Euler(0f, 0f, angle) * Vector2.up * raiseSpeed;  // 这两句作用貌似一致

        spriteRenderer.color = defaultColor;
        rigidbody2D.gravityScale = 3f;

        StartCoroutine(nameof(FadeOut));
    }

    IEnumerator FadeOut() {
        yield return waitForRaiseTime;
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.gravityScale = 0f;

        elapsedTime = 0f;
        while (elapsedTime < fadeTime) {
            spriteRenderer.color = Color.Lerp(defaultColor, fadeColor, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }

}
