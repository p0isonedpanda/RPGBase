using System;

namespace RPGBase
{
    public enum MoveType { Physical, Special }

    public class PlayerMove
    {
        public string name { get; set; }
        public MoveType type { get; set; }
        public int damage { get; set; }
        
        public PlayerMove(string _name, MoveType _type, int _damage)
        {
            name = _name;
            type = _type;
            damage = _damage;
        }
    }

    public class Player
    {
        public string name { get; set; }
        public int health { get; set; }
        public int physAttack { get; set; }
        public int physDefence { get; set; }
        public int specAttack { get; set; }
        public int specDefence { get; set; }
        public int speed { get; set; }
        public PlayerMove[] moves { get; set; }

        public Player(string _name, int _health, int _physAttack, int _physDefence, int _specAttack, int _specDefence, int _speed, PlayerMove[] _moves)
        {
            name = _name;
            health = _health;
            physAttack = _physAttack;
            physDefence = _physDefence;
            specAttack = _specAttack;
            specDefence = _specDefence;
            speed = _speed;
            moves = _moves;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player mainPlayer = // Create the user's player
                new Player("Player", 100, 10, 10, 5, 5, 10,
                new PlayerMove[] {
                    new PlayerMove("Smack", MoveType.Physical, 5),
                    new PlayerMove("Dab", MoveType.Special, 10),
                    new PlayerMove("Yeet", MoveType.Special, 20),
                    new PlayerMove("Skrrt", MoveType.Physical, 15)
                });

            Player enemyPlayer = // Create the enemy's player
                new Player("Enemy", 100, 10, 10, 5, 5, 10,
                new PlayerMove[] {
                    new PlayerMove("Smack", MoveType.Physical, 5),
                    new PlayerMove("Dab", MoveType.Special, 10),
                    new PlayerMove("Yeet", MoveType.Special, 20),
                    new PlayerMove("Skrrt", MoveType.Physical, 15)
                });
            Console.WriteLine("Loaded all thingies successfully");
        }
    }
}
