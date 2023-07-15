﻿using GXPEngine.Core;

namespace GXPEngine
{
	/// <summary>
	/// Implements a line with normal representation
	/// </summary>
	public class NLineSegment : LineSegment
	{
		private Arrow _normal;
		public readonly float lineBounciness;
		public readonly float lineSpeedBoost;

		public NLineSegment (float pStartX, float pStartY, float pEndX, float pEndY, uint pColor = 0xffffffff, uint pLineWidth = 1)
			: this (new Vec2 (pStartX, pStartY), new Vec2 (pEndX, pEndY), pColor, pLineWidth)
		{
		}

		public NLineSegment (Vec2 pStart, Vec2 pEnd, uint pColor = 0xffffffff, uint pLineWidth = 1, float pBounciness=0f, float pSpeed=0f)
			: base (pStart, pEnd, pColor, pLineWidth)
		{
			_normal = new Arrow (new Vec2(0,0), new Vec2(0,0), 40, 0xffff0000, 1);
			AddChild (_normal);
			lineBounciness = pBounciness;
			lineSpeedBoost = pSpeed;
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														RenderSelf()
		//------------------------------------------------------------------------------------------------------------------------
		override protected void RenderSelf(GLContext glContext) {
			if (game != null) {
				recalculateArrowPosition ();
				Gizmos.RenderLine(start.x, start.y, end.x, end.y, color, lineWidth);
			}
		}

		private void recalculateArrowPosition() {
			_normal.startPoint = (start + end) * 0.5f;
			_normal.vector = (end-start).Normal ();
		}

	}
}
