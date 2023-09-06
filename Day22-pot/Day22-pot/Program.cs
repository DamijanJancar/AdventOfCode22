using static System.Net.Mime.MediaTypeNames;

namespace Day22_pot
{
    /*
--- Day 22: Monkey Map ---
The monkeys take you on a surprisingly easy trail through the jungle. They're even going in roughly the right direction according to your handheld device's Grove Positioning System.

As you walk, the monkeys explain that the grove is protected by a force field. To pass through the force field, you have to enter a password; doing so involves tracing a specific path on a strangely-shaped board.

At least, you're pretty sure that's what you have to do; the elephants aren't exactly fluent in monkey.

The monkeys give you notes that they took when they last saw the password entered (your puzzle input).

For example:

        ...#
        .#..
        #...
        ....
...#.......#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.

10R5L5R10L4R5L5
The first half of the monkeys' notes is a map of the board. It is comprised of a set of open tiles (on which you can move, drawn .) and solid walls (tiles which you cannot enter, drawn #).

The second half is a description of the path you must follow. It consists of alternating numbers and letters:

A number indicates the number of tiles to move in the direction you are facing. If you run into a wall, you stop moving forward and continue with the next instruction.
A letter indicates whether to turn 90 degrees clockwise (R) or counterclockwise (L). Turning happens in-place; it does not change your current tile.
So, a path like 10R5 means "go forward 10 tiles, then turn clockwise 90 degrees, then go forward 5 tiles".

You begin the path in the leftmost open tile of the top row of tiles. Initially, you are facing to the right (from the perspective of how the map is drawn).

If a movement instruction would take you off of the map, you wrap around to the other side of the board. In other words, if your next tile is off of the board, you should instead look in the direction opposite of your current facing as far as you can until you find the opposite edge of the board, then reappear there.

For example, if you are at A and facing to the right, the tile in front of you is marked B; if you are at C and facing down, the tile in front of you is marked D:

        ...#
        .#..
        #...
        ....
...#.D.....#
........#...
B.#....#...A
.....C....#.
        ...#....
        .....#..
        .#......
        ......#.
It is possible for the next tile (after wrapping around) to be a wall; this still counts as there being a wall in front of you, and so movement stops before you actually wrap to the other side of the board.

By drawing the last facing you had with an arrow on each tile you visit, the full path taken by the above example looks like this:

        >>v#    
        .#v.    
        #.v.    
        ..v.    
...#...v..v#    
>>>v...>#.>>    
..#v...#....    
...>>>>v..#.    
        ...#....
        .....#..
        .#......
        ......#.
To finish providing the password to this strange input device, you need to determine numbers for your final row, column, and facing as your final position appears from the perspective of the original map. Rows start from 1 at the top and count downward; columns start from 1 at the left and count rightward. (In the above example, row 1, column 1 refers to the empty space with no tile on it in the top-left corner.) Facing is 0 for right (>), 1 for down (v), 2 for left (<), and 3 for up (^). The final password is the sum of 1000 times the row, 4 times the column, and the facing.

In the above example, the final row is 6, the final column is 8, and the final facing is 0. So, the final password is 1000 * 6 + 4 * 8 + 0: 6032.

Follow the path given in the monkeys' notes. What is the final password?    
     
-----------------------------------------------------------------------------------------------------------------------------
    
 IDEJA.... preberem maptiko, preberem pot
    pot: -> List
    potem bereš List -> če je levo ali desno obrneš smer (0,1,2,3) +- po modulu 4
                        če je številka premakneš pozicijo za toliko polj
     
     matriko sem odmaknil od roba za eno polje, da ne potrebujem dvojnih robnih kriterijev
     
 ------------------------------------------------------------------------------------------------------------------------------    
     
     
     --- Part Two ---
As you reach the force field, you think you hear some Elves in the distance. Perhaps they've already arrived?

You approach the strange input device, but it isn't quite what the monkeys drew in their notes. Instead, you are met with a large cube; each of its six faces is a square of 50x50 tiles.

To be fair, the monkeys' map does have six 50x50 regions on it. If you were to carefully fold the map, you should be able to shape it into a cube!

In the example above, the six (smaller, 4x4) faces of the cube are:

        1111
        1111
        1111
        1111
222233334444
222233334444
222233334444
222233334444
        55556666
        55556666
        55556666
        55556666
You still start in the same position and with the same facing as before, but the wrapping rules are different. Now, if you would walk off the board, you instead proceed around the cube. From the perspective of the map, this can look a little strange. In the above example, if you are at A and move to the right, you would arrive at B facing down; if you are at C and move down, you would arrive at D facing up:

        ...#
        .#..
        #...
        ....
...#.......#
........#..A
..#....#....
.D........#.
        ...#..B.
        .....#..
        .#......
        ..C...#.
Walls still block your path, even if they are on a different face of the cube. If you are at E facing up, your movement is blocked by the wall marked by the arrow:

        ...#
        .#..
     -->#...
        ....
...#..E....#
........#...
..#....#....
..........#.
        ...#....
        .....#..
        .#......
        ......#.
Using the same method of drawing the last facing you had with an arrow on each tile you visit, the full path taken by the above example now looks like this:

        >>v#    
        .#v.    
        #.v.    
        ..v.    
...#..^...v#    
.>>>>>^.#.>>    
.^#....#....    
.^........#.    
        ...#..v.
        .....#v.
        .#v<<<<.
        ..v...#.
The final password is still calculated from your final position and facing from the perspective of the map. In this example, the final row is 5, the final column is 7, and the final facing is 3, so the final password is 1000 * 5 + 4 * 7 + 3 = 5031.

Fold the map into a cube, then follow the path given in the monkeys' notes. What is the final password?

     ------------------------------------------------------------------------
    IDEJA
    razdelim na polja kocke in na rognih prenikih definiram za vsako polje kako se pretvorijo koordinate in smer

    klical bom vsak premik posebej
    

     
     */
    internal class Program
    {
        public struct Pozicija
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Smer { get; set; }
            public Pozicija(int x, int y, int smer)
            {
                this.X = x;
                this.Y = y;
                this.Smer = smer;
            }
            public void ObrniLevo()
            {
                this.Smer = (this.Smer - 1) % 4;
                if (this.Smer == -1) this.Smer = 3;
            }
            public void ObrniDesno()
            {
                this.Smer = (this.Smer + 1) % 4;

            }
        }
        static void NarisiMatriko(char[,] matrika)
        {
            Console.WriteLine("Risem matriko");
            for (int x = 0; x < matrika.GetLength(0); x++)
            {
                for (int y = 0; y < matrika.GetLength(1); y++)
                {
                    Console.Write(matrika[x, y]);
                }
                Console.WriteLine();
            }
        }
        static char[,] BeriMatriko(string fileName)
        {
            //preberem datoteko v 2d matrico
            //in jo premaknem za eno polje, da ne rabim dvojnih robnih pogojev
            string[] vrstica = File.ReadLines(fileName).ToArray();
            char[,] matrika = new char[202, 152];
            //char[,] matrika = new char[vrstica.Length, vrstica[0].Length];


            for (int x = 0; x < 202; x++)
            {
                for (int y = 0; y < 152; y++)
                {
                    matrika[x, y] = ' ';
                }
            }

            for (int i = 0; i < vrstica.Length; i++)
            {
                for (int j = 0; j < vrstica[i].Length; j++)
                {
                    matrika[i + 1, j + 1] = char.Parse("" + vrstica[i][j]);
                }
            }

            return matrika;

        }

