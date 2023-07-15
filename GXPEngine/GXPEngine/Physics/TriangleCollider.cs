using System.Collections.Generic;

namespace GXPEngine.Physics
{
    public class TriangleCollider : PolygonCollider
    {
        public TriangleCollider(int pBase, int pHeight, Vec2 pPosition) :
            base(
            new List<Vec2>()
            {
                new Vec2(-pBase/2, pHeight/2),
                new Vec2(pBase/2, pHeight/2),
                new Vec2(pBase/16, -pHeight/2),
            },
            pBase, pHeight, pPosition)
        {
        }

        public TriangleCollider(int pBase, int pHeight, int pX, int pY) : this(pBase, pHeight, new Vec2(pX, pY))
        {
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
        }

        protected override void Draw()
        {
            base.Draw();
        }
    }
}
