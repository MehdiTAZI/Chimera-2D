using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;

namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame {
    public class ConvertUnits {
        private static float _pixelsToMetersRatio = 50;
        private static float _metersToPixelsRatio = 1 / _pixelsToMetersRatio;

        public static void SetPixelToMeterRatio(float pixelsPerMeter){
            _pixelsToMetersRatio = pixelsPerMeter;
            _metersToPixelsRatio = 1 / pixelsPerMeter;
        }

        public static float ToPixels(float pixels) {
            return pixels * _pixelsToMetersRatio;
        }

        public static float ToMeters(float meters) {
            return meters * _metersToPixelsRatio;
        }

        public static Vector2 ToPixels(Vector2 meters) {
            return _pixelsToMetersRatio * meters;
        }

        public static Vector2 ToMeters(Vector2 pixels) {
            return _metersToPixelsRatio * pixels;
        }
    }
}
