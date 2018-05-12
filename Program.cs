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

        public void ApplyDamage(Player _player, PlayerMove _move)
        {
            int damageDealt = _move.damage;
            if (_move.type == MoveType.Physical)
            {
                damageDealt += _player.physAttack - physDefence;
            }
            else
            {
                damageDealt += _player.specAttack - specDefence;
            }

            if (damageDealt < 0) damageDealt = 0;
            health -= damageDealt;
            if (health < 0) health = 0;
            Console.WriteLine("{0} attacked with {1} dealing {2} damage!", _player.name, _move.name, damageDealt);
        }
    }

    class Program
    {
        static int ReadIntegerRange(string prompt, int min, int max)
        {
            int val;
            bool success;

            Console.Write(prompt);
            success = Int32.TryParse(Console.ReadLine(), out val);
            while (success && (val < min || val > max))
            {
                Console.Write("Please enter a whole number between {0} and {1}\n{2}", min, max, prompt);
                success = Int32.TryParse(Console.ReadLine(), out val);
            }

            return val;
        }

        static void DisplayGameStatus(Player mainPlayer, Player enemyPlayer)
        {
            Console.WriteLine("{0}'s health: {1}", mainPlayer.name, mainPlayer.health);
            Console.WriteLine("{0}'s health: {1}", enemyPlayer.name, enemyPlayer.health);
        }

        static void AttackMenu(Player attacker, Player victim)
        {
            for (int i = 0; i < attacker.moves.Length; i++)
            {
                Console.WriteLine("{0} - {1} {2} {3}", i + 1, attacker.moves[i].name, attacker.moves[i].type, attacker.moves[i].damage);
            }
            int selection = ReadIntegerRange("Select a move: ", 1, attacker.moves.Length);

            victim.ApplyDamage(attacker, attacker.moves[selection - 1]);
        }
        
        static void Run()
        {
            Console.WriteLine("Run stuff goes here");
        }

        static void MainMenu(Player mainPlayer, Player enemyPlayer)
        {
            Console.WriteLine("1 - Attack");
            Console.WriteLine("2 - Run");

            int selection = ReadIntegerRange("Select an option: ", 1, 2);
            switch (selection)
            {
                case 1:
                    AttackMenu(mainPlayer, enemyPlayer);
                    break;
                
                case 2:
                    Run();
                    break;
            }
        }

        static bool CheckLostStatus(Player _player)
        {
            if (_player.health <= 0) return true;
            else return false;
        }

        static void Main(string[] args)
        {
            Player mainPlayer = // Create the user's player
                new Player("Player", 100, 10, 5, 10, 5, 10,
                new PlayerMove[] {
                    new PlayerMove("Smack", MoveType.Physical, 5),
                    new PlayerMove("Dab", MoveType.Special, 10),
                    new PlayerMove("Yeet", MoveType.Special, 20),
                    new PlayerMove("Skrrt", MoveType.Physical, 15)
                });

            Player enemyPlayer = // Create the enemy's player
                new Player("Enemy", 100, 10, 5, 10, 5, 10,
                new PlayerMove[] {
                    new PlayerMove("Smack", MoveType.Physical, 5),
                    new PlayerMove("Dab", MoveType.Special, 10),
                    new PlayerMove("Yeet", MoveType.Special, 20),
                    new PlayerMove("Skrrt", MoveType.Physical, 15)
                });

            while (true)
            {
                DisplayGameStatus(mainPlayer, enemyPlayer);
                MainMenu(mainPlayer, enemyPlayer);
                if (CheckLostStatus(enemyPlayer))
                {
                    Console.WriteLine("Congratulations you win!");
                    Console.WriteLine("Final result: {0}: {1} | {2}: {3}", mainPlayer.name, mainPlayer.health, enemyPlayer.name, enemyPlayer.health);
                    break;
                }
            }
        }
    }
}
