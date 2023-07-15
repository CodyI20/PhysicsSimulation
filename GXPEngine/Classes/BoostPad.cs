using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;
using System;

class BoostPad : RectangleCollider
{
    AnimationSprite boostSprite;
    float boostAmount = 0.85f;
    public BoostPad(int pWidth, int pHeight, int pX, int pY, int angle = 0) : base(pWidth, pHeight, new Vec2(pX,pY))
    {
        trigger = true;
        scale = 0.2f;
        boostSprite = new AnimationSprite(Settings.ASSET_PATH + "Art/boost.png",10,1,-1,false,true);
        boostSprite.SetCycle(0,10);
        boostSprite.SetOrigin(boostSprite.width/2,boostSprite.height/2);
        boostSprite.SetXY(0, 0);
        boostSprite.rotation = angle;
        AddChild(boostSprite);
        SetXY(pX, pY);
        SetColor(System.Drawing.Color.White);
    }
    public override void Update()
    {
        base.Update();
        boostSprite.Animate(0.05f);
    }
    public override void Collide(PhysicsObject other)
    {
        PlayerBall animalBall = (PlayerBall)other;
        if (other is PlayerBall)
        {
            animalBall.velocity.Boost(boostAmount, boostSprite.rotation);
            LateDestroy();
        }
    }
}
