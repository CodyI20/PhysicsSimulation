using GXPEngine.Core;
using System.Collections.Generic;

namespace GXPEngine.Physics
{
    public class RectangleCollider : PolygonCollider
    {
        Sound bounceSound = null;

        public RectangleCollider(int pWidth, int pHeight, Vec2 pPosition) :
            base(
            new List<Vec2>()
            {
                new Vec2(- pWidth / 2, - pHeight / 2),
                new Vec2(pWidth / 2, - pHeight / 2),
                new Vec2(pWidth / 2, pHeight / 2),
                new Vec2(- pWidth / 2,pHeight / 2)
            },
            pWidth, pHeight, pPosition)
        {
        }

        public RectangleCollider(int pWidth, int pHeight, int pX, int pY) : this(pWidth, pHeight, new Vec2(pX, pY))
        {
            //SetColor(System.Drawing.Color.HotPink);
        }

        public override void Update()
        {
            base.Update();
            collided = false;
        }

        public override void Collide(PhysicsObject other)
        {
            base.Collide(other);
            collided = true;
            if (bounceSound != null)
                bounceSound.Play();
        }

        protected override void Draw()
        {
            base.Draw();
            //draw.Rect(0, 0, width * 2, height * 2);
        }
    }
}
