using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game //PLACE STUFF WITH game.width/*** ; game/heigh/*** to make it scale with the screen size
{
    public MyGame() : base(1920, 1080, false)
	{
		new UnitTests();
		_sceneManager.addscene(new Menu());
		_sceneManager.addscene(new PlayableLevel());
		_sceneManager.addscene(new Menu());
    }

    void Update()
	{
		Input.PrintMouseCoordinates();
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}
