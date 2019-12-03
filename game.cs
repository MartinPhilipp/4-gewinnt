using System;

namespace C_sharp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            int input = -1;
            string entry;
            string[] coins = new string[] { "x", "o" };
            string[,] spielfeld = new string[8, 8];
            //initialisiere Spielfeld
            for (int y=0;  y<8; y++) 
            {
                for (int x=0; x<8; x++)
                {
                    spielfeld[y, x] = ".";
                }
            }
            //Print Spielfeld
            Viewer(spielfeld);
            while (true)
            { 
                for (int player = 1; player < 3; player++)
                {
                    do
                    {
                        Console.Write($"Ihre Eingabe Spieler {coins[player-1]} >>>");
                        entry = Console.ReadLine();
                        try
                        {
                            input = Convert.ToInt32(entry);
                        }
                        catch
                        {
                            Console.WriteLine("Bitte nur Zahlen eingeben!");
                            continue;
                        }
                        //Legal move check
                        if (spielfeld[0, input] != ".")
                        {
                            Console.WriteLine("This move isn't possible!");
                            input = -1;
                            continue;
                        }
                    } while (input < 0 || input > 7);
                    Console.WriteLine($"Spieler {coins[player-1]} setzt auf {input}");
                    //Finalize move
                    for (int y = 7; y > -1; y--)
                    {
                        if (spielfeld[y, input] == ".")
                        {
                            spielfeld[y, input] = coins[player - 1];
                            break;
                        }
                    }
                    Viewer(spielfeld);
                    //Check for win
                    if (Wincheck(spielfeld) == true)
                    {
                        Console.WriteLine($"Spieler {player} hat gewonnen!");
                        return;
                    }
                    
                }
            }
        }

        static bool Wincheck(string[,] spielfeld)
        {
            int[] dx = new int[] { 1, 1, 0, -1, -1, -1, 0, 1 };
            int[] dy = new int[] { 0, -1, -1, -1, 0, 1, 1, 1 };
            string me;
            bool ok;
            for (int y = 7; y > -1; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    me = spielfeld[y, x];
                    if (me==".")
                    {
                        continue;
                    }
                    for (int richtung=0; richtung<8; richtung++)
                    {
                        int deltax = dx[richtung];
                        int deltay = dy[richtung];
                        ok = true;
                        for (int a=1;a<4;a++)
                        {
                            try
                            {
                                if (spielfeld[y+deltay*a,x+deltax*a] != me)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                            catch
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok==true)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static void Viewer(string[,] spielfeld)
        {
            Console.WriteLine("Spielfeld:");
            Console.WriteLine("    0 1 2 3 4 5 6 7");
            //Console.WriteLine("    - - - - - - - -");
            for (int y=0; y<8; y++)
            {
                Console.Write(y);
                Console.Write(": |");
                for (int x=0; x<8; x++)
                {
                    Console.Write(spielfeld[y, x]);
                    Console.Write("|");
                }
                Console.WriteLine();
            }
        }
    }
}
