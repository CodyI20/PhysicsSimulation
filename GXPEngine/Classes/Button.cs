using GXPEngine;

public class ButtonAssemblyw : GameObject
{
    public ButtonAssemblyw(int pX, int pY, int amount = 3, float pScale = 1) : base(false)
    {
        x = pX;
        y = pY;

        scale = pScale;

        if (amount > 1) AddChild(new LvSwtchButton(75, 100, new Menu(), "Menu.png", 1,0.27f)); 
        if (amount > 2) AddChild(new ResetButton(215, 100));
    }
}

class LvSwtchButton : Button
{
    private string nextLevel;

    public string level
    {
        set { nextLevel = value; }
    }

    public LvSwtchButton(float inpX, float inpY, Scene nextLevelInp, string path, int cols = 1, float scale = 1f) : base(inpX, inpY, path, cols)
    {
        this.scale = scale;
        nextLevel = nextLevelInp.name;
    }

    public LvSwtchButton(float inpX, float inpY, string nextLevelInp, string path) : base(inpX, inpY, path)
    {
        nextLevel = nextLevelInp;
    }

    protected override void click()
    {
        base.click();
        if (nextLevel != null)
        {
            game.SceneManager.SetScene(nextLevel);
        }
        else
        {
            base.click();
        }
        //new Sound(Settings.ASSET_PATH + "SFX/Button.wav").Play(false, 0, Settings.sfxVolume, 0);
    }
}

public class ExitButton : Button
{
    public ExitButton(float inpX, float inpY) : base(inpX, inpY, "exit.png")
    {
        scale = 0.6f;
    }

    protected override void click()
    {
        base.click();
        game.LateDestroy();
    }
}

public class ResetButton : Button
{
    public ResetButton(float inpX, float inpY) : base(inpX, inpY, "Reset.png")
    {
        scale = 0.27f;
    }

    protected override void click()
    {
        game.SceneManager.Reloadscene();
        //new Sound(Settings.ASSET_PATH + "SFX/LevelRetry.wav").Play(false, 0, Settings.sfxVolume, 0);
    }
}

public class Button : AnimationSprite
{
    public Button(float inpX, float inpY, string path = "button.png", int cols = 1) : base(Settings.ASSET_PATH + "Art/" + path, cols, 1, cols, false)
    {
        x = inpX;
        y = inpY;
        SetOrigin(width / 2, height / 2);
    }

    protected virtual void click()
    {
        int num = Utils.Random(-1, 7);
        //new Sound(Settings.ASSET_PATH + "SFX/Button" + num + ".wav").Play(false, 0, Settings.sfxVolume, 0);
    }

    public virtual void Update()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            SetFrame(1);

            if (Input.GetMouseButtonDown(0))
            {
                click();
            }
        }
        else
        {
            SetFrame(0);
            //overlay.y = -3;
            //overlay.x = 1;
        }
    }
}