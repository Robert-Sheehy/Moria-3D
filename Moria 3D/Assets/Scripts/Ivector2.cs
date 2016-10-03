using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Ivector2
{
    public int x;
    public int y;

    public Ivector2(int X, int Y)
    {
        x = X;
        y = Y;
    }

    public static Ivector2 operator +(Ivector2 a, Ivector2 b)
    {
        return new Ivector2(a.x + b.x, a.y + b.y);

    }

    public static Ivector2 operator *(int a, Ivector2 b)
    {
        return new Ivector2(a*(b.x), a*(b.y));

    }
    public static Ivector2 operator -(Ivector2 a, Ivector2 b)
    {
        return new Ivector2(a.x - b.x, a.y - b.y);

    }
    public static Ivector2 operator ~(Ivector2 a)
    {
        return new Ivector2(a.y, -a.x);


    }
    public static bool operator ==(Ivector2 a, Ivector2 b)
    {
        return (a.x == b.x) && (a.y == b.y);


    }

    public static bool operator !=(Ivector2 a, Ivector2 b)
    {
        return !(a == b);
    }
    public static Ivector2 operator -(Ivector2 a)
    {
        return new Ivector2(-a.x, -a.y);
    }

}
