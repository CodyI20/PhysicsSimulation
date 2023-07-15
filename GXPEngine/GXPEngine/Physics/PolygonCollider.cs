using System;
using System.Collections.Generic;

namespace GXPEngine.Physics
{
    public class PolygonCollider : PhysicsObject
    {
        public bool collided;
        public List<CircleCollider> points
        {
            get { return createPoints(_positions); }
        }

        public List<LineCollider> lines
        {
            get { return createLines(points); }
        }

        protected List<Vec2> _positions;

        public PolygonCollider(List<Vec2> pPositions, Vec2 pPosition) : base(calculateWidth(pPositions), calculateHeight(pPositions), pPosition)
        {
            _positions = pPositions;
        }

        public PolygonCollider(List<Vec2> pPositions, int pWidth, int pHeight, Vec2 pPosition) : base(pWidth, pHeight, pPosition)
        {
            _positions = pPositions;
        }

        private static int calculateWidth(List<Vec2> pPoints)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (Vec2 point in pPoints)
            {
                int pointX = (int)point.x;

                if (pointX < min) min = pointX;
                if (pointX > max) max = pointX;
            }

            return max - min + 4;
        }

        private static int calculateHeight(List<Vec2> pPoints)
        {
            int min = int.MaxValue;
            int max = int.MinValue;

            foreach (Vec2 point in pPoints)
            {
                int pointY = (int)point.y;

                if (pointY < min) min = pointY;
                if (pointY > max) max = pointY;
            }

            return max - min + 4;
        }

        protected List<CircleCollider> createPoints(List<Vec2> positions)
        {
            List<CircleCollider> outP = new List<CircleCollider>();

            foreach (Vec2 pos in positions)
            {
                Vec2 newPos = pos;

                newPos.x += position.x;
                newPos.y += position.y;
                newPos.RotateAroundDegrees(vecRotation.GetAngleDegrees(), position);

                outP.Add(new CircleCollider(1, newPos));
            }

            return outP;
        }

        protected List<LineCollider> createLines(List<CircleCollider> points)
        {
            List<LineCollider> outP = new List<LineCollider>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                outP.Add(new LineCollider(points[i].position, points[i + 1].position, _bounciness, trigger));
            }

            outP.Add(new LineCollider(points[points.Count - 1].position, points[0].position, _bounciness, trigger));

            return outP;
        }

        public override bool Colliding(LineCollider lineSegment)
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

        protected override void Draw()
        {
            draw.StrokeWeight(3);

            for (int i = 0; i < _positions.Count-1; i++)
            {
                draw.Line(_positions[i].x + width / 2, _positions[i].y + height / 2, _positions[i + 1].x + width / 2, _positions[i + 1].y + height / 2);
            }

            draw.Line(_positions[points.Count - 1].x + width / 2, _positions[points.Count - 1].y + height / 2, _positions[0].x + width / 2, _positions[0].y + height / 2);
        }
    }
}