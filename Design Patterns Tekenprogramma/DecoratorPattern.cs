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
        private static MainWindow myWin = (MainWindow)Application.Current.MainWindow;
        private MyShape decoratedMyShape;
        private string position;
        private string text;
        public OrnamentShapeDecorator(MyShape decoratedMyShape, string position, string text)
        {
            this.decoratedMyShape = decoratedMyShape;
            this.text = text;
            this.position = position;
            AddOrnament();
        }
        TextBlock textBlock = new TextBlock();
        public void AddOrnament()
        {
            
            textBlock.Text = text;
            textBlock.FontSize = 15;
            textBlock.Background = Brushes.AntiqueWhite;

            switch (position)
            {
                case "top":
                    Canvas.SetTop(textBlock, Canvas.GetTop(decoratedMyShape.GetShape()) - MeasureString(text).Height);
                    Canvas.SetLeft(textBlock, Canvas.GetLeft(decoratedMyShape.GetShape()) + decoratedMyShape.w/2 - MeasureString(text).Width/2);
                    break;
                case "left":
                    Canvas.SetTop(textBlock, Canvas.GetTop(decoratedMyShape.GetShape()) + decoratedMyShape.h/2 - MeasureString(text).Height/2);
                    Canvas.SetLeft(textBlock, Canvas.GetLeft(decoratedMyShape.GetShape()) - MeasureString(text).Width);
                    break;
                case "bottom":
                    Canvas.SetTop(textBlock, Canvas.GetTop(decoratedMyShape.GetShape()) + decoratedMyShape.h);
                    Canvas.SetLeft(textBlock, Canvas.GetLeft(decoratedMyShape.GetShape()) + decoratedMyShape.w / 2 - MeasureString(text).Width / 2);
                    break;
                case "right":
                    Canvas.SetTop(textBlock, Canvas.GetTop(decoratedMyShape.GetShape()) + decoratedMyShape.h / 2 - MeasureString(text).Height / 2);
                    Canvas.SetLeft(textBlock, Canvas.GetLeft(decoratedMyShape.GetShape()) + decoratedMyShape.w);
                    break;
            }

            myWin.canvas.Children.Add(textBlock);
            decoratedMyShape.AddDecorator(this);
        }
        public void RemoveOrnament()
        {
            myWin.canvas.Children.Remove(textBlock);
            decoratedMyShape.RemoveDecorator(this);
        }

        public void MoveOrnament()
        {
            RemoveOrnament();
            AddOrnament();
        }

        public void UndoOrnament()
        {
            RemoveOrnament();

            textBlock.Text = text;
            textBlock.FontSize = 15;
            textBlock.Background = Brushes.AntiqueWhite;

            switch (position)
            {
                case "top":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves-1].Y - MeasureString(text).Height);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves-1].X + decoratedMyShape.w / 2 - MeasureString(text).Width / 2);
                    break;
                case "left":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves - 1].Y + decoratedMyShape.h / 2 - MeasureString(text).Height / 2);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves - 1].X - MeasureString(text).Width);
                    break;
                case "bottom":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves - 1].Y + decoratedMyShape.h);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves - 1].X + decoratedMyShape.w / 2 - MeasureString(text).Width / 2);
                    break;
                case "right":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves - 1].Y + decoratedMyShape.h / 2 - MeasureString(text).Height / 2);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves - 1].X + decoratedMyShape.w);
                    break;
            }

            myWin.canvas.Children.Add(textBlock);
            decoratedMyShape.AddDecorator(this);
        }
        public void RedoOrnament()
        {
            textBlock.Text = text;
            textBlock.FontSize = 15;
            textBlock.Background = Brushes.AntiqueWhite;

            switch (position)
            {
                case "top":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves].Y - MeasureString(text).Height);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves].X + decoratedMyShape.w / 2 - MeasureString(text).Width / 2);
                    break;
                case "left":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves].Y + decoratedMyShape.h / 2 - MeasureString(text).Height / 2);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves].X - MeasureString(text).Width);
                    break;
                case "bottom":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves].Y + decoratedMyShape.h);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves].X + decoratedMyShape.w / 2 - MeasureString(text).Width / 2);
                    break;
                case "right":
                    Canvas.SetTop(textBlock, decoratedMyShape.points[decoratedMyShape.moves].Y + decoratedMyShape.h / 2 - MeasureString(text).Height / 2);
                    Canvas.SetLeft(textBlock, decoratedMyShape.points[decoratedMyShape.moves].X + decoratedMyShape.w);
                    break;
            }

            myWin.canvas.Children.Add(textBlock);
            decoratedMyShape.AddDecorator(this);
        }


        private Size MeasureString(string candidate)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width, formattedText.Height);
        }
    }
    public class RedFillShapeDecorator
    {
        MyShape decoratedMyShape;
        public RedFillShapeDecorator(MyShape decoratedMyShape)
        {
            this.decoratedMyShape = decoratedMyShape;
            SetRedFill();
        }

        private void SetRedFill()
        {
            decoratedMyShape.GetShape().Fill = Brushes.Red;
        }

    }
}
