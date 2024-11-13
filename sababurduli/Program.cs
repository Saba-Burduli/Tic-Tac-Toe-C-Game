using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace sababurduli
{
    internal class Program
    {

        public static void WriteWinNotification(string notification)
        {
            string filePath = @"C:\Users\User\Desktop\sababurduli main project\win_notifications.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine(notification);
            }
        }







        public static char playerSignature = ' ';
        static int turns = 0;

        static char[] ArrBoard =
            {

            '1', '2', '3', '4', '5', '6', '7', '8', '9'

            };

        public static void BoardReset()
        {
            char[] arrBoardInitialize =
                {

                '1', '2', '3', '4', '5', '6', '7', '8', '9'

                };

            ArrBoard = arrBoardInitialize;
            turns = 0;
            DrawBoard();
        }

        public static void DrawBoard()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|     {0}   |    {1}    |     {2}   |", ArrBoard[0], ArrBoard[1], ArrBoard[2]);
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|     {0}   |    {1}    |     {2}   |", ArrBoard[3], ArrBoard[4], ArrBoard[5]);
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("|     {0}   |    {1}    |     {2}   |", ArrBoard[6], ArrBoard[7], ArrBoard[8]);
            Console.WriteLine("|         |         |         |");
            Console.WriteLine("-------------------------------");
        }


        public static void Introduction()
        {
            Console.ForegroundColor = ConsoleColor.Yellow; ;
            Console.WriteLine(".-------.   _               .-------.                   .-------.                   ");
            Console.WriteLine("`._   _.'  :_;              `._   _.'                   `._   _.'                  ");
            Console.WriteLine("  :   :    .-.                :   :                       :   :                     ");
            Console.WriteLine("  :   :    : :   .--.         :   :   .--.    .--.        :   :   .--.  .--.       ");
            Console.WriteLine("  :   :    : :  '  ..'        :   :  ' .;';  '  ..'       :   :  ' .; :' '_.'   ");
            Console.WriteLine("  : - ;    : :  `.__.'        : - ;  `.__,_; `.__.'       : - ;  `.__.'`.__.'   ");

            Console.ResetColor();

            //Thread.Sleep(2000);                                        //edited
            //Console.WriteLine("mogesalmebit  tic tac toe -Shi , gtxovt daawiret nebismier gilaks");
            //Console.ReadKey();
            //Console.Clear();                                          //edited


            //Console.WriteLine("\n 1. shen  VS  mowinaagmdege  ,  2. Human vs AI\"  ... ");
            //Console.Write("Enter your choice (1 or 2): ");

            //int gameMode = Convert.ToInt32(Console.ReadLine());
            //bool isHumanVsAI = (gameMode == 2);


            //Console.WriteLine("\n gtxovt daawiret nebismier gilaks gasagrdzeleblad ");  ///edited
            //Console.ReadKey();
            Thread.Sleep(2000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Rules:");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("this 2 player have different keys (X) or (Y)" + "\n Player 1 - X.   Player 2 - 0.    ");
            Console.WriteLine("\n first player who can fill  Horizontal line or  Vertical or diagonal line  wins game !!!");
            Console.WriteLine("have nice game <3 ....");
            //Console.ReadKey();
            Thread.Sleep(2000);
            Console.WriteLine("\n 1. Human   VS  Human  ,  2. Human vs AI\"  ... ");
            Console.Write("Enter your choice (1 or 2): ");
        }

        static int CheckDuplicateData(ref int input)
        {
            if ((input >= 1) && (input <= 9) && (ArrBoard[input - 1] == 'X' || ArrBoard[input - 1] == 'O'))
            {
                string msg = "Invalid move! Cell already occupied or out of range.Input the number again:";
                Console.Write(msg);

                int currentY = Console.CursorTop;
                input = Convert.ToInt32(Console.ReadLine());

                Console.SetCursorPosition(0, currentY);
                Console.Write(new string(' ', Console.WindowWidth));

                CheckDuplicateData(ref input);
            }

            return input;
        }

        static void Main(string[] args)
        {
            Introduction();

            int gameMode;
            bool validChoice = int.TryParse(Console.ReadLine(), out gameMode) && (gameMode == 1 || gameMode == 2);
            if (!validChoice)
            {
                Console.WriteLine("Invalid choice. Defaulting to Human vs Human mode.");
                gameMode = 1;
            }
            bool isHumanVsAI = gameMode == 2;

            Random rnd = new Random();
            int player = rnd.Next(1, 3);
            int input = 0;
            //bool inputCorrect = true;                          ////edited

            do
            {
                DrawBoard();

                if (isHumanVsAI && player == 2)
                {
                    PlayAI();
                }
                else
                {
                    Console.Write("\nPlayer {0}'s turn:", player);
                    try
                    {
                        input = Convert.ToInt32(Console.ReadLine());
                        if (input >= 1 && input <= 9 && ArrBoard[input - 1] != 'X' && ArrBoard[input - 1] != 'O')
                        {
                            Xor0(player, input);
                            turns++;
                        }
                        else
                        {
                            input = CheckDuplicateData(ref input);
                            Xor0(player, input);
                            turns++;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Please enter a valid number between 1 and 9.");
                        continue;
                    }
                }

                DrawBoard();

                HorizontalWin();
               
                VerticalWin();
                DiagonalWin();

                if (turns == 9)
                {
                    Draw();
                    break;
                }

                player = player == 1 ? 2 : 1;
            } while (true);
        }

        public static void Xor0(int player, int input)
        {
            playerSignature = turns % 2 == 0 ? 'X' : 'O';
            ConsoleColor color = playerSignature == 'X' ? ConsoleColor.Green : ConsoleColor.White;
            switch (input)
            {
                case 1:
                    ArrBoard[0] = playerSignature;
                    break;
                case 2:
                    ArrBoard[1] = playerSignature;
                    break;
                case 3:
                    ArrBoard[2] = playerSignature;
                    break;
                case 4:
                    ArrBoard[3] = playerSignature;
                    break;
                case 5:
                    ArrBoard[4] = playerSignature;
                    break;
                case 6:
                    ArrBoard[5] = playerSignature;
                    break;
                case 7:
                    ArrBoard[6] = playerSignature;
                    break;
                case 8:
                    ArrBoard[7] = playerSignature;
                    break;
                case 9:
                    ArrBoard[8] = playerSignature;
                    break;
            }

            Console.ForegroundColor = color;

        }

        public static void PlayAI()
        {
            Random rnd = new Random();
            int aiMove;
            do
            {
                aiMove = rnd.Next(1, 10);
            } while (ArrBoard[aiMove - 1] == 'X' || ArrBoard[aiMove - 1] == 'O');

            Console.WriteLine("AI chooses position: " + aiMove);
            Xor0(2, aiMove);
            turns++;
        }

        public static void HorizontalWin()
        {

            char[] playerSignatures = { 'X', 'O' };

            foreach (char playerSignature in playerSignatures)

            {

                if ((ArrBoard[0] == playerSignature && ArrBoard[1] == playerSignature && ArrBoard[2] == playerSignature) ||
                    (ArrBoard[3] == playerSignature && ArrBoard[4] == playerSignature && ArrBoard[5] == playerSignature) ||
                    (ArrBoard[6] == playerSignature && ArrBoard[7] == playerSignature && ArrBoard[8] == playerSignature))

                {
                    WinArt();
                    Console.WriteLine($"Player {(playerSignature == 'X' ? 1 : 2)} WOW player 1 you win with Horizontal line .... <33 \n press any key \\n");
                    WriteWinNotification($"Player {(playerSignature == 'X' ? 1 : 2)} wins with a horizontal line."); ///for txt file
                    Console.ReadKey(); // added after checking
                    BoardReset();
                    break;
                }
            }
        }

        public static void VerticalWin()
        {
            char[] playerSignatures = { 'X', 'O' };
            foreach (char playerSignature in playerSignatures)
            {
                if ((ArrBoard[0] == playerSignature && ArrBoard[3] == playerSignature && ArrBoard[6] == playerSignature) ||
                    (ArrBoard[1] == playerSignature && ArrBoard[4] == playerSignature && ArrBoard[7] == playerSignature) ||
                    (ArrBoard[2] == playerSignature && ArrBoard[5] == playerSignature && ArrBoard[8] == playerSignature))
                {
                    Console.WriteLine($"Player {(playerSignature == 'X' ? 1 : 2)} " + "WOW player 1 , you win with Vertical line .... <33" +
                            "\nYou are best in this Game");
                    WriteWinNotification($"Player {(playerSignature == 'X' ? 1 : 2)} wins with a vertical line.");   ///for txt file
                    WinArt();
                    Console.WriteLine("Press any key ...");
                    Console.ReadKey();
                    BoardReset();
                    break;
                }
            }
        }

        public static void DiagonalWin()
        {
            char[] playerSignatures = { 'X', 'O' };
            foreach (char playerSignature in playerSignatures)
            {
                if ((ArrBoard[0] == playerSignature && ArrBoard[4] == playerSignature && ArrBoard[8] == playerSignature) ||
                    (ArrBoard[2] == playerSignature && ArrBoard[4] == playerSignature && ArrBoard[6] == playerSignature))
                {
                    Console.WriteLine($"Player {(playerSignature == 'X' ? 1 : 2)}" + "WOW!, player 1 , you win with Diagonal line !!! <3 " +

                                          "\n You are best in this Game !\n \n \n");

                    WriteWinNotification($"Player {(playerSignature == 'X' ? 1 : 2)} wins with a diagonal line."); //for txt file

                    WinArt();
                    Console.WriteLine("Press any key ...");
                    Console.ReadKey();
                    BoardReset();
                    break;
                }
        
            }
        }

        public static void Draw()
        {
            Console.WriteLine("Draw ... Nice play ...." + "\n  Press any key .......");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("0000  0000       00     00     00         ");
            Console.WriteLine("0   0 0   0     0  0     0     0        ");
            Console.WriteLine("0   0 0000     000000    0  0  0        ");
            Console.WriteLine("0   0 0  0     0    0    0 0 0 0       ");
            Console.WriteLine("0   0 0   0   0      0    0 0 0         ");
            Console.WriteLine("0 0 0 0    0 0        0    0 0        ");
            Console.ResetColor();


            Console.ReadKey();
            BoardReset();
        }

        public static void WinArt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" ÛÛÛÛÛ ÛÛÛÛÛ                        ÛÛÛÛÛ   ÛÛÛ   ÛÛÛÛÛ                     ÛÛÛ ÛÛÛ     ");
            Console.WriteLine("°°ÛÛÛ °°ÛÛÛ                        °°ÛÛÛ   °ÛÛÛ  °°ÛÛÛ                     °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine(" °°ÛÛÛ ÛÛÛ    ÛÛÛÛÛÛ  ÛÛÛÛÛ ÛÛÛÛ    °ÛÛÛ   °ÛÛÛ   °ÛÛÛ   ÛÛÛÛÛÛ  ÛÛÛÛÛÛÛÛ  °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine("  °°ÛÛÛÛÛ    ÛÛÛ°°ÛÛÛ°°ÛÛÛ °ÛÛÛ     °ÛÛÛ   °ÛÛÛ   °ÛÛÛ  ÛÛÛ°°ÛÛÛ°°ÛÛÛ°°ÛÛÛ °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine("   °°ÛÛÛ    °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ     °°ÛÛÛ  ÛÛÛÛÛ  ÛÛÛ  °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ°ÛÛÛ     ");
            Console.WriteLine("    °ÛÛÛ    °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ      °°°ÛÛÛÛÛ°ÛÛÛÛÛ°   °ÛÛÛ °ÛÛÛ °ÛÛÛ °ÛÛÛ °°° °°°      ");
            Console.WriteLine("    ÛÛÛÛÛ   °°ÛÛÛÛÛÛ  °°ÛÛÛÛÛÛÛÛ       °°ÛÛÛ °°ÛÛÛ     °°ÛÛÛÛÛÛ  ÛÛÛÛ ÛÛÛÛÛ ÛÛÛ ÛÛÛ     ");
            Console.WriteLine("    °°°°°     °°°°°°    °°°°°°°°         °°°   °°°       °°°°°°  °°°° °°°°° °°° °°°     ");
            Console.ResetColor();
        }

             
    }
}
