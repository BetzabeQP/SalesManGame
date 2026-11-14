using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Quad
{
    VertexPositionTexture[] vertices;
    short[] indices;
    BasicEffect effect;
    Game game;
    public Quad(Game game)
    {
        this.game = game;
        InitializeVertices();
        InitializeIndices();
        InitializeEffect();
    }
    public void InitializeVertices() {
        vertices = new VertexPositionTexture[4];
        // (top left)
        vertices[0].Position = new Vector3(-1, 1, 0);
        vertices[0].TextureCoordinate = new Vector2(0, -1);
        // (top right)
        vertices[1].Position = new Vector3(1, 1, 0);
        vertices[1].TextureCoordinate = new Vector2(1, -1);
        // (bottom right)
        vertices[2].Position = new Vector3(1, -1, 0);
        vertices[2].TextureCoordinate = new Vector2(1, 0);
        // (bottom left) 
        vertices[3].Position = new Vector3(-1, -1, 0);
        vertices[3].TextureCoordinate = new Vector2(0, 0);
    }
    /// <summary>
    /// Initialize the indices of our quad
    /// </summary>
    public void InitializeIndices() {
        indices = new short[6];

        // Define triangle 0 
        indices[0] = 0;
        indices[1] = 1;
        indices[2] = 2;
        // define triangle 1
        indices[3] = 2;
        indices[4] = 3;
        indices[5] = 0;
    }
    /// <summary>
    /// Initializes the basic effect used to draw the quad
    /// </summary>
    public void InitializeEffect()
    {
        effect = new BasicEffect(game.GraphicsDevice);
        effect.World = Matrix.Identity;
        effect.View = Matrix.CreateLookAt(
            new Vector3(0, 0, 4), // The camera position
            new Vector3(0, 0, 0), // The camera target,
            Vector3.Up            // The camera up vector
        );
        effect.Projection = Matrix.CreatePerspectiveFieldOfView(
            MathHelper.PiOver4,                         // The field-of-view 
            game.GraphicsDevice.Viewport.AspectRatio,   // The aspect ratio
            0.1f, // The near plane distance 
            100.0f // The far plane distance
        );
        effect.TextureEnabled = true;
        effect.Texture = game.Content.Load<Texture2D>("CardSprite");
    }
    public void Draw()
    {
         
        BlendState oldBlendState = game.GraphicsDevice.BlendState;
        game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
        effect.CurrentTechnique.Passes[0].Apply();
        game.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
            PrimitiveType.TriangleList,
            vertices,   // The vertex collection
            0,          // The starting index in the vertex array
            4,          // The number of indices in the shape
            indices,    // The index collection
            0,          // The starting index in the index array
            2           // The number of triangles to draw
        );
        game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
    }
}