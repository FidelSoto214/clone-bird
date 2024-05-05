using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CloneBird;

public class Bird
{
	
	public bool Dead {get; set;}
	
	private Texture2D texture;
	private Rectangle collider;
	private int width; 
	private int height;
	private int gravity;
	private int flapSrenght;
	private double coolDownSeconds;
	private double coolDownSecondsMax;
	private double deltaTime;
	private int deltaTimei;
	private bool started;
	
	private int yVelocity;
	private int Score;
	
	private Vector2 debugTestLocation;

		
	public Bird(GraphicsDeviceManager graphicsDeviceManager,GraphicsDevice graphicsDevice)
	{
		width = 40;
		height = 40;
		collider = new Rectangle (graphicsDeviceManager.PreferredBackBufferWidth / 4,
									graphicsDeviceManager.PreferredBackBufferHeight / 2 - height,
									width,
									height);
		texture = new Texture2D(graphicsDevice,1,1);
		texture.SetData<Color>([Color.White]);
		gravity = 2;
		flapSrenght = 25;
		coolDownSecondsMax = .2;
		coolDownSeconds = coolDownSecondsMax;
		yVelocity = 0;
		Score = 0;
		
		debugTestLocation = new Vector2(20,10);
		
		Dead = false;
		
	}
	
	public void Update(KeyboardState keyboardState, GameTime gameTime, GraphicsDeviceManager graphicsDeviceManager)
	{
		
		deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
		deltaTimei = (int)Math.Ceiling(deltaTime);
		
		
		if (coolDownSeconds > 0)
		{
			coolDownSeconds -= deltaTime;
		}
		
		if (!Dead)
		{
			if (keyboardState.IsKeyDown(Keys.Space) && coolDownSeconds <= 0)
			{
				// collider.Y -= flapSrenght;
				yVelocity = -flapSrenght;
				// yVelocity -= flapSrenght;
				coolDownSeconds = coolDownSecondsMax;
				started = true;
			}
			
			if (keyboardState.IsKeyDown(Keys.Right))
			{
				flapSrenght++;
			}
			else if (keyboardState.IsKeyDown(Keys.Left))
			{
				flapSrenght--;
			}
			if (keyboardState.IsKeyDown(Keys.Up))
			{
				gravity++;
			}
			else if (keyboardState.IsKeyDown(Keys.Down))
			{
				gravity--;
			}
		}
		
		if (!started) {return;}
		
		yVelocity += gravity * deltaTimei;
		
		collider.Y += yVelocity * deltaTimei;
		
		if (!Dead && 
			(collider.Intersects(Game1.obstacles[0].bottomPipe.bodyCollider) ||
			collider.Intersects(Game1.obstacles[0].bottomPipe.topCollider) ||
			collider.Intersects(Game1.obstacles[0].topPipe.bodyCollider) ||
			collider.Intersects(Game1.obstacles[0].topPipe.topCollider))
			)
			{
				Dead = true;
			}
		
		if (!Dead && collider.Intersects(Game1.obstacles[0].gap) && !Game1.obstacles[0].gapCollided && !Dead)
		{
			Game1.obstacles[0].gapCollided = true;
			Score++;
		}
		
		if (collider.Bottom > graphicsDeviceManager.PreferredBackBufferHeight)
		{
			collider.Y = graphicsDeviceManager.PreferredBackBufferHeight - collider.Height;
			yVelocity = 0;
		}
		
		if (collider.Top < 0)
		{
			collider.Y = 0;
			yVelocity = 0;
		}
		
		
	}
		
	public void Draw(SpriteBatch spriteBatch, SpriteFont font)
	{
		spriteBatch.Draw(Game1.birdTexture,collider,Color.White);
		spriteBatch.DrawString(font,$"flap:{flapSrenght}\ngravity:{gravity}\nScore:{Score}\nDead:{Dead}",debugTestLocation,Color.Black);
	}
	
}