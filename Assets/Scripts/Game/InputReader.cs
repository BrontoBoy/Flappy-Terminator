using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const int AttackMouseButtonIndex = 0;
    public const KeyCode JumpKey = KeyCode.Space;
    
    public bool IsJump { get; private set; }
    public bool IsAttack { get; private set; }
    
    private void Update()
    {
        if (Input.GetKeyDown(JumpKey))
            IsJump = true;
        
        if (Input.GetMouseButtonDown(AttackMouseButtonIndex))
            IsAttack = true;
    }
    
    public void ResetJump()
    {
        IsJump = false;
    }
    
    public void ResetAttack()
    {
        IsAttack = false;
    }
    
    public void ResetAllInput()
    {
        ResetJump();
        ResetAttack();
    }
}