using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private string _isDeadAnimationKey;

    private bool _isDead = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_isDead)
        {
            _isDead = true;
            _animator.SetBool(_isDeadAnimationKey, true);

            GetComponent<InputReader>().enabled = false;
        }
    }
}
