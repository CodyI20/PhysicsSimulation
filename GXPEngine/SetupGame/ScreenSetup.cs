using GXPEngine;

public class Menu : Screen
{
    public Menu() : base("Menu")
    {
        //
    }

    public override void onLoad()
    {
        base.onLoad();

        AddChild(new ExitButton(((MyGame)game).width / 2, ((MyGame)game).height / 2 + 100));

        AddChild(new LvSwtchButton(((MyGame)game).width/2, ((MyGame)game).height/2 - 200, new PlayableLevel(), Settings.ASSET_PATH + "Art/play.png"));
    }
}


public class Screen : Scene
{
    protected Canvas overlay;

    public Screen(string name) : base(name)
    {
    }

    public override void onLoad()
    {
        base.onLoad();

        overlay = new Canvas(width, height, false);
        AddChild(overlay);
    }
}
