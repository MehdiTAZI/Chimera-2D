
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Components {
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class KeyboardInputComponent : Microsoft.Xna.Framework.GameComponent, IKeyboardInputService {
        List<KeyDownAction> keyDownActionList = new List<KeyDownAction>();
        List<KeyUpAction> keyUpActionList = new List<KeyUpAction>();
        List<KeyPressedAction> keyPressedActionList = new List<KeyPressedAction>();

        Keys[] _keysPressed;
        List<Keys> keysDown = new List<Keys>();
        List<Keys> keysUp = new List<Keys>();

        //A
        public event EventHandler<KeyEventArgs> AKeyDown;
        public event EventHandler<KeyEventArgs> AKeyUp;
        public event EventHandler<KeyEventArgs> AKeyPressed;
        //Add

        //Alt

        //B

        //Back

        //C

        //Cancel

        //CapsLock

        //Clear

        //Control

        //ControlKey

        //D
        public event EventHandler<KeyEventArgs> DKeyDown;
        public event EventHandler<KeyEventArgs> DKeyUp;
        public event EventHandler<KeyEventArgs> DKeyPressed;

        //D0

        //D1

        //D2

        //D3

        //D4

        //D5

        //D6

        //D7

        //D8

        //D9

        //Decimal

        //Delete

        //Divide

        //Down

        //E

        //End

        //Enter

        //Escape
        public event EventHandler<KeyEventArgs> EscapeKeyDown;
        public event EventHandler<KeyEventArgs> EscapeKeyUp;
        public event EventHandler<KeyEventArgs> EscapeKeyPressed;

        //Execute

        //F

        //F1

        //F10

        //F11

        //F12

        //F13

        //F14

        //F15

        //F16

        //F17

        //F18

        //F19

        //F2

        //F20

        //F21

        //F22

        //F23

        //F24

        //F3

        //F4

        //F5

        //F6

        //F7

        //F8

        //F9

        //G

        //H

        //Help

        //Home

        //I

        //Insert

        //J

        //K

        //L

        //LButton

        //LControlKey

        //LMenu

        //LShiftKey

        //LWin

        //Left
        public event EventHandler<KeyEventArgs> LeftKeyDown;
        public event EventHandler<KeyEventArgs> LeftKeyUp;
        public event EventHandler<KeyEventArgs> LeftKeyPressed;

        //LineFeed

        //M

        //MButton

        //Menu

        //Multiply

        //N

        //NumLock

        //NumPad0

        //NumPad1

        //NumPad2

        //NumPad3

        //NumPad4

        //NumPad5

        //NumPad6

        //NumPad7

        //NumPad8

        //NumPad9

        //O

        //P

        //PageDown

        //PageUp

        //Pause
        ///MehdiModification Never Used
        /*public event EventHandler<KeyEventArgs> PauseKeyDown;
        public event EventHandler<KeyEventArgs> PauseKeyUp;
        public event EventHandler<KeyEventArgs> PauseKeyPressed;*/

        //Play

        //Print

        //PrintScreen

        //Q

        //R

        //RButton

        //RControlKey

        //RMenu

        //RShiftKey

        //RWin

        //Right
        public event EventHandler<KeyEventArgs> RightKeyDown;
        public event EventHandler<KeyEventArgs> RightKeyUp;
        public event EventHandler<KeyEventArgs> RightKeyPressed;

        //S
        public event EventHandler<KeyEventArgs> SKeyDown;
        public event EventHandler<KeyEventArgs> SKeyUp;
        public event EventHandler<KeyEventArgs> SKeyPressed;

        //Separator

        //Shift

        //ShiftKey

        //Space

        //Subtract

        //T

        //Tab

        //U

        //Up

        //V

        //W
        public event EventHandler<KeyEventArgs> WKeyDown;
        public event EventHandler<KeyEventArgs> WKeyUp;
        public event EventHandler<KeyEventArgs> WKeyPressed;

        //X

        //Y

        //Z

        public KeyboardInputComponent(Game game)
            : base(game) {
            // TODO: Construct any child components here
            _keysPressed = Keyboard.GetState().GetPressedKeys();            
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
            UpdateInput();
            RaiseKeyEvents();
            base.Update(gameTime);
        }

        private void RaiseKeyEvents() {
            keysDown.ForEach(RaiseKeyDownEvent);
            keysUp.ForEach(RaiseKeyUpEvent);
            for (int i = 0; i < _keysPressed.Length; i++) {
                RaisKeyPressedEvent(_keysPressed[i]);
            }
        }

        private void RaiseKeyDownEvent(Keys key) {
            switch (key) {
                case Keys.A:
                    if (AKeyDown != null) { AKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.Add:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Attn:
                    break;
                case Keys.B:
                    break;
                case Keys.Back:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.C:
                    break;
                case Keys.CapsLock:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.D:
                    if (DKeyDown != null) { DKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.D0:
                    break;
                case Keys.D1:
                    break;
                case Keys.D2:
                    break;
                case Keys.D3:
                    break;
                case Keys.D4:
                    break;
                case Keys.D5:
                    break;
                case Keys.D6:
                    break;
                case Keys.D7:
                    break;
                case Keys.D8:
                    break;
                case Keys.D9:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Delete:
                    break;
                case Keys.Divide:
                    break;
                case Keys.Down:
                    break;
                case Keys.E:
                    break;
                case Keys.End:
                    break;
                case Keys.Enter:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Escape:
                    if (EscapeKeyDown != null) { EscapeKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.Execute:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.F:
                    break;
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.G:
                    break;
                case Keys.H:
                    break;
                case Keys.Help:
                    break;
                case Keys.Home:
                    break;
                case Keys.I:
                    break;
                case Keys.Insert:
                    break;
                case Keys.J:
                    break;
                case Keys.K:
                    break;
                case Keys.L:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.Left:
                    if (LeftKeyDown != null) { LeftKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.LeftAlt:
                    break;
                case Keys.LeftControl:
                    break;
                case Keys.LeftShift:
                    break;
                case Keys.LeftWindows:
                    break;
                case Keys.M:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.Multiply:
                    break;
                case Keys.N:
                    break;
                case Keys.None:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.NumPad0:
                    break;
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                case Keys.NumPad3:
                    break;
                case Keys.NumPad4:
                    break;
                case Keys.NumPad5:
                    break;
                case Keys.NumPad6:
                    break;
                case Keys.NumPad7:
                    break;
                case Keys.NumPad8:
                    break;
                case Keys.NumPad9:
                    break;
                case Keys.O:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemBackslash:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.OemCloseBrackets:
                    break;
                case Keys.OemComma:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemOpenBrackets:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.OemPipe:
                    break;
                case Keys.OemPlus:
                    break;
                case Keys.OemQuestion:
                    break;
                case Keys.OemQuotes:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.OemTilde:
                    break;
                case Keys.P:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.PageDown:
                    break;
                case Keys.PageUp:
                    break;
                case Keys.Play:
                    //if (PauseKeyDown != null) { PauseKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.Print:
                    break;
                case Keys.PrintScreen:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Q:
                    break;
                case Keys.R:
                    break;
                case Keys.Right:
                    if (RightKeyDown != null) { RightKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.RightAlt:
                    break;
                case Keys.RightControl:
                    break;
                case Keys.RightShift:
                    break;
                case Keys.RightWindows:
                    break;
                case Keys.S:
                    if (SKeyDown != null) { SKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.Scroll:
                    break;
                case Keys.Select:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.Space:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.T:
                    break;
                case Keys.Tab:
                    break;
                case Keys.U:
                    break;
                case Keys.Up:
                    break;
                case Keys.V:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.W:
                    if (WKeyDown != null) { WKeyDown(this, new KeyEventArgs()); }
                    break;
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                case Keys.Zoom:
                    break;
                default:
                    break;
            }
        }

        private void RaiseKeyUpEvent(Keys key) {
            switch (key) {
                case Keys.A:
                    if (AKeyUp != null) { AKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.Add:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Attn:
                    break;
                case Keys.B:
                    break;
                case Keys.Back:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.C:
                    break;
                case Keys.CapsLock:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.D:
                    if (DKeyUp != null) { DKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.D0:
                    break;
                case Keys.D1:
                    break;
                case Keys.D2:
                    break;
                case Keys.D3:
                    break;
                case Keys.D4:
                    break;
                case Keys.D5:
                    break;
                case Keys.D6:
                    break;
                case Keys.D7:
                    break;
                case Keys.D8:
                    break;
                case Keys.D9:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Delete:
                    break;
                case Keys.Divide:
                    break;
                case Keys.Down:
                    break;
                case Keys.E:
                    break;
                case Keys.End:
                    break;
                case Keys.Enter:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Escape:
                    if (EscapeKeyUp != null) { EscapeKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.Execute:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.F:
                    break;
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.G:
                    break;
                case Keys.H:
                    break;
                case Keys.Help:
                    break;
                case Keys.Home:
                    break;
                case Keys.I:
                    break;
                case Keys.Insert:
                    break;
                case Keys.J:
                    break;
                case Keys.K:
                    break;
                case Keys.L:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.Left:
                    if (LeftKeyUp != null) { LeftKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.LeftAlt:
                    break;
                case Keys.LeftControl:
                    break;
                case Keys.LeftShift:
                    break;
                case Keys.LeftWindows:
                    break;
                case Keys.M:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.Multiply:
                    break;
                case Keys.N:
                    break;
                case Keys.None:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.NumPad0:
                    break;
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                case Keys.NumPad3:
                    break;
                case Keys.NumPad4:
                    break;
                case Keys.NumPad5:
                    break;
                case Keys.NumPad6:
                    break;
                case Keys.NumPad7:
                    break;
                case Keys.NumPad8:
                    break;
                case Keys.NumPad9:
                    break;
                case Keys.O:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemBackslash:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.OemCloseBrackets:
                    break;
                case Keys.OemComma:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemOpenBrackets:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.OemPipe:
                    break;
                case Keys.OemPlus:
                    break;
                case Keys.OemQuestion:
                    break;
                case Keys.OemQuotes:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.OemTilde:
                    break;
                case Keys.P:                    
                    break;
                case Keys.Pa1:
                    break;
                case Keys.PageDown:
                    break;
                case Keys.PageUp:
                    break;
                case Keys.Play:
                    //if (PauseKeyUp != null) { PauseKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.Print:
                    break;
                case Keys.PrintScreen:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Q:
                    break;
                case Keys.R:
                    break;
                case Keys.Right:
                    if (RightKeyUp != null) { RightKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.RightAlt:
                    break;
                case Keys.RightControl:
                    break;
                case Keys.RightShift:
                    break;
                case Keys.RightWindows:
                    break;
                case Keys.S:
                    if (SKeyUp != null) { SKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.Scroll:
                    break;
                case Keys.Select:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.Space:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.T:
                    break;
                case Keys.Tab:
                    break;
                case Keys.U:
                    break;
                case Keys.Up:
                    break;
                case Keys.V:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.W:
                    if (WKeyUp != null) { WKeyUp(this, new KeyEventArgs()); }
                    break;
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                case Keys.Zoom:
                    break;
                default:
                    break;
            }

   

        }

        private void RaisKeyPressedEvent(Keys key) {
            switch (key) {
                case Keys.A:
                    if (AKeyPressed != null) { AKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.Add:
                    break;
                case Keys.Apps:
                    break;
                case Keys.Attn:
                    break;
                case Keys.B:
                    break;
                case Keys.Back:
                    break;
                case Keys.BrowserBack:
                    break;
                case Keys.BrowserFavorites:
                    break;
                case Keys.BrowserForward:
                    break;
                case Keys.BrowserHome:
                    break;
                case Keys.BrowserRefresh:
                    break;
                case Keys.BrowserSearch:
                    break;
                case Keys.BrowserStop:
                    break;
                case Keys.C:
                    break;
                case Keys.CapsLock:
                    break;
                case Keys.Crsel:
                    break;
                case Keys.D:
                    if (DKeyPressed != null) { DKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.D0:
                    break;
                case Keys.D1:
                    break;
                case Keys.D2:
                    break;
                case Keys.D3:
                    break;
                case Keys.D4:
                    break;
                case Keys.D5:
                    break;
                case Keys.D6:
                    break;
                case Keys.D7:
                    break;
                case Keys.D8:
                    break;
                case Keys.D9:
                    break;
                case Keys.Decimal:
                    break;
                case Keys.Delete:
                    break;
                case Keys.Divide:
                    break;
                case Keys.Down:
                    break;
                case Keys.E:
                    break;
                case Keys.End:
                    break;
                case Keys.Enter:
                    break;
                case Keys.EraseEof:
                    break;
                case Keys.Escape:
                    if (EscapeKeyPressed != null) { EscapeKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.Execute:
                    break;
                case Keys.Exsel:
                    break;
                case Keys.F:
                    break;
                case Keys.F1:
                    break;
                case Keys.F10:
                    break;
                case Keys.F11:
                    break;
                case Keys.F12:
                    break;
                case Keys.F13:
                    break;
                case Keys.F14:
                    break;
                case Keys.F15:
                    break;
                case Keys.F16:
                    break;
                case Keys.F17:
                    break;
                case Keys.F18:
                    break;
                case Keys.F19:
                    break;
                case Keys.F2:
                    break;
                case Keys.F20:
                    break;
                case Keys.F21:
                    break;
                case Keys.F22:
                    break;
                case Keys.F23:
                    break;
                case Keys.F24:
                    break;
                case Keys.F3:
                    break;
                case Keys.F4:
                    break;
                case Keys.F5:
                    break;
                case Keys.F6:
                    break;
                case Keys.F7:
                    break;
                case Keys.F8:
                    break;
                case Keys.F9:
                    break;
                case Keys.G:
                    break;
                case Keys.H:
                    break;
                case Keys.Help:
                    break;
                case Keys.Home:
                    break;
                case Keys.I:
                    break;
                case Keys.Insert:
                    break;
                case Keys.J:
                    break;
                case Keys.K:
                    break;
                case Keys.L:
                    break;
                case Keys.LaunchApplication1:
                    break;
                case Keys.LaunchApplication2:
                    break;
                case Keys.LaunchMail:
                    break;
                case Keys.Left:
                    if (LeftKeyPressed != null) { LeftKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.LeftAlt:
                    break;
                case Keys.LeftControl:
                    break;
                case Keys.LeftShift:
                    break;
                case Keys.LeftWindows:
                    break;
                case Keys.M:
                    break;
                case Keys.MediaNextTrack:
                    break;
                case Keys.MediaPlayPause:
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
                case Keys.Multiply:
                    break;
                case Keys.N:
                    break;
                case Keys.None:
                    break;
                case Keys.NumLock:
                    break;
                case Keys.NumPad0:
                    break;
                case Keys.NumPad1:
                    break;
                case Keys.NumPad2:
                    break;
                case Keys.NumPad3:
                    break;
                case Keys.NumPad4:
                    break;
                case Keys.NumPad5:
                    break;
                case Keys.NumPad6:
                    break;
                case Keys.NumPad7:
                    break;
                case Keys.NumPad8:
                    break;
                case Keys.NumPad9:
                    break;
                case Keys.O:
                    break;
                case Keys.Oem8:
                    break;
                case Keys.OemBackslash:
                    break;
                case Keys.OemClear:
                    break;
                case Keys.OemCloseBrackets:
                    break;
                case Keys.OemComma:
                    break;
                case Keys.OemMinus:
                    break;
                case Keys.OemOpenBrackets:
                    break;
                case Keys.OemPeriod:
                    break;
                case Keys.OemPipe:
                    break;
                case Keys.OemPlus:
                    break;
                case Keys.OemQuestion:
                    break;
                case Keys.OemQuotes:
                    break;
                case Keys.OemSemicolon:
                    break;
                case Keys.OemTilde:
                    break;
                case Keys.P:
                    break;
                case Keys.Pa1:
                    break;
                case Keys.PageDown:
                    break;
                case Keys.PageUp:
                    break;
                case Keys.Play:
                    //if (PauseKeyPressed != null) { PauseKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.Print:
                    break;
                case Keys.PrintScreen:
                    break;
                case Keys.ProcessKey:
                    break;
                case Keys.Q:
                    break;
                case Keys.R:
                    break;
                case Keys.Right:
                    if (RightKeyPressed != null) { RightKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.RightAlt:
                    break;
                case Keys.RightControl:
                    break;
                case Keys.RightShift:
                    break;
                case Keys.RightWindows:
                    break;
                case Keys.S:
                    if (SKeyPressed != null) { SKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.Scroll:
                    break;
                case Keys.Select:
                    break;
                case Keys.SelectMedia:
                    break;
                case Keys.Separator:
                    break;
                case Keys.Sleep:
                    break;
                case Keys.Space:
                    break;
                case Keys.Subtract:
                    break;
                case Keys.T:
                    break;
                case Keys.Tab:
                    break;
                case Keys.U:
                    break;
                case Keys.Up:
                    break;
                case Keys.V:
                    break;
                case Keys.VolumeDown:
                    break;
                case Keys.VolumeMute:
                    break;
                case Keys.VolumeUp:
                    break;
                case Keys.W:
                    if (WKeyPressed != null) { WKeyPressed(this, new KeyEventArgs()); }
                    break;
                case Keys.X:
                    break;
                case Keys.Y:
                    break;
                case Keys.Z:
                    break;
                case Keys.Zoom:
                    break;
                default:
                    break;
            }
        }

        private bool KeyDown(Keys key) {
            if (keysDown.Contains(key)) {
                return true;
            }
            return false;
        }

        private bool KeyUp(Keys key) {
            if (keysUp.Contains(key)) {
                return true;
            }
            return false;
        }

        public void AddKeyAction(KeyDownAction keyDownAction) {
            keyDownActionList.Add(keyDownAction);
        }

        public void AddKeyAction(KeyUpAction keyUpAction) {
            keyUpActionList.Add(keyUpAction);
        }

        public void AddKeyAction(KeyPressedAction keyPressedAction) {
            keyPressedActionList.Add(keyPressedAction);
        }

        public void RemoveKeyAction(KeyDownAction keyDownAction) {
            keyDownActionList.Remove(keyDownAction);
        }

        public void RemoveKeyAction(KeyUpAction keyUpAction) {
            keyUpActionList.Remove(keyUpAction);
        }

        public void RemoveKeyAction(KeyPressedAction keyPressedAction) {
            keyPressedActionList.Remove(keyPressedAction);
        }

        public void ClearKeyActions() {
            //keyDownActionList.Clear();
            //keyUpActionList.Clear();
            //KeyPressedActionList.clear();
        }

        void UpdateInput() {
            // Clear our pressed and released lists.
            keysDown.Clear();
            keysUp.Clear();

            // Interpret pressed key data between arrays to
            // figure out just-pressed and just-released keys.
            KeyboardState currentState = Keyboard.GetState();
            Keys[] currentKeys = currentState.GetPressedKeys();

            // First loop, looking for keys just pressed.
            for (int currentKey = 0; currentKey < currentKeys.Length; currentKey++) {
                bool found = false;
                for (int previousKey = 0; previousKey < _keysPressed.Length; previousKey++) {
                    if (currentKeys[currentKey] == _keysPressed[previousKey]) {
                        // The key was pressed both this frame and last; ignore.
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    // The key was pressed this frame, but not last frame; it was just pressed.
                    keysDown.Add(currentKeys[currentKey]);
                }
            }

            // Second loop, looking for keys just released.
            for (int previousKey = 0; previousKey < _keysPressed.Length; previousKey++) {
                bool found = false;
                for (int currentKey = 0; currentKey < currentKeys.Length; currentKey++) {
                    if (_keysPressed[previousKey] == currentKeys[currentKey]) {
                        // The key was pressed both this frame and last; ignore.
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    // The key was pressed last frame, but not this frame; it was just released.
                    keysUp.Add(_keysPressed[previousKey]);
                }
            }

            // Set the held state to the current state.
            _keysPressed = currentKeys;

            ProcessKeyActions();
        }

        private void ProcessKeyActions() {
            keysDown.ForEach(ProcessKeyDownActions);
            keysUp.ForEach(ProcessKeyUpActions);
            //_keysPressed is an array rather than an List<>
            for (int i = 0; i < _keysPressed.Length; i++) {
                ProcessKeyPressedActions(_keysPressed[i]);
            }
        }

        private void ProcessKeyDownActions(Keys key) {
            foreach (KeyDownAction keyDownAction in keyDownActionList) {
                if (key == keyDownAction.Key) { keyDownAction.DoKeyAction(); }
            }
        }

        private void ProcessKeyUpActions(Keys key) {
            foreach (KeyUpAction keyUpAction in keyUpActionList) {
                if (key == keyUpAction.Key) { keyUpAction.DoKeyAction(); }
            }
        }

        private void ProcessKeyPressedActions(Keys key) {
            foreach (KeyPressedAction keyPressedAction in keyPressedActionList) {
                if (key == keyPressedAction.Key) { keyPressedAction.DoKeyAction(); }
            }
        }
    }

    public class KeyEventArgs : EventArgs {
        //empty for now.
    }

    #region public class KeyAction 
    public class KeyAction {
        public delegate void KeyActionHandler();

        private KeyActionHandler keyActionHandler;
        private Keys key;

        public KeyAction(Keys key, KeyActionHandler keyActionHandler) {
            this.key = key;
            this.keyActionHandler = keyActionHandler;
        }

        public Keys Key {
            get { return key; }
            set { key = value; }
        }

        public void DoKeyAction() {
            keyActionHandler();
        }
    }
#endregion

    #region public class KeyDownAction
    public class KeyDownAction : KeyAction {
        public KeyDownAction(Keys key, KeyActionHandler keyActionHandler)
            : base(key, keyActionHandler) {
        }
    }
    #endregion

    #region public class KeyPressedAction
    public class KeyPressedAction : KeyAction {
        public KeyPressedAction(Keys key, KeyActionHandler keyActionHandler)
            : base(key, keyActionHandler) {
        }
    }
    #endregion

    #region public class KeyPressedAction
    public class KeyUpAction : KeyAction {
        public KeyUpAction(Keys key, KeyActionHandler keyActionHandler)
            : base(key, keyActionHandler) {
        }
    }
    #endregion

    #region interface IKeyboardInputService
    public interface IKeyboardInputService {
        void AddKeyAction(KeyDownAction keyDownAction);

        void AddKeyAction(KeyUpAction keyUpAction);

        void AddKeyAction(KeyPressedAction keyPressedAction);

        void RemoveKeyAction(KeyDownAction keyDownAction);

        void RemoveKeyAction(KeyUpAction keyUpAction);

        void RemoveKeyAction(KeyPressedAction keyPressedAction);

        void ClearKeyActions();
        #endregion

    }

}


