public class VAnimCharacterController : VAnimController<VAnimCharacterController>
{
    public VClipsVelocity4D walkingStartClips;
    public VClipsVelocity4D walkingLoopClips;
    public VClipsVelocity4D walkingEndClips;
    public VClips4D idleClips;

    public CharacterMovement characterMovement { get; private set; }

    public int loopsLeft { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();

        Initialize();
        ChangeState(new VCharacterIdle(this));
    }
}
