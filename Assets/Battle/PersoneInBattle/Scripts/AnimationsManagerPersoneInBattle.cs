using UnityEngine;

public class AnimationsManagerPersoneInBattle : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _animator = GetComponent<PersoneInBattle>().Animator;        
        _spriteRenderer = GetComponent<PersoneInBattle>().SpriteRenderer;        
    }
    
    public void ChangeFlip(Vector3 vector)
    {
        if(vector.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if(vector.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    public void ChangedWalk(bool isWalk)
    {
            _animator.SetBool("Walk", isWalk);
    }

    public void AttackAnimation()
    {
        _animator.SetTrigger("Attack");
    }

    public void HitAnimation()
    {
        _animator.SetTrigger("Hit");
    }
    public void DeadAnimation()
    {
        _animator.SetTrigger("Dead");
    }
}
