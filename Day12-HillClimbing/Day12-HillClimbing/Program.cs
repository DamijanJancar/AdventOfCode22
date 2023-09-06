namespace Day12

/*
--- Day 12: Hill Climbing Algorithm ---
You try contacting the Elves using your handheld device, but the river you're following must be too low to get a decent signal.

You ask the device for a heightmap of the surrounding area (your puzzle input). The heightmap shows the local area from above broken into a grid; the elevation of each square of the grid is given by a single lowercase letter, where a is the lowest elevation, b is the next-lowest, and so on up to the highest elevation, z.

Also included on the heightmap are marks for your current position (S) and the location that should get the best signal (E). Your current position (S) has elevation a, and the location that should get the best signal (E) has elevation z.

You'd like to reach E, but to save energy, you should do it in as few steps as possible. During each step, you can move exactly one square up, down, left, or right. To avoid needing to get out your climbing gear, the elevation of the destination square can be at most one higher than the elevation of your current square; that is, if your current elevation is m, you could step to elevation n, but not to elevation o. (This also means that the elevation of the destination square can be much lower than the elevation of your current square.)

For example:

Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi
Here, you start in the top-left corner; your goal is near the middle. You could start by moving down or right, but eventually you'll need to head toward the e at the bottom. From there, you can spiral around to the goal:

v..v<<<<
>v.vv<<^
.>vv>E^^
..v>>>^^
..>>>>>^
In the above diagram, the symbols indicate whether the path exits each square moving up (^), down (v), left (<), or right (>). The location that should get the best signal is still E, and . marks unvisited squares.

This path reaches the goal in 31 steps, the fewest possible.

What is the fewest steps required to move from your current position to the location that should get the best signal? 

 ********************
 *
 *Ideja - začnemo na S
 *pogledamo vse sosede in če je za 1 višji ga damo v queue
 *prištevamo korake in če pridemo do vrha, primerjamo če je minimum
 *POPRAVEK---
 *če delamo queue dobimo samo, če je možna pot, ne pa najkrajše
 *Moramo popravit va Dijkstra
 *
 *Še en problem... med sosede doda tudi prejšno točko !!!
 *bo potrebno dat shahTable, da ne dodaja večkrat istih sosedov
 
 Your puzzle answer was 394.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
As you walk up the hill, you suspect that the Elves will want to turn this into a hiking trail. The beginning isn't very scenic, though; perhaps you can find a better starting point.

To maximize exercise while hiking, the trail should start as low as possible: elevation a. The goal is still the square marked E. However, the trail should still be direct, taking the fewest steps to reach its goal. So, you'll need to find the shortest path from any square at elevation a to the square marked E.

Again consider the example from above:

Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi
Now, there are six choices for starting position (five marked a, plus the square marked S that counts as being at elevation a). If you start at the bottom-left square, you can reach the goal most quickly:

...v<<<<
...vv<<^
...v>E^^
.>v>>>^^
>^>>>>>^
This path reaches the goal in only 29 steps, the fewest possible.

What is the fewest steps required to move starting from any square with elevation a to the location that should get the best signal?
 */

