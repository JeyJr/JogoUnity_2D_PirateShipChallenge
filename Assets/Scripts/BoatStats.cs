using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoatStats : MonoBehaviour
{
    [Header("BOAT OBJ")]
    [SerializeField] private bool player;
    public SpawnEnemies SpawnEnemies { get; set; }

    [Header("LIFE")]
    [SerializeField] private float maxLife = 10f;
    [SerializeField] private float currentLife;
    [SerializeField] private Slider lifeBar;
    public float CurrentLife { get => currentLife;}

    [Space(5)]
    [Header("SPRITES")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Sprite initialSprite, midSprite, deadSprite;

    [Header("HIT and DEATH")]
    public GameObject explosion;
    public UnityEvent onDeath, onHit;
    private bool _isDead;
    public bool IsDead
    {
        get { return _isDead; }
        private set
        {
            if (_isDead == value)
                return;
            _isDead = value;

            if (player)
            {
                return;
            }
            else
            {
                GetComponent<EnemyCheckDistance>().IsDead = value;
                GetComponent<EnemyMovement>().IsDead = value;
            }
        }
    }
    private void Start()
    {
        SetInitialValues();
    }

    public void SetInitialValues()
    {
        currentLife = maxLife;
        lifeBar.maxValue = maxLife;
        lifeBar.value = maxLife;

        if (!player)
        {
            lifeBar.gameObject.SetActive(false);
        }

        IsDead = false;
        UpdateSprite(initialSprite);
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        currentLife -= damage;
        onHit.Invoke();

        if (currentLife <= 0)
        {
            onDeath.Invoke();
        }
    }

    public void OnHit()
    {
        if (currentLife <= maxLife / 2)
        {
            UpdateSprite(midSprite);
        }

        if (!player)
        {
            lifeBar.gameObject.SetActive(true);
            StartCoroutine(OnHitHideLifeBar(2f));
        }

        lifeBar.value = currentLife;
    }

    private IEnumerator OnHitHideLifeBar(float time)
    {
        yield return new WaitForSeconds(time);
        lifeBar.gameObject.SetActive(false);
    }
    public void OnDeath()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        UpdateSprite(deadSprite);
        IsDead = true;

        if (player)
        {
            return;
        }
        else
        {
            StartCoroutine(OnDeathHideBoat());
        }

        //SFX?
    }
    private IEnumerator OnDeathHideBoat()
    {
        float waitTime = 1.5f;
        yield return new WaitForSeconds(waitTime);
        gameObject.SetActive(false);
        SpawnEnemies.Enqueue(gameObject);
    }

    private void UpdateSprite(Sprite sprite)
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.sprite = sprite;
    }
}
