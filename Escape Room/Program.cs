using System.Timers;
namespace Escape_Room
{
    internal class Program
    {
        #region Methode Map Size


        #region slider 

        private static string[] regulatorA = new string[]
            {
                "<", //0
                ">", //1
                "--------------", //2
                "[ ", //3
                 " ]", //4
                "-" //5
            };

        private static int sizeX1 = 3; // 2 & 13
        private static int sizeX2 = 5;
        #endregion
        #region Input

        private static bool hasChoosen = false;

        #endregion


        #endregion

        #region Methode Print Game

      /*  private enum EMapTiles
        {
            floor = -1,
            wall
        }*/

        private static int mapSize = 11;
        private static int[,] mapA;
        private static char WALL = '█';
        private static char DOOR = 'D';
        private static char KEY = '¶';
        private static int keyX;
        private static int keyY;
        private static int door;
        private static int doorY;
        private static int doorX;

        #endregion

        #region Menu & Tutorial 


        #region Menu
        private static char[] cursorA = new char[]
            {
                ' ',
                '<'
            };

        private static bool validInput = false;

        private static string[] titleA = new string[]
        {
            " --------------------------------------------------------  ",
            "|  ┌────  ┌───  ┌────  ┌──────   ┌────┐  ┌───────┐      |  ",
            "|  |      └───┐ |      │         └────┘  │       │      |  ",
            "|  ├────   ───┘ │      │         ┌────┐  └───────┘      |  ",
            "|  |       ──   └────  ├───      │  ■ │    ┌─┐    ┌─┐   |  ",
            "|  └──── /     ┌───┐   │         │ ───┘    │  |__|  │   |  ",
            "|       /      │   │   │         │ |       │        │   |  ",
            "|      / ───── │───┘   │         │  |      │        │   |  ",
            "|     /        │       └──────   │   |     │        │   |  ",
            " --------------------------------------------------------    "
        };

        private static string[] startA = new string[]
        {
            "--START--",
            "--EXIT--"
        };

        public static int pick = 1;
        #endregion

        #region Countdown 

        private static string[] countDowm = new string[]
        {
            " ",
            "▲"
        };



        #endregion


        #endregion

        #region Methode Movement

        private static bool movement = true;

        private static int posX = 3;
        private static int posY = 3;

        #region Player

        private static bool hasKey = false;

        private static char[] playerA = new char[]
        {
            ' ', //0
           'U', // PC bzw Windows 10 :'▲', //1
           'D',// PC bzw Windows 10 : '▼', //2
           'R',// PC bzw Windows 10 : '►', //3
           'L',// PC bzw Windows 10 : '◄'  //4
        };

        #endregion

        #endregion

        #region End 

        private static string[] congratsA = new string[]
        {
         " ┌────┐                                  ┌─┐     ┌─────┐",
         " │ ┌──┘                  ┌────┐   ┌──┐┌──┘ └──┐  │ ┌───┘",
         " │ │  ┌───┐ ┌────┐ ┌───┐ │ ┌──┘ ┌─── │└──┐ ┌──┘  │ └──┐ ",
         " │ │  │ o │ │ ┌┐ │ │ o │ │ │    │ o  │   │ │   ┌─└─── │ ",
         " │ │  └───┘ └─┘└─┘ └─┐ │ └─┘    └────┘   └─┘   └──────┘ ",
         " │ └─────────────────│ │ ──────────────────────────────┐",
         " └───────────────────┘ │ ──────────────────────────────┘",
         "                 └─────┘                                "
        };

        #endregion

        #region Timer

        private static int seconds = 0;

        private static bool hasTime = true;

        private static System.Timers.Timer e_timer = new System.Timers.Timer(1000);
        #endregion
        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            Title();

            Menu();
            Console.Clear();
            MapSize();
            Console.Clear();

            Tutorial();
            Console.Clear();

            PrintGame();

            Countdown();

            StartTimer();

        }

        private static void StartTimer()
        {
            if (hasTime == true)
            {
                e_timer.Elapsed += E_timer_Elapsed;
                e_timer.Enabled = true;
                e_timer.AutoReset = true;
                e_timer.Start();


                Movement();
            }

            e_timer.Stop();
            End();
        }