        static char[,] BeriMatriko2(string fileName)
        {
            //preberem datoteko v 2d matrico
            //za drugi del moram popravit vse robne pogoje - še enktar preberem

            string[] vrstica = File.ReadLines(fileName).ToArray();
            char[,] matrika = new char[200, 150];



            for (int x = 0; x < 200; x++)
            {
                for (int y = 0; y < 150; y++)
                {
                    matrika[x, y] = ' ';
                }
            }

            for (int i = 0; i < vrstica.Length; i++)
            {
                for (int j = 0; j < vrstica[i].Length; j++)
                {
                    matrika[i, j] = char.Parse("" + vrstica[i][j]);
                }
            }

            return matrika;

        }


        static List<string> PreberiUkaze(string vhodniPodatki)
        {
            List<string> ukazi = new List<string>();

            string premik = "";
            foreach (char v in vhodniPodatki)
            {
                if (char.IsDigit(v))
                {
                    premik += v;
                }
                else if (v == 'R' || v == 'L')
                {
                    if (!string.IsNullOrEmpty(premik))
                    {
                        ukazi.Add(premik);
                        premik = "";
                    }

                    if (v == 'R')
                    {
                        ukazi.Add("Desno");
                    }
                    else if (v == 'L')
                    {
                        ukazi.Add("Levo");
                    }
                }
            }
            // Dodaj zadnje številke, če niso bile dodane
            if (!string.IsNullOrEmpty(premik))
            {
                ukazi.Add(premik);
            }
            return ukazi;
        }

