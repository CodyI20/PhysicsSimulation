using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;
using System;

class Explosions : RectangleCollider
{
    int particleCount;
    float explosionForce;
    Vec2 explosionPosition;
    public Explosions(Vec2 pPosition) : base(2500,2500, pPosition)
    {
        explosionPosition = new Vec2(x, y);

        Explosion();
    }

    void Explosion()
    {
        Random random = new Random(Time.time);

        //Generate a random number for the amount of particles
        particleCount = random.Next(250, 300);

        for (int i = 0; i < particleCount; i++)
        {
            // Generate a random number for the force of the explosion
            explosionForce = (float)random.NextDouble() * 3f;

            // Generate a random angle in radians
            float angle = (float)(random.NextDouble() * 2 * Mathf.PI);

            // Calculate the position of the particle using the angle and explosion position
            Vec2 particlePosition = explosionPosition + new Vec2(Mathf.Cos(angle), Mathf.Sin(angle));

            // Calculate the direction from the explosion center to the particle position
            Vec2 direction = Vec2.Displacement(explosionPosition, particlePosition).Normalized();

            // Calculate the particle's velocity based on the explosion force and direction
            Vec2 particleVelocity = direction * explosionForce * random.Next(5,10);

            // Create a new AnimalBall with the generated parameters
            ParticleBall particle = new ParticleBall(15, particlePosition, particleVelocity);

            // Add the particle to the game scene
            ((MyGame)game).Currentscene.AddChild(particle);
        }

        LateDestroy();
    }
}