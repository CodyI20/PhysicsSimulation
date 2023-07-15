using GXPEngine;

/// <summary>
/// An EasyDraw class used for particle effects
/// </summary>
public class ParticleBall : EasyDraw
{
    public int radius
    {
        get
        {
            return _radius;
        }
    }
    private Vec2 acceleration = new Vec2(0, 0.1f);
    private Vec2 velocity;
    private float extraSpeed;
    private Vec2 oldPosition;
    private Vec2 position;

    int _radius;
    float _mass = 0.7f;
    //float _speed;

    public ParticleBall(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2()) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        _radius = pRadius;
        position = pPosition;
        //_speed = pSpeed;
        velocity = pVelocity;

        UpdateScreenPosition();
        SetOrigin(_radius, _radius);

        Draw(255, 255, 255);
    }

    void Draw(byte red, byte green, byte blue)
    {
        ClearTransparent();
        SetColor(1, 0, 0);
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(_radius, _radius, 2 * _radius, 2 * _radius);
    }

    //void FollowMouse() {
    //	position.SetXY (Input.mouseX, Input.mouseY);
    //}

    void Update()
    {
        Step();
        DestoryIfOffScreen();
        //Console.WriteLine("Ball velocity: " + velocity.Length().ToString());
    }

    void DestoryIfOffScreen()
    {
        if (x < -width / 2 || y < height / 2 || x > game.width + width / 2 || y > game.height + height / 2)
        {
            LateDestroy();
        }
    }

    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    public void Step()
    {
        velocity += acceleration * _mass;
        //Vec2 normalVelocity = velocity;
        //velocity += normalVelocity*extraSpeed;
        oldPosition = position;
        position += velocity;

        UpdateScreenPosition();
    }
}