        static void PremakniPozicijo(ref char[,] matrika, ref Pozicija trenutnaP, int koraki, ref char[,] matrika2)
        {
            //če si na robu -> na začetek
            //če je '.' lahko premakneš +1
            //če je '#' moraš break
            //če je ' ' greš naprej do '.' in premakneš
            //če je ' ' greš naprej če je '#' moraš nazaj in brake
            if (trenutnaP.X == 176 && trenutnaP.Y == 49)
            {
                //Console.WriteLine("PREVERKA: " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
            }
            if (trenutnaP.Smer == 1)
            {
                //Console.WriteLine("Obrnjen dol");
                //Console.WriteLine("premik: " + koraki);
                for (int i = 1; i <= koraki; i++)
                {
                    if (matrika[trenutnaP.X + 1, trenutnaP.Y] == ' ' || trenutnaP.X + 1 == matrika.GetLength(0))
                    {
                        //če smo na koncu in je na začetku stena
                        int tempX = trenutnaP.X;
                        trenutnaP.X = 1;
                        if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                        {
                            trenutnaP.X = tempX;
                            break;
                        }
                        else if (matrika[trenutnaP.X + 1, trenutnaP.Y] == ' ')
                        {
                            while (matrika[trenutnaP.X + 1, trenutnaP.Y] == ' ')
                            {
                                trenutnaP.X++;
                                if (trenutnaP.X == matrika.GetLength(0)) trenutnaP.X = 1;
                            }
                            if (matrika[trenutnaP.X + 1, trenutnaP.Y] == '.')
                            {
                                trenutnaP.X++;
                                matrika2[trenutnaP.X, trenutnaP.Y] = 'v';
                            }
                            else if (matrika[trenutnaP.X + 1, trenutnaP.Y] == '#')
                            {
                                trenutnaP.X = tempX;
                                break;
                            }

                        }
                    }
                    else if (matrika[trenutnaP.X + 1, trenutnaP.Y] == '.')
                    {
                        trenutnaP.X++;
                        matrika2[trenutnaP.X, trenutnaP.Y] = 'v';
                    }

                    else if (matrika[trenutnaP.X + 1, trenutnaP.Y] == '#')
                    {
                        //Console.WriteLine("Zaletel " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer + " " + matrika[trenutnaP.X + 1, trenutnaP.Y]);
                        break;

                    }
                    //Console.WriteLine("trenutna pozicija " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);

                }
            }

            if (trenutnaP.Smer == 0)
            {
                //Console.WriteLine("Obrnjen desno");
                //Console.WriteLine("premik: " + koraki);
                for (int i = 1; i <= koraki; i++)
                {
                    if (matrika[trenutnaP.X, trenutnaP.Y + 1] == ' ' || trenutnaP.Y + 1 == matrika.GetLength(1))
                    {
                        //če smo na koncu in je na začetku stena
                        int tempY = trenutnaP.Y;
                        trenutnaP.Y = 1;
                        if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                        {
                            trenutnaP.Y = tempY;
                            break;
                        }
                        else if (matrika[trenutnaP.X, trenutnaP.Y] == ' ')
                        {
                            while (matrika[trenutnaP.X, trenutnaP.Y + 1] == ' ')
                            {
                                trenutnaP.Y++;
                                if (trenutnaP.Y == matrika.GetLength(1)) trenutnaP.Y = 1;
                            }
                            if (matrika[trenutnaP.X, trenutnaP.Y + 1] == '.')
                            {
                                trenutnaP.Y++;
                                matrika2[trenutnaP.X, trenutnaP.Y] = '>';
                            }
                            else if (matrika[trenutnaP.X, trenutnaP.Y + 1] == '#')
                            {
                                trenutnaP.Y = tempY;
                                break;
                            }

                        }
                    }
                    else if (matrika[trenutnaP.X, trenutnaP.Y + 1] == '.')
                    {
                        trenutnaP.Y++;
                        matrika2[trenutnaP.X, trenutnaP.Y] = '>';
                    }
                    else if (matrika[trenutnaP.X, trenutnaP.Y + 1] == '#')
                    {
                        //Console.WriteLine("Zaletel " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer + " " + matrika[trenutnaP.X, trenutnaP.Y + 1]);
                        break;

                    }
                    //Console.WriteLine("trenutna pozicija " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);

                }
            }

            if (trenutnaP.Smer == 3)
            {
                //Console.WriteLine("Obrnjen gor");
                //Console.WriteLine("premik: " + koraki);
                for (int i = 1; i <= koraki; i++)
                {
                    if (matrika[trenutnaP.X - 1, trenutnaP.Y] == ' ' || trenutnaP.X == 0)
                    {
                        //če smo na koncu in je na začetku stena
                        int tempX = trenutnaP.X;
                        trenutnaP.X = matrika.GetLength(0) - 1;
                        if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                        {
                            trenutnaP.X = tempX;
                            break;
                        }
                        else if (matrika[trenutnaP.X, trenutnaP.Y] == ' ')
                        {
                            while (matrika[trenutnaP.X - 1, trenutnaP.Y] == ' ')
                            {
                                trenutnaP.X--;
                                if (trenutnaP.X == 0) trenutnaP.X = matrika.GetLength(0) - 1;
                                //if (trenutnaP.X == 0) trenutnaP.X = 201;
                            }
                            if (matrika[trenutnaP.X - 1, trenutnaP.Y] == '.')
                            {
                                trenutnaP.X--;
                                matrika2[trenutnaP.X, trenutnaP.Y] = '^';
                            }
                            else if (matrika[trenutnaP.X - 1, trenutnaP.Y] == '#')
                            {
                                trenutnaP.X = tempX;
                                break;
                            }

                        }
                    }
                    else if (matrika[trenutnaP.X - 1, trenutnaP.Y] == '.')
                    {
                        trenutnaP.X--;
                        matrika2[trenutnaP.X, trenutnaP.Y] = '^';
                    }

                    else if (matrika[trenutnaP.X - 1, trenutnaP.Y] == '#')
                    {
                        //Console.WriteLine("Zaletel " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer + " " + matrika[trenutnaP.X - 1, trenutnaP.Y]);
                        break;

                    }

                    //Console.WriteLine("trenutna pozicija " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);

                }
            }

            if (trenutnaP.Smer == 2)
            {
                //Console.WriteLine("Obrnjen levo");
                //Console.WriteLine("premik: " + koraki);
                for (int i = 1; i <= koraki; i++)
                {
                    if (matrika[trenutnaP.X, trenutnaP.Y - 1] == ' ' || trenutnaP.Y == 0)
                    {
                        //če smo na koncu in je na začetku stena
                        int tempY = trenutnaP.Y;
                        trenutnaP.Y = matrika.GetLength(1) - 1;
                        if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                        {
                            trenutnaP.Y = tempY;
                            break;
                        }
                        if (matrika[trenutnaP.X, trenutnaP.Y] == ' ')
                        {
                            while (matrika[trenutnaP.X, trenutnaP.Y - 1] == ' ')
                            {
                                trenutnaP.Y--;
                                if (trenutnaP.Y == 0) trenutnaP.Y = matrika.GetLength(1) - 1;
                                //if (trenutnaP.Y == 0) trenutnaP.Y = 151;
                            }
                            if (matrika[trenutnaP.X, trenutnaP.Y - 1] == '.')
                            {
                                trenutnaP.Y--;
                                matrika2[trenutnaP.X, trenutnaP.Y] = '<';
                                if (matrika[trenutnaP.X, trenutnaP.Y - 1] == '#') break;
                            }
                            if (matrika[trenutnaP.X, trenutnaP.Y - 1] == '#')
                            {
                                trenutnaP.Y = tempY;
                                break;
                            }

                        }
                    }
                    else if (matrika[trenutnaP.X, trenutnaP.Y - 1] == '.')
                    {
                        trenutnaP.Y--;
                        matrika2[trenutnaP.X, trenutnaP.Y] = '<';
                    }

                    else if (matrika[trenutnaP.X, trenutnaP.Y - 1] == '#')
                    {
                        //Console.WriteLine("Zaletel " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer + " " + matrika[trenutnaP.X, trenutnaP.Y - 1]);
                        break;

                    }
                    //Console.WriteLine("trenutna pozicija " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);

                }
            }
        }


