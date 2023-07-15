using GXPEngine; // Allows using Mathf functions
using System;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public void SetXY(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }//

    public void Normalize()
    {
        float l = Mathf.Sqrt(x * x + y * y);
        if (l != 0)
        {
            x /= l;
            y /= l;
        }
    }//

    public Vec2 Normalized()
    {
        Vec2 vec = this;
        vec.Normalize();
        return vec;
        //float l = Mathf.Sqrt(x * x + y * y);
        //if (x != 0 || y != 0)
        //    return new Vec2(x / l, y / l);
        //return new Vec2(x, y);
    }//
    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }//

    public void SetLength(float length)
    {
        Normalize();
        this *= length;
    }

    public float LengthSquared()
    {
        return x * x + y * y;
    }

    public static Vec2 Displacement(Vec2 left, Vec2 right)//Delta/Difference(displacement) calculation from LEFT to RIGHT
    {
        return right - left;
    }

    public Vec2 Displacement(Vec2 right)
    {
        x = right.x - x;
        y = right.y - y;
        return this;
    }

    public static Vec2 Substract(Vec2 left, Vec2 right)
    {
        return left - right;
    }

    public Vec2 Substract(Vec2 other)
    {
        x -= other.x;
        y -= other.y;
        return this;
    }

    public static Vec2 Add(Vec2 left, Vec2 right)
    {
        return left + right;
    }
    public Vec2 Add(Vec2 other)
    {
        x += other.x;
        y += other.y;
        return this;
    }

    public static Vec2 Scale(float number, Vec2 vector)
    {
        return number * vector;
    }

    public Vec2 Scale(float number)
    {
        x *= number;
        y *= number;
        return this;
    }

    public static Vec2 LinearInterpolation(Vec2 left, Vec2 right, float interpolator)
    {
        return left * (1-interpolator) + interpolator * right;
    }

    public static float normalizeDeg(float degree)
    {
        if (degree > 180)
        {
            degree -= 360;
        }
        else if (degree < -180)
        {
            degree += 360;
        }

        return degree;
    }

    public static float Deg2Rad(float degrees)
    {
        return degrees * Mathf.PI / 180;
    }//
    public static float Rad2Deg(float radians)
    {
        return radians * 180 / Mathf.PI;
    }//
    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        return GetUnitVectorRad(Deg2Rad(degrees));
    }//
    public static Vec2 GetUnitVectorRad(float radians)
    {
        return new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));
    }//
    public static Vec2 RandomUnitVector()
    {
        Random rnd = new Random(Time.now);
        float degrees = rnd.Next(0, 360);
        float radians = Deg2Rad(degrees);
        return GetUnitVectorRad(radians);
    }
    public void SetAngleDegrees(float degrees)
    {
        SetAngleRadians(Deg2Rad(degrees));
    }
    public void SetAngleRadians(float radians)
    {
        float length = Length();
        x = Mathf.Cos(radians) * length;
        y = Mathf.Sin(radians) * length;
    }
    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }
    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }
    public void RotateRadians(float radians)
    {
        SetXY(x * Mathf.Cos(radians) - y * Mathf.Sin(radians), x * Mathf.Sin(radians) + y * Mathf.Cos(radians));
    }
    public void RotateDegrees(float degrees)
    {
        RotateRadians(Deg2Rad(degrees));
    }
    public void RotateAroundDegrees(float degrees, Vec2 point)
    {
        RotateAroundRadians(Deg2Rad(degrees), point);
    }
    public void RotateAroundRadians(float radians, Vec2 point)
    {
        Substract(point);
        RotateRadians(radians);
        Add(point);
    }
    public float Dot(Vec2 other)
    {
        return x*other.x + y*other.y;
    }
    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalized();
    }
    public void Reflect(Vec2 vec, float bounciness = 1f)
    {
        vec.Normalize();
        Substract(vec.Scale((1 + bounciness) * Dot(vec)));
    }
    public static Vec2 PointOfImpact(Vec2 oldPos, float a, float b, Vec2 velo)
    {
        return oldPos + (a / b * velo);
    }

    public Vec2 ApplyExplosionImpulse(Vec2 position, Vec2 explosionCenter, float explosionForce, float explosionRadius)
    {
        Vec2 direction = Displacement(explosionCenter, position);
        float distance = direction.Length();

        Console.WriteLine("Position: {0} ; ExplosionCenter: {1}", position, explosionCenter);


        float force = explosionForce;
        direction.Normalize();

        /**/
        if (distance < explosionRadius) //Used if the explosion happens in a different way other than direct collision
            this += direction * force;
        /**/

        return this;
    }
    public Vec2 Boost(float boostAmount, float angle = 0, float timeWithBoost = 1f)
    {
        // Convert the angle to radians
        float angleInRadians = Deg2Rad(angle);

        angleInRadians = normalizeDeg(angleInRadians);

        // Calculate the boost vector using the specified angle
        float x = Mathf.Cos(angleInRadians) * boostAmount;
        float y = Mathf.Sin(angleInRadians) * boostAmount;
        Vec2 boostVector = new Vec2(x, y);


        // Add the boost vector to the current vector
        //this += boostVector;

        // Change the position of the velocity and then add the boost vector
        SetAngleDegrees(boostVector.GetAngleDegrees());
        this += boostVector;

        // Return the velocity to its original values
        UndoBoost(boostAmount, timeWithBoost);

        return this;
    }

    void UndoBoost(float boostAmount, float timeWithBoost = 1f)
    {
        Timer timer = new Timer(timeWithBoost);
        if (timer.done)
        {
            Vec2 normalVelo = Normalized() / boostAmount;
            this = normalVelo;
        }
    }

    public static Vec2 operator *(float number, Vec2 vector)
    {
        return new Vec2(number * vector.x, number * vector.y);
    }//

    public static Vec2 operator *(Vec2 vector, float number)
    {
        return new Vec2(vector.x * number, vector.y * number);
    }//

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }
    public static Vec2 operator -(Vec2 vector)
    {
        return new Vec2(-vector.x, -vector.y);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator /(Vec2 left, float right)
    {
        return new Vec2(left.x / right, left.y / right);
    }

    public override string ToString()
    {
        return string.Format("({0},{1})", x, y);
    }

}

