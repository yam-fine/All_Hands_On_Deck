using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimbing : IsState
{
    public void OnEnter(StateController sc) {
        throw new System.NotImplementedException();
    }

    public void OnExit(StateController sc) {
        // fade to black and teleport player to hull
    }

    public void UpdateState(StateController sc) {
        throw new System.NotImplementedException();
    }
}
