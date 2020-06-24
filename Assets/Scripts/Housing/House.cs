using System;

[System.Serializable]
public class House
{
    Random rnd = new Random();

    public int rooms;
    public int rent;
    public int quality;

    public House()
    {
        rooms = rnd.Next(1,4);
        quality = rnd.Next(-2, 2);

        rent = rooms * (quality + 3) * rnd.Next(3, 6);
        if (rent < 5) rent = 5;
    }

    public House(int seed)
    {
        rnd = new Random(seed);

        rooms = rnd.Next(1, 4);
        quality = rnd.Next(-2, 2);

        rent = rooms * (quality + 3) * rnd.Next(3, 6);
        if (rent < 5) rent = 5;
    }


    public House(int newRooms, int newRent, int newQual)
    {
        rooms = newRooms;
        rent = newRent;
        quality = newQual;
    }




}