using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace PoliceAndCarGame
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            string[,] map = new string[10, 5];
            int startSeconds = 11;
            Random random = new Random();
            int spawn = random.Next(0, 5);
            int carRol = 0;
            int carCol = spawn;
            int tryMoveRol = carRol;
            int tryMoveCol = carCol;
            int health = 100;
            int polceRol = carRol;
            int polceCol = carCol;
            StringBuilder mapPrint = new StringBuilder();

            bool isWin = true;
            Console.WriteLine($"Your health is {health}");
            for (int rol = 0; rol < map.GetLength(0); rol++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    if (rol == 0 || rol == map.GetLength(0) - 1)
                    {
                        map[rol, col] = " ";
                    }
                    else
                    {

                        int randomОbject = random.Next(0, 3);
                        if (randomОbject == 0)
                        {
                            map[rol, col] = "@";
                        }
                        else
                        {
                            map[rol, col] = " ";
                        }
                    }
                }
            }
            map[carRol, carCol] = "C";
            map[map.GetLength(0) - 1, (map.GetLength(1) / 2) - 1] = "F";
            PrintMap(map, mapPrint);
            Console.WriteLine(mapPrint.ToString());

            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                

                if (startSeconds <= 0)
                {
                    Console.WriteLine("The police caught you!");
                    Console.WriteLine($"You are too slow");
                    Console.WriteLine("!!!GAME OVER!!!");
                    return;
                }
                startSeconds--;
                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    tryMoveRol--;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    tryMoveRol++;
                }
                else if (keyInfo.Key == ConsoleKey.LeftArrow)
                {
                    tryMoveCol--;
                }
                else if (keyInfo.Key == ConsoleKey.RightArrow)
                {
                    tryMoveCol++;
                }


                if (tryMoveCol >= 0 && tryMoveRol >= 0
                    && tryMoveCol < map.GetLength(1) && tryMoveRol < map.GetLength(0))
                {
                    if (map[tryMoveRol, tryMoveCol] == "P")
                    {
                        PrintMap(map, mapPrint);
                        Console.Clear();
                        Console.WriteLine("The police caught you!");
                        Console.WriteLine("!!!GAME OVER!!!");
                        map[polceRol, polceCol] = "X";
                        map[carRol, carCol] = " ";
                        Console.WriteLine(mapPrint);
                        return;
                    }
                    if (map[tryMoveRol, tryMoveCol] == " ")
                    {
                        map[polceRol, polceCol] = " ";
                        polceRol = carRol;
                        polceCol = carCol;
                        map[carRol, carCol] = "P";
                        carRol = tryMoveRol;
                        carCol = tryMoveCol;
                        map[tryMoveRol, tryMoveCol] = "C";
                        PrintMap(map, mapPrint);
                    }
                    else if (map[tryMoveRol, tryMoveCol] == "@")
                    {
                        health -= 20;
                        if (health <= 0)
                        {
                            map[tryMoveRol, tryMoveCol] = "X";
                            isWin = false;
                            map[polceRol, polceCol] = " ";
                            map[carRol, carCol] = " ";
                            PrintMap(map, mapPrint);
                            Console.Clear();
                            Console.WriteLine("GAME OVER");
                            break;
                        }
                        else
                        {
                            map[polceRol, polceCol] = " ";
                            polceRol = carRol;
                            polceCol = carCol;
                            map[carRol, carCol] = "P";
                            carRol = tryMoveRol;
                            carCol = tryMoveCol;
                            map[tryMoveRol, tryMoveCol] = "C";
                            PrintMap(map, mapPrint);
                        }
                    }
                    else if (map[tryMoveRol, tryMoveCol] == "F")
                    {
                        map[polceRol, polceCol] = " ";
                        map[carRol, carCol] = " ";
                        map[tryMoveRol, tryMoveCol] = "WIN";
                        PrintMap(map, mapPrint);
                        Console.Clear();
                        break;
                    }

                }
                else
                {
                    Console.Clear();
                    PrintMap(map, mapPrint);
                    Console.WriteLine("Wrong roald!");
                    Console.WriteLine("The police caught you!");
                    Console.WriteLine("!!!GAME OVER!!!");
                    Console.WriteLine(mapPrint);
                    map[polceRol, polceCol] = " ";
                    map[carRol, carCol] = "X";
                    return;
                }
                Console.Clear();
                Console.WriteLine($"Your health is {health}");
                Console.WriteLine($"You still have {startSeconds}s time!");
                Console.WriteLine(mapPrint);
            }

            if (isWin)
            {
                Console.WriteLine("You ran from the police!");
                Console.WriteLine($"Your health is {health}");
                Console.WriteLine(mapPrint);
            }
            else
            {
                Console.WriteLine("!!!GAME OVER!!!");
                Console.WriteLine(mapPrint);
            }
        }

        private static StringBuilder PrintMap(string[,] map, StringBuilder mapPrint)
        {
            mapPrint.Clear();
            for (int rol = 0; rol < map.GetLength(0); rol++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    mapPrint.Append(map[rol, col]);
                }
                mapPrint.Append(Environment.NewLine);
            }
            return mapPrint;
        }
    }
}
