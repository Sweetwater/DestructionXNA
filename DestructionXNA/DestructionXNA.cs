//#define DEBUG_WINDOW

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using DebugTool;
using JigLibX.Physics;
using JigLibX.Collision;
using DestructionXNA.Block;

namespace DestructionXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DestructionXNA : Microsoft.Xna.Framework.Game
    {
        enum State {
            Pause,
            Play,
        }

        State state;

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        Matrix projection;
        public Matrix Projection
        {
            get { return projection; }
        }

        private Matrix view;
        public Matrix View
        {
            get { return view; }
        }

        private Vector3 cameraPosition;


        private DebugDrawer debugDrawer;
        public DebugDrawer DebugDrawer {
            get { return debugDrawer; }
        }

        DebugWindow debugWindow;
        public DebugWindow DebugWindow
        {
            get { return debugWindow; }
        }

        InputState inputState;
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

        Random random;

        PhysicsSystem physicSystem;

        NicoNicoTVChan nicoTVChan;
        Floor floor;

        WallBlock wallBlock;
        HalfWallBlock halfWallBlock;
        RoofBlock roofBlock;


        Model nicoTVchanModel;
        Model floorModel;
        Model wallBlockModel;
        Model halfWallBlockModel;
        Model roofBlockModel;

        public DestructionXNA()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            IsMouseVisible = true;

            random = new Random();
            inputState = new InputState();

            CreatePhysicsSystem();

            cameraPosition = new Vector3(0, 10, 30);
            UpdateViewMatrix();

            debugDrawer = new DebugDrawer(this);
            Components.Add(debugDrawer);
            
            //CreateContext();
            state = State.Pause;
        }

        //private void CreateContext()
        //{
        //    var camera = new Camera(this);
        //    context = new Context(new Camera(this), new DebugDrawer(this, camera));
        //    Components.Add(context.Camera);
        //    //Components.Add(context.DebugDrawer);
        //}

        private void CreatePhysicsSystem()
        {
            physicSystem = new PhysicsSystem();
            physicSystem.CollisionSystem = new CollisionSystemSAP();

            physicSystem.EnableFreezing = true;
            physicSystem.SolverType = PhysicsSystem.Solver.Normal;
            physicSystem.CollisionSystem.UseSweepTests = true;

            physicSystem.NumCollisionIterations = 10;
            physicSystem.NumContactIterations = 10;
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

            float viewWidth = GraphicsDevice.Viewport.Width;
            float viewHeight = GraphicsDevice.Viewport.Height;
            float aspectRatio = viewWidth / viewHeight;
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45.0f),
                aspectRatio,
                0.005f,
                1000.0f);

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

            Reset();
        }

        private void Reset() {
            Destroy();

            this.Components.Add(debugDrawer);

            this.nicoTVChan = new NicoNicoTVChan(this, nicoTVchanModel);
            this.Components.Add(nicoTVChan);

            this.floor = new Floor(this, floorModel);
            this.Components.Add(floor);

            this.wallBlock = new WallBlock(this, wallBlockModel);
            this.wallBlock.Position = new Vector3(12.5f, 2.5f, 0);
            this.Components.Add(wallBlock);

            this.halfWallBlock = new HalfWallBlock(this, halfWallBlockModel);
            this.halfWallBlock.Position = new Vector3(-11.25f, 2.5f, 0);
            this.Components.Add(halfWallBlock);

            this.roofBlock = new RoofBlock(this, roofBlockModel);
            this.roofBlock.Position = new Vector3(0, 20.0f, 0);
            this.roofBlock.Orientation = Matrix.CreateRotationZ(MathHelper.ToRadians(90));
            this.Components.Add(roofBlock);

            state = State.Pause;
        }

        private void Destroy()
        {
            this.Components.Clear();
        }

        private void UpdateViewMatrix()
        {
            view = Matrix.CreateLookAt(
                cameraPosition,
                Vector3.Zero,
                Vector3.Up);
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
            Vector3 cameraMove = new Vector3(0.1f, 0.1f, 0.1f);

            if (inputState.IsDown(Keys.LeftShift) ||
                inputState.IsDown(Keys.RightShift))
            {
                if (inputState.IsDown(Keys.Left)) cameraPosition.X -= cameraMove.X;
                if (inputState.IsDown(Keys.Right)) cameraPosition.X += cameraMove.X;
                if (inputState.IsDown(Keys.Up)) cameraPosition.Y -= cameraMove.Y;
                if (inputState.IsDown(Keys.Down)) cameraPosition.Y += cameraMove.Y;
                if (inputState.IsDown(Keys.Z)) cameraPosition.Z -= cameraMove.Z;
                if (inputState.IsDown(Keys.X)) cameraPosition.Z += cameraMove.Z;

                if (inputState.IsDown(Keys.D1)) cameraPosition = new Vector3(0, 10, 30);
                if (inputState.IsDown(Keys.D2)) cameraPosition = new Vector3(30, 0, 0);
                if (inputState.IsDown(Keys.D3)) cameraPosition = new Vector3(0, 30, 0.01f);

                UpdateViewMatrix();
            }

            debugDrawer.Enabled = InputState.IsDown(Keys.C);


            switch (state)
            {
                case State.Pause:
                    if (inputState.IsTrigger(Keys.Enter)) {
                        state = State.Play;
                    }
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
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.World = world;
                    effect.View = View;
                    effect.Projection = Projection;
                }
                mesh.Draw();
            }
        }
    }
}


        //Context context;
        //PhysicsSystem physicSystem = new PhysicsSystem();

        //public class Context
        //{
        //    public Context(Camera camera, DebugDrawer debugDrawer)
        //    {
        //        this.Camera = camera;
        //        this.DebugDrawer = debugDrawer;
        //    }
        //    public readonly Camera Camera;
        //    public readonly DebugDrawer DebugDrawer;
        //}
        //readonly List<PhysicObject> items = new List<PhysicObject>();
        //List<Constraint> constraints = new List<Constraint>();
        //List<HingeJoint> joints = new List<HingeJoint>();
        //readonly List<Controller> controllers = new List<Controller>();
        //PhysicObject currentItem;


        //void Add(PhysicObject item)
        //{
        //    Components.Add(item);
        //    items.Add(item);
        //    var controller = new SampleController(item.PhysicsBody, new Vector3(0, 12, 0), Vector3.Zero);
        //    controller.EnableController();
        //    controllers.Add(controller);
        //}

        //void Add(PhysicObject item, Vector3 hingePosition)
        //{
        //    Components.Add(item);
        //    items.Add(item);

        //    var previousItem = currentItem;
        //    currentItem = item;
        //    if (previousItem != null)
        //    {
        //        // ジョイントを作成する
        //        var joint = new HingeJoint();
        //        joint.Initialise(
        //            previousItem.PhysicsBody, currentItem.PhysicsBody,
        //            new Vector3(0, 0, 1),
        //            hingePosition, 1.0f, 180, -180, 0.1f, 0.5f);
        //        joint.EnableHinge();
        //        joints.Add(joint);
        //    }
        //}

        //void InitializeScene() {
        //    // カメラを設定する
        //    context.Camera.Position = new Vector3(0, 10, 10);
        //    context.Camera.Target = new Vector3(0, 0, 0);

        //    // 古いボディとそのコンポーネントを削除する
        //    foreach (var item in items) {
        //        Components.Remove(item);
        //        item.PhysicsBody.DisableBody();
        //        item.Dispose();
        //    }
        //    items.Clear();


            //foreach (var joint in joints)
            //{
            //    joint.DisableController();
            //}
            //joints.Clear();

            //currentItem = null;

            //foreach (var controller in controllers)
            //{
            //    physicSystem.RemoveController(controller);
            //}
            //controllers.Clear();


            //// 重力
            //physicSystem.Gravity = new Vector3(0, -9.8f, 0);

            //// 固定の床を作る
            //var floor = new BoxObject(this, context, Content.Load<Model>("Crate"),
            //    new Vector3(10, 1, 10), Matrix.Identity, Vector3.Zero);
            //floor.PhysicsBody.Immovable = true;
            //Add(floor);
            ////Add(floor, Vector3.Zero);

            //AddCrate(new Vector3(3, 0, 0), new Vector3(0.0f, 0, 0));
            //AddCrate(new Vector3(6, 0, 0), new Vector3(1.5f, 0, 0));
            //AddCrate(new Vector3(9, 0, 0), new Vector3(1.5f, 0, 0));
            //AddCrate(new Vector3(12, 0, 0), new Vector3(1.5f, 0, 0));
            //AddCrate(new Vector3(15, 0, 0), new Vector3(1.5f, 0, 0));
        //}
		/// <summary>
		/// 木箱を追加する。
		/// </summary>
        //void AddCrate()
        //{
        //    var crate = new BoxObject(this, context, Content.Load<Model>("Crate"),
        //        new Vector3(1, 1, 1),
        //        Matrix.CreateRotationX((float)random.NextDouble()) *
        //        Matrix.CreateRotationY((float)random.NextDouble()), new Vector3(0, 5, 0));
        //    Add(crate);
        //}
        //void AddCrate(Vector3 position, Vector3 hingePosition)
        //{
        //    var crate = new BoxObject(this, context, Content.Load<Model>("Crate"),
        //        new Vector3(2, 0.25f, 2), Matrix.Identity, position);
        //    Add(crate, hingePosition);
        //}

    //#region 入力関連
    //    KeyboardState previous;
    //    KeyboardState current;
    //    bool IsKeyPress(Keys key) {
    //        return current.IsKeyDown(key) && previous.IsKeyUp(key);
    //    }
    //    public void HandleInput() {
    //        previous = current;
    //        current = Keyboard.GetState();
    //        if (IsKeyPress(Keys.Space)) AddCrate();
    //        if (IsKeyPress(Keys.X))
    //        {
    //            foreach (var controller in controllers)
    //                controller.EnableController();
    //        }
    //        if (IsKeyPress(Keys.Z))
    //        {
    //            foreach (var controller in controllers)
    //                controller.DisableController();
    //        }
    //        if (IsKeyPress(Keys.Delete)) InitializeScene();
    //    }
    //#endregion

    //}

    //class SampleController : Controller
    //{
    //    Body body;
    //    Vector3 force;
    //    Vector3 torque;

    //    public SampleController(Body body, Vector3 force, Vector3 torque)
    //    {
    //      7  this.body = body;
    //        this.force = force;
    //        this.torque = torque;
    //    }
    //    public override void UpdateController(float dt)
    //    {
    //        if (body == null) return;
    //        if (force != Vector3.Zero || torque != Vector3.Zero)
    //        {
    //            body.AddWorldForce(force);
    //            body.AddWorldTorque(torque);
    //            if (!body.IsActive) body.SetActive();
    //        }
    //    }
    //}
