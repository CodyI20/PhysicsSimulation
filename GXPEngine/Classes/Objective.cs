using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;

class Objective : RectangleCollider
{
    Timer deadTimer;
    bool played = false;

    public Objective(int pWidth, int pHeight, int pX, int pY) : base(pWidth, pHeight, new Vec2(pX, pY))
    {
        trigger = true;
        SetColor(System.Drawing.Color.Green);
    }

    public override void Update()
    {
        base.Update();

        FinishLevel();
    }

    void FinishLevel()
    {
        if (deadTimer != null && deadTimer.done)
        {
            LateRemove();
            game.SceneManager.GotoNextscene();
        }
    }

    public override void Collide(PhysicsObject other)
    {
        if (other is PlayerBall)
        {
            ((PlayerBall)other).LateDestroy();

            if (deadTimer == null)
            {
                deadTimer = new Timer(0.1f);

                AddChild(deadTimer);
                /**
                new Sound(Settings.ASSET_PATH + "SFX/Finish.mp3").Play(false, 0, Settings.sfxVolume, 0);
                if (played == false)
                {
                    //new Sound(Settings.ASSET_PATH + "SFX/Rocket.wav").Play(false, 0, Settings.sfxVolume, 0);
                    played = true;
                }
                /**/
            }
        }
    }
}
