//#define DEBUG_WINDOW

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DebugTool;
using JigLibX.Physics;
using JigLibX.Collision;
using DestructionXNA.Block;
using DestructionXNA.Camera;
using DestructionXNA.Utility;
using DestructionXNA.Tvchan;

namespace DestructionXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DestructionXNA : Microsoft.Xna.Framework.Game
    {
        private enum State {
            Pause,
            Play,
        }

        private State state;

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public Matrix Projection
        {
            get { return camera.Projection; }
        }
        public Matrix View
        {
            get { return camera.View; }
        }

        private BaseCamera camera;
        private BaseCamera[] staticCameras;
        private ThirdPersonCamera thirdPersonCamera;

        private DebugDrawer debugDrawer;
        public DebugDrawer DebugDrawer {
            get { return debugDrawer; }
        }

        private DebugWindow debugWindow;
        public DebugWindow DebugWindow
        {
            get { return debugWindow; }
        }

        private InputState inputState;
        public InputState InputState
        {
            get { return inputState; }
        }

        public int ScreenWidth
        {
            get { return 640; }
        }
        public int ScreenHeight
        {
            get { return 480; }
        }

        private Random random;
        public Random Random {
            get { return random; }
        }

        private PhysicsSystem physicSystem;

        private NicoNicoTVChan nicoTVChan;
        private Beam beam;

        private TVChanController controller;
        
        private Floor floor;
        private House house;

        public Model nicoTVchanModel;
        public Model floorModel;
        public Model wallBlockModel;
        public Model halfWallBlockModel;
        public Model roofBlockModel;
        public Model doorBlockModel;

        public Model beamModel;
        public Texture2D beamTexture;

        public DestructionXNA()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            IsFixedTimeStep = true;
            TargetElapsedTime = new TimeSpan(10000000L / 30L);

            IsMouseVisible = true;

            random = new Random();
            inputState = new InputState();

            CreatePhysicsSystem();

            debugDrawer = new DebugDrawer(this);
            Components.Add(debugDrawer);

            state = State.Pause;
        }

        private void CreateCamera()
        {
            Vector3[] cameraPositions = {
                new Vector3(0, 10, 30),
                new Vector3(30, 0, 0),
                new Vector3(0, 30, 0.01f),
                new Vector3(0, 100, 100),
            };

            this.staticCameras = new BaseCamera[cameraPositions.Length];
            for (int i = 0; i < staticCameras.Length; i++)
			{
                staticCameras[i] = new BaseCamera(this);
                staticCameras[i].Position = cameraPositions[i];
			}

            this.thirdPersonCamera = new ThirdPersonCamera(this);

            camera = thirdPersonCamera;
        }

        private void CreatePhysicsSystem()
        {
            physicSystem = new PhysicsSystem();
            physicSystem.CollisionSystem = new CollisionSystemSAP();

            physicSystem.EnableFreezing = true;
            physicSystem.SolverType = PhysicsSystem.Solver.Normal;
            physicSystem.CollisionSystem.UseSweepTests = true;

            physicSystem.NumCollisionIterations = 5;
            physicSystem.NumContactIterations = 5;
            physicSystem.NumPenetrationRelaxtionTimesteps = 15;
            physicSystem.Gravity = new Vector3(0, -9.8f, 0);
        }

        private float GetRandom(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            CreateCamera();

#if DEBUG && DEBUG_WINDOW
            debugWindow = new DebugWindow();
            debugWindow.Show();
#endif
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            this.nicoTVchanModel = Content.Load<Model>("niconicoTVChan");
            this.floorModel = Content.Load<Model>("floor");
            this.wallBlockModel = Content.Load<Model>("wall");
            this.halfWallBlockModel = Content.Load<Model>("halfWall");
            this.roofBlockModel = Content.Load<Model>("roof");
            this.doorBlockModel = Content.Load<Model>("door");
            this.beamModel = Content.Load<Model>("beam2");
            this.beamTexture = Content.Load<Texture2D>("comment_m9_2");

            this.floor = new Floor(this, floorModel);
            this.house = new House(this);

            this.nicoTVChan = new NicoNicoTVChan(this, nicoTVchanModel);

            this.beam = new Beam(this, beamTexture, beamModel);
            this.beam.DrawOrder = int.MaxValue;
            this.beam.CollisionSkin.NonCollidables.Add(floor.CollisionSkin);
            this.beam.CollisionSkin.NonCollidables.Add(nicoTVChan.CollisionSkin);

            this.controller = new TVChanController(this);
            this.controller.TVChan = nicoTVChan;
            this.controller.Beam = beam;

            this.thirdPersonCamera.TargetBody = nicoTVChan.Body;

            Reset();
        }

        private void Reset() {
            Destroy();

            Components.Add(camera);

            this.Components.Add(debugDrawer);

            this.nicoTVChan.MoveTo(new Vector3(-10, 1, 0), Matrix.Identity);
            this.Components.Add(nicoTVChan);

            this.beam.Disable();
            this.Components.Add(beam);

            this.Components.Add(controller);

            this.Components.Add(floor);

            this.house.Build(new Vector3(10, 0, 0));
            this.Components.Add(house);

            state = State.Pause;
        }

        private void Destroy()
        {
            this.house.Disable();
            this.Components.Clear();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            inputState.Update();

            // Allows the game to exit
            if (InputState.IsDown(Keys.Escape) ||
                GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
#if DEBUG && DEBUG_WINDOW
            {
                if (this.IsActive &&
                    inputState.NewMouseState.LeftButton == ButtonState.Pressed)
                {

                    float x = inputState.NewMouseState.X - 320;
                    float y = inputState.NewMouseState.Y - 240;
                    debugWindow.SetPositionText(x, y);
                }
            }
#endif
            BaseCamera oldCamera = camera;
            if (inputState.IsDown(Keys.D1)) camera = thirdPersonCamera;
            if (inputState.IsDown(Keys.D2)) camera = staticCameras[0];
            if (inputState.IsDown(Keys.D3)) camera = staticCameras[1];
            if (inputState.IsDown(Keys.D4)) camera = staticCameras[2];
            if (inputState.IsDown(Keys.D5)) camera = staticCameras[3];

            if (oldCamera != camera) {
                Components.Remove(oldCamera);
                Components.Add(camera);
            }

            debugDrawer.Enabled = InputState.IsDown(Keys.C);

            switch (state)
            {
                case State.Pause:
                    //if (inputState.IsTrigger(Keys.Enter)) {
                    //    state = State.Play;
                    //}
                    state = State.Play;
                    break;
                case State.Play:
                    if (inputState.IsTrigger(Keys.Back))
                    {
                        Reset();
                    }
                    physicSystem.Integrate((float)gameTime.ElapsedGameTime.TotalSeconds);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void DrawModel(Model model, Matrix world)
        {
            this.GraphicsDevice.RenderState.AlphaBlendEnable = true;
            this.GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
            this.GraphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha;

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = this.View;
                    effect.Projection = this.Projection;
                }
                mesh.Draw();
            }
        }
    }
}