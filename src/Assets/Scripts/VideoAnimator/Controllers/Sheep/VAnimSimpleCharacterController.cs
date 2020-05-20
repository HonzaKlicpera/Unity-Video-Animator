public class VAnimSimpleCharacterController : VAnimController<VAnimSimpleCharacterController>
{
    public VClips4D walkingClips;
    public VClips4D idleClips;

    public CharacterSimpleMovement characterMovement { get; private set; }

    public float velocity { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterSimpleMovement>();

        Initialize(false);
        ChangeState(new VSimpleCharacterIdle(this));
    }

    public new void SetLookDirection(float lookX, float lookZ)
    {
        if (lookX > 0)
            lookDirection = LookDirection.RIGHT;
        else
            lookDirection = LookDirection.LEFT;
    }
}
