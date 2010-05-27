#define DEBUG_WINDOW

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using FarseerGames.FarseerPhysics;
using FarseerGames.FarseerPhysics.Dynamics;
using FarseerGames.FarseerPhysics.Collisions;
using FarseerPhysicsTutorial1;
using FarseerGames.FarseerPhysics.Dynamics.Joints;
using FarseerGames.FarseerPhysics.Dynamics.Springs;
using DebugTool;
using FarseerGames.FarseerPhysics.Factories;

namespace DestructionXNA
{

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum State {
            Start,
            Play,
        }
        State state;

        GraphicsDeviceManager graphics;

        SpriteBatch spriteBatch;
        public SpriteBatch SpriteBatch {
            get { return spriteBatch; }
        }

        PhysicsSimulator physicsSimulator;
        public PhysicsSimulator PhysicsSimulator{
            get { return physicsSimulator; }
        }

        Matrix projection;
        public Matrix Projection {
            get { return projection; }
        }

        DebugWindow debugWindow;
        public DebugWindow DebugWindow {
            get { return debugWindow; }
        }

        InputState inputState;
        public InputState InputState
        {
            get { return inputState; }
        }


        Drawer drawer;
        public Drawer Drawer {
            get { return drawer; }
        }

        public int ScreenWidth {
            get { return 640; }
        }
        public int ScreenHeight
        {
            get { return 480; }
        }

        Random random;

        Player player;
        Controller controller;

        Body groundBody;
        Geom groundGeom;

        Body[] blockBodys;
        Geom[] blockGeoms;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;

            graphics.PreferMultiSampling = false;

            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 10);
            IsFixedTimeStep = true;
            IsMouseVisible = true;

            random = new Random();
            inputState = new InputState();
        }


        private int rectNum = 5;
        private int circleNum = 5;
        private int triNum = 5;

        private float fluctuation = 3;

        private float rectWidth = 20;
        private float rectHeight = 20;
        private float rectMass = 1;

        private float circleRadius = 10;
        private int circleEdge = 10;

        private float blockY = 50;

        private void CreateBlock()
        {
            //blockBodys = new Body[rectNum + circleNum + triNum];
            //blockGeoms = new Geom[rectNum + circleNum + triNum];
            blockBodys = new Body[rectNum];
            blockGeoms = new Geom[rectNum];

            CreateRectBlock();
            //CreateCircleBlock();
        }

        private void CreateRectBlock()
        {
            for (int i = 0; i < rectNum; i++)
            {
                float width = rectWidth * GetRandom(1, fluctuation);
                float height = rectHeight * GetRandom(1, fluctuation);
                float mass = rectMass * GetRandom(1, fluctuation);
                Vector2 position = new Vector2(GetRandom(-ScreenWidth / 2, ScreenWidth / 2), blockY);

                blockBodys[i] = BodyFactory.Instance.CreateRectangleBody(
                    physicsSimulator,
                    width, height, mass);
                blockGeoms[i] = GeomFactory.Instance.CreateRectangleGeom(
                    physicsSimulator,
                    blockBodys[i], width, height);

                blockGeoms[i].FrictionCoefficient = 0.8f;
                blockBodys[i].Position = position;
            }

        }

        private void CreateCircleBlock()
        {
            for (int i = 0; i < circleNum; i++)
            {
                float radius = circleRadius * GetRandom(1, fluctuation);
                float mass = rectMass * GetRandom(1, fluctuation);
                Vector2 position = new Vector2(GetRandom(-ScreenWidth / 2, ScreenWidth / 2), blockY);

                blockBodys[i] = BodyFactory.Instance.CreateCircleBody(
                    physicsSimulator,
                    radius, mass);
                blockGeoms[i] = GeomFactory.Instance.CreateCircleGeom(
                    physicsSimulator,
                    blockBodys[i], radius, circleEdge);

                blockBodys[i].Position = position;
            }
        }


        private float GetRandom(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        private void CreateGround()
        {
            groundBody = BodyFactory.Instance.CreateRectangleBody(physicsSimulator, 600, 20, 1);
            groundBody.IsStatic = true;

            groundGeom = GeomFactory.Instance.CreateRectangleGeom(physicsSimulator, groundBody, 600, 20);
            groundGeom.RestitutionCoefficient = 0.2f;
            groundGeom.FrictionCoefficient = 0.8f;

            groundBody.Position = new Vector2(0, 230);
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
            float viewWidth = GraphicsDevice.Viewport.Width;
            float viewHeight = GraphicsDevice.Viewport.Height;
            projection = Matrix.CreateOrthographicOffCenter(-viewWidth / 2, viewWidth / 2, viewHeight / 2, -viewHeight / 2, -10, 10);

            drawer = new Drawer(GraphicsDevice);

            physicsSimulator = new PhysicsSimulator(new Vector2(0, 100));
            CreateGround();
            CreateBlock();


            player = new Player(
                this,
                Content.Load<Texture2D>("niconicoTVchan64"),
                Content.Load<Model>("anttenaChip"));

            controller = new Controller(this, player, blockBodys);

            state = State.Start;
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

#if DEBUG && DEBUG_WINDOW            
            if (this.IsActive &&
                inputState.NewMouseState.LeftButton == ButtonState.Pressed) {

                float x = inputState.NewMouseState.X - 320;
                float y = inputState.NewMouseState.Y - 240;
                debugWindow.SetPositionText(x, y);
            }
#endif

            // TODO: Add your update logic here
            switch (state)
            {
                case State.Start:
                    if (inputState.IsTrigger(Keys.Enter)) {
                        state = State.Play;
                    }
                    break;
                case State.Play:
                    UpdatePlay(gameTime);
                    break;
                default:
                    break;
            }

            base.Update(gameTime);
        }

        private void UpdatePlay(GameTime gameTime)
        {
            controller.Update();
            player.Update(gameTime);
            physicsSimulator.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        void Draw(Geom geom)
        {
            drawer.DrawClosedPolyline(geom.WorldVertices.GetVerticesArray());
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            drawer.Begin(projection);
            Draw(groundGeom);

            foreach (Geom blockGeom in blockGeoms)
            {
                Draw(blockGeom);
            }
            drawer.End();

            player.Draw();

            base.Draw(gameTime);
        }
    }
}
