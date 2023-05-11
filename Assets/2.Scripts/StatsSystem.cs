using UnityEngine;

public class StatsSystem : MonoBehaviour
{
    [Header("•Attributes")]
    [Tooltip("Strength")]
    public int STR;
    [Tooltip("Dexterity")]
    public int DEX;
    [Tooltip("Constitution")]
    public int CON;
    [Tooltip("Intelligence")]
    public int INT;
    [Tooltip("Spirit")]
    public int SPR;

    [Header("•Stats")]
    [Min(1)]public int level;
    public int experienceNeeded;
    public int maxHealth;
    public int maxMana;
    public int minDamage;
    public int maxDamage;
    public int minMagicDamage;
    public int maxMagicDamage;
    public int defense;
    public int magicDefense;
    public int evasion;
    public int aim;
    [Tooltip("Damage Multiplier in case of a critical hit. (%)")][Min(1)]
    public float criticalMultiplier;
    [Tooltip("Attack Speed. (%)")][Min(1)]
    public float attackSpeed = 1.0f;
    
    [Header("•Bonus Attributes")]
    [Tooltip("Increase Strength Points.")][Min(0)]
    public int bonusSTR;
    [Tooltip("Increase Dexterity Points.")][Min(0)]
    public int bonusDEX;
    [Tooltip("Increase Constitution Points.")][Min(0)]
    public int bonusCON;
    [Tooltip("Increase Intelligence Points.")][Min(0)]
    public int bonusINT;
    [Tooltip("Increase Spirit Points.")][Min(0)]
    public int bonusSPR;

    [Header("•Bonus Stats")]
    [Tooltip("Increase Health.")][Min(0)]
    public int bonusHealth;
    [Tooltip("Increase Mana.")][Min(0)]
    public int bonusMana;
    [Tooltip("Increase Damage")][Min(0)]
    public int bonusDamage;
    [Tooltip("Increase M.Damage")][Min(0)]
    public int bonusMagicDamage;
    [Tooltip("Increase Defense.")][Min(0)]
    public int bonusDefense;
    [Tooltip("Increase M. Defense.")][Min(0)]
    public int bonusMagicDefense;
    [Tooltip("Increase Evasion.")][Min(0)]
    public float bonusEvasion;
    [Tooltip("Increase Aim.")][Min(0)]
    public float bonusAim;
    [Tooltip("Chance to deal a critical hit. (%)")][Min(0)]
    public float criticalChance;
    [Tooltip("Chance to block a hit. (%)")][Min(0)]
    public float blockChance;

    [Header("•References")]
    [SerializeField] public ProgressBar healthBar;
    [SerializeField] public ProgressBar manaBar;
    [SerializeField] public ProgressBar expBar;
    [SerializeField] private PopupText popupTextPrefab;
    [SerializeField] private UICharacter uiCharacter;

    /* ============== VARIABLES ============== */
    private int currentHealth;
    private int currentMana;
    private int currentExperience = 0;

    private void Awake() {
        
    }

    private void Start() {
        CalculateBonusStats();
        CalculateStats();
        // Initialize Health
        currentHealth = maxHealth;
        if(healthBar) healthBar.SetMaxValue(maxHealth);
        if(healthBar) healthBar.SetCurrentValue(currentHealth);
        // Initialize Mana
        currentMana = maxMana;
        if(manaBar) manaBar.SetMaxValue(maxMana); 
        if(manaBar) manaBar.SetCurrentValue(currentMana); 
        SetupUI();
    }

