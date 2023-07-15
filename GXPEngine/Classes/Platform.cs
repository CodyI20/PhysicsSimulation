using GXPEngine;
using GXPEngine.Physics;
using System;

class Platform : GameObject
{
    RectangleCollider rect;

    public Platform(int width, int height, int pX, int pY, bool hasCollision = true, bool isBouncy = false, int angle = 0, float scale = 1f)
    {
        this.scale = scale;
        SetXY(pX, pY);

        rotation = angle;

        if (hasCollision)
        {
            rect = new RectangleCollider(width, height, pX, pY);
            rect.vecRotation.SetAngleDegrees(rotation);
            rect.position.RotateAroundDegrees(rotation, new Vec2(x, y));
            game.Currentscene.AddChild(rect);
            if (isBouncy)
            {
                rect._bounciness = 0.8f;
                rect.SetColor(System.Drawing.Color.Yellow);
            }
            else
            {
                rect.SetColor(System.Drawing.Color.Turquoise);
            }
        }
    }
}

