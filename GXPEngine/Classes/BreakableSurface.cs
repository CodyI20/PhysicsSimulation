using GXPEngine;
using GXPEngine.Physics;

class BreakableSurface : RectangleCollider
{
    bool active;
    public BreakableSurface(int pWidth, int pHeight, Vec2 pPosition, int angle = 0) : base(pWidth, pHeight, pPosition)
    {
        Restart();
        rotation = angle;
        SetColor(System.Drawing.Color.SaddleBrown);
    }
    public BreakableSurface(int pWidth, int pHeight, int pX, int pY, int angle = 0) : this(pWidth, pHeight, new Vec2(pX, pY), angle)
    {
    }

    public void Restart()
    {
        active = true;
    }

    public override void Update()
    {
        base.Update();
        if (active == false)
        {
            LateDestroy();
        }
    }

    public override void Collide(PhysicsObject other)
    {
        if (other is PlayerBall && active && ((PlayerBall)other)._mass == ((PlayerBall)other)._heavyMass)
        {
            active = false;
        }
    }
}