    private void CalculateStats(){
        // TODO: #1 - Calculate the "Base Stat" from its respective attribute.
        // TODO: #2 - Include stats from equipment.
        // TODO: #3 - Add "Bonus Stat" for each stat.
        
        #region STRENGTH
            /* ============== STRENGTH ============== */
            // if(equipment.length() > 0){
            //     STR += equipment.STR; 
            // }

            minDamage = STR;
            maxDamage = STR;

            if(STR >= 100){
                minDamage = minDamage/10;
                maxDamage = maxDamage/10;
            }

            // if(equipment.weapon.isEquiped == False && STR >=100){
            //     minDamage = minDamage/10;
            //     maxDamage = maxDamage/10;
            // }
            // else
            // {
            //     minDamage += equipment.weapon.minDamage;
            //     maxDamage += equipment.weapon.maxDamage;
            // }

            minDamage += bonusDamage;
            maxDamage += bonusDamage;
        #endregion

        #region DEXTERITY
            /* ============== DEXTERITY ============== */
            // if(equipment.length() > 0){
            //     DEX += equipment.DEX;
            // }

            evasion = DEX;
            aim = DEX;

            // if(equipment.length() > 0){
            //     aim += equipment.aim;
            //     evasion += equipment.evasion;
            // }

            evasion = Mathf.RoundToInt(evasion * bonusEvasion);
            aim = Mathf.RoundToInt(aim * bonusAim);
        #endregion

        #region CONSTITUTION
            /* ============== CONSTITUTION ============== */
            // if(equipment.length() > 0){
            //     CON += equipment.CON; 
            // }

            maxHealth = CON*10;
            defense = CON;

            // if(equipment.length() > 0){
            //     maxHealth += equipment.maxHealth;
            //     defense += equipment.defense;
            // }

            // if(equipment.shield.isEquiped == True){
            //     blockChance += equipment.shield.blockChance;
            // }

            maxHealth += bonusHealth;
            defense += bonusDefense;
        #endregion

        #region INTELLIGENCE
            /* ============== INTELLIGENCE ============== */
            // if(equipment.length() > 0){
            //     INT += equipment.INT; 
            // }
            minMagicDamage = INT;
            maxMagicDamage = INT;

            if(INT >= 100){
                minMagicDamage = minMagicDamage/10;
                maxMagicDamage = maxMagicDamage/10;
            }

            // if(equipment.weapon.isEquiped == False && INT >=100){
            //     minMagicDamage = minMagicDamage/10;
            //     maxMagicDamage = maxMagicDamage/10;
            // }
            // else
            // {
            //     minMagicDamage += equipment.weapon.minMagicDamage;
            //     maxMagicDamage += equipment.weapon.maxMagicDamage;
            // }

            minMagicDamage += bonusMagicDamage;
            maxMagicDamage += bonusMagicDamage;
        #endregion

        #region SPIRIT
            /* ============== SPIRIT ============== */
            // if(equipment.length() > 0){
            //     SPR += equipment.SPR; 
            // }

            maxMana = SPR*10;
            magicDefense = SPR;

            // if(equipment.length() > 0){
            //     maxMana += equipment.maxMana;
            //     magicDefense += equipment.magicDefense;
            //     criticalChance += equipment.criticalChance;
            // }

            maxMana += bonusMana;
            magicDefense += bonusMagicDefense;
        #endregion

        #region OTHERS
            /* ============== OTHERS ============== */
             // if(equipment.weapon.isEquiped == True){
            //     attackSpeed = equipment.weapon.attackSpeed;
            // }
            //
            // if(equipment.length() > 0){
            //     criticalMultiplier += equipment.criticalMultiplier;
            // }
        #endregion
    }

    private void CalculateBonusStats(){
        /* ============== STRENGTH ============== */
        bonusDamage = Mathf.RoundToInt(bonusSTR/2);
    
        /* ============== DEXTERITY ============== */
        bonusEvasion = bonusDEX/5.0f;
        bonusAim = bonusDEX/3.0f;
    
        /* ============== CONSTITUTION ============== */
        bonusDefense = Mathf.RoundToInt(bonusCON/2);
        bonusHealth = Mathf.RoundToInt(bonusCON*5);
        blockChance = bonusCON/10.0f;

        /* ============== INTELLIGENCE ============== */
        bonusMagicDamage = Mathf.RoundToInt(bonusINT/2);

        /* ============== SPIRIT ============== */
        bonusMagicDefense = Mathf.RoundToInt(bonusSPR/2);
        bonusMana = Mathf.RoundToInt(bonusSPR*5);
        criticalChance = bonusSPR/5.0f;
    }

    private void SetupUI(){
        /* ============== ATTRIBUTES ============== */
        uiCharacter.strengthUI.SetText(STR.ToString());
        uiCharacter.dexterityUI.SetText(DEX.ToString());
        uiCharacter.constitutionUI.SetText(CON.ToString());
        uiCharacter.intelligenceUI.SetText(INT.ToString());
        uiCharacter.spiritUI.SetText(SPR.ToString());
        uiCharacter.healthUI.SetText(currentHealth.ToString() + "/" + maxHealth.ToString());
        uiCharacter.manaUI.SetText(currentMana.ToString() + "/" + maxMana.ToString());


        /* ============== STATS ============== */
        uiCharacter.damageUI.SetText(minDamage.ToString() + " ~ " + maxDamage.ToString());
        uiCharacter.mDamageUI.SetText(minMagicDamage.ToString() + " ~ " + maxMagicDamage.ToString());
        uiCharacter.aimUI.SetText(aim.ToString());
        uiCharacter.defenseUI.SetText(defense.ToString());
        uiCharacter.mDefenseUI.SetText(magicDefense.ToString());
        uiCharacter.evasionUI.SetText(evasion.ToString());

    }

    /* ============== STATS CODE ============== */
    public void TakeDamage(int _damage, bool _isCritical){
        // Apply damage reduction from own defense.
        _damage -= defense;

        // Reduce the current health by the amount of damage sustained.
        currentHealth -= _damage;

        // Sets the new current health value to the health bar.
        healthBar.SetCurrentValue(currentHealth);

        // Triggers a popup text with the amount of damage sustained.
        if(popupTextPrefab != null){
            showPopupTextPrefab(_damage, _isCritical);
        }
    }

    // INSTANTIATE UI DAMAGE POPUPTEXT 
    private void showPopupTextPrefab(int _damage, bool _isCritical){
        // Instantiate the Popup Text Prefab and set this transform as parent.
        PopupText popupText = Instantiate(popupTextPrefab, transform.position, Quaternion.identity, transform);
        popupText.Setup(_damage, _isCritical);
    }

    public void ConsumeMana(int _mana){
        currentMana -= _mana;
        manaBar.SetCurrentValue(currentMana);
    }

    public void GainExperience(int _expPoints){
        currentExperience += _expPoints;
        expBar.SetCurrentValue(currentExperience);
    }
}
