public static class Combat
{
    public static void Attack(Character attacker, Character defender, int damage)
    {
        defender.TakeDamage(damage);
        System.Console.WriteLine($"{attacker.Name} attacks {defender.Name} for {damage} damage!");
    }
}
