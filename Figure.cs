using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimpleVectorGraphicsEditor
{
    public enum DrawingKind
    {
        Polygon,
        Polyline
    }

    /// <summary>
    /// Описание класса базовой фигуры 
    /// </summary>
    [Serializable]
    public class Figure
    {
        // контейнер для хранения точек фигуры
        protected readonly List<PointF> Points = new List<PointF>();

        /// <summary>
        ///  Конструктор без параметра с настройкой по умолчанию
        /// </summary>
        public Figure()
        {
            Stroke = new Stroke();
            Fill = new Fill();
        }

        /// <summary>
        ///  Конструктор без параметра с настройкой по умолчанию
        /// </summary>
        public Figure(DrawingKind kind, IEnumerable<PointF> points)
            : this()
        {
            Kind = kind;
            Points.AddRange(points);
        }

        /// <summary>
        /// Свойство определяющее вид фигуры
        /// </summary>
        public DrawingKind Kind { get; set; }

        /// <summary>
        /// Базовая точка, обычно левый верхний угол прямоугольника,
        /// описывающего фигуру
        /// </summary>
        public virtual PointF Location { get; set; }

        /// <summary>
        /// Размер фигуры, ширина и высота прямоугольника,
        /// описывающего фигуру
        /// </summary>
        public virtual SizeF Size { get; set; }

        /// <summary>
        /// Карандаш для рисования контура фигуры
        /// </summary>
        public Stroke Stroke { get; set; }

        /// <summary>
        /// Кисть для заливки контура фигуры
        /// </summary>
        public Fill Fill { get; set; }

        /// <summary>
        /// Метод рисования фигуры по точкам базового списка
        /// </summary>
        /// <param name="graphics">"холст" - объект для рисования</param>
        public virtual void DrawFigure(Graphics graphics)
        {
            var points = Points.ToArray();
            if (Kind == DrawingKind.Polygon)
            {
                // заливаем фон кистью
                using (var brush = new SolidBrush(Color.White))
                    graphics.FillPolygon(Fill.UpdateBrush(brush), points);
                // рисуем контур карандашом
                using (var pen = new Pen(Color.Black))
                    graphics.DrawPolygon(Stroke.UpdatePen(pen), points);
            }
            else
            {
                // рисуем контур карандашом
                using (var pen = new Pen(Color.Black))
                    graphics.DrawLines(Stroke.UpdatePen(pen), points);
            }
        }

        /// <summary>
        /// Метод проверяет принадлежность точки фигуре
        /// </summary>
        /// <param name="point">проверяемая точка</param>
        /// <returns>True - точка принадлежит фигуре</returns>
        public virtual bool PointInFigure(PointF point)
        {
            switch (Kind)
            {
                case DrawingKind.Polygon:
                    using (var gp = new GraphicsPath())
                    {
                        gp.AddPolygon(GetPoints());
                        return (gp.IsVisible(point));
                    }
                case DrawingKind.Polyline:
                    using (var gp = new GraphicsPath())
                    {
                        var ps = GetPoints();
                        for (var i = 1; i < ps.Length; i++)
                        {
                            gp.StartFigure();
                            gp.AddLine(ps[i - 1], ps[i]);
                            gp.CloseFigure();
                        }
                        using (var pen = new Pen(Color.Black, Stroke.Width * 5f))
                            return gp.IsOutlineVisible(point, pen);
                    }
            }
            return false;
        }

        /// <summary>
        /// Свойство возвращает реальный прямоугольник, охватывающий точки фигуры
        /// </summary>
        public virtual RectangleF GetBounds
        {
            get
            {
                using (var gp = new GraphicsPath())
                {
                    if (Kind == DrawingKind.Polygon)
                        gp.AddPolygon(GetPoints());
                    else
                        gp.AddLines(GetPoints());
                    var rect = gp.GetBounds();
                    // если фигура очень узкая по горизонтали
                    if (Math.Abs(rect.Width - 0) < float.Epsilon)
                    {
                        rect.X -= 2;
                        rect.Width += 4;
                    }
                    // если фигура очень узкая по вертикали
                    if (Math.Abs(rect.Height - 0) < float.Epsilon)
                    {
                        rect.Y -= 2;
                        rect.Height += 4;
                    }
                    return rect;
                }
            }
        }

        /// <summary>
        /// Метод возвращает массив точек фигуры
        /// </summary>
        /// <returns>Массив, копия списка точек фигуры</returns>
        public virtual PointF[] GetPoints()
        {   
            // возвращает массив точек линии
            var ps = new PointF[Points.Count];
            Points.CopyTo(ps);
            return ps;
        }
        
        /// <summary>
        /// Метод восстанавливает массив точек из внешнего массива
        /// </summary>
        /// <param name="ps">внешний массив точек</param>
        public virtual void SetPoints(PointF[] ps)
        {
            Points.Clear();
            Points.AddRange(ps);
        }

        /// <summary>
        /// Смещение фигуры
        /// </summary>
        /// <param name="p">величина смещения</param>
        public virtual void Offset(PointF p)
        {
            var pts = GetPoints();
            for (var i = 0; i < pts.Length; i++)
            {
                pts[i].X += p.X;
                pts[i].Y += p.Y;
            }
            SetPoints(pts);
        }

        /// <summary>
        /// Отражение сверху-вниз
        /// </summary>
        public virtual void FlipVertical()
        {
            var rect = GetBounds;
            var cx = rect.X + rect.Width * 0.5F;
            var pts = GetPoints();
            for (var i = 0; i < pts.Length; i++)
            {
                if (pts[i].X < cx)
                    pts[i].X += (cx - pts[i].X) * 2F;
                else
                    if (pts[i].X > cx)
                        pts[i].X -= (pts[i].X - cx) * 2F;
            }
            SetPoints(pts);
        }

        /// <summary>
        /// Отражение слева-направо
        /// </summary>
        public virtual void FlipHorizontal()
        {
            var rect = GetBounds;
            var cy = rect.Y + rect.Height * 0.5F;
            var pts = GetPoints();
            for (var i = 0; i < pts.Length; i++)
            {
                if (pts[i].Y < cy) pts[i].Y += (cy - pts[i].Y) * 2F;
                else if (pts[i].Y > cy) pts[i].Y -= (pts[i].Y - cy) * 2F;
            }
            SetPoints(pts);
        }

        /// <summary>
        /// Поворот на угол
        /// </summary>
        /// <param name="angle">угол поворота</param>
        public virtual void Rotate(float angle)
        {
            var rect = GetBounds;
            var cx = rect.X + rect.Width * 0.5F;
            var cy = rect.Y + rect.Height * 0.5F;
            RotateAt(angle, cx, cy);
        }

        /// <summary>
        /// Поворот вокруг точки
        /// </summary>
        /// <param name="angle">угол поворота</param>
        /// <param name="cx">центр X</param>
        /// <param name="cy">центр Y</param>
        public virtual void RotateAt(float angle, float cx, float cy)
        {
            using (var gp = new GraphicsPath())
            {
                if (Kind == DrawingKind.Polygon)
                    gp.AddPolygon(GetPoints());
                else
                    gp.AddLines(GetPoints());
                using (var m = new Matrix())
                {
                    m.RotateAt(angle, new PointF(cx, cy));
                    gp.Transform(m);
                }
                var ps = gp.PathPoints;
                SetPoints(ps);
            }
        }

    }
}