        private static void E_timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.White;
            seconds++;
            Console.SetCursorPosition(mapSize + 3, 3);
            Console.WriteLine($"{seconds} Sekunden");
            Console.ForegroundColor = ConsoleColor.Blue;
            //throw new NotImplementedException();
        }

        private static void Title()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            foreach (string temp in titleA)
            {
                Console.WriteLine(temp);
            }
        }

        private static void Menu()
        {
            Console.SetCursorPosition(25, 11);
            Console.Write(startA[0]);

            Console.SetCursorPosition(25, 13);
            Console.Write(startA[1]);

            Console.SetCursorPosition(35, 11);
            Console.WriteLine(cursorA[1]);

            while (!validInput)
            {

                ConsoleKeyInfo input = Console.ReadKey(true);
                switch (input.Key)
                {
                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(35, 11);
                        Console.Write(cursorA[0]);

                        Console.SetCursorPosition(35, 13);
                        Console.WriteLine(cursorA[1]);

                        pick = 2;
                        break;

                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(35, 13);
                        Console.WriteLine(cursorA[0]);
                        Console.SetCursorPosition(35, 11);
                        Console.WriteLine(cursorA[1]);

                        pick = 1;
                        break;

                    case ConsoleKey.Enter:

                        if (pick == 1)
                        {
                            validInput = true;
                        }
                        else if (pick == 2)
                        {
                            Environment.Exit(0);
                        }
                        break;
                }

            }

        }

        private static void MapSize()
        {

            Console.WriteLine("Wie groß soll die Map sein ?");

            #region Print Slider
            Console.SetCursorPosition(1, 2);
            Console.Write(regulatorA[0]);
            Console.SetCursorPosition(16, 2);
            Console.Write(regulatorA[1]);


            Console.SetCursorPosition(2, 2);
            Console.Write(regulatorA[2]);

            Console.SetCursorPosition(sizeX1, 2);
            Console.Write(regulatorA[3]);
            Console.SetCursorPosition(sizeX2, 2);
            Console.Write(regulatorA[4]);

            Console.SetCursorPosition(20, 2);
            Console.Write(mapSize);
            #endregion


            while (!hasChoosen)
            {
                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {

                    case ConsoleKey.LeftArrow:
                        #region remove []
                        Console.SetCursorPosition(sizeX1, 2);
                        Console.Write(regulatorA[5]);
                        Console.SetCursorPosition(sizeX2, 2);
                        Console.Write(regulatorA[5]);
                        Console.SetCursorPosition(sizeX2 + 1, 2);
                        Console.Write(regulatorA[5]);
                        #endregion

                        sizeX1--;
                        sizeX2--;
                        mapSize--;

                        if (sizeX1 == 1)
                        {
                            sizeX1++;
                            sizeX2++;
                            mapSize++;

                            #region set []
                            Console.SetCursorPosition(sizeX1, 2);
                            Console.Write(regulatorA[3]);
                            Console.SetCursorPosition(sizeX2, 2);
                            Console.Write(regulatorA[4]);

                            Console.SetCursorPosition(20, 2);
                            Console.Write("  ");
                            Console.SetCursorPosition(20, 2);
                            Console.Write(mapSize);
                            #endregion
                            break;
                        }
                        else
                        {
                            #region set []
                            Console.SetCursorPosition(sizeX1, 2);
                            Console.Write(regulatorA[3]);
                            Console.SetCursorPosition(sizeX2, 2);
                            Console.Write(regulatorA[4]);

                            Console.SetCursorPosition(20, 2);
                            Console.Write("  ");
                            Console.SetCursorPosition(20, 2);
                            Console.Write(mapSize);
                            #endregion

                        }

                        break;

                    case ConsoleKey.RightArrow:

                        #region remove []
                        Console.SetCursorPosition(sizeX1, 2);
                        Console.Write(regulatorA[5]);
                        Console.SetCursorPosition(sizeX2, 2);
                        Console.Write(regulatorA[5]);
                        #endregion

                        sizeX1++;
                        sizeX2++;
                        mapSize++;

                        if (sizeX1 == 13)
                        {
                            sizeX1--;
                            sizeX2--;
                            mapSize--;
                            #region set []

                            Console.SetCursorPosition(sizeX1, 2);
                            Console.Write(regulatorA[3]);
                            Console.SetCursorPosition(sizeX2, 2);
                            Console.Write(regulatorA[4]);

                            Console.SetCursorPosition(20, 2);
                            Console.Write("  ");
                            Console.SetCursorPosition(20, 2);
                            Console.Write(mapSize);
                            #endregion
                            break;
                        }
                        else
                        {
                            #region set []
                            Console.SetCursorPosition(sizeX1, 2);
                            Console.Write(regulatorA[3]);
                            Console.SetCursorPosition(sizeX2, 2);
                            Console.Write(regulatorA[4]);

                            Console.SetCursorPosition(20, 2);
                            Console.Write("  ");
                            Console.SetCursorPosition(20, 2);
                            Console.Write(mapSize);

                            #endregion

                        }


                        break;

                    case ConsoleKey.Enter:
                        hasChoosen = true;
                        break;

                }
            }
        }

        private static void Tutorial()
        {
            string tutorial = "Bewege deinen Spieler [►] und sammle mit ihm den Schlüssel ein. Verlasse darauf mit diesem den Raum";
            for (int i = 0; i < tutorial.Length; i++)
            {
                Console.Write(tutorial[i]);
                Thread.Sleep(20);
            }

            Console.ReadKey(true);
        }

        private static void PrintGame()
        {
            //Print map & Door & Key

            //Map
            Console.ForegroundColor = ConsoleColor.DarkGray;
            mapA = new int[mapSize, mapSize];

            for (int y = 0; y < mapSize; y++)
            {
                for (int x = 0; x < mapSize; x++)
                {
                    if (y == 0 || x == 0 || y == mapSize - 1 || x == mapSize - 1)
                    {
                        Console.SetCursorPosition(x, y);
                        Console.Write(WALL);

                    }
                }
            }

            // Door
            if (!hasKey)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Random number = new Random();
                int temp = number.Next(1, 5);

                if (temp == 1)
                {
                    //y1 achse
                    doorX = 0;

                    Random rnd = new Random();
                    door = rnd.Next(1, mapSize -1);
                    Console.SetCursorPosition(0, door);
                    Console.Write(DOOR);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (temp == 2)
                {
                    //y2 achse
                    doorX = mapSize - 1;

                    Random rnd = new Random();
                    door = rnd.Next(1, mapSize -1); // -1 ? um zu verhindern dass die Tür in den ecken spawnt
                    Console.SetCursorPosition(mapSize - 1, door);
                    Console.Write(DOOR);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (temp == 3)
                {
                    //x1 achse
                    doorY = 0;

                    Random rnd = new Random();
                    door = rnd.Next(1, mapSize -1 );
                    Console.SetCursorPosition(door, 0);
                    Console.Write(DOOR);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (temp == 4)
                {
                    //x2 achse
                    doorY = mapSize - 1;

                    Random rnd = new Random();
                    door = rnd.Next(1, mapSize -1 );
                    Console.SetCursorPosition(door, mapSize - 1);
                    Console.Write(DOOR);
                    Console.ForegroundColor = ConsoleColor.White;
                }


            }

            // Key

            Random fstRnd = new Random();
            keyX = fstRnd.Next(2, mapSize - 2);

            Random secRnd = new Random();
            keyY = secRnd.Next(2, mapSize - 2);
            Console.SetCursorPosition(keyX, keyY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(KEY);

        }

        private static void Countdown()
        {

            for (int a = 3; a >= 0; a--)
            {
                Console.SetCursorPosition(5, 5);
                Console.WriteLine(a);
                Thread.Sleep(1000);
            }

            Console.SetCursorPosition(5, 5);
            Console.Write(countDowm[0]);
        }

        private static void Movement()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(posX, posY);
            Console.Write(playerA[3]);

            while (movement)
            {

                ConsoleKeyInfo input = Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[0]);

                        posY--;

                        if (posX == keyX && posY == keyY)
                        {
                            hasKey = true;

                        }

                        if (posX == doorX && posY == door && hasKey || posX == door && posY == doorY && hasKey)
                        {
                            movement = false;
                        }
                        else if (posY <= 0)
                        {
                            posY = 1;
                        }
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[1]);

                        break;

                    case ConsoleKey.DownArrow:
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[0]);

                        posY++;


                        if (posX == keyX && posY == keyY)
                        {
                            hasKey = true;

                        }

                        if (posX == doorX && posY == door && hasKey || posX == door && posY == doorY && hasKey)
                        {
                            movement = false;
                        }
                        else if (posY >= mapSize - 2)
                        {
                            posY = mapSize - 2;
                        }

                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[2]);



                        break;

                    case ConsoleKey.RightArrow:
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[0]);

                        posX++;

                        if (posX == keyX && posY == keyY)
                        {
                            hasKey = true;

                        }

                        if (posX == doorX && posY == door && hasKey || posX == door && posY == doorY && hasKey)
                        {
                            movement = false;
                        }
                        else if (posX >= mapSize - 2)
                        {
                            posX = mapSize - 2;
                        }

                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[3]);



                        break;

                    case ConsoleKey.LeftArrow:
                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[0]);

                        posX--;

                        if (posX == keyX && posY == keyY)
                        {
                            hasKey = true;

                        }

                        if (posX == doorX && posY == door && hasKey || posX == door && posY == doorY && hasKey)
                        {
                            movement = false;
                        }
                        else if (posX <= 1)
                        {
                            posX = 1;
                        }

                        Console.SetCursorPosition(posX, posY);
                        Console.Write(playerA[4]);

                        break;
                }


            }
            hasTime = false;


        }

        private static void End()
        {


            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();

            foreach (string temp in congratsA)
            {
                Console.WriteLine(temp);
            }

            Console.WriteLine($"Du hast {seconds} Sekunden gebraucht!");
            Console.ReadKey(true);

        }

    }
}