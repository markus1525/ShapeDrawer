using System;
using System.Collections.Generic;
using System.IO;
using SplashKitSDK;

namespace ShapeDrawer
{

    public class Drawing
    {
        // Private fields
        private readonly List<Shape> _shapes;
        private Color _background;

        // Constructor
        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
        }

        // Default constructor using Color.White
        public Drawing() : this(Color.White)
        {
            // other steps could go here…
        }

        //Properties
        public List<Shape> SelectedShapes
        {
            get
            {
                List<Shape> result = new List<Shape>();
                foreach (Shape s in _shapes)
                {
                    if (s.Selected)
                    {
                        result.Add(s);
                    }
                }
                return result;
            }
        }

        public int ShapeCount
        {
            get { return _shapes.Count; }
        }

        public Color Background
        {
            get { return _background; }
            set { _background = value; }
        }

        public List<Shape> AllShapes
        {
            get { return _shapes; }
        }

        //Methods
        public void Draw()
        {
            SplashKit.ClearScreen(_background);
            foreach (Shape s in _shapes)
            {
                s.Draw();
            }
        }

        // SelectShapesAt method that selects/deselects shapes at given point
        public void SelectShapesAt(Point2D pt)
        {
            foreach (Shape s in _shapes)
            {
                if (s.IsAt(pt))
                {
                    s.Selected = true;
                }
                else
                {
                    s.Selected = false;
                }
            }
        }

        public void AddShape(Shape s)
        {
            _shapes.Add(s);
        }

        public void RemoveShape(Shape s)
        {
            _shapes.Remove(s);
        }

