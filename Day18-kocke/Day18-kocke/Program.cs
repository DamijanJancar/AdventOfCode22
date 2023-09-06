/*
 * --- Day 18: Boiling Boulders ---
You and the elephants finally reach fresh air. You've emerged near the base of a large volcano that seems to be actively erupting! Fortunately, the lava seems to be flowing away from you and toward the ocean.

Bits of lava are still being ejected toward you, so you're sheltering in the cavern exit a little longer. Outside the cave, you can see the lava landing in a pond and hear it loudly hissing as it solidifies.

Depending on the specific compounds in the lava and speed at which it cools, it might be forming obsidian! The cooling rate should be based on the surface area of the lava droplets, so you take a quick scan of a droplet as it flies past you (your puzzle input).

Because of how quickly the lava is moving, the scan isn't very good; its resolution is quite low and, as a result, it approximates the shape of the lava droplet with 1x1x1 cubes on a 3D grid, each given as its x,y,z position.

To approximate the surface area, count the number of sides of each cube that are not immediately connected to another cube. So, if your scan were only two adjacent cubes like 1,1,1 and 2,1,1, each cube would have a single side covered and five sides exposed, a total surface area of 10 sides.

Here's a larger example:

2,2,2
1,2,2
3,2,2
2,1,2
2,3,2
2,2,1
2,2,3
2,2,4
2,2,6
1,2,5
3,2,5
2,1,5
2,3,5
In the above example, after counting up all the sides that aren't connected to another cube, the total surface area is 64.

What is the surface area of your scanned lava droplet?
-----------------------------------
IDEJA: naredim kocko ki je malo večja od max koordinate, napolnim z .
preberem podatke in točke zamenjam z *

iskanje iunanjih stranic:
čez vse 0-25 (x,y,z) če je * prešteješ koliko sosedov je . - toliko jih je zunanjih

-----------------------------------------
Your puzzle answer was 4322.

The first half of this puzzle is complete! It provides one gold star: *

--- Part Two ---
Something seems off about your calculation. The cooling rate depends on exterior surface area, but your calculation also included the surface area of air pockets trapped in the lava droplet.

Instead, consider only cube sides that could be reached by the water and steam as the lava droplet tumbles into the pond. The steam will expand to reach as much as possible, completely displacing any air on the outside of the lava droplet but never expanding diagonally.

In the larger example above, exactly one cube of air is trapped within the lava droplet (at 2,2,5), so the exterior surface area of the lava droplet is 58.

What is the exterior surface area of your scanned lava droplet?

--------------------------------
IDEJA: zunanje točke '.' rekurzivno iščemo sosede in jih spremenim v ' '.
potem identično preverjam zunanje samo ' ' ne pa '.'

!!! Problem StackOverflow !!!!

Bom poskusil z pregledovanjem kot pri drevesih....
iz vsake strani do * pobrišem .

Lepo pobriše vendar zgleda, da je kje še kak žep ki ni viden direktno !! -> premalo stranic
dodal še pobriši sodede, ko pride do * preveri, če je kaka '.' sosed, in pobriše - ni OK !!! -> preveč stranic

Še en poskus -> točke okolice spravim v HashSet - pregledujem sosede, potem pa vse pobrišem
Spet StackOverflow  !!!!



*/

