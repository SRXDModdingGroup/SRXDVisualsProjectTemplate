using DefaultNamespace;
using SRXDCustomVisuals.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneManager : MonoBehaviour {
    [SerializeField] private GameObject gameplayOverlay;
    [SerializeField] private Volume postProcessingVolume;

    private bool showGameplayOverlay = true;
    private bool enablePostProcessing = true;
    private float bloom = 0.5f;
    private Bloom bloomComponent;
    private bool holding;
    private bool beatHolding;
    private bool spinningRight;
    private bool spinningLeft;
    private bool scratching;

    private void Start() {
        postProcessingVolume.profile.TryGet(out bloomComponent);
    }

    private void OnGUI() {
        GUILayout.Label("Visuals");
        showGameplayOverlay = GUILayout.Toggle(showGameplayOverlay, "Show Gameplay Overlay");
        enablePostProcessing = GUILayout.Toggle(enablePostProcessing, "Enable Post-Processing");
        GUILayout.Label("Bloom");
        bloom = GUILayout.HorizontalSlider(bloom, 0f, 1f);
        
        gameplayOverlay.SetActive(showGameplayOverlay);
        postProcessingVolume.gameObject.SetActive(enablePostProcessing);

        if (bloomComponent != null)
            bloomComponent.intensity.value = bloom;
        
        GUILayout.Space(20f);
        GUILayout.Label("Events");

        if (GUILayout.Button("Hit Match"))
            SendNoteHit(NoteIndex.HitMatch);
        
        if (GUILayout.Button("Hit Tap"))
            SendNoteHit(NoteIndex.HitTap);
        
        if (GUILayout.Button("Hit Beat"))
            SendNoteHit(NoteIndex.HitBeat);
        
        if (GUILayout.Button("Hit Spin Right"))
            SendNoteHit(NoteIndex.HitSpinRight);
        
        if (GUILayout.Button("Hit Spin Left"))
            SendNoteHit(NoteIndex.HitSpinLeft);
        
        if (GUILayout.Button("Hit Scratch"))
            SendNoteHit(NoteIndex.HitScratch);
        
        UpdateNoteHold(ref holding, GUILayout.Toggle(holding, "Holding"), NoteIndex.Hold);
        UpdateNoteHold(ref beatHolding, GUILayout.Toggle(beatHolding, "Beat Holding"), NoteIndex.HoldBeat);
        UpdateNoteHold(ref spinningRight, GUILayout.Toggle(spinningRight, "Spinning Right"), NoteIndex.HoldSpinRight);
        UpdateNoteHold(ref spinningLeft, GUILayout.Toggle(spinningLeft, "Spinning Left"), NoteIndex.HoldSpinLeft);
        UpdateNoteHold(ref scratching, GUILayout.Toggle(scratching, "Scratching"), NoteIndex.HoldScratch);
    }

    private static void SendNoteHit(NoteIndex index) {
        var visualsEventManager = VisualsEventManager.Instance;
        
        visualsEventManager.SendEvent(new VisualsEvent(VisualsEventType.On, (int) index, 255d));
        visualsEventManager.SendEvent(new VisualsEvent(VisualsEventType.Off, (int) index, 255d));
    }

    private static void UpdateNoteHold(ref bool state, bool newState, NoteIndex index) {
        var visualsEventManager = VisualsEventManager.Instance;
        
        if (!state && newState)
            visualsEventManager.SendEvent(new VisualsEvent(VisualsEventType.On, (int) index, 255d));
        else if (state && !newState)
            visualsEventManager.SendEvent(new VisualsEvent(VisualsEventType.Off, (int) index, 255d));

        state = newState;
    }
}
