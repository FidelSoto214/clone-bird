using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CloneBird;

public class Game1 : Game
{
	GraphicsDeviceManager _graphics;
	SpriteBatch _spriteBatch;
	Bird bird;
	List<PipeObstacle> obstacles;
	double pipeSpawnCoolDownInitValue;
	double pipeSpawnCoolDown;
	
	
		
	public Game1()
	{
		_graphics = new GraphicsDeviceManager(this);
		Content.RootDirectory = "Content";
		IsMouseVisible = true;
	}

	protected override void Initialize()
	{
		// TODO: Add your initialization logic here
		bird = new Bird(_graphics,GraphicsDevice);
		obstacles = new List<PipeObstacle>()
		{
			new(_graphics,GraphicsDevice, _graphics.PreferredBackBufferWidth - 20)
		};
		pipeSpawnCoolDownInitValue = 1.5;
		pipeSpawnCoolDown = pipeSpawnCoolDownInitValue;
		base.Initialize();
	}
	

	protected override void LoadContent()
	{
		_spriteBatch = new SpriteBatch(GraphicsDevice);

		// TODO: use this.Content to load your game content here
	}

	protected override void Update(GameTime gameTime)
	{
		if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			Exit();

		// TODO: Add your update logic here
		pipeSpawnCoolDown -= gameTime.ElapsedGameTime.TotalSeconds;
		if (pipeSpawnCoolDown < 0)
		{
			pipeSpawnCoolDown = pipeSpawnCoolDownInitValue;
			obstacles.Add(new(_graphics,GraphicsDevice, _graphics.PreferredBackBufferWidth));
		}
		for(int i = obstacles.Count - 1; i >= 0; i--)
		{
			obstacles[i].Update(gameTime);
			if (obstacles[i].KillMe)
			{
				obstacles.RemoveAt(i);
				Debug.WriteLine("[obstacles]: Removed obstacle.");
			}
		}
		bird.Update(Keyboard.GetState(),gameTime,_graphics);
		
		base.Update(gameTime);
	}

	protected override void Draw(GameTime gameTime)
	{
		GraphicsDevice.Clear(Color.CornflowerBlue);
		_spriteBatch.Begin();
		for (int i = 0; i < obstacles.Count; i++)
		{
			obstacles[i].Draw(_spriteBatch);
		}
		bird.Draw(_spriteBatch);
		_spriteBatch.End();
		base.Draw(gameTime);
	}
}
