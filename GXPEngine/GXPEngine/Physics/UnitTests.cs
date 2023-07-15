using GXPEngine;
using System;

class UnitTests
{
    public UnitTests()
    {
        UnitTest();
    }

    void UnitTest()
    {
        Vec2 myVec = new Vec2(6, 2);
        Vec2 myVec2 = new Vec2(2, 4);
        Vec2 result = myVec + myVec2;
        Console.WriteLine("Vector addition ok?: " + (result.x == 8 && result.y == 6 && myVec.x == 6 && myVec.y == 2 && myVec2.x == 2 && myVec2.y == 4));

        myVec = new Vec2(2, 3);
        Vec2 result2 = myVec * 3;
        Console.WriteLine("Scalar multiplication right ok ?: " + (result2.x == 6 && result2.y == 9 && myVec.x == 2 && myVec.y == 3));

        myVec = new Vec2(3, 4);
        Vec2 result3 = 4 * myVec;
        Console.WriteLine("Scalar multiplication left ok ?: " + (result3.x == 12 && result3.y == 16 && myVec.x == 3 && myVec.y == 4));

        myVec = new Vec2(5, 3);
        myVec2 = new Vec2(4, 5);
        Vec2 result4 = myVec - myVec2;
        Console.WriteLine("Vector substraction ok?: " + (result4.x == 1 && result4.y == -2 && myVec.x == 5 && myVec.y == 3 && myVec2.x == 4 && myVec2.y == 5));
        
        myVec = new Vec2(10, 12);
        myVec2 = myVec;
        myVec.SetXY(4, 4);
        Console.WriteLine("Vector SetXY ok?: " + (myVec2.x == 10 && myVec2.y == 12 && myVec.x == 4 && myVec.y == 4));

        myVec = new Vec2(5, 0);
        myVec2 = myVec;
        myVec.Normalize();
        Console.WriteLine("Vector normalization ok?: " + (myVec2.x == 5 && myVec2.y == 0 && myVec.x == 1 && myVec.y == 0));

        myVec = new Vec2(3, 4);
        Vec2 result5 = myVec.Normalized();
        Console.WriteLine("Vector normalized ok?: " + (myVec.x == 3 && myVec.y == 4 && result5.x == 0.6f && result5.y == 0.8f));

        myVec = new Vec2(6, 8);
        float result6 = myVec.Length();
        Console.WriteLine("Vector length ok?: " + (myVec.x == 6 && myVec.y == 8 && result6 == 10));

        myVec = new Vec2(5, 7);
        myVec2 = new Vec2(4, 1);
        Vec2 result7 = Vec2.Displacement(myVec, myVec2);
        Console.WriteLine("Vector substraction function(delta/difference calculation) ok:? " + (myVec.x == 5 && myVec.y == 7 && myVec2.x == 4 && myVec2.y == 1 && result7.x == -1 && result7.y == -6));

        myVec = new Vec2(3, 5);
        float scaleNumber = 3;
        Vec2 result8 = Vec2.Scale(scaleNumber,myVec);
        Console.WriteLine("Vector scaling function ok?: " + (myVec.x == 3 && myVec.y == 5 && result8.x == 9 && result8.y == 15));

        float angleInDegrees = 135;
        float deg2RadResult = Vec2.Deg2Rad(angleInDegrees);
        Console.WriteLine("Degrees to radians conversion ok?: " + (angleInDegrees == 135 && FloatPointCheck(deg2RadResult, (float)0.75*Mathf.PI)));

        float angleInRadians = Mathf.PI;
        float rad2DegResult = Vec2.Rad2Deg(angleInRadians);
        Console.WriteLine("Radians to degrees conversion ok?: " + (angleInRadians == Mathf.PI && rad2DegResult == 180));

        float angleInDegrees2 = 90;
        myVec = Vec2.GetUnitVectorDeg(angleInDegrees2);
        Console.WriteLine("GetUnitVectorDegrees ok?: " + (angleInDegrees2 == 90 && FloatPointCheck(0,myVec.x) && myVec.y == 1));

        float angleInRadians2 = Mathf.PI;
        myVec = Vec2.GetUnitVectorRad(angleInRadians2);
        Console.WriteLine("GetUnitVectorRad ok?: " + (angleInRadians2 == Mathf.PI && myVec.x==-1 && FloatPointCheck(0,myVec.y)));

        myVec = Vec2.RandomUnitVector();
        myVec2 = Vec2.RandomUnitVector();
        Console.WriteLine("myVec: " + myVec.x + " myVec2: " + myVec2.x);

        myVec = new Vec2(10,11);
        myVec2 = new Vec2(5, 4);
        Vec2 result9 = Vec2.Substract(myVec,myVec2);
        Console.WriteLine("Vector substraction via Substract() ok?: " + (myVec.x==10 && myVec.y==11 && myVec2.x==5 && myVec2.y==4 && result9.x==5 && result9.y==7));

        myVec = new Vec2(3, 4);
        myVec.SetLength(10);
        Console.WriteLine("SetLength ok?: " + (myVec.x==6&&myVec.y==8));

        myVec = new Vec2(2, 10);
        float result10 = myVec.LengthSquared();
        Console.WriteLine($"LengthSquared ok?: {result10==104}");

        myVec = new Vec2(1, 4);
        myVec2 = new Vec2(3, 5);
        float linearInterpolator = 0.5f;
        Vec2 result11 = Vec2.LinearInterpolation(myVec,myVec2,linearInterpolator);
        Console.WriteLine($"LinearInterpolation ok?: {result11.x==2&&result11.y==4.5}");

        float degrees1 = 270;
        float result12 = Vec2.normalizeDeg(degrees1);
        Console.WriteLine($"NormalizeDeg ok?: {result12 == -90}");

        myVec = new Vec2(3, 4);
        float newAngleDegrees = 90;
        myVec.SetAngleDegrees(newAngleDegrees);
        Console.WriteLine($"SetAngle ok?: {FloatPointCheck(myVec.x,0)&&myVec.y==5}");

        myVec = new Vec2(0, 5);
        float result13 = myVec.GetAngleDegrees();
        Console.WriteLine($"GetAngle ok?: {result13 == 90}");

        myVec = new Vec2(0, 5);
        float degrees2 = 90;
        myVec.RotateDegrees(degrees2);
        Console.WriteLine($"Rotate ok?: {myVec.x==-5&&FloatPointCheck(myVec.y,0)}");

        myVec = new Vec2(4, 5);
        Vec2 newPoint = new Vec2(2, 0);
        float degrees3 = 180;
        myVec.RotateAroundDegrees(degrees3, newPoint);
        Console.WriteLine($"RotateAround ok?: {FloatPointCheck(myVec.x,0) && myVec.y==-5}");

        myVec = new Vec2(2, 3);
        myVec2 = new Vec2(1, 2);
        float result14 = myVec.Dot(myVec2);
        Console.WriteLine($"Dot ok?: {result14==8}");

        myVec = new Vec2(4, -3);
        Vec2 result15 = myVec.Normal();
        Console.WriteLine($"Normal ok?: {result15.x == (float)0.6 && result15.y == (float)0.8}");

        myVec = new Vec2(1, 4);
        myVec2 = new Vec2(3, 4);
        myVec.Reflect(myVec2);
        Console.WriteLine($"Reflect ok?: {FloatPointCheck((float)-3.56,myVec.x) && FloatPointCheck((float)-2.08,myVec.y)}");
    }


    bool FloatPointCheck(float left, float right)
    {
        return Mathf.Abs(left - right) <= 0.00001f;
    }
}
