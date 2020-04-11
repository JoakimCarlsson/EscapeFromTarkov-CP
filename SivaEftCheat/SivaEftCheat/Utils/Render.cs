using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SivaEftCheat.Utils
{
    class Render
    {
        public static Texture2D _currentTexture = null;
        public static Color _currentTextureColor = Color.black;
        private static Texture2D _texture;
        private static Texture2D _boxTexture;

        private static readonly GUIStyle _espLabelStyle = new GUIStyle
        {
            fontSize = 11,
        };

        //FIX ME
        public static void DrawTextOutline(Vector2 position, string text, Color outColor, Color inColor)
        {
            GUIStyle espLabelStyle = _espLabelStyle;
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
                if (_currentTexture == null)
                {
                    _currentTexture = new Texture2D(1, 1);
                    _currentTexture.SetPixel(0, 0, color);
                    _currentTexture.Apply();
                    _currentTextureColor = color;
                }
                else if (color != _currentTextureColor)
                {
                    _currentTexture.SetPixel(0, 0, color);
                    _currentTexture.Apply();
                    _currentTextureColor = color;
                }
                GUI.DrawTexture(rect, _currentTexture);
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
                if (!_texture)
                {
                    _texture = new Texture2D(1, 1)
                    {
                        filterMode = 0
                    };
                }
                Matrix4x4 matrix = GUI.matrix;
                Color color2 = GUI.color;
                GUI.color = color;
                Vector2 vector = lineEnd - lineStart;
                float num = (float)(57.29577951308232 * (double)Mathf.Atan(vector.y / vector.x));
                if (vector.x < 0f)
                {
                    num += 180f;
                }
                if (thickness < 1)
                {
                    thickness = 1;
                }
                int num2 = (int)Mathf.Ceil((float)(thickness / 2));
                GUIUtility.RotateAroundPivot(num, lineStart);
                GUI.DrawTexture(new Rect(lineStart.x, lineStart.y - (float)num2, vector.magnitude, (float)thickness), _texture);
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
            if (!_boxTexture)
            {
                _boxTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            }
            _boxTexture.SetPixel(0, 0, color);
            _boxTexture.SetPixel(1, 0, color);
            _boxTexture.SetPixel(0, 1, color);
            _boxTexture.SetPixel(1, 1, color);
            _boxTexture.Apply();

            RectOutlined(x - width / 2f, y - height, width, height, _boxTexture, thickness);
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
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y - 1f, (float)(num + 2), 3f, Color.black);
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y - 1f, 3f, (float)(num2 + 2), Color.black);
                RectFilled(headPosition.x + width / 2f - (float)num - 1f, headPosition.y - 1f, (float)(num + 2), 3f, Color.black);
                RectFilled(headPosition.x + width / 2f - 1f, headPosition.y - 1f, 3f, (float)(num2 + 2), Color.black);
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y + height - 4f, (float)(num + 2), 3f, Color.black);
                RectFilled(headPosition.x - width / 2f - 1f, headPosition.y + height - (float)num2 - 4f, 3f, (float)(num2 + 2), Color.black);
                RectFilled(headPosition.x + width / 2f - (float)num - 1f, headPosition.y + height - 4f, (float)(num + 2), 3f, Color.black);
                RectFilled(headPosition.x + width / 2f - 1f, headPosition.y + height - (float)num2 - 4f, 3f, (float)(num2 + 3), Color.black);
            }
            RectFilled(headPosition.x - width / 2f, headPosition.y, (float)num, 1f, color);
            RectFilled(headPosition.x - width / 2f, headPosition.y, 1f, (float)num2, color);
            RectFilled(headPosition.x + width / 2f - (float)num, headPosition.y, (float)num, 1f, color);
            RectFilled(headPosition.x + width / 2f, headPosition.y, 1f, (float)num2, color);
            RectFilled(headPosition.x - width / 2f, headPosition.y + height - 3f, (float)num, 1f, color);
            RectFilled(headPosition.x - width / 2f, headPosition.y + height - (float)num2 - 3f, 1f, (float)num2, color);
            RectFilled(headPosition.x + width / 2f - (float)num, headPosition.y + height - 3f, (float)num, 1f, color);
            RectFilled(headPosition.x + width / 2f, headPosition.y + height - (float)num2 - 3f, 1f, (float)(num2 + 1), color);
        }

        private static Color textureColor;
        private static Texture2D texture = new Texture2D(1, 1);

        public static void RectFilled(float x, float y, float width, float height, Color color)
        {
            if (color != textureColor)
            {
                textureColor = color;
                texture.SetPixel(0, 0, color);
                texture.Apply();
            }
            GUI.DrawTexture(new Rect(x, y, width, height), texture);
        }
    }
}
