using System;

namespace example_of_basics_in_Class
{
    class Player
    {
        public int health;
        public string weapon;
        public float level;
        public string name;
        public int kills;
        public int Ammo;
        public int sniper_ammo;
        public int lmg_ammo;
        public int smg_ammo;
        public int hmg_ammo;
        public int assault_ammo;
    

        //Class Constructure
        public Player(string _name, string _weapon)
        {
            name = _name;
            weapon = _weapon.ToLower();
            health = 100;
            level = 1;
            kills = 0;
            if (weapon=="sniper")
            {
                Ammo = 5;
                sniper_ammo = 5;
            }
            else if (weapon=="assault")
            {
                Ammo = 30;
                assault_ammo = 30;
            }
            else if (weapon=="smg")
            {
                Ammo = 25;
                smg_ammo = 25;
            }
            else if (weapon=="lmg")
            {
                Ammo = 60;
                lmg_ammo = 60;
            }
            else if (weapon=="hmg"){
                Ammo = 100;
                hmg_ammo = 100;
            }
        }

        //When player kills Opponent
        public void Kills_Opponent()
        {
            if (Ammo<=0){
                Console.WriteLine("Reload Your weapon");
            }
            else if (health<=15)
            {
                Console.WriteLine("Revive Your self with a first Aid");   
            }
            else
            {
                kills++;
                level += 0.4f;
                health -= 8;
                Ammo = (weapon == "sniper") ? Ammo-=1 : Ammo-=25;
            }
        }


        //When Player Reloads
        public void Reload()
        {
            if (weapon == "sniper")
            {
                Ammo = sniper_ammo;
            }
            else if (weapon=="assault"){
                Ammo = assault_ammo;
            }
            else if (weapon=="lmg"){
                    Ammo = lmg_ammo;
            }
            else if (weapon=="hmg"){
                Ammo = hmg_ammo;
            }
            else if (weapon=="smg"){
                Ammo = smg_ammo;
            }
        }

        //UnderDevelopment process
        public void Campaign()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Game is starting be ready");
            Console.WriteLine("3");
            Console.WriteLine("2");
            Console.WriteLine("1");

            Console.WriteLine("----------------------------------------------------------------------");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t\t\t First Mission \t\t\t");
            Console.WriteLine("Mission Leader:- Its a stealth mission in this mission you have to............"); 
            Console.WriteLine($"{name}:- Hello?");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Game:- Your voice has been disconnected from the mission leader");
            Console.WriteLine("You have to create your own ways");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Your Main Objective is to get the info about next mission of the our enemy ");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("Scene:");
            Console.WriteLine("You are in the Enemies territory");
            Console.WriteLine("On the front there is a guard");
            Console.WriteLine($"You i.e,'{name}':- Put supressor on the {weapon}");
            Console.WriteLine($"Press Enter to shoot with your {weapon}");
            Console.ReadKey();
            // To be written
        }

        public void Quick_Match(string _name_)
        {
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to a Shooter Game");
            Console.WriteLine("A Game Built By Resolute Studio");


            //User Data
            Console.Write("Enter Your Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Choose a weapon:");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("SMG");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LMG");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("HMG");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Sniper");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Assault");

            // Default Color set
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine(">");
            string weapon =Console.ReadLine();

            Player user = new Player(name,weapon);

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("To start the game type start"); //Underdeveloped

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("To change or get info about your weapon and all stats type stats");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("To exit type exit");

                Console.WriteLine(">");
                string? User_choice = Console.ReadLine();
                if (User_choice == "start")
                {
                    while(true){
                        Console.WriteLine("There are two mode currently available:-");
                        Console.WriteLine("1.) Campaign"); //UnderDevelopment process
                        Console.WriteLine("2.) Quick Match");
                        Console.WriteLine("3.) Back to back to the menue");
                        Console.Write(">");
                        string user_choice_start = Console.ReadLine();
                        if (user_choice_start=="Campaign" || user_choice_start=="1")
                        {
                            user.Campaign();
                        }
                        else if(user_choice_start=="Quick Match" || user_choice_start=="2")
                        {
                            user.Quick_Match();
                        }
                        else if (user_choice_start=="back" || user_choice_start=="3")
                        {
                            break;
                        }
                    }
                }
                else if (User_choice=="exit")
                {
                    break;
                }
                else if (User_choice=="Stats")
                {
                    Console.WriteLine($"Name:- {user.name}");
                    Console.WriteLine($"Weapon:-{user.weapon}");
                    Console.WriteLine($"This weapons has:-{user.Ammo}");
                    Console.WriteLine($"Level:-{user.level}");
                    Console.WriteLine($"Health:-{user.health}");
                }

            }
        
            //Wait for user to end
            // Console.ReadKey();
        }
    }
}
