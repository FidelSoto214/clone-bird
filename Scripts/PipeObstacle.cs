using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace CloneBird;

public class PipeObstacle
{
	static readonly Random gapSpawnRandomizer = new(Guid.NewGuid().GetHashCode());
	public Pipe topPipe;
	public Pipe bottomPipe;
	public Rectangle gap;
	public bool gapCollided;
	int gapSpawnY;
	int gapSpawnHeight;
	private int obstacleWidth = 100;
	Texture2D gapTexture;
	
	Rectangle obstacleContainer;
	public bool KillMe {get;private set;} = false;
	
	public PipeObstacle(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice, int xSpawnLocation)
	{
		gapSpawnHeight = gapSpawnRandomizer.Next((int)(graphicsDeviceManager.PreferredBackBufferHeight * .20),
													(int)(graphicsDeviceManager.PreferredBackBufferHeight * .40));
													
		gapSpawnY = gapSpawnRandomizer.Next((int)(graphicsDeviceManager.PreferredBackBufferHeight * .10),
											(int)(graphicsDeviceManager.PreferredBackBufferHeight * .90) - gapSpawnHeight);
							
		Debug.WriteLine("[Constructor][PipeObstacle]: gapSpawnY is " + gapSpawnY);

		gap = new Rectangle(xSpawnLocation
							,gapSpawnY
							,obstacleWidth
							,gapSpawnHeight);
		gapCollided = false;
							
		obstacleContainer = new Rectangle(gap.X,
											0, 
											obstacleWidth,
											graphicsDeviceManager.PreferredBackBufferHeight);
											
		bottomPipe = new Pipe(gap.X,
								gap.Bottom,
								obstacleContainer.Width, 
								graphicsDeviceManager.PreferredBackBufferHeight - gap.Bottom,
								true,
								graphicsDevice);
								
		topPipe = new Pipe(gap.X,
							0,
							obstacleContainer.Width,
							gap.Top,
							false,
							graphicsDevice);
							
		gapTexture = new Texture2D(graphicsDevice,1,1);
		gapTexture.SetData<Color>([Color.White]);
	}
	
	public void Update(GameTime gameTime)
	{
		
		if (obstacleContainer.X + obstacleContainer.Width + 20 < 0)
		{
			KillMe = true;
			return;
		}
		
		obstacleContainer.X -= 3 * (int)Math.Ceiling(gameTime.ElapsedGameTime.TotalSeconds);
		gap.X = obstacleContainer.X; 
		bottomPipe.Update(obstacleContainer.X);
		topPipe.Update(obstacleContainer.X);
	}
	
	public void Draw(SpriteBatch spriteBatch)
	{
		bottomPipe.Draw(spriteBatch);
		topPipe.Draw(spriteBatch);
		// spriteBatch.Draw(gapTexture,gap,Color.Goldenrod);
	}
}