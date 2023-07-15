using GXPEngine.Physics;

class LandMine : RectangleCollider
{
    float explosionRadius;
    float explosionForce;
    public LandMine(int pWidth, int pHeight, Vec2 pPosition, int angle = 0, float pExplosionRadius = 70f,float pExplosionForce=3f) : base(pWidth, pHeight, pPosition)
    {
        trigger = true;
        explosionForce = pExplosionForce;
        explosionRadius = pExplosionRadius;
        vecRotation.RotateDegrees(angle);
        SetColor(System.Drawing.Color.MediumVioletRed);
    }

    public LandMine(int pWidth, int pHeight, float pX, float pY, int angle = 0, float pExplosionRadius = 70f, float pExplosionForce = 3f) : this(pWidth, pHeight, new Vec2(pX, pY),angle,pExplosionRadius,pExplosionForce)
    {
    }

    public override void Collide(PhysicsObject other)
    {
        base.Collide(other);
        if (other is PlayerBall)
        {
            ((PlayerBall)other).velocity.ApplyExplosionImpulse(other.position, position, explosionForce, explosionRadius);
            ((MyGame)game).Currentscene.AddChild(new Explosions(new Vec2(x, y)));
            LateDestroy();
        }
    }
}
