//using System.Collections.Generic;
//using GXPEngine.Core;

//namespace GXPEngine.Physics
//{
//    public class PhysicsPolygonDebugger : EasyDraw
//    {
//        private PolygonCollider _polygon;

//        public PhysicsPolygonDebugger(PolygonCollider polygon) : base(200,50,false)
//        {
//            _polygon = polygon;
//        }

//        void Update()
//        {
//            foreach (var line in _polygon.lines)
//            {
//                LineSegment lineDrawn = new LineSegment(line.start.x, line.start.y, line.end.x, line.end.y, 0xffff0000, 3);
//                LateAddChild(lineDrawn);
//            }
//        }
//    }
//}
