using System;
using System.Drawing;

namespace GXPEngine.Physics
{
	public abstract class PhysicsObject : GameObject
	{
		public Vec2 position;
		public Vec2 vecRotation;
		public Vec2 velocity;
		public Vec2 acceleration;
		public float _bounciness = 0.0f;
		public bool trigger = false;

		public virtual bool moving
		{
			get { return velocity.Length() > 0; }
		}
		public Vec2 oldPosition
		{
			get { return position - velocity; }
		}
		public bool affectedByGravity;

		public int width
		{
			get { return _width; }
		}
		public int height
		{
			get { return _height; }
		}
		protected EasyDraw draw
		{
			get { return _easyDraw; }
		}
		protected Scene _currentScene
		{
			get { return game.Currentscene; }
		}

		protected int _width;
		protected int _height;
		private EasyDraw _easyDraw;

		public PhysicsObject(int pWidth, int pHeight, Vec2 pPosition) : base(false)
		{
			position = pPosition;
			vecRotation = Vec2.GetUnitVectorDeg(0);

			if (pWidth <= 0 || pHeight <= 0)
			{
				throw new Exception("Dimensions of a Physicsobject cannot be smaller or equal to 0");
			}
			else
			{
				_width = pWidth;
				_height = pHeight;
			}

			UpdateScreenPosition();
		}

		protected abstract void Draw();

		public void SetColor(Color fillColor)
		{
			if (_easyDraw == null)
			{
				_easyDraw = new EasyDraw(width + 10, height, false);
				_easyDraw.SetOrigin(_easyDraw.width / 2, _easyDraw.height / 2);

				AddChild(_easyDraw);
			}

			_easyDraw.ClearTransparent();
			_easyDraw.Stroke(fillColor);
			_easyDraw.StrokeWeight(3);

			Draw();
		}

		protected virtual void UpdateScreenPosition()
		{
			x = position.x;
			y = position.y;
			rotation = vecRotation.GetAngleDegrees();
		}

		public bool Colliding(PhysicsObject other)
		{
			bool output;

			if (other is PolygonCollider)
			{
				output = Colliding((PolygonCollider)other);
			}
			else if (other is LineCollider)
			{
				output = Colliding((LineCollider)other);
			}
			else if (other is CircleCollider)
			{
				output = Colliding((CircleCollider)other);
			}
			else
			{
				return false;
			}

			if (output)
			{
				other.Collide(this);

				if (!other.trigger)
				{
					 Collide(other);
				}
			}

			return output;
		}

		public abstract bool Colliding(LineCollider lineSegment); //Check collisions for Lines
		public abstract bool Colliding(CircleCollider circle); //Check collisions for circles
		public virtual bool Colliding(PolygonCollider poly) //Check collisions for any polygon
		{
			foreach (PhysicsObject line in poly.lines) //This is pretty much just checks all lines of a polygon, and if it collides with it, it will return true
			{
				if (Colliding(line))
				{
					return true;
				}
			}

			return false;
		}
		public abstract void Collide(PhysicsObject other);

		public virtual void Step()
		{
			UpdateScreenPosition();
			setAccelleration();
			applyAcceleration();
			applyVelocity();
		}

        public virtual void Update()
		{
			Step();
		}

		protected virtual void applyVelocity()
		{
			position += velocity * Time.deltaTime;
			//Console.WriteLine(1f / game.currentFps);
		}

		protected virtual void applyAcceleration()
		{
			velocity += acceleration * Time.deltaTime;
			acceleration.SetXY(0, 0);
		}

		protected virtual void setAccelleration()
		{
			if (affectedByGravity && _currentScene is Level)
			{
				acceleration += ((Level)_currentScene).gravity;
			}
		}
	}
}
