using UnityEngine;

namespace SivaEftCheat.Utils
{
    class Render
    {
        private static Color _texturesColor;

        private static Texture2D _texture = new Texture2D(1, 1) { filterMode = 0 };
        public static Texture2D CurrentTexture;
        public static Color CurrentTextureColor = Color.black;
        private static Texture2D texture2D_0 = new Texture2D(1, 1);
        private static Texture2D test = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);

        public static void DrawRadarBackground(Rect rect)
        {
            Color color = new Color(0f, 0f, 0f, 0.5f);
            texture2D_0.SetPixel(0, 0, color);
            texture2D_0.Apply();
            GUI.color = color;
            GUI.DrawTexture(rect, texture2D_0);
        }

        private static GUIStyle _testStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 12
        };

        private static GUIStyle _textOutlineStyle = new GUIStyle(GUI.skin.label)
        {
            fontSize = 12
        };


        //Credits too who ever that made this
        public static void DrawString(Vector2 pos, string text, Color color, bool center = true, int size = 12, FontStyle fontStyle = FontStyle.Normal)
        {
            _testStyle.fontSize = size;
            _testStyle.richText = true;
            _testStyle.normal.textColor = color;
            _testStyle.fontStyle = fontStyle;
            _textOutlineStyle.fontSize = size;
            _textOutlineStyle.richText = true;
            _textOutlineStyle.normal.textColor = new Color(0f, 0f, 0f, 1f);
            _textOutlineStyle.fontStyle = fontStyle;
            GUIContent contention = new GUIContent(text);
            GUIContent contention2 = new GUIContent(text);
            if (center)
            {
                pos.x -= _testStyle.CalcSize(contention).x / 2f;
            }

            GUI.Label(new Rect(pos.x + 1f, pos.y + 1f, 300f, 35f), contention2, _textOutlineStyle);
            GUI.Label(new Rect(pos.x, pos.y, 300f, 35f), contention, _testStyle);

        }

        public static void BoxRect(Rect rect, Color color)
        {
            if (CurrentTexture == null)
            {
                CurrentTexture = new Texture2D(1, 1);
                CurrentTexture.SetPixel(0, 0, color);
                CurrentTexture.Apply();
                CurrentTextureColor = color;
            }
            else if (color != CurrentTextureColor)
            {
                CurrentTexture.SetPixel(0, 0, color);
                CurrentTexture.Apply();
                CurrentTextureColor = color;
            }
            GUI.DrawTexture(rect, CurrentTexture);
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, float width, Color color)
        {
            DrawLineStretched(pointA, pointB, (int)width, color);
            //RectFilled(pointA.x, pointB.y, 1f, 1f, color);
        }

        public static void DrawLineStretched(Vector2 lineStart, Vector2 lineEnd, int thickness, Color color)
        {
            Matrix4x4 matrix = GUI.matrix;
            Color color2 = GUI.color;
            GUI.color = color;
            Vector2 vector = lineEnd - lineStart;
            float num = (float)(57.29577951308232 * Mathf.Atan(vector.y / vector.x));
            if (vector.x < 0f)
            {
                num += 180f;
            }
            if (thickness < 1)
            {
                thickness = 1;
            }
            int num2 = (int)Mathf.Ceil(thickness / 2);
            GUIUtility.RotateAroundPivot(num, lineStart);
            _texture.SetPixel(0,0, color);
            _texture.Apply();
            GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - num2, vector.magnitude, thickness), _texture);
            GUIUtility.RotateAroundPivot(-num, lineStart);
            GUI.color = color2;
            GUI.matrix = matrix;
        }

        public static void DrawHealth(Vector2 pos, float width, float height, float health, float maxHealth)
        {
            BoxRect(new Rect(pos.x, pos.y, width, height), Color.black);
            pos.x += 1f;
            pos.y += 1f;
            Color color = Color.green;
            if (health <= maxHealth / 2)
            {
                color = Color.yellow;
            }
            if (health <= maxHealth / 4)
            {
                color = Color.red;
            }
            BoxRect(new Rect(pos.x, pos.y, width * (health / maxHealth * 100f) / 100f - 2f, height - 2f), color);
        }

        public static void DrawCornerBox(Vector2 headPosition, float width, float height, Color color, bool outline)
        {
            int num = (int)(width / 4f);
            int num2 = num;

            if (outline)
            {
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y - 1f, num + 2, 3f, Color.black);
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y - 1f, 3f, num2 + 2, Color.black);
                RectFilled(headPosition.x + width / 2f - num - 1f, headPosition.y - 1f, num + 2, 3f, Color.black);
                RectFilled(headPosition.x + width / 2f - 1f, headPosition.y - 1f, 3f, num2 + 2, Color.black);
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y + height - 4f, num + 2, 3f, Color.black);
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y + height - num2 - 4f, 3f, num2 + 2, Color.black);
                RectFilled(headPosition.x + width / 2f - num - 1f, headPosition.y + height - 4f, num + 2, 3f, Color.black);
                RectFilled(headPosition.x + width / 2f - 1f, headPosition.y + height - num2 - 4f, 3f, num2 + 3, Color.black);
            }
            RectFilled(headPosition.x - width / 2f, headPosition.y, num, 1f, color);
            RectFilled(headPosition.x - width / 2f, headPosition.y, 1f, num2, color);
            RectFilled(headPosition.x + width / 2f - num, headPosition.y, num, 1f, color);
            RectFilled(headPosition.x + width / 2f, headPosition.y, 1f, num2, color);
            RectFilled(headPosition.x - width / 2f, headPosition.y + height - 3f, num, 1f, color);
            RectFilled(headPosition.x - width / 2f, headPosition.y + height - num2 - 3f, 1f, num2, color);
            RectFilled(headPosition.x + width / 2f - num, headPosition.y + height - 3f, num, 1f, color);
            RectFilled(headPosition.x + width / 2f, headPosition.y + height - num2 - 3f, 1f, num2 + 1, color);
        }

        public static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (color != _texturesColor)
            {
                _texturesColor = color;
                _texture.SetPixel(0, 0, color);
                _texture.Apply();
            }
            GUI.DrawTexture(new Rect(x, y, width, height), _texture);
        }

        public static void DrawCircle(Vector2 center, float radius, Color color, float width, bool antiAlias, int segmentsPerQuarter)
        {
            float num = radius / 2f;
            Vector2 vector = new Vector2(center.x, center.y - radius);
            Vector2 endTangent = new Vector2(center.x - num, center.y - radius);
            Vector2 startTangent = new Vector2(center.x + num, center.y - radius);
            Vector2 vector2 = new Vector2(center.x + radius, center.y);
            Vector2 endTangent2 = new Vector2(center.x + radius, center.y - num);
            Vector2 startTangent2 = new Vector2(center.x + radius, center.y + num);
            Vector2 vector3 = new Vector2(center.x, center.y + radius);
            Vector2 startTangent3 = new Vector2(center.x - num, center.y + radius);
            Vector2 endTangent3 = new Vector2(center.x + num, center.y + radius);
            Vector2 vector4 = new Vector2(center.x - radius, center.y);
            Vector2 startTangent4 = new Vector2(center.x - radius, center.y - num);
            Vector2 endTangent4 = new Vector2(center.x - radius, center.y + num);
            DrawBezierLine(vector, startTangent, vector2, endTangent2, color, width, antiAlias, segmentsPerQuarter);
            DrawBezierLine(vector2, startTangent2, vector3, endTangent3, color, width, antiAlias, segmentsPerQuarter);
            DrawBezierLine(vector3, startTangent3, vector4, endTangent4, color, width, antiAlias, segmentsPerQuarter);
            DrawBezierLine(vector4, startTangent4, vector, endTangent, color, width, antiAlias, segmentsPerQuarter);
        }

        public static void DrawBezierLine(Vector2 start, Vector2 startTangent, Vector2 end, Vector2 endTangent, Color color, float width, bool antiAlias, int segments)
        {
            Vector2 pointA = CubeBezier(start, startTangent, end, endTangent, 0f);
            for (int i = 1; i < segments + 1; i++)
            {
                Vector2 vector = CubeBezier(start, startTangent, end, endTangent, (float)i / (float)segments);
                DrawLine(pointA, vector, color, width, antiAlias);
                pointA = vector;
            }
        }

        private static Vector2 CubeBezier(Vector2 s, Vector2 st, Vector2 e, Vector2 et, float t)
        {
            float num = 1f - t;
            return num * num * num * s + 3f * num * num * t * st + 3f * num * t * t * et + t * t * t * e;
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
        {
            float num = pointB.x - pointA.x;
            float num2 = pointB.y - pointA.y;
            float num3 = Mathf.Sqrt(num * num + num2 * num2);
            if (num3 >= 0.001f)
            {
                Texture2D texture2D;
                if (antiAlias)
                {
                    width *= 3f;
                    texture2D = test;
                }
                else
                {
                    texture2D = test;
                }
                float num4 = width * num2 / num3;
                float num5 = width * num / num3;
                Matrix4x4 identity = Matrix4x4.identity;
                identity.m00 = num;
                identity.m01 = -num4;
                identity.m03 = pointA.x + 0.5f * num4;
                identity.m10 = num2;
                identity.m11 = num5;
                identity.m13 = pointA.y - 0.5f * num5;
                GL.PushMatrix();
                GL.MultMatrix(identity);
                GUI.color = color;
                GUI.DrawTexture(lineRect, texture2D);
                GL.PopMatrix();
            }
        }

    }
}
