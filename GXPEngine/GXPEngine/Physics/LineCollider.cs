using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine.Core;

namespace GXPEngine.Physics
{
    public class LineCollider : PhysicsObject
    {
        public Vec2 start;
        public Vec2 end;
        public Vec2 middle
        {
            get { return CalculateMiddle(start, end); }
        }
        public Vec2 lineVector
        {
            get { return start - end; }
        }

        public LineCollider(Vec2 pStart, Vec2 pEnd, float pBounciness = 0f, bool pTrigger = false) : base((int)Vec2.Displacement(pEnd, pStart).Length(), 1, CalculateMiddle(pStart, pEnd))
        {
            start = pStart;
            end = pEnd;

            _bounciness = pBounciness;
            trigger = pTrigger;
            vecRotation.SetAngleDegrees(lineVector.GetAngleDegrees());

            UpdateScreenPosition();
        }

        public LineCollider(int startX, int startY, int endX, int endY ) : this(new Vec2(startX, startY), new Vec2(endX, endY))
        {
            SetColor(System.Drawing.Color.Pink);
        }

        private static Vec2 CalculateMiddle(Vec2 _start, Vec2 _end)
        {
            Vec2 middle = _end + _start;
            middle /= 2f;
            return middle;
        }

        protected override void Draw()
        {
            draw.Rect(0, 0, width*2, height*2);
            //draw.Clear(System.Drawing.Color.Green);
        }

        public override bool Colliding(LineCollider other)
        {
            throw new NotImplementedException();
        }

        public override bool Colliding(CircleCollider circle)
        {
            throw new NotImplementedException();
        }

        public override void Collide(PhysicsObject other)
        {
        }
    }
}
