public class Effect
{
    public string EffectName { get; set; }
    public string EffectID { get; set; }

    public Effect(string effectName, string effectID)
    {
        EffectName = effectName;
        EffectID = effectID;
    }
}

public class Creature
{
    public string Name { get; set; }
    public string ID { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Shield { get; set; }
    public List<Effect> Effects { get; set; }

    public Creature(string name, string id, int health, int maxHealth, int shield, List<Effect> effects)
    {
        Name = name;
        ID = id;
        Health = health;
        MaxHealth = maxHealth;
        Shield = shield;
        Effects = effects;
    }
}

class Card
{
    public string Name { get; set; }
    public int Actions { get; set; }
    public string CardAbilitys { get; set; }
}