        // Step 4,31: Save method to save drawing to file
        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            
            try
            {
                writer.WriteColor(Background);
                writer.WriteLine(ShapeCount);
                foreach (Shape s in _shapes)
                {
                    s.SaveTo(writer);
                }
            }
            finally
            {
                writer.Close();
            }
        }

        // Step 1,30: Load method to load drawing from file
        public void Load(string filename)
        {
            StreamReader reader = new StreamReader(filename);
            string? kind;
            Shape? s;

            try
            {
                Background = reader.ReadColor();
                int count = reader.ReadInteger();
                _shapes.Clear();

                for (int i = 0; i < count; i++)
                {
                    kind = reader.ReadLine();
                    switch (kind)
                    {
                        case "Rectangle":
                            s = new MyRectangle();
                            break;
                        case "Circle":
                            s = new MyCircle();
                            break;
                        case "Line":
                            s = new MyLine();
                            break;
                        default:
                            throw new InvalidDataException("Unknown shape kind: " + kind); // Step 28: Handle unknown shape kind
                    }

                    s.LoadFrom(reader);
                    AddShape(s);
                }
            }
            finally
            {
                reader.Close();
            }
        }

        // Option 1 & 2 - Feature 1: Draw random shapes
        public void DrawRandomShapes()
        {
            Random rnd = new Random();
            int numShapes = rnd.Next(5, 16);

            for (int i = 0; i < numShapes; i++)
            {
                int shapeType = rnd.Next(0, 3);
                Color randomColor = SplashKit.RandomColor();
                float randomX = rnd.Next(50, 700);
                float randomY = rnd.Next(50, 500);

                Shape newShape;

                if (shapeType == 0)
                {
                    newShape = new MyRectangle(randomColor, randomX, randomY, 100, 80);
                }
                else if (shapeType == 1)
                {
                    newShape = new MyCircle(randomColor, randomX, randomY, 50);
                }
                else
                {
                    newShape = new MyLine(randomColor, randomX, randomY, randomX + 100, randomY + 50);
                }

                AddShape(newShape);
            }
        }

        // Option 1 - Feature 2: Draw letter M
        public void DrawLetterM()
        {
            Color letterColor = Color.Blue;
            float startX = 300;
            float startY = 200;

            MyLine line1 = new MyLine(letterColor, startX, startY, startX, startY + 150);
            AddShape(line1);

            MyLine line2 = new MyLine(letterColor, startX, startY, startX + 50, startY + 75);
            AddShape(line2);

            MyLine line3 = new MyLine(letterColor, startX + 50, startY + 75, startX + 100, startY);
            AddShape(line3);

            MyLine line4 = new MyLine(letterColor, startX + 100, startY, startX + 100, startY + 150);
            AddShape(line4);
        }

        // Option 2 - Feature 2: Draw first name MIN THU
        public void DrawFirstName()
        {
            Color letterColor = Color.Blue;
            float startX = 50;
            float startY = 150;
            float spacing = 70;

            // Draw M
            AddShape(new MyLine(letterColor, startX, startY, startX, startY + 100));
            AddShape(new MyLine(letterColor, startX, startY, startX + 25, startY + 50));
            AddShape(new MyLine(letterColor, startX + 25, startY + 50, startX + 50, startY));
            AddShape(new MyLine(letterColor, startX + 50, startY, startX + 50, startY + 100));

            startX += spacing;

            // Draw I
            AddShape(new MyLine(letterColor, startX, startY, startX + 30, startY));
            AddShape(new MyLine(letterColor, startX + 15, startY, startX + 15, startY + 100));
            AddShape(new MyLine(letterColor, startX, startY + 100, startX + 30, startY + 100));

            startX += spacing;

            // Draw N
            AddShape(new MyLine(letterColor, startX, startY, startX, startY + 100));
            AddShape(new MyLine(letterColor, startX, startY, startX + 40, startY + 100));
            AddShape(new MyLine(letterColor, startX + 40, startY, startX + 40, startY + 100));

            startX += spacing;

            // Draw T
            AddShape(new MyLine(letterColor, startX, startY, startX + 40, startY));
            AddShape(new MyLine(letterColor, startX + 20, startY, startX + 20, startY + 100));

            startX += spacing;

            // Draw H
            AddShape(new MyLine(letterColor, startX, startY, startX, startY + 100));
            AddShape(new MyLine(letterColor, startX, startY + 50, startX + 40, startY + 50));
            AddShape(new MyLine(letterColor, startX + 40, startY, startX + 40, startY + 100));

            startX += spacing;

            // Draw U
            AddShape(new MyLine(letterColor, startX, startY, startX, startY + 100)); 
            AddShape(new MyLine(letterColor, startX, startY + 100, startX + 40, startY + 100));
            AddShape(new MyLine(letterColor, startX + 40, startY, startX + 40, startY + 100));
        }

        // Option 1 - Feature 3: Change all colors randomly
        public void ChangeAllColorsRandom()
        {
            foreach (Shape s in _shapes)
            {
                s.Color = SplashKit.RandomColor();
            }
        }

        // Option 2 - Feature 3: Scale down all shapes by 20%
        public void ScaleDownShapes()
        {
            foreach (Shape s in _shapes)
            {
                if (s is MyRectangle rect)
                {
                    rect.Width = (int)(rect.Width * 0.8);
                    rect.Height = (int)(rect.Height * 0.8);
                }
                else if (s is MyCircle circle)
                {
                    circle.Radius = (int)(circle.Radius * 0.8);
                }
                else if (s is MyLine line)
                {
                    // Scale line by moving endpoint closer to start
                    float deltaX = line.EndX - line.X;
                    float deltaY = line.EndY - line.Y;
                    line.EndX = line.X + (deltaX * 0.8f);
                    line.EndY = line.Y + (deltaY * 0.8f);
                }
            }
        }

        // Additional - Scale up all shapes by 20%
        public void ScaleUpShapes()
        {
            foreach (Shape s in _shapes)
            {
                if (s is MyRectangle rect)
                {
                    rect.Width = (int)(rect.Width * 1.2);
                    rect.Height = (int)(rect.Height * 1.2);
                }
                else if (s is MyCircle circle)
                {
                    circle.Radius = (int)(circle.Radius * 1.2);
                }
                else if (s is MyLine line)
                {
                    // Scale line by moving endpoint further from start
                    float deltaX = line.EndX - line.X;
                    float deltaY = line.EndY - line.Y;
                    line.EndX = line.X + (deltaX * 1.2f);
                    line.EndY = line.Y + (deltaY * 1.2f);
                }
            }
        }

        // Additional - Select all shapes
        public void SelectAll()
        {
            foreach (Shape s in _shapes)
            {
                s.Selected = true;
            }
        }
    }
}