using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    public static void NarisiKocko(char[,,] input)
    {
        Console.WriteLine("x");
        for (int x = 0; x < 25; x++)
        {
            Console.Write(x);
            for (int y = 0; y < 25; y++)
            {
                Console.WriteLine();
                for (int z = 0; z < 25; z++)
                {
                    Console.Write(input[x, y, z]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("y");
        for (int y = 0; y < 25; y++)
        {
            Console.Write(y);
            for (int x = 0; x < 25; x++)
            {
                Console.WriteLine();
                for (int z = 0; z < 25; z++)
                {
                    Console.Write(input[x, y, z]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("z");
        for (int z = 0; z < 25; z++)
        {
            Console.Write(z);
            for (int y = 0; y < 25; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < 25; x++)
                {
                    Console.Write(input[x, y, z]);
                }
            }
            Console.WriteLine();
        }
    }
    public static void PobrisiOkolico(ref char[,,] kocka, int tockaX, int tockaY, int tockaZ, List<int[,,]> sosedi)
    {
        //rekurzivno gre po vseh sosedih, in jih spremeni v ' '
        int x = tockaX;
        int y = tockaY;
        int z = tockaZ;



        if (sosedi.Count == 0) { return; }

        List<int[,,]> listSosed = new List<int[,,]>();

        kocka[x, y, z] = ' ';
        Console.WriteLine("Pobrisal: " + x + " " + y + " " + z);

        foreach (int[,,] s in sosedi)
        {
            if (x > 0 && kocka[x - 1, y, z] == '.')
            {
                kocka[x - 1, y, z] = ' ';
                int[,,] novSosed = new int[,,] { { { x - 1, y, z } } };
                listSosed.Add(novSosed);
            }
            if (x < 24 && kocka[x + 1, y, z] == '.')
            {
                kocka[x + 1, y, z] = ' ';
                int[,,] novSosed = new int[,,] { { { x + 1, y, z } } };
                listSosed.Add(novSosed);
            }
            if (y > 0 && kocka[x, y - 1, z] == '.')
            {
                kocka[x, y - 1, z] = ' ';
                int[,,] novSosed = new int[,,] { { { x, y - 1, z } } };
                listSosed.Add(novSosed);
            }
            if (y < 24 && kocka[x, y + 1, z] == '.')
            {
                kocka[x, y + 1, z] = ' ';
                int[,,] novSosed = new int[,,] { { { x, y + 1, z } } };
                listSosed.Add(novSosed);
            }
            if (z > 0 && kocka[x, y, z - 1] == '.')
            {
                kocka[x, y, z - 1] = ' ';
                int[,,] novSosed = new int[,,] { { { x, y, z - 1 } } };
                listSosed.Add(novSosed);
            }
            if (z < 24 && kocka[x, y, z + 1] == '.')
            {
                kocka[x, y, z + 1] = ' ';
                int[,,] novSosed = new int[,,] { { { x, y, z + 1 } } };
                listSosed.Add(novSosed);
            }

            if (listSosed.Count > 0)
            {
                int[,,] prvaMatrika = listSosed[0]; // Pridobimo prvo matriko iz seznama

                int xVrednost = prvaMatrika[0, 0, 0]; // Dostopamo do x vrednosti
                int yVrednost = prvaMatrika[0, 0, 1]; // Dostopamo do y vrednosti
                int zVrednost = prvaMatrika[0, 0, 2]; // Dostopamo do z vrednosti

                x = xVrednost; // Priredimo vrednosti x
                y = yVrednost; // Priredimo vrednosti y
                z = zVrednost; // Priredimo vrednosti z
            }
            PobrisiOkolico(ref kocka, x, y, z, listSosed);

        }
        return;

    }

    public static void PobrisiOkolico2(ref char[,,] kocka)
    {
        //pregledam z vseh strani - če je '.' jo spremenim v ' '
        //x+ y+ z+
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    if (kocka[x, y, z] == '.')
                    {
                        kocka[x, y, z] = ' ';
                    }
                    else if (kocka[x, y, z] == '*')
                    {
                        //PobrisiSosede(ref kocka, x, y, z);
                        break;
                    }
                }
            }
        }
        //x+ y+ z-
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 24; z > 0; z--)
                {
                    if (kocka[x, y, z] == '.')
                    {
                        kocka[x, y, z] = ' ';
                    }
                    else if (kocka[x, y, z] == '*')
                    {
                        //PobrisiSosede(ref kocka, x, y, z);
                        break;
                    }
                }
            }
        }
        //x+ z+ y+
        for (int x = 0; x < 25; x++)
        {
            for (int z = 0; z < 25; z++)
            {
                for (int y = 0; y < 25; y++)
                {
                    if (kocka[x, y, z] == '.')
                    {
                        kocka[x, y, z] = ' ';
                    }
                    else if (kocka[x, y, z] == '*')
                    {
                        //PobrisiSosede(ref kocka, x, y, z);
                        break;
                    }
                }
            }
        }
        //x+ z+ y-
        for (int x = 0; x < 25; x++)
        {
            for (int z = 0; z < 25; z++)
            {
                for (int y = 24; y > 0; y--)
                {
                    if (kocka[x, y, z] == '.')
                    {
                        kocka[x, y, z] = ' ';
                    }
                    else if (kocka[x, y, z] == '*')
                    {
                        //PobrisiSosede(ref kocka, x, y, z);
                        break;
                    }
                }
            }
        }
        //z+ y+ x+
        for (int z = 0; z < 25; z++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    if (kocka[x, y, z] == '.')
                    {
                        kocka[x, y, z] = ' ';
                    }
                    else if (kocka[x, y, z] == '*')
                    {
                        //PobrisiSosede(ref kocka, x, y, z);
                        break;
                    }
                }
            }
        }
        //z+ y+ x-
        for (int z = 0; z < 25; z++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int x = 24; x > 0; x--)
                {
                    if (kocka[x, y, z] == '.')
                    {
                        kocka[x, y, z] = ' ';
                    }
                    else if (kocka[x, y, z] == '*')
                    {
                        //PobrisiSosede(ref kocka, x, y, z);
                        break;
                    }

                }
            }
        }
    }
    public static void PobrisiSosede(ref char[,,] kocka, int x, int y, int z)
    {
        if (x > 0 && kocka[x - 1, y, z] == '.') { kocka[x - 1, y, z] = ' '; }
        if (x < 24 && kocka[x + 1, y, z] == '.') { kocka[x + 1, y, z] = ' '; }
        if (y > 0 && kocka[x, y - 1, z] == '.') { kocka[x, y - 1, z] = ' '; }
        if (y < 24 && kocka[x, y + 1, z] == '.') { kocka[x, y + 1, z] = ' '; }
        if (z > 0 && kocka[x, y, z - 1] == '.') { kocka[x, y, z - 1] = ' '; }
        if (z < 24 && kocka[x, y, z + 1] == '.') { kocka[x, y, z + 1] = ' '; }
    }


    public static void PoisciOkolico(ref char[,,] kocka, ref HashSet<Tuple<int, int, int>> koordinateOkolice, int tockaX, int tockaY, int tockaZ)
    {
        Console.WriteLine(tockaX + " " + tockaY + ' ' + tockaZ);

        //POPRAVEK - zdaj iščem samo še ostale '.'
        if (tockaX > 0 && kocka[tockaX - 1, tockaY, tockaZ] == '.')
        {
            if (!koordinateOkolice.Contains(Tuple.Create(tockaX - 1, tockaY, tockaZ)))
            {
                koordinateOkolice.Add(Tuple.Create(tockaX - 1, tockaY, tockaZ));
                PoisciOkolico(ref kocka, ref koordinateOkolice, tockaX - 1, tockaY, tockaZ);
            }
        }
        if (tockaX < 24 && kocka[tockaX + 1, tockaY, tockaZ] == '.')
        {
            if (!koordinateOkolice.Contains(Tuple.Create(tockaX + 1, tockaY, tockaZ)))
            {
                koordinateOkolice.Add(Tuple.Create(tockaX + 1, tockaY, tockaZ));
                PoisciOkolico(ref kocka, ref koordinateOkolice, tockaX + 1, tockaY, tockaZ);
            }
        }

        if (tockaY > 0 && kocka[tockaX, tockaY - 1, tockaZ] == '.')
        {
            if (!koordinateOkolice.Contains(Tuple.Create(tockaX, tockaY - 1, tockaZ)))
            {
                koordinateOkolice.Add(Tuple.Create(tockaX, tockaY - 1, tockaZ));
                PoisciOkolico(ref kocka, ref koordinateOkolice, tockaX, tockaY - 1, tockaZ);
            }
        }
        if (tockaY < 24 && kocka[tockaX, tockaY + 1, tockaZ] == '.')
        {
            if (!koordinateOkolice.Contains(Tuple.Create(tockaX, tockaY + 1, tockaZ)))
            {
                koordinateOkolice.Add(Tuple.Create(tockaX, tockaY + 1, tockaZ));
                PoisciOkolico(ref kocka, ref koordinateOkolice, tockaX, tockaY + 1, tockaZ);
            }
        }

        if (tockaZ > 0 && kocka[tockaX, tockaY, tockaZ - 1] == '.')
        {
            if (!koordinateOkolice.Contains(Tuple.Create(tockaX, tockaY, tockaZ - 1)))
            {
                koordinateOkolice.Add(Tuple.Create(tockaX, tockaY, tockaZ - 1));
                PoisciOkolico(ref kocka, ref koordinateOkolice, tockaX, tockaY, tockaZ - 1);
            }
        }
        if (tockaZ < 24 && kocka[tockaX, tockaY, tockaZ + 1] == '.')
        {
            if (!koordinateOkolice.Contains(Tuple.Create(tockaX, tockaY, tockaZ + 1)))
            {
                koordinateOkolice.Add(Tuple.Create(tockaX, tockaY, tockaZ + 1));
                PoisciOkolico(ref kocka, ref koordinateOkolice, tockaX, tockaY, tockaZ + 1);
            }
        }

    }

    public static void PobrisiOkolico3(ref char[,,] kocka, ref HashSet<Tuple<int, int, int>> koordinateOkolice)
    {
        foreach (var koordinata in koordinateOkolice)
        {
            int x = koordinata.Item1;
            int y = koordinata.Item2;
            int z = koordinata.Item3;
            Console.WriteLine($"Brisem koordinato: ({x}, {y}, {z})");
            kocka[x, y, z] = ' ';
        }
    }








    static void Main(string[] args)
    {
        // preberemo in napolnimo kocko, mislim da ni večjega od 25 (na hitro pogledano je 21 največja + rezerva)
        string[] podatki = File.ReadAllLines("d:\\FAX\\M10 - Algoritmi\\Naloga\\Day18-kocke\\day18.txt");
        char[,,] kocka = new char[25, 25, 25];
        //napolnimo pike
        for (int i = 0; i < 25; i++)
        {
            for (int j = 0; j < 25; j++)
            {
                for (int k = 0; k < 25; k++)
                {
                    kocka[i, j, k] = '.';
                }
            }
        }
        //kjer je točka zamenjamo z *
        foreach (string line in podatki)
        {
            string[] coordinate = line.Split(',');
            int x = int.Parse(coordinate[0]);
            int y = int.Parse(coordinate[1]);
            int z = int.Parse(coordinate[2]);
            kocka[x + 1, y + 1, z + 1] = '*';
        }

        int izpostavljenaStranica = 0;
        //iteracija po vseh sočkah - če nima soseda je izpostavljena stranica
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    if (kocka[x, y, z] == '*')
                    {
                        char[] sosedi = new char[6];
                        if (x > 0 && kocka[x - 1, y, z] == '.') { sosedi[0] = '.'; }
                        if (x < 24 && kocka[x + 1, y, z] == '.') { sosedi[1] = '.'; }
                        if (y > 0 && kocka[x, y - 1, z] == '.') { sosedi[2] = '.'; }
                        if (y < 24 && kocka[x, y + 1, z] == '.') { sosedi[3] = '.'; }
                        if (z > 0 && kocka[x, y, z - 1] == '.') { sosedi[4] = '.'; }
                        if (z < 24 && kocka[x, y, z + 1] == '.') { sosedi[5] = '.'; }
                        /*problemček, če je zunanja je ne štejem
                        if (x == 0) { sosedi[0] = '.'; }
                        if (y == 0) { sosedi[2] = '.'; }
                        if (z == 0) { sosedi[4] = '.'; }
                        ne potrebujem več, ker sem premaknil za +1 vse koordinate */



                        int sCount = 0;
                        foreach (char s in sosedi)
                        {
                            if (s == '.')
                            {
                                sCount++;

                            }

                        }
                        //izpostavljena stranica - seštevamo
                        izpostavljenaStranica += sCount;
                    }
                }
            }
        }
        //NarisiKocko(kocka);

        Console.WriteLine("Stevilo izpostavljenih stranic je: " + izpostavljenaStranica); //4322

        //****************************************************
        // DRUGI DEL



        PobrisiOkolico2(ref kocka);
        //Lepo pobriše vendar zgleda, da je kje še kak žep ki ni viden direktno
        //dodal še pobriši sodede, ko pride do * preveri, če je kaka '.' sosed, in pobriše - ni OK !!!
        //NarisiKocko(kocka);


        //zdaj naj bi bile skoraj vse točke okolice ' ' 
        //zato grem po vseh ' ' in pregledam če je sosed '.' in ga dam v hashset


        //vse zunanje točke vpišem v HashSet - da ni podvojitev
        HashSet<Tuple<int, int, int>> koordinateOkolice = new HashSet<Tuple<int, int, int>>();

        Console.WriteLine("Iscem ostanke okolice");

        




        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    if (kocka[x, y, z] == ' ')
                    {
                        PoisciOkolico(ref kocka, ref koordinateOkolice, x, y, z);
                    }
                }
            }
        }








        Console.WriteLine("Brisem okolico");
        PobrisiOkolico3(ref kocka, ref koordinateOkolice);
        NarisiKocko(kocka);

        izpostavljenaStranica = 0;

        //iteracija po vseh sočkah - če nima soseda je izpostavljena stranica
        for (int x = 0; x < 25; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                for (int z = 0; z < 25; z++)
                {
                    if (kocka[x, y, z] == '*')
                    {
                        char[] sosedi = new char[6];
                        if (x > 0 && kocka[x - 1, y, z] == ' ') { sosedi[0] = ' '; }
                        if (x < 24 && kocka[x + 1, y, z] == ' ') { sosedi[1] = ' '; }
                        if (y > 0 && kocka[x, y - 1, z] == ' ') { sosedi[2] = ' '; }
                        if (y < 24 && kocka[x, y + 1, z] == ' ') { sosedi[3] = ' '; }
                        if (z > 0 && kocka[x, y, z - 1] == ' ') { sosedi[4] = ' '; }
                        if (z < 24 && kocka[x, y, z + 1] == ' ') { sosedi[5] = ' '; }


                        int sCount = 0;
                        foreach (char s in sosedi)
                        {
                            if (s == ' ')
                            {
                                sCount++;

                            }

                        }
                        //izpostavljena stranica - seštevamo
                        izpostavljenaStranica += sCount;
                    }
                }
            }
        }

        Console.WriteLine("Popravljeno stevilo izpostavljenih stranic je: " + izpostavljenaStranica); //2516




    }
}
