using System;

namespace RPGBase
{
    // Changed Special to Magic
    public enum MoveType { Physical, Magic }

    public class PlayerMove
    {
        public string name { get; set; }
        public MoveType type { get; set; }
        public int damage { get; set; }
        public int mana { get; set; }
        public int stamina { get; set; }

        // Added magic and stamina as attributes(?) to the fuction
        public PlayerMove(string _name, MoveType _type, int _damage, int _mana, int _stamina)
        {
            name = _name;
            type = _type;
            damage = _damage;
            mana = _mana;
            stamina = _stamina;
        }
    }

    public class Player
    {
        public string name { get; set; }
        public int health { get; set; }
        public int physAttack { get; set; }
        public int physDefence { get; set; }
        // Changed Special to Magic as it would be in line with mana
        public int magicAttack { get; set; }
        public int magicDefence { get; set; }
        public int speed { get; set; }
        // Added Mana and Stamina as resources to be used to attack
        public int mana { get; set; }
        public int stamina { get; set; }
        public PlayerMove[] moves { get; set; }

        // Added magic and stamina as attributes(?) to the fuction
        // Changed special to magic
        public Player(string _name, int _health, int _physAttack, int _physDefence, int _magicAttack, int _magicDefence, int _speed, int _mana, int _stamina, PlayerMove[] _moves)
        {
            name = _name;
            health = _health;
            physAttack = _physAttack;
            physDefence = _physDefence;
            // Changed special to magic
            magicAttack = _magicAttack;
            magicDefence = _magicDefence;
            speed = _speed;
            // Added mana and stamina
            mana = _mana;
            stamina = _stamina;
            moves = _moves;
        }

        public void ApplyDamage(Player _player, PlayerMove _move)
        {
            // Damage modifier calulcations
            int damageDealt = _move.damage;
            if (_move.type == MoveType.Physical)
            {
                damageDealt += _player.physAttack - physDefence;
            }
            else
            {
                //Changed special to magic
                damageDealt += _player.magicAttack - magicDefence;
            }

            if (damageDealt < 0) damageDealt = 0;
            health -= damageDealt;
            if (health < 0) health = 0;
            Console.WriteLine("{0} attacked with {1} dealing {2} damage!", _player.name, _move.name, damageDealt);
        }

        // Tried to make a resource take and give system
        public void takeResource(Player _player, PlayerMove _move)
        {
            int resourceTaken = 0;
            if (_move.type == MoveType.Physical)
            {
                _player.stamina -= _move.stamina;
                if (_player.stamina < 20) resourceTaken = 1;
                _player.stamina += resourceTaken;
                if (_player.stamina > 20) _player.stamina = 20;
                if (_player.stamina < 0) _player.stamina = 0;
                Console.WriteLine("{0} now has {1} stamina left over!", _player.name, _player.stamina);
            }
            else if (_move.type == MoveType.Magic)
            {
                _player.mana -= _move.mana;
                if (_player.mana < 20) resourceTaken = 1;
                _player.mana += resourceTaken;
                if (_player.mana > 20) _player.mana = 20;
                if (_player.mana < 0) _player.mana = 0;
                Console.WriteLine("{0} now has {1} mana left over!", _player.name, _player.mana);
            }
            else 
            {
                pass
            }
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
            // Added stamina and mana to the status and rearanged the look of how it's displayed
            Console.WriteLine("{0} - health: {1}, stamina: {2}, mana: {3}", mainPlayer.name, mainPlayer.health, mainPlayer.stamina, mainPlayer.mana);
            Console.WriteLine("{0} - health: {1}, stamina: {2}, mana: {3}", enemyPlayer.name, enemyPlayer.health, enemyPlayer.stamina, enemyPlayer.mana);
        }

        static void AttackMenu(Player attacker, Player victim)
        {
            for (int i = 0; i < attacker.moves.Length; i++)
            {
                // Added mana and stamina requirements for the moves
                Console.WriteLine("{0} - {1} {2} {3} {4} {5}", i + 1, attacker.moves[i].name, attacker.moves[i].type, attacker.moves[i].damage, attacker.moves[i].mana, attacker.moves[i].stamina);
            }
            int selection = ReadIntegerRange("Select a move: ", 1, attacker.moves.Length);

            victim.ApplyDamage(attacker, attacker.moves[selection - 1]);
            // Tried to make a resource take and give system
            attacker.takeResource(attacker, attacker.moves);
        }
        
