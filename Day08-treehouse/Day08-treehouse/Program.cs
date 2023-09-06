namespace Day08

/*
 * --- Day 8: Treetop Tree House ---
The expedition comes across a peculiar patch of tall trees all planted carefully in a grid. The Elves explain that a previous expedition planted these trees as a reforestation effort. Now, they're curious if this would be a good location for a tree house.

First, determine whether there is enough tree cover here to keep a tree house hidden. To do this, you need to count the number of trees that are visible from outside the grid when looking directly along a row or column.

The Elves have already launched a quadcopter to generate a map with the height of each tree (your puzzle input). For example:

30373
25512
65332
33549
35390
Each tree is represented as a single digit whose value is its height, where 0 is the shortest and 9 is the tallest.

A tree is visible if all of the other trees between it and an edge of the grid are shorter than it. Only consider trees in the same row or column; that is, only look up, down, left, or right from any given tree.

All of the trees around the edge of the grid are visible - since they are already on the edge, there are no trees to block the view. In this example, that only leaves the interior nine trees to consider:

The top-left 5 is visible from the left and top. (It isn't visible from the right or bottom since other trees of height 5 are in the way.)
The top-middle 5 is visible from the top and right.
The top-right 1 is not visible from any direction; for it to be visible, there would need to only be trees of height 0 between it and an edge.
The left-middle 5 is visible, but only from the right.
The center 3 is not visible from any direction; for it to be visible, there would need to be only trees of at most height 2 between it and an edge.
The right-middle 3 is visible from the right.
In the bottom row, the middle 5 is visible, but the 3 and 4 are not.
With 16 trees visible on the edge and another 5 visible in the interior, a total of 21 trees are visible in this arrangement.

Consider your map; how many trees are visible from outside the grid?

**************************************************************
IDEJA:
zunanja drevesa lahko takoj preštejemo
potem gledamo od 2 do n-1 stolpce/vrstice (gor, dol, levo, desno)
    če je višji (dokler je višji) od prejšnega ga prištejemo

    problemček se je pojavil, ker sem 2x štel nekatera drevesa.... -> HashTable, da izločim dvojnike

**************************************************************

Your puzzle answer was 1843.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
Content with the amount of tree cover available, the Elves just need to know the best spot to build their tree house: they would like to be able to see a lot of trees.

To measure the viewing distance from a given tree, look up, down, left, and right from that tree; stop if you reach an edge or at the first tree that is the same height or taller than the tree under consideration. (If a tree is right on the edge, at least one of its viewing distances will be zero.)

The Elves don't care about distant trees taller than those found by the rules above; the proposed tree house has large eaves to keep it dry, so they wouldn't be able to see higher than the tree house anyway.

In the example above, consider the middle 5 in the second row:

30373
25512
65332
33549
35390
Looking up, its view is not blocked; it can see 1 tree (of height 3).
Looking left, its view is blocked immediately; it can see only 1 tree (of height 5, right next to it).
Looking right, its view is not blocked; it can see 2 trees.
Looking down, its view is blocked eventually; it can see 2 trees (one of height 3, then the tree of height 5 that blocks its view).
A tree's scenic score is found by multiplying together its viewing distance in each of the four directions. For this tree, this is 4 (found by multiplying 1 * 1 * 2 * 2).

However, you can do even better: consider the tree of height 5 in the middle of the fourth row:

30373
25512
65332
33549
35390
Looking up, its view is blocked at 2 trees (by another tree with a height of 5).
Looking left, its view is not blocked; it can see 2 trees.
Looking down, its view is also not blocked; it can see 1 tree.
Looking right, its view is blocked at 2 trees (by a massive tree of height 9).
This tree's scenic score is 8 (2 * 2 * 1 * 2); this is the ideal spot for the tree house.

Consider each tree on your map. What is the highest scenic score possible for any tree?




*/
{
    internal class Program
    {
        static int StejVidnaDrevesa(int[,] matrica)
        {
            /*for (int x = 0; x < matrica.GetLength(1); x++)
            {
                for (int y = 0; y < matrica.GetLength(0); y++)
                {
                    Console.Write(matrica[x, y]);
                }
                Console.WriteLine();
            }*/


            // preštejem obrobna drevesa
            HashSet<(int, int)> stevecVidnaDrevesa = new HashSet<(int, int)>();
            //Console.WriteLine("Dolžina '0' stranice: " + matrica.GetLength(0));
            //Console.WriteLine("Dolžina '1' stranice: " + matrica.GetLength(1));

            for (int x = 0; x < matrica.GetLength(1); x++)
                stevecVidnaDrevesa.Add((x, 0));
            for (int x = 0; x < matrica.GetLength(1); x++)
                stevecVidnaDrevesa.Add((x, matrica.GetLength(0) - 1));
            for (int y = 0; y < matrica.GetLength(0); y++)
                stevecVidnaDrevesa.Add((0, y));
            for (int y = 0; y < matrica.GetLength(0); y++)
                stevecVidnaDrevesa.Add((matrica.GetLength(0) - 1, y));

            //Console.WriteLine("drevesa okoli: " + stevecVidnaDrevesa.Count);
            //Foreach (var st in stevecVidnaDrevesa)
            //Console.WriteLine(st);

            // iz vzhoda
            for (int x = 1; x < matrica.GetLength(1) - 1; x++)
            {
                int max = matrica[x, 0];
                //Console.WriteLine("obdelujem: vzhod: " + x);
                for (int y = 1; y < matrica.GetLength(0) - 1; y++)
                {
                    //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                    if (matrica[x, y] > max)
                    {
                        //Console.WriteLine("vzhod " + x + y + matrica[x, y]);
                        stevecVidnaDrevesa.Add((x, y));
                        max = matrica[x, y];
                    }
                }
            }
            // iz zahoda
            for (int x = 1; x < matrica.GetLength(1) - 1; x++)
            {
                int max = matrica[x, matrica.GetLength(1) - 1];
                //Console.WriteLine("obdelujem: zahod: " + x);
                for (int y = matrica.GetLength(0) - 1; y > 0; y--)
                {
                    //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                    if (matrica[x, y] > max)
                    {
                        //Console.WriteLine("vzhod " + x + y + matrica[x, y]);
                        stevecVidnaDrevesa.Add((x, y));
                        max = matrica[x, y];
                    }
                }
            }
            // iz severa
            for (int y = 1; y < matrica.GetLength(0) - 1; y++)
            {
                int max = matrica[0, y];
                //Console.WriteLine("obdelujem: sever: " + y);
                for (int x = 1; x < matrica.GetLength(1) - 1; x++)
                {
                    //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                    if (matrica[x, y] > max)
                    {
                        //Console.WriteLine("sever " + x + y + matrica[x, y]);
                        stevecVidnaDrevesa.Add((x, y));
                        max = matrica[x, y];
                    }
                }
            }
            // iz jug
            for (int y = 1; y < matrica.GetLength(0) - 1; y++)
            {
                int max = matrica[matrica.GetLength(1) - 1, y];
                //Console.WriteLine("obdelujem: jug: " + y + " max: " + max);
                for (int x = matrica.GetLength(1) - 1; x > 0; x--)
                {
                    //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                    if (matrica[x, y] > max)
                    {
                        //Console.WriteLine("jug " + "x: " + (x+1) + "  y: " +(y+1)+" max: "+ max + "  " + matrica[x, y]);
                        stevecVidnaDrevesa.Add((x, y));
                        max = matrica[x, y];
                    }
                }
            }

            //foreach (var st in stevecVidnaDrevesa)
            //  Console.WriteLine(st);


            return stevecVidnaDrevesa.Count;
        }




        static int StejVidnaDrevesaIzHiske(int[,] matrica, int xIn, int yIn)
        {
            /* IDEJA - iz izbrane točke gledam gor, dol, levo, desno
             * prva naslednja točka je min, in prištejem vsako večjo, dokler ni višji od trenutne 
             * in tako za vse smeri
             * 
             * POPRAVEK - štejemo vsa manjša drevesa.... ne samo tista ki se vidijo !!!! */

            int stevecDrevesVzhod = 0;
            int stevecDrevesZahod = 0;
            int stevecDrevesSever = 0;
            int stevecDrevesJug = 0;
            int x = xIn;
            int y = yIn;
            int min = 0;
            int max = 0;

            // proti vzhodu
            x = xIn;
            min = matrica[xIn, yIn + 1];
            max = matrica[xIn, yIn];
            //Console.WriteLine("obdelujem: vzhod: x:" + x + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min);
            for (y = yIn + 1; y < matrica.GetLength(0); y++)
            {
                //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                if (matrica[x, y] >= max)
                {
                    stevecDrevesVzhod++;
                    //Console.WriteLine("prištel 1");
                    break;
                }
                //if ((matrica[x, y] < max && matrica[x, y] >= min) || y == yIn + 1)
                if (matrica[x, y] < max || y == yIn + 1)
                {
                    stevecDrevesVzhod++;
                    //Console.WriteLine("prištel 1");
                    //if (matrica[x, y] > min) min = matrica[x, y];
                    
                }
            }
            Console.WriteLine("vzhod: x:" + xIn + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min + " skupaj: " + stevecDrevesVzhod);


            // proti zahodu
            x = xIn;
            min = matrica[xIn, yIn - 1];
            max = matrica[xIn, yIn];
            //Console.WriteLine("obdelujem: zahod: x:" + x + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min);
            for (y = yIn - 1; y >= 0; y--)
            {
                //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                if (matrica[x, y] >= max)
                {
                    stevecDrevesZahod++;
                    //min = matrica[x, y];
                    //Console.WriteLine("prištel 1");
                    break;
                }
                //if ((matrica[x, y] < max && matrica[x, y] >= min) || y == yIn - 1)
                if (matrica[x, y] < max || y == yIn - 1)
                {
                    stevecDrevesZahod++;
                    //Console.WriteLine("prištel 1");
                    //if (matrica[x, y] > min) min = matrica[x, y];
                }
            }
            Console.WriteLine("zahod: x:" + xIn + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min + " skupaj: " + stevecDrevesZahod);

            // proti severu
            y = yIn;
            min = matrica[xIn - 1, yIn];
            max = matrica[xIn, yIn];
            //Console.WriteLine("obdelujem: sever: x:" + x + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min);
            for (x = xIn - 1; x >= 0; x--)
            {
                //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                if (matrica[x, y] >= max)
                {
                    stevecDrevesSever++;
                    //min = matrica[x, y];
                    //Console.WriteLine("prištel 1");
                    break;
                }
                //if ((matrica[x, y] < max && matrica[x, y] >= min) || x == xIn - 1)
                if (matrica[x, y] < max || x == xIn - 1)
                    {
                        stevecDrevesSever++;
                    //Console.WriteLine("prištel 1");
                    //if (matrica[x, y] > min) min = matrica[x, y];
                }
            }
            Console.WriteLine("sever: x:" + xIn + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min + " skupaj: " + stevecDrevesSever);


            // proti jugu
            y = yIn;
            min = matrica[xIn + 1, yIn];
            max = matrica[xIn, yIn];
            //Console.WriteLine("obdelujem: jug: x:" + xIn + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min);
            for (x = xIn + 1; x < matrica.GetLength(1); x++)
            {
                //Console.WriteLine("obdelujem: " + x + y + matrica[x, y]);
                if (matrica[x, y] >= max)
                {
                    stevecDrevesJug++;
                    //min = matrica[x, y];
                    //Console.WriteLine("prištel 1");
                    break;
                }
                //if ((matrica[x, y] < max && matrica[x, y] >= min) || x == xIn + 1)
                if (matrica[x, y] < max || x == xIn + 1)
                {
                    stevecDrevesJug++;
                    //Console.WriteLine("prištel 1");
                    //if (matrica[x, y] > min) min = matrica[x, y];
                }
            }
            Console.WriteLine("jug: x:" + xIn + " y:" + yIn + " visina je: " + matrica[xIn, yIn] + " min: " + min + " skupaj: " + stevecDrevesJug);


            int stevec = stevecDrevesJug * stevecDrevesSever * stevecDrevesVzhod * stevecDrevesZahod;
            Console.WriteLine("zmnožek: " + stevec);
            return stevec;
        }

        static int PreglejVsaDrevesa(int[,] matrica)
        {
            int res = 0;
            int maxRes = 0;
            for (int x = 1; x < matrica.GetLength(1) - 1; x++)
            {
                for (int y = 1; y < matrica.GetLength(0) - 1; y++)
                {
                    //Console.WriteLine("pregledujem : " + x + " " + y + " - " + matrica[x, y]);
                    res = StejVidnaDrevesaIzHiske(matrica, x, y);
                    if (res > maxRes)
                        maxRes = res;
                }
            }
            return maxRes;
        }



        static int[,] ReadMatrix(string fileName)
        {
            var numbers = File.ReadLines(fileName).Select(line => line.Select(c => int.Parse("" + c))).ToArray();
            var firstDimension = numbers.Count();
            var secondDimension = numbers.First().Count();

            int[,] matrix = new int[firstDimension, secondDimension];

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
            Console.WriteLine("Hello TREES!");

            int[,] m = {
                {3, 0, 3, 7, 3 },
                {2, 5, 5, 1, 2 },
                {6, 5, 3, 3, 2 },
                {3, 3, 5, 4, 9 },
                {3, 5, 3, 9, 0 },
            };

            int[,] m3 = {
                {3, 4, 3, 7, 3 },
                {2, 3, 5, 1, 2 },
                {6, 5, 2, 6, 4 },
                {3, 3, 5, 4, 9 },
                {3, 5, 3, 9, 0 },
            };

            int[,] m4 = {
                {3, 4, 3, 7, 3, 2, 3 },
                {2, 3, 5, 5, 2, 3, 4 },
                {6, 5, 2, 3, 2, 4, 4 },
                {3, 3, 3, 5, 3, 3, 6,},
                {3, 5, 3, 3, 2, 4, 5,},
                {7, 3, 5, 5, 9, 2, 4 },
                {3, 5, 3, 9, 0, 4, 5,},
            };

            //Console.WriteLine("Vseh vidnih dreves je: " + StejVidnaDrevesa(m1));




            string fileName = "d:\\FAX\\M10 - Algoritmi\\Naloga\\Day08-treehouse\\m1.txt";
            int[,] m1 = ReadMatrix(fileName);
            Console.WriteLine("Vseh vidnih dreves male matrice je: " + StejVidnaDrevesa(m1));
            fileName = "d:\\FAX\\M10 - Algoritmi\\Naloga\\Day08-treehouse\\m2.txt";
            int[,] m2 = ReadMatrix(fileName);
            Console.WriteLine("Vseh vidnih dreves male matrice je: " + StejVidnaDrevesa(m2));

            //Console.WriteLine(StejVidnaDrevesaIzHiske(m3, 2, 1));
            //Console.WriteLine("Mala matrica - maxZmnožek: " + PreglejVsaDrevesa(m4));
            Console.WriteLine("Velika matrica - maxZmnožek: " + PreglejVsaDrevesa(m2));
            //Console.WriteLine(StejVidnaDrevesaIzHiske(m2, 56, 4));


        }
    }
}