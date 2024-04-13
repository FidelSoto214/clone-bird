using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CloneBird;

public class Bird
{
	private Texture2D texture;
	private Rectangle collider;
	private int width; 
	private int height;
	private int yAcceleration;
	private int gravity;
	private int jumpForce;
	private double coolDownSeconds;
	private double coolDownSecondsMax;
	private double deltaTime;
	private int deltaTimei;
	private bool started;
	
		
	public Bird(GraphicsDeviceManager graphicsDeviceManager,GraphicsDevice graphicsDevice)
	{
		width = 50;
		height = 50;
		collider = new Rectangle (graphicsDeviceManager.PreferredBackBufferWidth / 4,
									graphicsDeviceManager.PreferredBackBufferHeight / 2 - height,
									width,
									height);
		texture = new Texture2D(graphicsDevice,1,1);
		texture.SetData<Color>([Color.White]);
		gravity = 2;
		yAcceleration = gravity;
		jumpForce = 50;
		coolDownSecondsMax = .3 ;
		coolDownSeconds = coolDownSecondsMax;
		
	}
	
	public void Update(KeyboardState keyboardState, GameTime gameTime, GraphicsDeviceManager graphicsDeviceManager)
	{
		
		deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
		deltaTimei = (int)Math.Ceiling(deltaTime);
		
		
		if (coolDownSeconds > 0)
		{
			coolDownSeconds -= deltaTime;
		}
		
		if (keyboardState.IsKeyDown(Keys.Space) && coolDownSeconds <= 0)
		{
			yAcceleration -= jumpForce;
			coolDownSeconds = coolDownSecondsMax;
			started = true;
		}
		
		if (!started) {return;}
		
		yAcceleration += gravity;
		collider.Y += yAcceleration * deltaTimei * deltaTimei;
		
		// TODO: player should DIE, not just halt.
		if (collider.Bottom > graphicsDeviceManager.PreferredBackBufferHeight)
		{
			collider.Y = graphicsDeviceManager.PreferredBackBufferHeight - collider.Height;
			yAcceleration = 0;
		}
		
		if (collider.Top < 0)
		{
			collider.Y = 0;
			yAcceleration = 2;
		}
		
		
	}	
		
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(texture,collider,Color.White);	
	}
	
}