using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameBase
{
    public class PixelCollider
    {
        public bool CheckIntersection(Rectangle Rect1, Rectangle Rect2)
        {
            if (Rect1.Intersects(Rect2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IntersectPixels(Matrix transformA, int widthA, int heightA, Color[] dataA, Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);


            for (int yA = 0; yA < heightA; yA++)
            {

                Vector2 posInB = yPosInB;

                for (int xA = 0; xA < widthA; xA++)
                {

                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);


                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {

                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        if (colorA.A != 0 && colorB.A != 0)
                        {

                            return true;
                        }
                    }


                    posInB += stepX;
                }


                yPosInB += stepY;
            }

            return false;
        }
    }
}
