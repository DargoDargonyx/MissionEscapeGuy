using System;

// Most of the code here was taken from https://github.com/Auburn/FastNoiseLite/blob/master/CSharp/FastNoiseLite.cs

public static class PerlinNoise {
    public static int seed = new Random().Next();

    // Constants
    private const int PrimeX = 501125321;
    private const int PrimeY = 1136930381;
    private static readonly float[] Gradients2D = {
         0.130526192220052f,  0.99144486137381f,   0.38268343236509f,   0.923879532511287f,  0.608761429008721f,  0.793353340291235f,  0.793353340291235f,  0.608761429008721f,
         0.923879532511287f,  0.38268343236509f,   0.99144486137381f,   0.130526192220051f,  0.99144486137381f,  -0.130526192220051f,  0.923879532511287f, -0.38268343236509f,
         0.793353340291235f, -0.60876142900872f,   0.608761429008721f, -0.793353340291235f,  0.38268343236509f,  -0.923879532511287f,  0.130526192220052f, -0.99144486137381f,
        -0.130526192220052f, -0.99144486137381f,  -0.38268343236509f,  -0.923879532511287f, -0.608761429008721f, -0.793353340291235f, -0.793353340291235f, -0.608761429008721f,
        -0.923879532511287f, -0.38268343236509f,  -0.99144486137381f,  -0.130526192220052f, -0.99144486137381f,   0.130526192220051f, -0.923879532511287f,  0.38268343236509f,
        -0.793353340291235f,  0.608761429008721f, -0.608761429008721f,  0.793353340291235f, -0.38268343236509f,   0.923879532511287f, -0.130526192220052f,  0.99144486137381f,
         0.130526192220052f,  0.99144486137381f,   0.38268343236509f,   0.923879532511287f,  0.608761429008721f,  0.793353340291235f,  0.793353340291235f,  0.608761429008721f,
         0.923879532511287f,  0.38268343236509f,   0.99144486137381f,   0.130526192220051f,  0.99144486137381f,  -0.130526192220051f,  0.923879532511287f, -0.38268343236509f,
         0.793353340291235f, -0.60876142900872f,   0.608761429008721f, -0.793353340291235f,  0.38268343236509f,  -0.923879532511287f,  0.130526192220052f, -0.99144486137381f,
        -0.130526192220052f, -0.99144486137381f,  -0.38268343236509f,  -0.923879532511287f, -0.608761429008721f, -0.793353340291235f, -0.793353340291235f, -0.608761429008721f,
        -0.923879532511287f, -0.38268343236509f,  -0.99144486137381f,  -0.130526192220052f, -0.99144486137381f,   0.130526192220051f, -0.923879532511287f,  0.38268343236509f,
        -0.793353340291235f,  0.608761429008721f, -0.608761429008721f,  0.793353340291235f, -0.38268343236509f,   0.923879532511287f, -0.130526192220052f,  0.99144486137381f,
         0.130526192220052f,  0.99144486137381f,   0.38268343236509f,   0.923879532511287f,  0.608761429008721f,  0.793353340291235f,  0.793353340291235f,  0.608761429008721f,
         0.923879532511287f,  0.38268343236509f,   0.99144486137381f,   0.130526192220051f,  0.99144486137381f,  -0.130526192220051f,  0.923879532511287f, -0.38268343236509f,
         0.793353340291235f, -0.60876142900872f,   0.608761429008721f, -0.793353340291235f,  0.38268343236509f,  -0.923879532511287f,  0.130526192220052f, -0.99144486137381f,
        -0.130526192220052f, -0.99144486137381f,  -0.38268343236509f,  -0.923879532511287f, -0.608761429008721f, -0.793353340291235f, -0.793353340291235f, -0.608761429008721f,
        -0.923879532511287f, -0.38268343236509f,  -0.99144486137381f,  -0.130526192220052f, -0.99144486137381f,   0.130526192220051f, -0.923879532511287f,  0.38268343236509f,
        -0.793353340291235f,  0.608761429008721f, -0.608761429008721f,  0.793353340291235f, -0.38268343236509f,   0.923879532511287f, -0.130526192220052f,  0.99144486137381f,
         0.130526192220052f,  0.99144486137381f,   0.38268343236509f,   0.923879532511287f,  0.608761429008721f,  0.793353340291235f,  0.793353340291235f,  0.608761429008721f,
         0.923879532511287f,  0.38268343236509f,   0.99144486137381f,   0.130526192220051f,  0.99144486137381f,  -0.130526192220051f,  0.923879532511287f, -0.38268343236509f,
         0.793353340291235f, -0.60876142900872f,   0.608761429008721f, -0.793353340291235f,  0.38268343236509f,  -0.923879532511287f,  0.130526192220052f, -0.99144486137381f,
        -0.130526192220052f, -0.99144486137381f,  -0.38268343236509f,  -0.923879532511287f, -0.608761429008721f, -0.793353340291235f, -0.793353340291235f, -0.608761429008721f,
        -0.923879532511287f, -0.38268343236509f,  -0.99144486137381f,  -0.130526192220052f, -0.99144486137381f,   0.130526192220051f, -0.923879532511287f,  0.38268343236509f,
        -0.793353340291235f,  0.608761429008721f, -0.608761429008721f,  0.793353340291235f, -0.38268343236509f,   0.923879532511287f, -0.130526192220052f,  0.99144486137381f,
         0.130526192220052f,  0.99144486137381f,   0.38268343236509f,   0.923879532511287f,  0.608761429008721f,  0.793353340291235f,  0.793353340291235f,  0.608761429008721f,
         0.923879532511287f,  0.38268343236509f,   0.99144486137381f,   0.130526192220051f,  0.99144486137381f,  -0.130526192220051f,  0.923879532511287f, -0.38268343236509f,
         0.793353340291235f, -0.60876142900872f,   0.608761429008721f, -0.793353340291235f,  0.38268343236509f,  -0.923879532511287f,  0.130526192220052f, -0.99144486137381f,
        -0.130526192220052f, -0.99144486137381f,  -0.38268343236509f,  -0.923879532511287f, -0.608761429008721f, -0.793353340291235f, -0.793353340291235f, -0.608761429008721f,
        -0.923879532511287f, -0.38268343236509f,  -0.99144486137381f,  -0.130526192220052f, -0.99144486137381f,   0.130526192220051f, -0.923879532511287f,  0.38268343236509f,
        -0.793353340291235f,  0.608761429008721f, -0.608761429008721f,  0.793353340291235f, -0.38268343236509f,   0.923879532511287f, -0.130526192220052f,  0.99144486137381f,
         0.38268343236509f,   0.923879532511287f,  0.923879532511287f,  0.38268343236509f,   0.923879532511287f, -0.38268343236509f,   0.38268343236509f,  -0.923879532511287f,
        -0.38268343236509f,  -0.923879532511287f, -0.923879532511287f, -0.38268343236509f,  -0.923879532511287f,  0.38268343236509f,  -0.38268343236509f,   0.923879532511287f,
    };

