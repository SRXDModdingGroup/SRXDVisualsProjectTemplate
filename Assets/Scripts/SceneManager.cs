using SRXDCustomVisuals.Core;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SceneManager : MonoBehaviour {
    [SerializeField] private VisualElement[] visualElements;
    [SerializeField] private GameObject gameplayOverlay;
    [SerializeField] private Volume postProcessingVolume;

    private VisualsSceneManager sceneManager;
    private bool showGameplayOverlay = true;
    private bool enablePostProcessing = true;
    private float bloom = 0.5f;
    private Bloom bloomComponent;
    private bool holding;
    private bool beatHolding;
    private bool spinningRight;
    private bool spinningLeft;
    private bool scratching;
    private IVisualsEvent hitMatchEvent;
    private IVisualsEvent hitTapEvent;
    private IVisualsEvent hitBeatEvent;
    private IVisualsEvent hitSpinRightEvent;
    private IVisualsEvent hitSpinLeftEvent;
    private IVisualsEvent hitScratchEvent;
    private IVisualsProperty holdingProperty;
    private IVisualsProperty beatHoldingProperty;
    private IVisualsProperty spinningRightProperty;
    private IVisualsProperty spinningLeftProperty;
    private IVisualsProperty scratchingProperty;

    private void Start() {
        var scene = new VisualsScene(visualElements);

        sceneManager = new VisualsSceneManager();
        sceneManager.SetScene(scene);

        hitMatchEvent = sceneManager.GetEvent("HitMatch");
        hitTapEvent = sceneManager.GetEvent("HitTap");
        hitBeatEvent = sceneManager.GetEvent("HitBeat");
        hitSpinRightEvent = sceneManager.GetEvent("HitSpinRight");
        hitSpinLeftEvent = sceneManager.GetEvent("HitSpinLeft");
        hitScratchEvent = sceneManager.GetEvent("HitScratch");

        holdingProperty = sceneManager.GetProperty("Holding");
        beatHoldingProperty = sceneManager.GetProperty("BeatHolding");
        spinningRightProperty = sceneManager.GetProperty("SpinningRight");
        spinningLeftProperty = sceneManager.GetProperty("SpinningLeft");
        scratchingProperty = sceneManager.GetProperty("Scratching");

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
        GUILayout.Label("Events and Properties");
        
        if (GUILayout.Button("Hit Match"))
            hitMatchEvent.Invoke();
        
        if (GUILayout.Button("Hit Tap"))
            hitTapEvent.Invoke();
        
        if (GUILayout.Button("Hit Beat"))
            hitBeatEvent.Invoke();
        
        if (GUILayout.Button("Hit Spin Right"))
            hitSpinRightEvent.Invoke();
        
        if (GUILayout.Button("Hit Spin Left"))
            hitSpinLeftEvent.Invoke();
        
        if (GUILayout.Button("Hit Scratch"))
            hitScratchEvent.Invoke();

        holding = GUILayout.Toggle(holding, "Holding");
        beatHolding = GUILayout.Toggle(beatHolding, "Beat Holding");
        spinningRight = GUILayout.Toggle(spinningRight, "Spinning Right");
        spinningLeft = GUILayout.Toggle(spinningLeft, "Spinning Left");
        scratching = GUILayout.Toggle(scratching, "Scratching");
        
        holdingProperty.SetBool(holding);
        beatHoldingProperty.SetBool(beatHolding);
        spinningRightProperty.SetBool(spinningRight);
        spinningLeftProperty.SetBool(spinningLeft);
        scratchingProperty.SetBool(scratching);
    }
}
