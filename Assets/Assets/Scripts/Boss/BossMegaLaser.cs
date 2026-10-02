using System.Collections;
using UnityEngine;

public class BossMegaLaser : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyBossShip boss;
    [SerializeField] private Transform[] chargeOrbs;      // the 3 orbs
    [SerializeField] private Transform beamMuzzle;
    [SerializeField] private Transform beamVisual;
    [SerializeField] private Collider2D beamCollider;
    [SerializeField] private SpriteRenderer beamRenderer;

    [Header("Timing")]
    [SerializeField] private float firstDelay = 8f;
    [SerializeField] private float cooldown = 15f;
    [SerializeField] private float chargeTime = 2f;
    [SerializeField] private float convergeTime = 0.8f;
    [SerializeField] private float fireTime = 1.5f;

    [Header("Beam")]
    [SerializeField] private float beamLength = 30f;
    [SerializeField] private float beamWidth = 1.5f;
    [SerializeField] private float orbSize = 0.6f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip chargeSound;
    [SerializeField] private AudioClip fireSound;
    
    [Header("Beam Flicker")]
    [SerializeField] private Sprite flickerSpriteA;
    [SerializeField] private Sprite flickerSpriteB;
    [SerializeField] private float flickerSpeed = 25f;   // swaps per second

    private bool beamFlickering;

    private Vector3[] orbStartLocalPos;

    private void Start()
    {
        orbStartLocalPos = new Vector3[chargeOrbs.Length];
        for (int i = 0; i < chargeOrbs.Length; i++)
        {
            orbStartLocalPos[i] = chargeOrbs[i].localPosition;
            chargeOrbs[i].gameObject.SetActive(false);
        }

        beamVisual.gameObject.SetActive(false);
        StartCoroutine(MegaLaserLoop());
    }

    private IEnumerator MegaLaserLoop()
    {
        yield return new WaitForSeconds(firstDelay);

        while (true)
        {
            yield return FireSequence();
            yield return new WaitForSeconds(cooldown);
        }
    }

    private IEnumerator FireSequence()
    {
        boss.movementPaused = true;

        if (audioSource != null && chargeSound != null)
            audioSource.PlayOneShot(chargeSound);

        // 1. Charge all Orbs to 1 central point in: 2
        foreach (Transform orb in chargeOrbs)
        {
            orb.localScale = Vector3.zero;
            orb.gameObject.SetActive(true);
        }

        for (float t = 0; t < chargeTime; t += Time.deltaTime)
        {
            float p = t / chargeTime;
            foreach (Transform orb in chargeOrbs)
                orb.localScale = Vector3.one * (orbSize * p);
            yield return null;
        }

        // 2. Orbs fly to muzzle converging to fire: 3
        ShowBeam(0.05f, 0.4f, false);

        for (float t = 0; t < convergeTime; t += Time.deltaTime)
        {
            float p = t / convergeTime;
            for (int i = 0; i < chargeOrbs.Length; i++)
            {
                chargeOrbs[i].position = Vector3.Lerp(
                    chargeOrbs[i].parent.TransformPoint(orbStartLocalPos[i]),
                    beamMuzzle.position, p);
            }
            yield return null;
        }

        foreach (Transform orb in chargeOrbs)
            orb.gameObject.SetActive(false);

        // 3. Fire and enjoy
        if (audioSource != null && fireSound != null)
            audioSource.PlayOneShot(fireSound);

        beamFlickering = true;
        ShowBeam(beamWidth, 1f, true);
        yield return new WaitForSeconds(fireTime);
        
        for (float t = 0; t < 0.3f; t += Time.deltaTime)
        {
            float w = Mathf.Lerp(beamWidth, 0f, t / 0.3f);
            ShowBeam(w, 1f, true);
            yield return null;
        }

        beamFlickering = false;
        beamCollider.enabled = false;
        beamVisual.gameObject.SetActive(false); 

        // Reset orbs
        for (int i = 0; i < chargeOrbs.Length; i++)
            chargeOrbs[i].localPosition = orbStartLocalPos[i];

        boss.movementPaused = false;
    }

    private void ShowBeam(float width, float alpha, bool damaging)
    {
        beamVisual.gameObject.SetActive(true);
        beamVisual.localScale = new Vector3(beamLength, width, 1f);
        beamVisual.localPosition = new Vector3(-beamLength / 2f, 0f, 0f);

        Color c = beamRenderer.color;
        c.a = alpha;
        beamRenderer.color = c;

        beamCollider.enabled = damaging;
    }
    
    
    private void Update()
    {
        if (!beamFlickering) return;
        
        bool useA = Mathf.FloorToInt(Time.time * flickerSpeed) % 2 == 0;
        beamRenderer.sprite = useA ? flickerSpriteA : flickerSpriteB;
    }
}