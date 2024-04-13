using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CloneBird;

public class Pipe
{
	private Rectangle bounds;
	Rectangle topCollider; // top of pipe.
	Rectangle bodyCollider; // body of pipe
	
	Texture2D texture;
	
	// public Pipe(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
	public Pipe(int x, int y, int width, int height, bool isBottom, GraphicsDevice graphicsDevice)
	{
		// bounds = new Rectangle(graphicsDeviceManager.PreferredBackBufferWidth - 200,
								// 200, width, height);
		if (isBottom)
		{
			bounds = new Rectangle(x,y,width,height);
			topCollider = new Rectangle(bounds.X,
										bounds.Y,
										bounds.Width,
										// bounds.Height / 5);
										20);
										
			bodyCollider = new Rectangle(bounds.X + (topCollider.Width / 4),
										bounds.Y + topCollider.Height,
										bounds.Width / 2,
										bounds.Height - topCollider.Height);
		}
		else
		{
			bounds = new Rectangle(x,y,width,height);
			topCollider = new Rectangle(bounds.X, 
										bounds.Bottom - 20,
										bounds.Width,
										// bounds.Height / 5);
										20);
										
			bodyCollider = new Rectangle(bounds.X + (topCollider.Width / 4), 
										bounds.Y,
										bounds.Width / 2,
										bounds.Height - topCollider.Height);
		}

		texture = new Texture2D(graphicsDevice,1,1);
		texture.SetData<Color>([Color.White]);
			
	}
	
	public void Update(int x)
	{
		bounds.X = x;
		topCollider.X = bounds.X;
		bodyCollider.X = bounds.X + (topCollider.Width / 4);
	}
		
	public void Draw(SpriteBatch spriteBatch)
	{
		spriteBatch.Draw(texture,topCollider,Color.White);
		spriteBatch.Draw(texture,bodyCollider,Color.Red);
	}
	
	
}
