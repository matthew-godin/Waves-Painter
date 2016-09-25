/*
InputManager.cs
---------------

By Matthew Godin

Role : Component taking care of
       the input of different
       devices connected to the computer
       being used

Created : 12 September 2016
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Painter_for_Waves
{
    /// <summary>
    /// Component taking care of the inputs
    /// </summary>
    public class InputManager : Microsoft.Xna.Framework.GameComponent
    {
        Keys[] PreviousKeys { get; set; }
        Keys[] CurrentKeys { get; set; }
        MouseState PreviousMouseState { get; set; }
        MouseState CurrentMouseState { get; set; }
        KeyboardState KeyboardState { get; set; }

        /// <summary>
        /// True if at least one keyboard key is pressed
        /// </summary>
        public bool IsKeyboardActivated
        {
            get
            {
                return CurrentKeys.Length > 0;
            }
        }

        /// <summary>
        /// True if the mouse is visible on the screen
        /// </summary>
        public bool IsMouseActive
        {
            get
            {
                return Game.IsMouseVisible;
            }
        }

        /// <summary>
        /// Constructor only calling the parent one
        /// </summary>
        /// <param name="game">Game object</param>
        public InputManager(Game game) : base(game) { }

        /// <summary>
        /// Initializes the properties of the object
        /// </summary>
        public override void Initialize()
        {
            PreviousKeys = new Keys[0];
            CurrentKeys = new Keys[0];
        }

        /// <summary>
        /// Updates the state of the different devices
        /// </summary>
        /// <param name="gameTime">Time information</param>
        public override void Update(GameTime gameTime)
        {
            PreviousKeys = CurrentKeys;
            KeyboardState = Keyboard.GetState();
            CurrentKeys = KeyboardState.GetPressedKeys();
            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
        }

        /// <summary>
        /// True if the enumeration key is pressed
        /// </summary>
        /// <param name="key">Key of enumeration type Keys</param>
        /// <returns>Bool</returns>
        public bool isPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// True if the right click is pressed but already
        /// </summary>
        /// <returns>Bool</returns>
        public bool IsOldRightClick()
        {
            return PreviousMouseState.RightButton == ButtonState.Pressed && CurrentMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// True if the left click is pressed but already
        /// </summary>
        /// <returns>Bool</returns>
        public bool IsOldLeftClick()
        {
            return PreviousMouseState.LeftButton == ButtonState.Pressed && CurrentMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// True if it is a new right click
        /// </summary>
        /// <returns>Bool</returns>
        public bool IsNewRightClick()
        {
            return PreviousMouseState.RightButton == ButtonState.Released && CurrentMouseState.RightButton == ButtonState.Pressed;
        }

        /// <summary>
        /// True if it is a new left click
        /// </summary>
        /// <returns>Bool</returns>
        public bool IsNewLeftClick()
        {
            return PreviousMouseState.LeftButton == ButtonState.Released && CurrentMouseState.LeftButton == ButtonState.Pressed;
        }

        /// <summary>
        /// Returns the current position of the mouse in the game window
        /// </summary>
        /// <returns>Point</returns>
        public Point GetMousePosition()
        {
            return new Point(CurrentMouseState.X, CurrentMouseState.Y);
        }

        /// <summary>
        /// True if the passed key just got pressed
        /// </summary>
        /// <param name="key">Key of enumeration type Keys</param>
        /// <returns>Bool</returns>
        public bool IsNewKey(Keys key)
        {
            int numKeys = PreviousKeys.Length;
            bool IsNewKey = KeyboardState.IsKeyDown(key);
            int i = 0;

            while (i < numKeys && IsNewKey)
            {
                IsNewKey = PreviousKeys[i] != key;
                ++i;
            }

            return IsNewKey;
        }
    }
}