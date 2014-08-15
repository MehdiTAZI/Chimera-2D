
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
#endregion
#if !XBOX
namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Components {
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public partial class MouseInputComponent : Microsoft.Xna.Framework.GameComponent {
        private MouseState _mouseState;

        List<ButtonDownAction> buttonDownActionList = new List<ButtonDownAction>();
        List<ButtonUpAction> buttonUpActionList = new List<ButtonUpAction>();
        List<ButtonPressedAction> buttonPressedActionList = new List<ButtonPressedAction>();
        List<MouseMoveAction> mouseMoveActionList = new List<MouseMoveAction>();

        List<Buttons> _currentButtonsPressed = new List<Buttons>();
        List<Buttons> _buttonsPressed = new List<Buttons>();
        List<Buttons> _buttonsDown = new List<Buttons>();
        List<Buttons> _buttonsUp = new List<Buttons>();


        public MouseInputComponent(Game game)
            : base(game) {
            // TODO: Construct any child components here
            GetPressedKeys(_buttonsPressed);
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize() {
            // TODO: Add your initialization code here
            base.Initialize();
        }


        /// <summary>
        /// Allows the game component to Update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your Update code here
            _mouseState = Mouse.GetState();
            UpdateInput();
            base.Update(gameTime);
        }

        public void AddButtonAction(ButtonDownAction buttonDownAction) {
            buttonDownActionList.Add(buttonDownAction);
        }

        public void AddButtonAction(ButtonUpAction buttonUpAction) {
            buttonUpActionList.Add(buttonUpAction);
        }

        public void AddButtonAction(ButtonPressedAction buttonPressedAction) {
            buttonPressedActionList.Add(buttonPressedAction);
        }

        public void AddMouseMoveAction(MouseMoveAction mouseMoveAction) {
            mouseMoveActionList.Add(mouseMoveAction);
        }

        public void RemoveButtonAction(ButtonDownAction buttonDownAction) {
            buttonDownActionList.Remove(buttonDownAction);
        }

        public void RemoveButtonAction(ButtonUpAction buttonUpAction) {
            buttonUpActionList.Remove(buttonUpAction);
        }

        public void RemoveButtonAction(ButtonPressedAction buttonPressedAction) {
            buttonPressedActionList.Remove(buttonPressedAction);
        }

        public void RemoveMouseMoveAction(MouseMoveAction mouseMoveAction) {
            mouseMoveActionList.Remove(mouseMoveAction);
        }

        private void UpdateInput() {
            _buttonsUp.Clear();
            _buttonsDown.Clear();
            _currentButtonsPressed.Clear();

            GetPressedKeys(_currentButtonsPressed);

            foreach (Buttons button in _currentButtonsPressed) {
                if (!_buttonsPressed.Contains(button)) { _buttonsDown.Add(button); }
            }

            foreach (Buttons button in _buttonsPressed) {
                if (!_currentButtonsPressed.Contains(button)) { _buttonsUp.Add(button); }
            }

            _buttonsPressed = new List<Buttons>(_currentButtonsPressed);
            _currentButtonsPressed.Clear();

            ProcessButtonActions();
            ProcessMouseMoveActions();
        }

        private void ProcessButtonActions() {
            _buttonsDown.ForEach(ProcessButtonDownActions);
            _buttonsUp.ForEach(ProcessButtonUpActions);
            _buttonsPressed.ForEach(ProcessButtonPressedActions);
        }

        private void ProcessButtonDownActions(Buttons button) {
            foreach (ButtonDownAction buttonDownAction in buttonDownActionList) {
                if (button == buttonDownAction.Button) { buttonDownAction.DoButtonAction(); }
            }
        }

        private void ProcessButtonUpActions(Buttons button) {
            foreach (ButtonUpAction buttonUpAction in buttonUpActionList) {
                if (button == buttonUpAction.Button) { buttonUpAction.DoButtonAction(); }
            }
        }

        private void ProcessButtonPressedActions(Buttons button) {
            foreach (ButtonPressedAction buttonPressedAction in buttonPressedActionList) {
                if (button == buttonPressedAction.Button) { buttonPressedAction.DoButtonAction(); }
            }
        }

        private void ProcessMouseMoveActions() {
            foreach (MouseMoveAction mouseMoveAction in mouseMoveActionList) {
                mouseMoveAction.DoMouseMoveAction(_mouseState.X, _mouseState.Y, _mouseState.ScrollWheelValue);
            }
        }

        private void GetPressedKeys(List<Buttons> keysPressed) {
            if (_mouseState.LeftButton == ButtonState.Pressed) { keysPressed.Add(Buttons.Left); }
            if (_mouseState.RightButton == ButtonState.Pressed) 
            { 
                keysPressed.Add(Buttons.Right); }
            if (_mouseState.MiddleButton == ButtonState.Pressed) { keysPressed.Add(Buttons.Middle); }
            if (_mouseState.XButton1 == ButtonState.Pressed) { keysPressed.Add(Buttons.XButton1); }
            if (_mouseState.XButton2 == ButtonState.Pressed) { keysPressed.Add(Buttons.XButton2); }
        }

    }

    #region public class ButtonAction
    public class ButtonAction {
        public delegate void ButtonActionHandler();

        private ButtonActionHandler _buttonActionHandler;
        private Buttons _button;

        public ButtonAction(Buttons button, ButtonActionHandler buttonActionHandler) {
            _button = button;
            _buttonActionHandler = buttonActionHandler;
        }

        public Buttons Button {
            get { return _button; }
            set { _button = value; }
        }

        public void DoButtonAction() {
            _buttonActionHandler();
        }
    }
    #endregion

    #region public class ButtonDownAction
    public class ButtonDownAction : ButtonAction {
        public ButtonDownAction(Buttons button, ButtonActionHandler buttonActionHandler)
            : base(button, buttonActionHandler) {
        }
    }
    #endregion

    #region public class ButtonPressedAction
    public class ButtonPressedAction : ButtonAction {
        public ButtonPressedAction(Buttons button, ButtonActionHandler buttonActionHandler)
            : base(button, buttonActionHandler) {
        }
    }
    #endregion

    #region public class ButtonUpAction
    public class ButtonUpAction : ButtonAction {
        public ButtonUpAction(Buttons button, ButtonActionHandler buttonActionHandler)
            : base(button, buttonActionHandler) {
        }
    }
    #endregion

    #region public class MouseMoveAction
    public class MouseMoveAction {
        public delegate void MouseMoveActionHandler(float x, float y, float scroll);

        private MouseMoveActionHandler _mouseMoveActionHandler;

        public MouseMoveAction(MouseMoveActionHandler mouseMoveActionHandler) {
            _mouseMoveActionHandler = mouseMoveActionHandler;
        }

        public void DoMouseMoveAction(float x, float y, float scroll) {
            _mouseMoveActionHandler(x, y, scroll);
        }
    }
    #endregion

    #region public class Buttons
    public enum Buttons {
        Left,
        Right,
        Middle,
        XButton1,
        XButton2
    }
    #endregion
}


#endif