        static bool Run()
        {
            Random runCheck = new Random();
            if (runCheck.Next(0, 2) == 1)
            {
                Console.WriteLine("Run successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Run unsuccessful");
                return false;
            }
        }

        static bool MainMenu(Player mainPlayer, Player enemyPlayer)
        {
            bool result = false;
            Console.WriteLine("1 - Attack");
            Console.WriteLine("2 - Run");

            int selection = ReadIntegerRange("Select an option: ", 1, 2);
            switch (selection)
            {
                case 1:
                    AttackMenu(mainPlayer, enemyPlayer);
                    break;
                
                case 2:
                    result = Run();
                    break;
            }

            return result;
        }

        static bool CheckLostStatus(Player _player)
        {
            if (_player.health <= 0) return true;
            else return false;
        }

        static void AIBehaviour(Player mainPlayer, Player enemyPlayer)
        {
            Random moveChoice = new Random();
            int selection = moveChoice.Next(0, enemyPlayer.moves.Length);
            mainPlayer.ApplyDamage(enemyPlayer, enemyPlayer.moves[selection]);
        }

        static void Main(string[] args)
        {
            Player mainPlayer = // Create the user's player
                // Added mana and stamina to the mainPlayer
                new Player("Player", 100, 10, 5, 10, 5, 10, 20, 20,
                new PlayerMove[] {
                    // Added resource requirments to the moves
                    new PlayerMove("Smack", MoveType.Physical, 5, 0, 2),
                    new PlayerMove("Dab", MoveType.Magic, 10, 4, 0),
                    new PlayerMove("Yeet", MoveType.Magic, 20, 8, 0),
                    new PlayerMove("Skrrt", MoveType.Physical, 15, 0, 6)
                });

            Player enemyPlayer = // Create the enemy's player
                // Added mana and stamina to the enemyPlayer
                new Player("Enemy", 100, 10, 5, 10, 5, 9, 20, 20,
                new PlayerMove[] {
                    // Added resource requirments to the moves
                    new PlayerMove("Smack", MoveType.Physical, 5, 0, 2),
                    new PlayerMove("Dab", MoveType.Magic, 10, 4, 0),
                    new PlayerMove("Yeet", MoveType.Magic, 20, 8, 0),
                    new PlayerMove("Skrrt", MoveType.Physical, 15, 0, 6)
                });

            while (true)
            {
                DisplayGameStatus(mainPlayer, enemyPlayer);

                // Check which player goes first based off speed stats
                if (mainPlayer.speed > enemyPlayer.speed)
                {
                    if (MainMenu(mainPlayer, enemyPlayer)) break; // MainMenu will return true if a run attempt was successful
                    if (CheckLostStatus(enemyPlayer))
                    {
                        Console.WriteLine("Congratulations you win!");
                        Console.WriteLine("Final result: {0}: {1} | {2}: {3}", mainPlayer.name, mainPlayer.health, enemyPlayer.name, enemyPlayer.health);
                        break;
                    }
                    AIBehaviour(mainPlayer, enemyPlayer);
                    if (CheckLostStatus(mainPlayer))
                    {
                        Console.WriteLine("Too bad, you lose!");
                        Console.WriteLine("Final result: {0}: {1} | {2}: {3}", mainPlayer.name, mainPlayer.health, enemyPlayer.name, enemyPlayer.health);
                        break;
                    }
                }
                else
                {
                    AIBehaviour(mainPlayer, enemyPlayer);
                    if (CheckLostStatus(mainPlayer))
                    {
                        Console.WriteLine("Too bad, you lose!");
                        Console.WriteLine("Final result: {0}: {1} | {2}: {3}", mainPlayer.name, mainPlayer.health, enemyPlayer.name, enemyPlayer.health);
                        break;
                    }
                    if (MainMenu(mainPlayer, enemyPlayer)) break; // MainMenu will return true if a run attempt was successful
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
}
