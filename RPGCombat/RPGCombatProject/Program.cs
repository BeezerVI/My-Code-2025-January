using System;
using System.Collections.Generic;
using System.Linq;

namespace RPGCombatProject
{
    public class Effect
    {
        public string EffectName { get; set; }
        public string EffectID { get; set; }
        public int Duration { get; set; }

        // Constructor to initialize an Effect object
        public Effect(string effectName, string effectID, int duration)
        {
            EffectName = effectName;
            EffectID = effectID;
            Duration = duration;
        }
    }

    public class Creature
    {
        public string Name { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsDead { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Shield { get; set; }
        public List<Effect> Effects { get; set; }

        // Constructor to initialize a Creature object
        public Creature(string name, bool isPlayer = false, bool isDead = false, int maxHealth = 100, int health = 100, int shield = 0, List<Effect>? effects = null)
        {
            Name = name;
            IsPlayer = isPlayer;
            IsDead = isDead;
            Health = health;
            MaxHealth = maxHealth;
            Shield = shield;
            Effects = effects ?? new List<Effect>(); // Handle null list gracefully
        }
    }

    public class Card
    {
        public string Name { get; set; }
        public int Actions { get; set; }
        public string CardAbilitys { get; set; }

        // Constructor to initialize a Card object
        public Card(string name, int actions, string cardAbilitys)
        {
            Name = name;
            Actions = actions;
            CardAbilitys = cardAbilitys;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Example data for testing
            var enemies = new List<Creature>
            {
                new Creature("Slim"),
                new Creature("Giant Bug", effects: new List<Effect>
                {
                    new Effect("Frost", "E1", 1),
                    new Effect("Poisoned", "E2", 2)
                })
            };

            // Making it a list to allow for multiple players in the future
            var player = new List<Creature>
            {
                new Creature("You", true, false, 56, 150, 10),
            };

            // This is an example hand of cards that the player can use
            var hand = new List<Card>
            {
                new Card("Sword", 1, "Deal 6 damage"),
                new Card("Frost", 0, "Deal 1 damage to all enemies. Afflict 3 Frost."),
                new Card("Deflect", 1, "Gain 5 Shield")
            };

            int actionsRemaining = 3;
            int enemieTarget = 0;
            int playerTarget = 0;

            // Display game state
            PrintCreatureList("Enemies", enemies, enemieTarget);
            PrintCreatureList("Your Team", player, playerTarget);
            CombatOptions(actionsRemaining, hand);
        }

        static void PrintCreatureList(string title, List<Creature> creatures, int targetedCreature)
        {
        // Print the title centered within a 60-character wide line, filled with '=' characters
        Console.WriteLine(CreateCenteredText(title, 60, '='));

        // Initialize an index variable to keep track of the current index in the foreach loop
        int index = 0;

        // Iterate through each creature in the list
            foreach (var creature in creatures)
            {
                // Determine if the current creature is the target by comparing indices
                string marker = index == targetedCreature ? ">> " : "   ";

                // Print the creature's name with a marker if it is the target
                Console.WriteLine($"{marker}{creature.Name}");

                // Print the creature's health, max health, and shield (if any)
                Console.WriteLine($"   - HP: {creature.Health} / {creature.MaxHealth}" +
                                $"{(creature.Shield > 0 ? $" | Shield: {creature.Shield}" : "")}");

                // Print the list of effects on the creature
                Console.WriteLine($"   - Effects: {EffectList(creature.Effects)}\n");

                // Increment the index variable
                index++;
            }
        }

        static string EffectList(List<Effect> effects)
        {
            if (effects.Count == 0) return "None";
            return string.Join(", ", effects.Select(e => $"{e.EffectName} ({e.Duration})"));
        }

        static void CombatOptions(int actionsRemaining, List<Card> playersHand)
        {
            Console.WriteLine(CreateCenteredText("Combat Options", 60, '-'));
            Console.WriteLine($"[{actionsRemaining} Actions Remaining]\n");
            for (int i = 0; i < playersHand.Count; i++)
            {
                var card = playersHand[i];
                Console.WriteLine($"{i + 1}. {card.Name}    [Cost: {card.Actions} Action(s)]");
                Console.WriteLine($"   - {card.CardAbilitys}\n");
            }
        }

        static string CreateCenteredText(string text = "Example", int width = 50, char fillChar = '-')
        {
            if (text.Length >= width) return text;
            int leftPadding = (width - text.Length) / 2;
            int rightPadding = width - text.Length - leftPadding;
            return new string(fillChar, leftPadding) + text + new string(fillChar, rightPadding);
        }
    }
}
