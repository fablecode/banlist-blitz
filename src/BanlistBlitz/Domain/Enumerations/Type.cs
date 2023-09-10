using Ardalis.SmartEnum;

namespace BanlistBlitz.Domain.Enumerations;

public class Type : SmartEnum<Type>
{
    public static readonly Type Aqua = new(nameof(Aqua), 1);
    public static readonly Type Beast = new(nameof(Beast), 2);
    public static readonly Type BeastWarrior = new ("Beast-Warrior", 3);
    public static readonly Type CreatorGod = new ("Creator God", 4);
    public static readonly Type Cyberse = new (nameof(Cyberse), 5);
    public static readonly Type Dinosaur = new (nameof(Dinosaur), 6);
    public static readonly Type DivineBeast = new ("Divine-Beast", 7);
    public static readonly Type Dragon = new (nameof(Dragon), 8);
    public static readonly Type Fairy = new (nameof(Fairy), 9);
    public static readonly Type Fiend = new (nameof(Fiend), 10);
    public static readonly Type Fish = new (nameof(Fish), 11);
    public static readonly Type Insect = new (nameof(Insect), 12);
    public static readonly Type Machine = new (nameof(Machine), 13);
    public static readonly Type Plant = new (nameof(Plant), 14);
    public static readonly Type Psychic = new (nameof(Psychic), 15);
    public static readonly Type Pyro = new (nameof(Pyro), 16);
    public static readonly Type Reptile = new (nameof(Reptile), 17);
    public static readonly Type Rock = new (nameof(Rock), 18);
    public static readonly Type SeaSerpent = new ("Sea Serpent", 19);
    public static readonly Type Spellcaster = new (nameof(Spellcaster), 20);
    public static readonly Type Thunder = new (nameof(Thunder), 21);
    public static readonly Type Warrior = new (nameof(Warrior), 22);
    public static readonly Type WingedBeast = new ("Winged Beast", 23);
    public static readonly Type Wyrm = new (nameof(Wyrm), 24);
    public static readonly Type Zombie = new (nameof(Zombie), 25);

    private Type(string name, int value) : base(name, value)
    {
    }
}