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
        public string ID { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsDead { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Shield { get; set; }
        public List<Effect> Effects { get; set; }

        // Constructor to initialize a Creature object
        public Creature(string name, string id, bool isPlayer, bool isDead, int health, int maxHealth, int shield, List<Effect> effects)
        {
            Name = name;
            ID = id;
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
                new Creature("Slim", "1", false, false, 30, 100, 5, new List<Effect>()),
                new Creature("Giant Bug", "2", false, false, 80, 120, 0, new List<Effect>
                {
                    new Effect("Frost", "E1", 1),
                    new Effect("Poisoned", "E2", 2)
                })
            };

            var player = new Creature("You", "P1", true, false, 56, 150, 10, new List<Effect>());

            var hand = new List<Card>
            {
                new Card("Sword", 1, "Deal 6 damage"),
                new Card("Frost", 0, "Deal 1 damage to all enemies. Afflict 3 Frost."),
                new Card("Deflect", 1, "Gain 5 Shield")
            };

            // Display game state
            PrintCreatureList("Enemies", enemies, "1");
            PrintCreatureList("Your Team", new List<Creature> { player }, "P1");
            CombatOptions(3, hand);
        }

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

        static void CombatOptions(int actionsRemaining, List<Card> playersHand)
        {
            Console.WriteLine(CreateCenteredText("Combat Options", 60, '-'));
            Console.WriteLine($"[{actionsRemaining} Actions Remaining]");
            for (int i = 0; i < playersHand.Count; i++)
            {
                var card = playersHand[i];
                Console.WriteLine($"{i + 1}. {card.Name}    [Cost: {card.Actions} Action(s)]");
                Console.WriteLine($"   - {card.CardAbilitys}");
            }
        }

        static string CreateCenteredText(string text, int width, char fillChar)
        {
            if (text.Length >= width) return text;
            int leftPadding = (width - text.Length) / 2;
            int rightPadding = width - text.Length - leftPadding;
            return new string(fillChar, leftPadding) + text + new string(fillChar, rightPadding);
        }
    }
}
