using GXPEngine.Physics;
using GXPEngine;
using System.Drawing;

class PlayerBall : CircleCollider
{
    Timer deadTimer;
    public float _mass;
    public float _lowMass=0.1f;
    public float _mediumMass=0.25f;
    public float _heavyMass=0.5f;
    public bool hasBeenBoosted = false;
    private new Vec2 acceleration = new Vec2(0, 0.01f);
    private Color _ballColor = Color.Red;

    public PlayerBall(int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2()) : base(pRadius, pPosition)
    {
        _radius = pRadius;
        position = pPosition;

        velocity = pVelocity;
        _mass = _mediumMass;

        SetColor(_ballColor);

        affectedByGravity = true;
        _bounciness = 0.6f;
        
        //SetColor(System.Drawing.Color.Pink);
    }

    public override bool moving
    {
        get { return true; }
    }

    public override void Collide(PhysicsObject other)
    {
        base.Collide(other); // Call base method to handle the collision
        float reflectStrength;
        reflectStrength = other._bounciness + _bounciness;

        if (other is LineCollider)
        {
            velocity.Reflect(((LineCollider)other).lineVector.Normal(), reflectStrength);
        }
        else if (other is CircleCollider)
        {
            velocity.Reflect(Vec2.Displacement(position, other.position).Normalized(), reflectStrength);
        }
        else if (other is PlayerBall)
        {
            velocity.Reflect(Vec2.Displacement(position, other.position).Normalized(), reflectStrength);
            ((PlayerBall)other).velocity.Reflect(Vec2.Displacement(other.position, position).Normalized(), reflectStrength);

            // Swap mass values
            float tempMass = _mass;
            _mass = ((PlayerBall)other)._mass;
            ((PlayerBall)other)._mass = tempMass;
        }
    }


    public override void Update()
    {
        ChangeAnimal();

        if (deadTimer != null)
        {
            if (deadTimer.done)
                LateRemove();
        }
        if (game.Currentscene is Level)
        {
            base.Update();
        }
        if (Input.GetKeyDown(Key.SPACE))
        {
            LateRemove();
        }
    }

    public void Die()
    {
        if (deadTimer == null)
        {
            deadTimer = new Timer(0.01f);
            AddChild(deadTimer);
            parent.recieveMessage("dead");
        }
    }

    void ChangeAnimal()
    {
        if (Input.GetKeyDown(Key.ONE))
        {
            SetColor(Color.Aqua);
            _mass = _lowMass;
        }
        if (Input.GetKeyDown(Key.TWO))
        {
            SetColor(Color.PaleVioletRed);
            _mass = _mediumMass;
        }
        if (Input.GetKeyDown(Key.THREE))
        {
            SetColor(Color.DarkSeaGreen);
            _mass = _heavyMass;
        }
    }

    public override void Step()
    {
        velocity += acceleration * _mass * Time.deltaTime;
        position += velocity * Time.deltaTime;
        UpdateScreenPosition();
    }
}