    // Utility Methods
    private static float Lerp(float a, float b, float t) 
    {
        return a + t * (b - a);
    }
    private static float InterpQuintic(float t) 
    {
        return t * t * t * (t * (t * 6 - 15) + 10); 
    }

    private static float GradCoord(int seed, int xPrimed, int yPrimed, float xd, float yd) 
    {
        int hash = Hash(seed, xPrimed, yPrimed);
        hash ^= hash >> 15;
        hash &= 127 << 1;

        float xg = Gradients2D[hash];
        float yg = Gradients2D[hash | 1];

        return xd * xg + yd * yg;
    }

    private static int Hash(int seed, int xPrimed, int yPrimed)
    {
        int hash = seed ^ xPrimed ^ yPrimed;

        hash *= 0x27d4eb2d;
        return hash;
    }

    // Noise Generation Function
    public static double Noise(double x, double y) {
        int x0 = (int) x;
        int y0 = (int) y;

        float xd0 = (float)(x - x0);
        float yd0 = (float)(y - y0);
        float xd1 = xd0 - 1;
        float yd1 = yd0 - 1;

        float xs = InterpQuintic(xd0);
        float ys = InterpQuintic(yd0);

        x0 *= PrimeX;
        y0 *= PrimeY;
        int x1 = x0 + PrimeX;
        int y1 = y0 + PrimeY;

        float xf0 = Lerp(GradCoord(seed, x0, y0, xd0, yd0), GradCoord(seed, x1, y0, xd1, yd0), xs);
        float xf1 = Lerp(GradCoord(seed, x0, y1, xd0, yd1), GradCoord(seed, x1, y1, xd1, yd1), xs);

        return (Lerp(xf0, xf1, ys) * 1.4247691104677813f + 1) / 2;
    }
}