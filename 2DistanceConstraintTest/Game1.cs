using GeoSketch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Procedural_animation_test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private int _scrollValue = 10;
        private int _baseScrollValue = 10;
        private int _mouseX;
        private int _mouseY;
        private Vector2 _point; //current position of the chain segment that we want to adjust
        private Vector2 _point2; //current position of the chain segment that we want to adjust
        private Vector2 _anchor; //the previous chain segment position, the segment that the point is attached to
        private float _distance; //the distance between the point and the anchor
        private int _amountOfPoints = 20;
        private List<Vector2> _points;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _point = Vector2.Zero;
            _point2 = Vector2.Zero;

            _points = new List<Vector2>();
            for (int i = 0; i < _amountOfPoints; i++)
            {
                _points.Add(new Vector2(0, 0));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _point = Vector2.Zero;
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState mouseState = Mouse.GetState();
            Vector2 mousePos = new Vector2(mouseState.X, mouseState.Y);
            _mouseX = mouseState.X;
            _anchor.X = mouseState.X;
            _mouseY = mouseState.Y;
            _anchor.Y = mouseState.Y;
            _scrollValue = _baseScrollValue + mouseState.ScrollWheelValue / 120;
            _scrollValue = Math.Clamp(_scrollValue, 1, 5000);
            _distance = _scrollValue;

            _points[0] = mousePos;
            for (int i = 1; i < _amountOfPoints; i++)
            {
                Vector2 direction = _points[i] - _points[i-1];
                if (direction.Length() > _distance)
                {
                    direction.Normalize();
                    _points[i] = _points[i - 1] + direction * _distance;
                }
            }


            Vector2 direction1 = _point - _anchor; //Calculate vector from anchor to the point, represents the relative direction between 2 points
            if (direction1.Length() > _distance) //Check if the direction length is greater then the distance allowed
            {
                direction1.Normalize(); //Normalize the vector to make it 1 while keeping it the same direction 
                _point = _anchor + direction1 * _distance; //keep the point within a certain distance from the
            }
            Vector2 direction2 = _point2 - _point;
            if (direction2.Length() > _distance)
            {
                direction2.Normalize();
                _point2 = _point + direction2 * _distance;
            }
            Console.WriteLine($"{_scrollValue}");
            Console.WriteLine($"1");
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.DrawCircle(_mouseX, _mouseY, _scrollValue, Color.Transparent, Color.White, 2);
            for (int i = 1; i < _amountOfPoints; i++)
            {
                _spriteBatch.DrawCircle((int)_points[i].X,(int) _points[i].Y, 20, Color.White, Color.White, 2);
                _spriteBatch.DrawCircle((int)_points[i].X,(int) _points[i].Y, _scrollValue, Color.Transparent, Color.White, 2);
            }
            //_spriteBatch.DrawCircle((int)_point.X,(int)_point.Y, 20, Color.White, Color.White, 2);
            //_spriteBatch.DrawCircle((int)_point.X,(int)_point.Y, _scrollValue, Color.Transparent, Color.White, 2);
            //_spriteBatch.DrawCircle((int)_point2.X, (int)_point2.Y, 20, Color.White, Color.White, 2);

            //_spriteBatch.DrawCircle(1, 1, 200, Color.Transparent, Color.White, 2);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
    }
}