{




    internal class Program
    {
        public struct Cell
        {
            public int X { get; set; }
            public int Y { get; set; }
            public char Vrednost { get; set; }
            public int Razdalja { get; set; }
            public Cell(int x, int y, char v, int r = 0)
            {
                this.X = x;
                this.Y = y;
                this.Vrednost = v;
                this.Razdalja = r;
            }
            public override bool Equals(object? obj)
            {
                Cell c = (Cell)obj;

                if (c.X == this.X && c.Y == this.Y)
                    return true;

                return false;
            }

            public override string ToString()
            {
                return $"X: {this.X}, Y: {this.Y}, Vrednost: {this.Vrednost}, Razdalja: {this.Razdalja}";
            }

            public void PoisciSosede(Queue<Cell> queue, char[,] matrica, HashSet<(int, int)> obiskani, int razdalja)
            {

                if (matrica[this.X, this.Y] == 'S') matrica[this.X, this.Y] = 'a';
                if (matrica[this.X, this.Y] == 'E') matrica[this.X, this.Y] = 'z';

                //zahod
                //Preverim, če je možen korak
                if (this.Y - 1 >= 0)
                {
                    int razlika = matrica[this.X, this.Y - 1] - matrica[this.X, this.Y];
                    if (razlika <= 1)
                    {
                        //Console.WriteLine("preverjam zahodno");
                        Cell c = (new Cell(this.X, this.Y - 1, matrica[this.X, this.Y - 1], razdalja + 1));
                        if (!obiskani.Contains((c.X, c.Y)))
                        {
                            queue.Enqueue(c);
                            //Console.WriteLine("dodal zahodno");
                        }
                    }
                }

                //vzhod
                //Preverim, če je možen korak
                if (this.Y + 1 <= matrica.GetLength(1) - 1)
                {
                    int razlika = matrica[this.X, this.Y + 1] - matrica[this.X, this.Y];
                    if (razlika <= 1)
                    {
                        //Console.WriteLine("preverjam vzhodno");
                        Cell c = (new Cell(this.X, this.Y + 1, matrica[this.X, this.Y + 1], razdalja + 1));
                        if (!obiskani.Contains((c.X, c.Y)))
                        {
                            queue.Enqueue(c);
                            //Console.WriteLine("dodal vzhodno");
                        }

                    }
                }


                //sever
                //Preverim, če je možen korak
                if (this.X - 1 >= 0)
                {
                    int razlika = matrica[this.X - 1, this.Y] - matrica[this.X, this.Y];
                    if (razlika <= 1)
                    {
                        //Console.WriteLine("preverjam severno");
                        Cell c = (new Cell(this.X - 1, this.Y, matrica[this.X - 1, this.Y], razdalja + 1));
                        if (!obiskani.Contains((c.X, c.Y)))
                        {
                            queue.Enqueue(c);
                            //Console.WriteLine("dodal severno");
                        }

                    }
                }

                //jug
                //Preverim, če je možen korak
                if (this.X + 1 <= matrica.GetLength(0) - 1)
                {
                    int razlika = matrica[this.X + 1, this.Y] - matrica[this.X, this.Y];
                    if (razlika <= 1)
                    {
                        //Console.WriteLine("preverjam južno");
                        Cell c = (new Cell(this.X + 1, this.Y, matrica[this.X + 1, this.Y], razdalja + 1));
                        if (!obiskani.Contains((c.X, c.Y)))
                        {
                            queue.Enqueue(c);
                            //Console.WriteLine("dodal južno");
                        }

                    }
                }

            }
        }


        static Cell PoisciStart(char[,] matrica)
        {

            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    if (matrica[i, j] == 'S')
                    {
                        Cell c = new Cell(i, j, matrica[i, j], 0);
                        return c;
                    };

                }
            }
            return PoisciStart(matrica);
        }

        static Cell PoisciEnd(char[,] matrica)
        {

            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    if (matrica[i, j] == 'E')
                    {
                        matrica[i, j] = 'z';
                        Cell c = new Cell(i, j, 'z', 0);
                        return c;
                    };

                }
            }
            return PoisciEnd(matrica);
        }

        static char[,] ReadMatrix(string fileName)
        {
            var numbers = File.ReadLines(fileName).Select(line => line.Select(c => char.Parse("" + c))).ToArray();
            var firstDimension = numbers.Count();
            var secondDimension = numbers.First().Count();

            char[,] matrix = new char[firstDimension, secondDimension];

            for (int x = 0; x < firstDimension; x++)
            {
                for (int y = 0; y < secondDimension; y++)
                {
                    matrix[x, y] = numbers[x].ToArray()[y];
                }
            }
            return matrix;
        }





        static void Main(string[] args)
        {
            Console.WriteLine("Gremo v hribe...");


            char[,] m1 = {
     { 'S','a','b','q','p','o','n','m' },
     { 'a','b','c','r','y','x','x','l' },
     { 'a','c','c','s','z','E','x','k' },
     { 'a','c','c','t','u','v','w','j' },
     { 'a','b','d','e','f','g','h','i' },
     };



            string fileName = "d:\\FAX\\M10 - Algoritmi\\Naloga\\Day12-HillClimbing\\m2.txt";
            //string fileName = "c:\\FAX\\M09 - Algoritmi\\Naloge\\Dan12\\m2.txt";
            char[,] m2 = ReadMatrix(fileName);




            Cell tockaStart;
            Cell tockaEnd;


            char[,] matrica = m2;



            HashSet<(int, int)> obiskani = new HashSet<(int, int)>();
            Queue<Cell> queue = new Queue<Cell>();


            tockaStart = PoisciStart(matrica);
            tockaEnd = PoisciEnd(matrica);
            //Console.WriteLine("Start je na " + tockaStart);
            //Console.WriteLine("Konec je na " + tockaEnd);


            Cell trenutnaTocka = tockaStart;
            trenutnaTocka.Razdalja = 0;
            Cell tempKoncna = tockaStart;

            queue.Enqueue(trenutnaTocka);

            while (queue.Count > 0)
            {
                trenutnaTocka = queue.Dequeue();
                if (obiskani.Contains((trenutnaTocka.X, trenutnaTocka.Y)))
                    continue;

                if ((trenutnaTocka.X == tockaEnd.X) && (trenutnaTocka.Y == tockaEnd.Y))
                {
                    Console.WriteLine("Končna točka: " + trenutnaTocka);
                    tempKoncna = trenutnaTocka;
                }



                //Console.WriteLine("queue: " + queue.Count);
                //Console.WriteLine(trenutnaTocka);
                obiskani.Add((trenutnaTocka.X, trenutnaTocka.Y));
                //Console.WriteLine("obiskani: " + obiskani.Count);
                trenutnaTocka.PoisciSosede(queue, matrica, obiskani, trenutnaTocka.Razdalja);
            }


            //}
            Console.WriteLine("na koncu " + trenutnaTocka);
            //tockaEnd.Razdalja = trenutnaTocka.Razdalja + 1;
            Console.WriteLine("Zadnja točka: " + tempKoncna);



            //isti algoritem, vendar ga ponovimo za vsak 'a' v tabeli, in zapomnimo najkrajšo pot
            Console.WriteLine("DRUGI DEL !!!");

            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {

                    Console.Write(matrica[i, j]);
                }
                Console.WriteLine();
            }

            int minKorakov = int.MaxValue;

            for (int i = 0; i < matrica.GetLength(0); i++)
            {
                for (int j = 0; j < matrica.GetLength(1); j++)
                {
                    if (matrica[i, j] == 'a')
                    {
                        //****************************************** samo ponovimo za vse 'a'

                        HashSet<(int, int)> obiskani2 = new HashSet<(int, int)>();
                        Queue<Cell> queue2 = new Queue<Cell>();

                        //tockaStart = ((i, j, 'a', 0));
                        Cell tockaStart2 = new Cell(i, j, matrica[i, j], 0);
                        Console.WriteLine("začetni a : " + tockaStart2);
                        //tockaStart = PoisciStart(matrica);
                        //tockaEnd = PoisciEnd(matrica);

                        Cell trenutnaTocka2 = tockaStart2;
                        trenutnaTocka2.Razdalja = 0;
                        Cell tempKoncna2 = tockaStart2;


                        //v vrsto dodamo trenutno točko
                        //Console.WriteLine("Enqueue: " + (trenutnaTocka.X, trenutnaTocka.Y));
                        queue2.Enqueue(trenutnaTocka2);

                        while (queue2.Count > 0)
                        {
                            trenutnaTocka2 = queue2.Dequeue();
                            if (obiskani2.Contains((trenutnaTocka2.X, trenutnaTocka2.Y)))
                                continue;

                            if ((trenutnaTocka2.X == tockaEnd.X) && (trenutnaTocka2.Y == tockaEnd.Y))
                            {
                                //Console.WriteLine("Končna točka: " + trenutnaTocka2);
                                tempKoncna2 = trenutnaTocka2;
                            }


                            obiskani2.Add((trenutnaTocka2.X, trenutnaTocka2.Y));
                            //Console.WriteLine("obiskani: " + obiskani.Count);
                            trenutnaTocka2.PoisciSosede(queue2, matrica, obiskani2, trenutnaTocka2.Razdalja);
                        }


                        Console.WriteLine("Zadnja točka: " + tempKoncna2 + " minKorakov: " + minKorakov);

                        if (minKorakov > tempKoncna2.Razdalja && tempKoncna2.Razdalja != 0)
                            minKorakov = tempKoncna2.Razdalja;

                    }

                }

            }
            Console.WriteLine("Najkrajša pot od a do vrha: " + minKorakov);
        }

    }
}