        static void PremakniPozicijo2(ref char[,] matrika, ref Pozicija trenutnaP, ref char[,] matrika2)
        {
            //za drugi del - samo po en korak
            //če si na robu -> preračunaj kam
            //
            //  0-49    50-99   100-149
            //          1       2
            //          3
            //  4       5
            //  6
            //
            //1 gor     -> 6 z leve
            //1 levo    -> 4 z leve
            // ....



            //spravimo začasno, če naletimo na '#'
            int tempx = trenutnaP.X;
            int tempy = trenutnaP.Y;
            int temps = trenutnaP.Smer;
            if (trenutnaP.X == 176 && trenutnaP.Y == 49)
            {
                Console.WriteLine("PREVERKA: " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
            }
            //polje 1
            if (trenutnaP.X >= 0 && trenutnaP.X < 50 && trenutnaP.Y >= 50 && trenutnaP.Y < 100)
            {
                //polje 1
                //Console.WriteLine("polje 1");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 1");
                    trenutnaP.Y++;
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 1");
                    trenutnaP.X++;
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 1");
                    if (trenutnaP.Y == 50)
                    {
                        trenutnaP.Smer = 0;
                        trenutnaP.X = 149 - trenutnaP.X;
                        trenutnaP.Y = 0;
                    }
                    else trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 1");

                    if (trenutnaP.X == 0)
                    {
                        trenutnaP.Smer = 0;
                        trenutnaP.X = trenutnaP.Y + 100;
                        trenutnaP.Y = 0;
                    }
                    else
                    {
                        trenutnaP.X--;
                    }
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }
            //polje 2
            else if (trenutnaP.X >= 0 && trenutnaP.X < 50 && trenutnaP.Y >= 100 && trenutnaP.Y < 150)
            {
                //polje 2
                //Console.WriteLine("polje 2");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 2");
                    if (trenutnaP.Y == 149)
                    {
                        trenutnaP.Smer = 2;
                        trenutnaP.X = 149 - trenutnaP.X;
                        trenutnaP.Y = 99;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 2");
                    if (trenutnaP.X == 49)
                    {
                        trenutnaP.Smer = 2;
                        trenutnaP.X = trenutnaP.Y - 50;
                        trenutnaP.Y = 99;
                    }
                    else
                    {
                        trenutnaP.X++;
                    }
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 2");

                    trenutnaP.Y--;

                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 2");
                    if (trenutnaP.X == 0)
                    {
                        trenutnaP.X = 199;
                        trenutnaP.Y = trenutnaP.Y - 100;
                    }
                    else
                    {
                        trenutnaP.X--;
                    }
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }

            //polje 3
            else if (trenutnaP.X >= 50 && trenutnaP.X < 100 && trenutnaP.Y >= 50 && trenutnaP.Y < 100)
            {
                //polje 3
                //Console.WriteLine("polje 3");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 3");
                    if (trenutnaP.Y == 99)
                    {
                        trenutnaP.Smer = 3;
                        trenutnaP.Y = trenutnaP.X + 50;
                        trenutnaP.X = 49;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 3");
                    trenutnaP.X++;
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 3");
                    if (trenutnaP.Y == 50)
                    {
                        trenutnaP.Smer = 1;
                        trenutnaP.Y = trenutnaP.X - 50;
                        trenutnaP.X = 100;
                    }
                    else trenutnaP.Y--;

                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 3");
                    trenutnaP.X--;
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }

            }
            //polje 4
            else if (trenutnaP.X >= 100 && trenutnaP.X < 150 && trenutnaP.Y >= 0 && trenutnaP.Y < 50)
            {
                //polje 4
                //Console.WriteLine("polje 4");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 4");
                    trenutnaP.Y++;
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 4");
                    trenutnaP.X++;
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 4");
                    if (trenutnaP.Y == 0)
                    {
                        trenutnaP.Smer = 0;
                        trenutnaP.X = 149 - trenutnaP.X;
                        trenutnaP.Y = 50;
                    }
                    else trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 4");

                    if (trenutnaP.X == 100)
                    {
                        trenutnaP.Smer = 0;
                        trenutnaP.X = trenutnaP.Y + 50;
                        trenutnaP.Y = 50;
                    }
                    else
                    {
                        trenutnaP.X--;
                    }
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }
            //polje 5
            else if (trenutnaP.X >= 100 && trenutnaP.X < 150 && trenutnaP.Y >= 50 && trenutnaP.Y < 100)
            {
                //polje 2
                //Console.WriteLine("polje 5");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 5");
                    if (trenutnaP.Y == 99)
                    {
                        trenutnaP.Smer = 2;
                        trenutnaP.X = 149 - trenutnaP.X;
                        trenutnaP.Y = 149;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 5");
                    if (trenutnaP.X == 149)
                    {
                        trenutnaP.Smer = 2;
                        trenutnaP.X = trenutnaP.Y + 100;
                        trenutnaP.Y = 49;
                    }
                    else
                    {
                        trenutnaP.X++;
                    }
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 5");
                    trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 5");
                    trenutnaP.X--;
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }

            //polje 6
            else if (trenutnaP.X >= 150 && trenutnaP.X < 200 && trenutnaP.Y >= 0 && trenutnaP.Y < 50)
            {
                //polje 6
                //Console.WriteLine("polje 6");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 6");
                    if (trenutnaP.Y == 49)
                    {
                        trenutnaP.Smer = 3;
                        trenutnaP.Y = trenutnaP.X - 100;
                        trenutnaP.X = 149;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 6");
                    if (trenutnaP.X == 199)
                    {
                        trenutnaP.Y = trenutnaP.Y + 100;
                        trenutnaP.X = 0;
                    }
                    else
                    {
                        trenutnaP.X++;
                    }
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 6");
                    if (trenutnaP.Y == 0)
                    {
                        trenutnaP.Smer = 1;
                        trenutnaP.Y = trenutnaP.X - 100;
                        trenutnaP.X = 0;
                    }
                    else
                    {
                        trenutnaP.Y--;
                    }
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 6");
                    trenutnaP.X--;
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }







        }
        static void PremakniPozicijo3(ref char[,] matrika, ref Pozicija trenutnaP, ref char[,] matrika2)
        {
            //za drugi del - samo po en korak
            //če si na robu -> preračunaj kam
            //
            //  0-49    50-99   100-149
            //          1       2
            //          3
            //  4       5
            //  6
            //
            //1 gor     -> 6 z leve
            //1 levo    -> 4 z leve
            // ....



            //spravimo začasno, če naletimo na '#'
            int tempx = trenutnaP.X;
            int tempy = trenutnaP.Y;
            int temps = trenutnaP.Smer;
            if (trenutnaP.X == 2 && trenutnaP.Y == 51)
            {
                //Console.WriteLine("PREVERKA: " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
            }
            //polje 1
            if (trenutnaP.X >= 0 && trenutnaP.X < 50 && trenutnaP.Y >= 50 && trenutnaP.Y < 100)
            {
                //polje 1
                //Console.WriteLine("polje 1");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 1");
                    trenutnaP.Y++;
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 1");
                    trenutnaP.X++;
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 1");
                    if (trenutnaP.Y == 50)
                    {
                        trenutnaP.Y = 149;
                    }
                    else trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 1");
                    if (trenutnaP.X == 0)
                    {
                        trenutnaP.X = 149;
                    }
                    else
                    {
                        trenutnaP.X--;
                    }
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }
            //polje 2
            else if (trenutnaP.X >= 0 && trenutnaP.X < 50 && trenutnaP.Y >= 100 && trenutnaP.Y < 150)
            {
                //polje 2
                //Console.WriteLine("polje 2");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 2");
                    if (trenutnaP.Y == 149)
                    {
                        trenutnaP.Y = 50;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 2");
                    if (trenutnaP.X == 49)
                    {
                        trenutnaP.X = 0;
                    }
                    else
                    {
                        trenutnaP.X++;
                    }
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 2");

                    trenutnaP.Y--;

                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 2");
                    if (trenutnaP.X == 0)
                    {
                        trenutnaP.X = 49;
                    }
                    else
                    {
                        trenutnaP.X--;
                    }
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }

            //polje 3
            else if (trenutnaP.X >= 50 && trenutnaP.X < 100 && trenutnaP.Y >= 50 && trenutnaP.Y < 100)
            {
                //polje 3
                //Console.WriteLine("polje 3");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 3");
                    if (trenutnaP.Y == 99)
                    {
                        trenutnaP.Y = 50;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 3");
                    trenutnaP.X++;
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 3");
                    if (trenutnaP.Y == 50)
                    {
                        trenutnaP.Y = 99;
                    }
                    else trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 3");
                    trenutnaP.X--;
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }

            }
            //polje 4
            else if (trenutnaP.X >= 100 && trenutnaP.X < 150 && trenutnaP.Y >= 0 && trenutnaP.Y < 50)
            {
                //polje 4
                //Console.WriteLine("polje 4");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 4");
                    trenutnaP.Y++;
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 4");
                    trenutnaP.X++;
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 4");
                    if (trenutnaP.Y == 0)
                    {
                        trenutnaP.Y = 99;
                    }
                    else trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 4");
                    if (trenutnaP.X == 100)
                    {
                        trenutnaP.X = 199;
                    }
                    else
                    {
                        trenutnaP.X--;
                    }
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }
            //polje 5
            else if (trenutnaP.X >= 100 && trenutnaP.X < 150 && trenutnaP.Y >= 50 && trenutnaP.Y < 100)
            {
                //polje 2
                //Console.WriteLine("polje 5");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 5");
                    if (trenutnaP.Y == 99)
                    {
                        trenutnaP.Y = 0;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 5");
                    if (trenutnaP.X == 149)
                    {
                        trenutnaP.X = 0;
                    }
                    else
                    {
                        trenutnaP.X++;
                    }
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 5");
                    trenutnaP.Y--;
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 5");
                    trenutnaP.X--;
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }

            //polje 6
            else if (trenutnaP.X >= 150 && trenutnaP.X < 200 && trenutnaP.Y >= 0 && trenutnaP.Y < 50)
            {
                //polje 6
                //Console.WriteLine("polje 6");
                if (trenutnaP.Smer == 0)
                {
                    //Console.WriteLine("Obrnjen desno - polje 6");
                    if (trenutnaP.Y == 49)
                    {
                        trenutnaP.Y = 0;
                    }
                    else
                    {
                        trenutnaP.Y++;
                    }
                }
                else if (trenutnaP.Smer == 1)
                {
                    //Console.WriteLine("Obrnjen dol - polje 6");
                    if (trenutnaP.X == 199)
                    {
                        trenutnaP.X = 100;
                    }
                    else
                    {
                        trenutnaP.X++;
                    }
                }
                else if (trenutnaP.Smer == 2)
                {
                    //Console.WriteLine("Obrnjen levo - polje 6");
                    if (trenutnaP.Y == 0)
                    {
                        trenutnaP.Y = 49;
                    }
                    else
                    {
                        trenutnaP.Y--;
                    }
                }
                else if (trenutnaP.Smer == 3)
                {
                    //Console.WriteLine("Obrnjen gor - polje 6");
                    trenutnaP.X--;
                }
                if (matrika[trenutnaP.X, trenutnaP.Y] == '#')
                {
                    //če je '#' ne moremo -> vrnem prejšno pozicijo
                    //Console.WriteLine("Zaletel v #  " + " " + trenutnaP.X + " " + trenutnaP.Y + " " + trenutnaP.Smer);
                    trenutnaP.X = tempx;
                    trenutnaP.Y = tempy;
                    trenutnaP.Smer = temps;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Berem podatke");
            //string fileNameM = "c:\\FAX\\M09 - Algoritmi\\Naloge\\Day22-pot\\day22mat.txt";
            string fileNameM = "d:\\FAX\\M10 - Algoritmi\\Naloga\\Day22-pot\\day22mat.txt";
            char[,] matrika = BeriMatriko(fileNameM);
            char[,] matrika2 = BeriMatriko(fileNameM);

            /*
            char[,] matrika2 = new char[202, 152];
            for (int x = 0; x < 202; x++)
            {
                for (int y = 0; y < 152; y++)
                {
                    matrika2[x, y] = ' ';
                }
            }
            */
            //matrika2 = matrika;

            //Preverim če pravilno preberem
            //NarisiMatriko(matrika2);

            //string fileNameP = "c:\\FAX\\M09 - Algoritmi\\Naloge\\Day22-pot\\day22pot.txt";
            string fileNameP = "d:\\FAX\\M10 - Algoritmi\\Naloga\\Day22-pot\\day22pot.txt";
            string vhodniPodatki = File.ReadAllText(fileNameP);
            List<string> ukazi = PreberiUkaze(vhodniPodatki);

            //Preverim če pravilno preberem
            //foreach (string ukaz in ukazi) Console.WriteLine(ukaz);

            Pozicija trenutnaPozicija = new Pozicija(1, 0, 0);
            //Console.WriteLine("Zacetna pozicija1: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
            //na začetno pozicijo
            while (matrika[trenutnaPozicija.X, trenutnaPozicija.Y + 1] == ' ')
            {
                trenutnaPozicija.Y++;
            }
            //trenutnaPozicija.Y++;
            //Console.WriteLine("Zacetna pozicija2: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer + " " + matrika[trenutnaPozicija.X, trenutnaPozicija.Y]);

            foreach (string ukaz in ukazi)
            {
                //Console.WriteLine(ukaz);
                if (ukaz.All(char.IsDigit))
                {
                    int premik = int.Parse(ukaz);
                    //Console.WriteLine("Premik za: " + premik);
                    PremakniPozicijo(ref matrika, ref trenutnaPozicija, premik, ref matrika2);

                }
                if (ukaz == "Levo")
                {
                    //Console.WriteLine("Obracan levo");
                    trenutnaPozicija.ObrniLevo();
                    matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'L';
                    //Console.WriteLine("L: " + trenutnaPozicija.Smer);
                }
                if (ukaz == "Desno")
                {
                    //Console.WriteLine("Obracan desno");
                    trenutnaPozicija.ObrniDesno();
                    matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'D';
                    //Console.WriteLine("D: " + trenutnaPozicija.Smer);
                }
                //Console.WriteLine("Trenutna lokacija: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);


            }
            //NarisiMatriko(matrika);
            //NarisiMatriko(matrika2);

            //Console.WriteLine("konec: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);  //75 97 1
            Console.WriteLine("Koncna pozicija: " + (trenutnaPozicija.X) + " " + (trenutnaPozicija.Y) + " " + (trenutnaPozicija.Smer));  //75 97 0
            Console.WriteLine("odgovor: " + (1000 * (trenutnaPozicija.X) + 4 * (trenutnaPozicija.Y) + (trenutnaPozicija.Smer)));
            //     75388

//********************* DRUGI DEL
            matrika = BeriMatriko2(fileNameM);
            matrika2 = BeriMatriko2(fileNameM);

            //NarisiMatriko(matrika);
            //NarisiMatriko(matrika2);

            //na začetno pozicijo
            trenutnaPozicija.X = 0;
            trenutnaPozicija.Y = 0;
            trenutnaPozicija.Smer = 0;

            while (matrika[trenutnaPozicija.X, trenutnaPozicija.Y] == ' ')
            {
                trenutnaPozicija.Y++;
            }

            Console.WriteLine("Zacetna pozicija1: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
            bool prviUkaz = false;

            foreach (string ukaz in ukazi)
            {
                //za vsak premik kličen 1x....
                //Console.WriteLine(ukaz);
                if (ukaz.All(char.IsDigit))
                {
                    int premik = int.Parse(ukaz);
                   
                        for (int i = 0; i < premik; i++)
                        {
                            PremakniPozicijo2(ref matrika, ref trenutnaPozicija, ref matrika2);
                            if (trenutnaPozicija.Smer == 0) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '>';
                            if (trenutnaPozicija.Smer == 1) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'v';
                            if (trenutnaPozicija.Smer == 2) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '<';
                            if (trenutnaPozicija.Smer == 3) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '^';
                        }
                        //Console.WriteLine("Premik za: " + premik + " / " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                    
                }
                if (ukaz == "Levo")
                {
                    //Console.WriteLine("Obracan levo");
                    trenutnaPozicija.ObrniLevo();
                    matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'L';
                    //Console.WriteLine("Levo: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                }
                if (ukaz == "Desno")
                {
                    //Console.WriteLine("Obracan desno");
                    trenutnaPozicija.ObrniDesno();
                    matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'D';
                    //Console.WriteLine("Desno: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                }
               // Console.WriteLine("Trenutna lokacija: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                //NarisiMatriko(matrika2);
                //Console.WriteLine();
            }




            //NarisiMatriko(matrika2);
            Console.WriteLine("konec drugega dela: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
            
            Console.WriteLine("Koncna popravljena pozicija drugega dela: " + (trenutnaPozicija.X + 1) + " " + (trenutnaPozicija.Y + 1) + " " + (trenutnaPozicija.Smer));  //75 97 0

            Console.WriteLine("Odgovor drugega dela: " + (1000 * (trenutnaPozicija.X + 1) + 4 * (trenutnaPozicija.Y + 1) + (trenutnaPozicija.Smer)));
            // 182170





            //********************* PRVI DEL PONOVNO
            matrika = BeriMatriko2(fileNameM);
            matrika2 = BeriMatriko2(fileNameM);

            //NarisiMatriko(matrika);
            //NarisiMatriko(matrika2);

            //na začetno pozicijo
            trenutnaPozicija.X = 0;
            trenutnaPozicija.Y = 0;
            trenutnaPozicija.Smer = 0;

            while (matrika[trenutnaPozicija.X, trenutnaPozicija.Y] == ' ')
            {
                trenutnaPozicija.Y++;
            }

            Console.WriteLine("Zacetna pozicija1: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
            prviUkaz = true;

            foreach (string ukaz in ukazi)
            {
                //za vsak premik kličen 1x....
                //Console.WriteLine(ukaz);
                if (ukaz.All(char.IsDigit))
                {
                    int premik = int.Parse(ukaz);
                    if (prviUkaz)
                    {
                        //samo za prvega - ker je pozicija eno polje preveč desno
                        for (int i = 1; i < premik; i++)
                        {
                            PremakniPozicijo3(ref matrika, ref trenutnaPozicija, ref matrika2);
                            matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '>';
                        }
                        prviUkaz = false;
                    }
                    else
                    {
                        for (int i = 0; i < premik; i++)
                        {
                            PremakniPozicijo3(ref matrika, ref trenutnaPozicija, ref matrika2);
                            if (trenutnaPozicija.Smer == 0) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '>';
                            if (trenutnaPozicija.Smer == 1) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'v';
                            if (trenutnaPozicija.Smer == 2) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '<';
                            if (trenutnaPozicija.Smer == 3) matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = '^';
                        }
                        //Console.WriteLine("Premik za: " + premik + " / " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                    }
                }
                if (ukaz == "Levo")
                {
                    //Console.WriteLine("Obracan levo");
                    trenutnaPozicija.ObrniLevo();
                    matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'L';
                    //Console.WriteLine("Levo: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                }
                if (ukaz == "Desno")
                {
                    //Console.WriteLine("Obracan desno");
                    trenutnaPozicija.ObrniDesno();
                    matrika2[trenutnaPozicija.X, trenutnaPozicija.Y] = 'D';
                    //Console.WriteLine("Desno: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                }
                //Console.WriteLine("Trenutna lokacija: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);
                //NarisiMatriko(matrika2);
                //Console.WriteLine();
            }




            //NarisiMatriko(matrika2);
            Console.WriteLine("konec tretjega (prvega) dela: " + trenutnaPozicija.X + " " + trenutnaPozicija.Y + " " + trenutnaPozicija.Smer);

            Console.WriteLine("Koncna popravljena pozicija tretjega (prvega) dela: " + (trenutnaPozicija.X + 1) + " " + (trenutnaPozicija.Y + 1) + " " + (trenutnaPozicija.Smer));  //75 97 0

            Console.WriteLine("Odgovor tretjega (prvega) dela: " + (1000 * (trenutnaPozicija.X + 1) + 4 * (trenutnaPozicija.Y + 1) + (trenutnaPozicija.Smer)));
            //     75388
        }
    }
}