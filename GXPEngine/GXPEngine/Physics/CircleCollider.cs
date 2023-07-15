using GXPEngine.Core;
using System;

namespace GXPEngine.Physics
{
    public class CircleCollider : PhysicsObject
    {
        public int radius
        {
            get { return _radius; }
        }
        protected int _radius;

        public CircleCollider(int pRadius, Vec2 pPosition) : base(pRadius * 2 + 1, pRadius * 2 + 1, pPosition)
        {
            _radius = pRadius;
            SetColor(System.Drawing.Color.AntiqueWhite);
        }

        public CircleCollider(int pRadius, int pX, int pY) : this(pRadius, new Vec2(pX, pY))
        {
        }

        public override bool Colliding(LineCollider line)
        {
            // get length of the line
            Vec2 distanceVec = line.lineVector;
            float len = distanceVec.Length();

            float dot = (((position.x - line.start.x) * (line.end.x - line.start.x)) + ((position.y - line.start.y) * (line.end.y - line.start.y))) / (len * len);

            Vec2 closest = line.start + dot * (line.end - line.start);

            float d1 = Vec2.Displacement(closest, line.start).Length();
            float d2 = Vec2.Displacement(closest, line.end).Length();

            // get the length of the line
            float lineLen = line.lineVector.Length();
            float buffer = 0.01f;    // higher # = less accurate

            if (!(d1 + d2 >= lineLen - buffer && d1 + d2 <= lineLen + buffer))
            {
                return false;
            }

            // get distance to closest point
            distanceVec = closest - position;

            if (distanceVec.Length() <= radius)
            {
                return true;
            }
            return false;
        }

        public override bool Colliding(CircleCollider other)
        {
            Vec2 Difference = position - other.position;
            float distance = Difference.Length();
            float minDistance = other.radius + radius;

            if (minDistance > distance)
            {
                return true;
            }

            return false;
        }


        public override void Collide(PhysicsObject other)
        {
            if (other is LineCollider)
            {
                Vec2 _lineVector = ((LineCollider)other).lineVector;
                Vec2 _lineToBall = position - ((LineCollider)other).start;
                float ballDistance = _lineToBall.Dot(_lineVector.Normal());
                position += (-ballDistance + radius) * _lineVector.Normal();
                return;
            }
            if (other is CircleCollider)
            {
                Vec2 Difference = position - other.position;
                float distance = Difference.Length();

                Vec2 normal = Difference.Normalized();
                float overlap = ((CircleCollider)other).radius + radius - distance;

                other.position -= normal * overlap;
                return;
            }
        }

        protected override void Draw()
        {
            draw.Ellipse(radius, radius, radius * 2, radius * 2);
        }
    }
}
