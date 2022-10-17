using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ShipControl : MonoBehaviour
{
    [field: SerializeField] public float shipMaxHealth;
    [field: SerializeField] public float shipDefense;
    
    [field: SerializeField] public float maxSpeed { get; private set; } //10               linear drag 0.5
    [field: SerializeField] public float maxAngularSpeed { get; private set; } //50         angular drag 0.5
    [field: SerializeField] public float shipForce { get; private set; } //10
    [field: SerializeField] public float shipAngularForce { get; private set; } //2
    [field: SerializeField] public GameObject Destory { get; private set; } // Destory particle animation
    [field: SerializeField] public GameObject Damaged { get; private set; } // Damaged particle animationa
    [field: SerializeField] public AudioSource collisionSound { get; private set; }
    [field: SerializeField] public AudioSource explosionSound { get; private set; }
    [field: SerializeField] public AudioSource damagedSound { get; private set; }
    [field: SerializeField] public AudioSource shipMoveSound { get; private set; }
    [field: SerializeField] public AudioSource Teleport { get; private set; }
    [field: SerializeField] public MissileLauncher missileLuncher { get; private set; }
    [field: SerializeField] public Volume PPV { get; private set; }
    [field: SerializeField] public Animator UIanimation { get; private set; }
    [field: SerializeField] public Animator UIanimation2 { get; private set; }
    public Renderer shipRender;
    public PolygonCollider2D shipCollider;
    public Rigidbody2D shipRb2d;
    public float shipHealth;

   
    public float shipSpeed;
    public float shipAngularSpeed;

    private Vector2 totalForce;

    private event Action shipDamaged;
    private event Action shipDestoried;

    public static ShipControl ship;

    private void Awake()
    {
        Damaged.SetActive(true);
        Destory.SetActive(true);
        Damaged.GetComponent<ParticleSystem>().Stop();
        Destory.GetComponent<ParticleSystem>().Stop();
        shipMoveSound.Stop();
    }
    void Start()
    {
        shipSpeed = 0;
        shipAngularSpeed = 0;

        shipHealth = shipMaxHealth;

        shipRender = GetComponent<Renderer>();
        shipCollider = GetComponent<PolygonCollider2D>();
        shipRb2d = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        if (ship != null) Destroy(ship.gameObject);
        ship = this;
        shipDamaged += PlayDamageEffect;
        shipDamaged += PPV_CA_Effect;
        shipDestoried += PlayDestoryEffect;
        shipDestoried += PPV_Bloom_Effect;
        shipDestoried += clearCA;
        shipDestoried += playUIAnimation;
    }
    private void OnDestroy()
    {
        ship = null;
        shipDamaged -= PlayDamageEffect;
        shipDamaged -= PPV_CA_Effect;
        shipDestoried -= PlayDestoryEffect;
        shipDestoried -= PPV_Bloom_Effect;
        shipDestoried -= clearCA;
        shipDestoried -= playUIAnimation;
    }
    void FixedUpdate()
    {
        if (PlayerInput.instance == null) return;
        if (!shipRender.enabled) return;
        shipMoveSound.Stop();
        UpdateSpeed();
        
        if (PlayerInput.instance.movement != Vector2.zero)
        {
            ApplyForce(PlayerInput.instance.movement * 10);
            if (TimeManager.instance.deScale) TimeManager.instance.startTimmer();
        }
        if (PlayerInput.instance.tourk != 0)
        {
            ApplyTorque(PlayerInput.instance.tourk);
            if (TimeManager.instance.deScale) TimeManager.instance.startTimmer();
        }
    }

    private void UpdateSpeed()
    {
        shipSpeed = shipRb2d.velocity.magnitude;
        shipAngularSpeed = shipRb2d.angularVelocity;
    }
    
    public void ApplyForce(Vector2 totalForce)
    {
        if (shipSpeed > maxSpeed) return;
        shipMoveSound.Play();
        shipRb2d.AddRelativeForce(totalForce);
        
        
    }
    public void ApplyTorque(float angularForce)
    {
        if (shipAngularSpeed > maxAngularSpeed) return;
        shipMoveSound.Play();
        shipRb2d.AddTorque(angularForce);
    }
    public IEnumerator takeContinueDMG(float amount, float dmg)
    {
        float total = 0;
        while (total < amount)
        {
            shipHealth -= dmg;
            total += dmg;
            ShipControl.ship.checkShipSta();
            yield return new WaitForSeconds(1f);
        }
    }

    public void startTakeContinueDMG(float amount, float dmg)
    {
        StartCoroutine(ShipControl.ship.takeContinueDMG(amount, dmg));
    }

    void PlayDestoryEffect()
    {
        ShipControl.ship.damagedSound.Stop();
        ShipControl.ship.shipMoveSound.Stop();
        ShipControl.ship.explosionSound.Play();
        ShipControl.ship.shipRender.enabled = false;
        ShipControl.ship.shipCollider.enabled = false;
        ShipControl.ship.Damaged.GetComponent<ParticleSystem>().Stop();
        ShipControl.ship.Destory.GetComponent<ParticleSystem>().Play();
        shipDestoried -= PlayDestoryEffect;

        StartCoroutine(reloadScene());
    }
    void PlayDamageEffect()
    {
        ShipControl.ship.damagedSound.Play();
        ShipControl.ship.Damaged.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator reloadScene()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
    public void checkShipSta()
    {
        if (ShipControl.ship.shipHealth < ShipControl.ship.shipMaxHealth * 0.7 && ShipControl.ship.shipHealth > 0)
        {
            shipDamaged?.Invoke();
        }
        if (ShipControl.ship.shipHealth <= 0)
        {
            ShipControl.ship.shipHealth = 0;
            shipDestoried?.Invoke();
        }
    }
    private void PPV_CA_Effect()
    {
        StartCoroutine("increaseCA");
    }
    IEnumerator increaseCA()
    {
        ChromaticAberration ca;
        if (PPV.profile.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.value = 0;
            while (ca.intensity.value < 0.6f){
                ca.intensity.value += 0.01f;
                yield return new WaitForSeconds(0.02f);
            }
            
        }
        yield return null;
    }
    void clearCA()
    {
        StopCoroutine("increaseCA");
        ChromaticAberration ca;
        if (PPV.profile.TryGet<ChromaticAberration>(out ca))
        {
            ca.intensity.value = 0;
        }
    }
    private void PPV_Bloom_Effect()
    {
        StartCoroutine(ExplosionBloom());
    }
    IEnumerator ExplosionBloom()
    {
        Bloom bl;
        if (PPV.profile.TryGet<Bloom>(out bl))
        {
            bl.intensity.value = 0;
            while (bl.intensity.value < 120)
            {
                bl.intensity.value += 3f;
                yield return new WaitForSeconds(0.03f);
            }
            yield return new WaitForSeconds(0.05f);
            while (bl.intensity.value >0)
            {
                bl.intensity.value -= 3f;
                yield return new WaitForSeconds(0.03f);
            }
        }
        yield return null;
    }

    void playUIAnimation()
    {
        UIanimation.Play("UIAnimation");
        UIanimation2.Play("rtAnimation");
    }
}
