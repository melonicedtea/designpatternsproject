using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Design_Patterns_Tekenprogramma
{
    public class OrnamentShapeDecorator
    {
        static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        MyShape decoratedMyShape;
        string text;
        public OrnamentShapeDecorator(MyShape decoratedMyShape, string position, string text)
        {
            this.decoratedMyShape = decoratedMyShape;
            this.text = text;
            SetOrnament();
        }
        TextBlock textBlock = new TextBlock();
        private void SetOrnament()
        {
            
            textBlock.Text = text;
            textBlock.FontSize = 15;
            textBlock.Background = Brushes.AntiqueWhite;
            string pos = "top";
            switch (pos)
            {
                case "top":
                    Canvas.SetTop(textBlock, Canvas.GetTop(decoratedMyShape.GetShape()) - MeasureString(text).Height);
                    Canvas.SetLeft(textBlock, Canvas.GetLeft(decoratedMyShape.GetShape()));
                    break;
                case "left":
                    Canvas.SetTop(textBlock, decoratedMyShape.GetStartPoint().Y);
                    Canvas.SetLeft(textBlock, decoratedMyShape.GetStartPoint().X - MeasureString(text).Width);
                    break;
                case "bottom":
                    Canvas.SetTop(textBlock, decoratedMyShape.GetStartPoint().Y );
                    Canvas.SetLeft(textBlock, decoratedMyShape.GetStartPoint().X);
                    break;
                case "right":
                    Canvas.SetTop(textBlock, decoratedMyShape.GetStartPoint().Y );
                    Canvas.SetLeft(textBlock, decoratedMyShape.GetStartPoint().X);
                    break;
            }

            myWin.canvas.Children.Add(textBlock);
        }
        private Size MeasureString(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(this.textBlock.FontFamily, this.textBlock.FontStyle, this.textBlock.FontWeight, this.textBlock.FontStretch),
                this.textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
}
