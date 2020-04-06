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
        public static void DrawTextOutline(Vector2 position, string text, Color outColor, Color inColor, int fontSize)
        {
            GUIStyle espLabelStyle = new GUIStyle
            {
                fontSize = fontSize,
            };

            Vector2 vector = espLabelStyle.CalcSize(new GUIContent(text));
            var rect = new Rect(position.x + 1f, position.y + 1f, vector.x + 12f, vector.y + 12f);
            espLabelStyle.normal.textColor = outColor;
            GUI.Label(rect, text, espLabelStyle);
            rect.x -= 1f;
            rect.y -= 1f;
            espLabelStyle.normal.textColor = inColor;
            GUI.Label(rect, text, espLabelStyle);
        }
    }
}
