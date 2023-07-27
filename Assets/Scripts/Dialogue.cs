using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DialogueEvents {
    public AudioManager.Sounds sound;
    public GameObject source;

    public DialogueEvents(AudioManager.Sounds s, GameObject g) {
        sound = s;
        source = g;
    }
}

public class Dialogue : MonoBehaviour {
    public List<DialogueEvents> dialogueEvents;

    public void PlayDialogue(StateController sc) {
        StartCoroutine(Plan(sc));
    }

    public void PlayRandom(StateController sc, float delta) {
        StartCoroutine(RandomEvent(sc));
    }

    public void StopDialogue() {
        StopAllCoroutines();
    }

    IEnumerator Plan(StateController sc) {
        AudioManager am = AudioManager.Instance;

        foreach (DialogueEvents line in dialogueEvents) {
            am.PlaySound(line.sound, line.source);
            while (am.IsPlaying(line.sound))
                yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator RandomEvent(StateController sc) {
        AudioManager am = AudioManager.Instance;

        while (true) {
            int randomIndex = Random.Range(0, dialogueEvents.Count);
            am.PlaySound(dialogueEvents[randomIndex].sound, dialogueEvents[randomIndex].source);
            while (am.IsPlaying(dialogueEvents[randomIndex].sound))
                yield return new WaitForEndOfFrame();
        }
    }
}