using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DecorationMainMenu : MonoBehaviour
{
    [SerializeField] private float timeStartDelay = 1.5f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("IsPlay", false);

        StartCoroutine(StartWithDelayRoutine());
    }

    private IEnumerator StartWithDelayRoutine()
    {
        yield return new WaitForSeconds(timeStartDelay);
        _animator.SetBool("IsPlay", true);
    }
}
