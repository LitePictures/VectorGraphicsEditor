using System;
using System.Drawing;

namespace SimpleVectorGraphicsEditor
{
    /// <summary>
    /// Описание класса хранения свойств для закрашивания поверхности фигуры
    /// </summary>
    [Serializable]
    public class Fill
    {
       /// <summary>
        /// Конструктор без параметров, с цветом по умолчанию
        /// </summary>
        public Fill()
        {
            Color = Color.White;
            Alpha = 255;
        }

        /// <summary>
        /// Цвет заливки
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Яркость:
        /// 0 - полностью прозрачный,
        /// 255 - полноцветный
        /// </summary>
        public int Alpha { get; set; }

        /// <summary>
        /// Свойство возвращает "кисть", настроенный по текущим свойствам fill
        /// </summary>
        /// <returns>Настроенная кисть для заполнения контура фигуры</returns>
        public Brush UpdateBrush(Brush brush)
        {
            if (brush == null)
                throw new ArgumentNullException();
            var solidBrush = brush as SolidBrush;
            if (solidBrush != null)
                solidBrush.Color = Color.FromArgb(Alpha, Color);
            return brush; 
        }
    }
}
