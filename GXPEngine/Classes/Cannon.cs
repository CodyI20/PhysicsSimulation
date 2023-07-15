using GXPEngine;
using System;

class Cannon : Sprite
{
    PlayerBall _ball;
    public Vec2 position
    {
        get { return _position; }
    }

    private Vec2 _position;
    private Vec2 velocity;
    private float _angle;
    public Cannon(Vec2 pPosition) : base(Settings.ASSET_PATH + "Art/e100.png", false, false) //Asset used from class assignments
    {
        scale = 0.5f;
        SetOrigin(0, height/2);
        _position = pPosition;
        Console.WriteLine(_position.ToString());
    }

    public void ShootBall()
    {
        if (Input.GetKeyDown(Key.SPACE))
        {
            Vec2 cannonTip = new Vec2(position.x + Mathf.Cos(Vec2.Deg2Rad(_angle)) * width, position.y + Mathf.Sin(Vec2.Deg2Rad(_angle)) * width);

            // Calculate the velocity vector based on the angle and power of the shot
            Vec2 velocity = Vec2.GetUnitVectorDeg(_angle)*0.4f;
            _ball = new PlayerBall(20, cannonTip, velocity*Time.deltaTime);

            // Add the ball to the game
            game.Currentscene.AddChildAt(_ball, 100);
        }
    }

    void UpdatePosition()
    {
        //_position = new Vec2(x+parent.x,y+parent.y);
        x = position.x;
        y = position.y;
    }

    void PointAtMouse()
    {
        Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
        Vec2 direction = Vec2.Displacement(_position, mousePos).Normalized();
        _angle = direction.GetAngleDegrees();

        #region BlockRotation
        //Console.WriteLine(_angle);
        /**/
        if (_angle > 4.16) //Can't go below the bottom boundary
            _angle = (float)4.16;
        /**
        if (_angle < -96)
            _angle = (float)-96;
        /**/
        #endregion

        rotation = _angle;
    }

    public void Update()
    {
        UpdatePosition();
        ShootBall();
        PointAtMouse();
    }
}
