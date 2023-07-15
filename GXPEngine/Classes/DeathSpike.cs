using GXPEngine;
using GXPEngine.Physics;

public class DeathSpike : TriangleCollider
{
    public DeathSpike(int pBase, int pHeight, int pX, int pY, int angle = 0) : base(pBase, pHeight, pX, pY)
    {
        SetColor(System.Drawing.Color.OrangeRed);
        trigger = true;
        vecRotation.RotateDegrees(angle);       
    }

    public override void Collide(PhysicsObject other)
    {
        if (other is PlayerBall)
        {
            ((PlayerBall)other).Die();
        }
    }
}

