using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;


namespace Crypto5
{
    class Program
    {
        private static BigInteger p = BigInteger.Parse("13407807929942597099574024998205846127479365820592393377723561443721764030073546976801874298166903427690031858186486050853753882811946569946433649006084171");
        private static BigInteger g = BigInteger.Parse("11717829880366207009516117596335367088558084999998952205599979459063929499736583746670572176471460312928594829675428279466566527115212748467589894601965568");
        private static BigInteger h = BigInteger.Parse("3239475104050450443565264378728065788649097520952449527834792452971981976143292558073856937958553180532878928001494706097394108577585732452307673444020333");

        private static int B = (int)Math.Pow(2, 20);
        private static BigInteger G_B = BigInteger.ModPow(g, B, p);
        private static BigInteger G_INV = BigInteger.ModPow(g, p - 2, p);


        // Implementing the formula at it is (using calc1 and calc2 functions) also works, but it is
        // very very slow, it is much better to simplify first and apply as it is done in Main
        static void Main(string[] args)
        {
            //CalculeExercices();

            int x1 = 0;
            int x0 = 0;

            var dict = new Dictionary<BigInteger, int>();

            BigInteger denominv = 1;

            for (int i = 0; i < B; i++)
            {
                //dict[calc1(i)] = i;

                dict[BigInteger.Multiply(h, denominv) % p] = i;

                denominv = BigInteger.Multiply(denominv, G_INV) % p;
            }


            BigInteger acumulator = 1;

            for (int i = 0; i < B; i++)
            {
                //BigInteger c2 = calc2(i);

                if (dict.ContainsKey(acumulator))
                {
                    Console.WriteLine("X0: " + i);
                    Console.WriteLine("X1: " + dict[acumulator]);
                    x1 = dict[acumulator];
                    x0 = i;
                    break;
                }

                acumulator = BigInteger.Multiply(acumulator, G_B) % p;
            }

            //x0 = 357984;
            //x1 = 787046;
            //x = 375374217830;
            Console.WriteLine("Result: ");
            BigInteger x = (BigInteger.Multiply(x0, B) + x1) % p;
            Console.WriteLine(x);


            Console.WriteLine("Test: ");
            BigInteger h2 = BigInteger.ModPow(g, x, p);
            Console.WriteLine(BigInteger.Compare(h, BigInteger.ModPow(g, x, p)));
        }

        private static BigInteger calc1(BigInteger x1)
        {
            // Three different ways to do the same
            BigInteger denominv = BigInteger.ModPow(g, x1, p);
            //BigInteger denom = BigInteger.ModPow(denominv, -1, p);
            //BigInteger denom2 = ModInverse(denominv,p);
            BigInteger denom3 = BigInteger.ModPow(denominv, p - 2, p);

            // Two ways to do the same
            BigInteger tval = BigInteger.Multiply(h, denom3);
            //BigInteger retval = BigInteger.ModPow(tval, 1, p);
            //BigInteger retval2 = tval%p;
            return tval % p;
        }

        private static BigInteger calc2(int i)
        {
            BigInteger result = BigInteger.ModPow(G_B, i, p);
            BigInteger result2 = result % p;
            return result;
        }

        private static BigInteger ModInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }


        // Auxiliar fuction to compute some numbers needed only for the exercices of the exam
        private static void CalculeExercices()
        {
            double result = Math.Pow(2, 245);
            Console.WriteLine(result);
            Console.WriteLine(result % 35);

            for (int i = 0; i < 1000000; i++)
            {
                Double result2 = Math.Log(i * 35 + 1, 2);


                if (result2 % 1 == 0)
                {
                    Console.WriteLine("Found!!!!!!");
                    Console.WriteLine(i);
                    Console.WriteLine(i * 35 + 1);
                    Console.WriteLine(result2);
                }
            }

            double result3 = Math.Pow(12, 6);
            Console.WriteLine(result3 % 23);
        }
    }
}
