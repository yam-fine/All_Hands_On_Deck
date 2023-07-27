public interface IsState {
    public void OnEnter(StateController sc);
    public void UpdateState(StateController sc);
    public void OnExit(StateController sc);
}