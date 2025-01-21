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

            StartCombat(enemies, player, hand);
            // Display game state
        }

        static void SetUpCombat()
        {
            // This is where the combat will be set up
            // This will include setting up the enemies, the player's team, and the player's hand
        }
        // This is the main combat loop
        static void StartCombat(List<Creature>enemieCreatures, List<Creature>playersTeam, List<Card>playersHand){
            // This is where the combat will be handled
            // These vairbels will be moved to SetUpCombat when the combat is fully set up
            int actionsRemaining = 3;
            int enemieTargeted = 0;
            int playerTargeted = 0;


            while (true)
            {
                // Display the current game state
                DisplayGameState(enemieCreatures, playersTeam, enemieTargeted, playerTargeted, actionsRemaining, playersHand);

                // Get the player's input
                Console.Write("Enter the number of the card you want to play: ");
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Write("Input cannot be null. Please enter a valid number.");
                    continue;
                }

                // Check if the input is a valid number
                if (!int.TryParse(input, out int cardNumber))
                {
                    Write("Invalid input. Please enter a number.");
                    continue;
                }

                // Check if the input is within the range of the player's hand
                if (cardNumber < 1 || cardNumber > playersHand.Count)
                {
                    Write("Invalid input. Please enter a number within the range of your hand.");
                    continue;
                }

                // Get the selected card from the player's hand
                Card selectedCard = playersHand[cardNumber - 1];

                // Check if the player has enough actions to play the card
                if (selectedCard.Actions > actionsRemaining)
                {
                    Write("Not enough actions to play this card. Please select another card.");
                    continue;
                }

                // Play the selected card
                PlayCard(selectedCard, enemieCreatures, playersTeam, ref actionsRemaining, ref enemieTargeted, ref playerTargeted);
                Write($"You played the card: {selectedCard.Name}");

                // Check if the combat is over
                if (IsCombatOver(enemieCreatures, playersTeam))
                {
                    // Display the final game state
                    DisplayGameState(enemieCreatures, playersTeam, enemieTargeted, playerTargeted, actionsRemaining, playersHand);

                    // End the combat
                    break;
                }
            }
        }

        static void PlayCard(Card card, List<Creature> enemieCreatures, List<Creature> playersTeam, ref int actionsRemaining, ref int enemieTargeted, ref int playerTargeted)
        {
            // Decrease the number of actions remaining by the cost of the card
            actionsRemaining -= card.Actions;

            // Perform the card's ability based on its name
            switch (card.Name)
            {
                case "Sword":
                    // Deal 6 damage to the targeted enemy
                    enemieCreatures[enemieTargeted].Health -= 6;
                    break;
                case "Deflect":
                    // Gain 5 Shield
                    playersTeam[playerTargeted].Shield += 5;
                    break;
                //case "Frost":
                    // Deal 1 damage to all enemies and afflict 3 Frost

            }
        }

        static void Write(string text)
        // Write a line of text to the console and wait for the user to press Enter
        {
            Console.WriteLine(text + "\nPress Enter to continue...");
            Console.ReadLine();
        }

        static void DisplayGameState(List<Creature> enemieCreatures, List<Creature> playersTeam, int enemieTargeted, int playerTargeted, int actionsRemaining, List<Card> playersHand)
        // Display the current game state
        {
            Console.Clear();
            PrintCreatureList("Enemies", enemieCreatures, enemieTargeted);
            PrintCreatureList("Your Team", playersTeam, playerTargeted);
            CombatOptions(actionsRemaining, playersHand);
        }


        static bool IsCombatOver(List<Creature> enemieCreatures, List<Creature> playersTeam)
        // Check if the combat is over
        {
            // Check if all enemies are dead
            if (enemieCreatures.All(e => e.IsDead))
            {
                Write("You have defeated all enemies!");
                return true;
            }

            // Check if all players are dead
            if (playersTeam.All(p => p.IsDead))
            {
                Write("All players have been defeated. Game over!");
                return true;
            }

            return false;
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
