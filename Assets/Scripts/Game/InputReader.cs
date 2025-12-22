using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const int AttackMouseButtonIndex = 0;
    
    public bool IsJump { get; private set; }
    public bool IsAttack { get; private set; }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsJump = true;
        }
        
        if (Input.GetMouseButtonDown(AttackMouseButtonIndex))
        {
            IsAttack = true;
        }
    }
    
    public void ResetJump()
    {
        IsJump = false;
    }
    
    public void ResetAttack()
    {
        IsAttack = false;
    }
}
