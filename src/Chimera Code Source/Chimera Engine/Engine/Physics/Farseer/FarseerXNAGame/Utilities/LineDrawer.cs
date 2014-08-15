//using System;
//using System.Collections.Generic;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;

//namespace Chimera.Physics.Farseer.FarseerGames.FarseerXNAGame.Utilities {
//    public class LineDrawer {

//        private static List<DebugLineVert> debugLines_ = new List<DebugLineVert>();

//        public static void DebugLine(Vector3 p, Vector3 d) {
//            DebugLine(p, d, 0xffffffff);
//        }

//        public static void DebugLine(Vector3 p, Vector3 d, uint c) {
//            if (debugLines_.Count >= MAX_DEBUG_LINES * 2) {
//                return;
//            }
//            DebugLineVert dlv = new DebugLineVert();
//            dlv.pos = p;
//            dlv.color = c;
//            debugLines_.Add(dlv);
//            dlv.pos = d;
//            dlv.color = c;
//            debugLines_.Add(dlv);
//        }

//        VertexBuffer debugLineVb_;
//        VertexDeclaration debugLineVdecl_;
//        Effect debugLineFx_;
//        EffectParameter debugLineWvp_;
//        const int MAX_DEBUG_LINES = 1024;

//        struct DebugLineVert {
//            public Vector3 pos;
//            public uint color;
//        }

//        public void LoadGraphicsContent(bool loadAllContent) {
//            debugLineVb_ = new VertexBuffer(dev_, MAX_DEBUG_LINES * 16 * 2,
//                ResourceUsage.Dynamic | ResourceUsage.WriteOnly,
//                ResourceManagementMode.Manual);
//            VertexElement[] ve = new VertexElement[2];
//            ve[0].Offset = 0;
//            ve[0].VertexElementFormat = VertexElementFormat.Vector3;
//            ve[0].VertexElementUsage = VertexElementUsage.Position;
//            ve[1].Offset = 12;
//            ve[1].VertexElementFormat = VertexElementFormat.Color;
//            ve[1].VertexElementUsage = VertexElementUsage.Color;
//            debugLineVdecl_ = new VertexDeclaration(dev_, ve);
//            debugLineFx_ = content_.Load<Effect>("content\\lines");
//            debugLineWvp_ = debugLineFx_.Parameters["g_mWorldViewProjection"];
//        }

//        public void UnloadGraphicsContent(bool unloadAllContent) {
//            debugLineVdecl_.Dispose();
//            debugLineVdecl_ = null;
//            debugLineVb_.Dispose();
//            debugLineVb_ = null;
//            debugLineFx_.Dispose();
//            debugLineFx_ = null;
//            debugLines_.Clear();
//        }

//        public static void ClearDebugLines() {
//            debugLines_.Clear();
//        }

//        public void Draw() {

//            /* other code goes here */

//            if (debugLines_.Count > 0) {
//                debugLineVb_.SetData<DebugLineVert>(debugLines_.ToArray());
//                dev_.Vertices[0].SetSource(debugLineVb_, 0, 16);
//                dev_.VertexDeclaration = debugLineVdecl_;
//                debugLineWvp_.SetValue(curCamera_.ViewMatrix * curCamera_.ProjectionMatrix);
//                debugLineFx_.Begin(SaveStateMode.None);
//                debugLineFx_.CurrentTechnique.Passes[0].Begin();
//                dev_.DrawPrimitives(PrimitiveType.LineList, 0, debugLines_.Count / 2);
//                debugLineFx_.CurrentTechnique.Passes[0].End();
//                debugLineFx_.End();
//            }
//            ClearDebugLines();
//        }


//    }
//}
