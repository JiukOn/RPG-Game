using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public float maxSpeed = 8;
	public float currentSpeed { get; private set; }

	public Stat damage;
	public Stat armor;
	public bool takedamage = false;

	public event System.Action<int,int> OnHealthChanged;

	void Awake ()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage (int damage)
	{
		takedamage = true;
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHealth -= damage;
		//Debug.Log(transform.name + " recebeu " + damage + " de dano.");

		if(OnHealthChanged != null){
			OnHealthChanged(maxHealth,currentHealth);
		}
		
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void LifeHeal(int lifeheal){
		if(currentHealth >= maxHealth){
			return;
		}

		else if(currentHealth <= maxHealth - lifeheal){
			currentHealth += lifeheal;
		}

		else if(currentHealth > maxHealth - lifeheal){
			currentHealth = maxHealth;
		}
	}

	public void checkSpeed(bool isWalking){
		if(isWalking){
			if(currentSpeed == maxSpeed){
				return;
			}else if(currentSpeed > maxSpeed){
				currentSpeed = maxSpeed;
			}else if(currentSpeed < maxSpeed){
				currentSpeed += 0.1f;
			}
		}else if(!isWalking){
			currentSpeed = 0;
		}
	}
	public virtual void Die ()
	{
		//Debug.Log(transform.name + " morreu.");
	}

}
