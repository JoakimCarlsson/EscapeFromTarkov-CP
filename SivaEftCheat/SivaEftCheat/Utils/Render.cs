using UnityEngine;

namespace SivaEftCheat.Utils
{
    class Render
    {
        private static Color _texturesColor;

        private static Texture2D _texture = new Texture2D(1, 1)
        {
            filterMode = 0
        };

        public static Texture2D CurrentTexture;
        public static Color CurrentTextureColor = Color.black;
        private static readonly GUIStyle EspLabelStyle = new GUIStyle
        {
            fontSize = 11,
        };

        public static void DrawTextOutline(Vector2 position, string text, Color outColor, Color inColor)
        {
            GUIStyle espLabelStyle = EspLabelStyle;
            Vector2 vector = espLabelStyle.CalcSize(new GUIContent(text));
            var rect = new Rect(position.x + 1f, position.y + 1f, vector.x + 12f, vector.y + 12f);
            espLabelStyle.normal.textColor = outColor;
            GUI.Label(rect, text, espLabelStyle);
            rect.x -= 1f;
            rect.y -= 1f;
            espLabelStyle.normal.textColor = inColor;
            GUI.Label(rect, text, espLabelStyle);
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
        public static void RectFilled(float x, float y, float width, float height, Texture2D texture)
        {
            GUI.DrawTexture(new Rect(x, y, width, height), texture);
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, float width, Color color)
        {
            DrawLineStretched(pointA, pointB, (int)width, color);
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
            GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - num2, vector.magnitude, thickness), _texture);
            GUIUtility.RotateAroundPivot(-num, lineStart);
            GUI.color = color2;
            GUI.matrix = matrix;
        }

        public static void RectOutlined(float x, float y, float width, float height, Texture2D text, float thickness = 1f)
        {
            RectFilled(x, y, thickness, height, text);
            RectFilled(x + width - thickness, y, thickness, height, text);
            RectFilled(x + thickness, y, width - thickness * 2f, thickness, text);
            RectFilled(x + thickness, y + height - thickness, width - thickness * 2f, thickness, text);
        }

        public static void DrawBox(float x, float y, float width, float height, Color color, float thickness = 1f)
        {
            _texture.SetPixel(0, 0, color);
            _texture.SetPixel(1, 0, color);
            _texture.SetPixel(0, 1, color);
            _texture.SetPixel(1, 1, color);
            _texture.Apply();

            RectOutlined(x - width / 2f, y - height, width, height, _texture, thickness);
        }
        public static void DrawHealth(Vector2 pos, float width, float height, float health, float maxHealth)
        {
            BoxRect(new Rect(pos.x, pos.y, width, height), Color.black);
            pos.x += 1f;
            pos.y += 1f;
            Color color = Color.green;
            if (health <= 50f)
            {
                color = Color.yellow;
            }
            if (health <= 25f)
            {
                color = Color.red;
            }
            BoxRect(new Rect(pos.x, pos.y, width * (health / maxHealth * 100f) / 100f - 2f, height - 2f), color);
        }

        public static void CornerBox(Vector2 headPosition, float width, float height, Color color, bool outline)
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
    }
}
