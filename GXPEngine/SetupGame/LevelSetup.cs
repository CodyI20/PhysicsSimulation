using GXPEngine;
using GXPEngine.Core;
using GXPEngine.Physics;

public class PlayableLevel : Level
{
    public PlayableLevel() : base("playableLevel")
    {
        name = "playableLevel";
        gravity = new Vec2(0, 9.81f);
    }

    public override void onLoad()
    {
        ///LAND MINE -- RED
        AddChild(new LandMine(35,10,1165,400,235));
        AddChild(new LandMine(35, 10, 438,535, 105));
        AddChild(new LandMine(35, 10, 518, 99, 125));

        ///DeathSpikes -- ORANGE
        AddChild(new DeathSpike(30, 50, 960, 655));
        AddChild(new DeathSpike(30, 50, 930, 655));
        AddChild(new DeathSpike(30, 50, 900, 655));
        AddChild(new DeathSpike(30, 50, 870, 655));
        AddChild(new DeathSpike(30, 50, 840, 655));
        AddChild(new DeathSpike(30, 50, 810, 655));
        AddChild(new DeathSpike(30, 50, 780, 655));

        ///BoostPad -- AnimationSprite
        AddChild(new BoostPad(51,51, 100,200));
        AddChild(new BoostPad(51, 51, 1325, 668,235));
        AddChild(new BoostPad(51, 51, 520, 551,270));
        AddChild(new BoostPad(51, 51, 1624, 811,270));
        AddChild(new BoostPad(51, 51, 339, 423, 315));


        ///BreakableObject -- Brown
        AddChild(new BreakableSurface(175,30,game.width-115,100));
        AddChild(new BreakableSurface(30, 150, game.width - 215, 75));


        ///Normal platforms -- Turquoise
        AddChild(new Platform(700,15,770,game.height-250));
        AddChild(new Platform(250, 15, 1185, 400, true, false, 235));
        AddChild(new Platform(250, 15, 450, 550, true, false, 105));
        AddChild(new Platform(150, 20, 500, 96, true, false, 125));
        AddChild(new Platform(150, 20, 1081, 79, true, false, 45));


        ///Bouncy platforms -- Yellow
        AddChild(new Platform(150, 10, 1469, 488, true, true));
        AddChild(new Platform(250, 10, 774, 27, true, true));

        ///Circle obstacle -- White
        AddChild(new CircleCollider(50, new Vec2(783, 188)));
        base.onLoad();

        ///Objective -- Green
        AddChild(new Objective(100, 35, 1575, 25));
    }
}


public class Level : Scene
{
    public Vec2 gravity;
    public float resistance;

    protected MyGame myGame;


    public Level(string name) : base(name)
    {
        myGame = (MyGame)game;
    }

    public override void Update()
    {
        base.Update();
    }


    public override void onLoad()
    {
        base.onLoad();

        AddChild(new ButtonAssemblyw(0, 0, 3, 0.5f));

        ///Boundries
        AddChild(new LineCollider(0, 0, 0, game.height));
        AddChild(new LineCollider(game.width, game.height, game.width, 0));
        AddChild(new LineCollider(game.width, 0, 0, 0));
        AddChild(new LineCollider(0, game.height - 100, game.width, game.height-100));

        AddChildAt(new Cannon(new Vec2(45, game.height-120)), 8);
    }

    public override void onLeave()
    {
        base.onLeave();
    }

    public override void recieveMessage(string message)
    {
    }
}
