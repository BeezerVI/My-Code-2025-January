
public class Program
{
    // This function centers a string and adds decorative characters for visual appeal
    static string CreateCenteredText(string text, int totalLength = 50, char lineChar = '-')
    {
        int padding = (totalLength - text.Length) / 2;
        return new string(lineChar, padding) + " " + text + " " + new string(lineChar, padding);
    }


    // Display enemies or team members with dynamic highlights for the targeted creature and their effects
    static void PrintCreatureList(string title, List<Creature> creatures, string targetID)
    {
        Console.WriteLine(CreateCenteredText(title, 60, '='));
        foreach (var creature in creatures)
        {
            string marker = creature.ID == targetID ? ">> " : "   ";
            Console.WriteLine($"{marker}{creature.Name}");
            Console.WriteLine($"   - HP: {creature.Health} / {creature.MaxHealth}" +
                            $"{(creature.Shield > 0 ? $" | Shield: {creature.Shield}" : "")}");
            Console.WriteLine($"   - Effects: {EffectList(creature.Effects)}");
        }
    }

    static string EffectList(List<Effect> effects)
    {
        if (effects.Count == 0) return "None";
        return string.Join(", ", effects.Select(e => $"{e.EffectName} ({e.Duration})"));
    }


    // Print available actions dynamically based on the player’s hand
    static void CombatOptions(int actionsRemaining, List<Card> playersHand)
    {
        Console.WriteLine(CreateCenteredText("Combat Options"));
        Console.WriteLine($"[{actionsRemaining} Actions Remaining]");
        for (int i = 0; i < playersHand.Count; i++)
        {
            var card = playersHand[i];
            Console.WriteLine($"{i + 1}. {card.Name}    [Cost: {card.Actions} Action(s)]");
            ShowCard(card);
        }
    }

    static void ShowCard(Card card)
    {
        Console.WriteLine($"   - {card.CardAbilitys}");
    }

    public static void Main(string[] args)
    {
        // Sample Data
        var enemies = new List<Creature>
        {
            new Creature { ID = "1", Name = "Slim", Health = 30, MaxHealth = 100, Shield = 5, Effects = new List<Effect>() },
            new Creature { ID = "2", Name = "Giant Bug", Health = 80, MaxHealth = 120, Effects = new List<Effect>
                { new Effect { EffectName = "Frost", Duration = 1 }, new Effect { EffectName = "Poisoned", Duration = 2 } } }
        };

        var player = new Creature { ID = "P1", Name = "You", Health = 56, MaxHealth = 150, Shield = 10, Effects = new List<Effect>() };
        var hand = new List<Card>
        {
            new Card { Name = "Sword", Actions = 1, CardAbilitys = "Deal 6 damage" },
            new Card { Name = "Frost", Actions = 0, CardAbilitys = "Deal 1 damage to all enemies. Afflict 3 Frost." },
            new Card { Name = "Deflect", Actions = 1, CardAbilitys = "Gain 5 Shield" }
        };

        // Display
        PrintCreatureList("Enemies", enemies, "1");
        PrintCreatureList("Your Team", new List<Creature> { player }, "P1");
        CombatOptions(3, hand);
    }
}
