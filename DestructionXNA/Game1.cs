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

        Drawer drawer;

        PhysicsSimulator world = new PhysicsSimulator();

        Body playerBody = new Body();
        Geom playerGeom;

        Body body = new Body();
        Geom geom;
        Body groundBody = new Body();
        Geom groundGeom;

        readonly List<Geom> geoms = new List<Geom>();

        KeyboardState oldKeyState;
        KeyboardState newKeyState;

        float forceAmount = 1;
        DebugWindow debugWindow;

        Geom AddObject(Vector2 position, float rotation, float mass, Vertices vertices)
        {
            var body = new Body();
            world.Add(body);
            body.Position = position;
            body.Rotation = rotation;
            vertices.SubDivideEdges(0.5f); // 辺を細分割する。
            var geom = new Geom(body, vertices, 1);
            body.Mass = mass;
            body.MomentOfInertia = mass * vertices.GetMomentOfInertia();
            world.Add(geom);
            geoms.Add(geom);
            return geom;
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 640;
            graphics.PreferredBackBufferHeight = 480;

            graphics.PreferMultiSampling = false;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 240);

            world.Gravity = new Vector2(0, 9.8f);

            // 箱
            {
                //world.Add(body);
                //body.Rotation = 0.1f;
                //var verticesVector = new Vector2[]{
                //    new Vector2(-1, -1),
                //    new Vector2(+1, -1),
                //    new Vector2(+1, +1),
                //    new Vector2(-1, +1),
                //};
                //var vertices = new Vertices(verticesVector);
                //vertices.SubDivideEdges(0.5f);
                //geom = new Geom(body, vertices, 1);
                //world.Add(geom);
            }

            // 地面
            {
                world.Add(groundBody);
                groundBody.IsStatic = true;

                var verticesVector = new Vector2[]{
                    new Vector2(-10, +9),
                    new Vector2(+10, +9),
                    new Vector2(+10, +10),
                    new Vector2(-10, +10),
                };
                var vertices = new Vertices(verticesVector);
                vertices.SubDivideEdges(0.125f);
                groundGeom = new Geom(groundBody, vertices, 1);
                groundGeom.FrictionCoefficient = 0.05f;
                world.Add(groundGeom);
            }

            //// 4角形の物体
            //var geom1 = AddObject(new Vector2(0, 0), 0.1f, 10, new Vertices(new Vector2[] {
            //    new Vector2(-1, -1),
            //    new Vector2(+1, -1),
            //    new Vector2(+1, +1),
            //    new Vector2(-1, +1),
            //}));

            //// 5角形の物体
            //var geom2 = AddObject(new Vector2(2, 0), 0.2f, 0.1f, new Vertices(new Vector2[] {
            //    new Vector2(-2, -2),
            //    new Vector2(+1, -1),
            //    new Vector2(+1, +0),
            //    new Vector2(+0, +1),
            //    new Vector2(-1, +1),
            //}));

            //// 4角形をワールドに回転関節でつなぐ。
            //world.Add(new FixedRevoluteJoint(geom1.Body, new Vector2(0.5f, 0)));
            //// 5角形をワールドにバネでつなぐ。
            //world.Add(new FixedLinearSpring(geom2.Body, new Vector2(0, 0), new Vector2(0, 0), 3.0f, 0.2f));

            CreatePlayer();
        }

        private void CreatePlayer() {
            world.Add(playerBody);
//            playerBody.Rotation = 0.1f;
            var verticesVector = new Vector2[]{
                    new Vector2(-1, -2),
                    new Vector2(+1, -2),
                    new Vector2(+1, +2),
                    new Vector2(-1, +2),
                };
            var vertices = new Vertices(verticesVector);
            vertices.SubDivideEdges(0.5f);
            playerGeom = new Geom(playerBody, vertices, 1);
            playerBody.Position = new Vector2(0, 8);
            world.Add(playerGeom);

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
            drawer = new Drawer(GraphicsDevice);

            state = State.Start;

            debugWindow = new DebugWindow();
            debugWindow.Value = playerBody.Position.Y;
            debugWindow.ApplyValue += delegate(float value)
            {
                Vector2 position = new Vector2(playerBody.Position.X, value);
                playerBody.Position = position;
            };
            debugWindow.Show();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            newKeyState = Keyboard.GetState();

            // TODO: Add your update logic here
            switch (state)
            {
                case State.Start:
                    if (newKeyState.IsKeyDown(Keys.Enter)) {
                        state = State.Play;
                    }
                    break;
                case State.Play:
                    UpdatePlay(gameTime);
                    break;
                default:
                    break;
            }


            oldKeyState = newKeyState;
            base.Update(gameTime);
        }

        private void UpdatePlay(GameTime gameTime)
        {
            const float moveAmount = 0.01f;
            Vector2 move = Vector2.Zero;
            Vector2 force = Vector2.Zero;

            if (newKeyState.IsKeyDown(Keys.Left))
            {
                move += new Vector2(-moveAmount, 0);
            }
            if (newKeyState.IsKeyDown(Keys.Right))
            {
                move += new Vector2(moveAmount, 0);
            }

            if (IsKeyRelease(Keys.Left))
            {
                force += new Vector2(-forceAmount, 0);
            }
            if (IsKeyRelease(Keys.Right))
            {
                force += new Vector2(forceAmount, 0);
            }

            playerBody.Position += move;
            playerBody.ApplyForce(force);
            world.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        private bool IsKeyRelease(Keys key) {
            return oldKeyState.IsKeyDown(key) && !newKeyState.IsKeyDown(key);
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
            var aspectRatio = GraphicsDevice.Viewport.AspectRatio;
            drawer.Begin(Matrix.CreateOrthographicOffCenter(-10 * aspectRatio, 10 * aspectRatio, 10, -10, -10, 10));
            foreach (var geom in geoms) {
                Draw(geom);
            }
            //Draw(this.geom);
            Draw(playerGeom);
            Draw(groundGeom);
            drawer.End();

            base.Draw(gameTime);
        }
    }
}
