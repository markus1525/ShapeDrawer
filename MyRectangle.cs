using System;
using System.IO;
using SplashKitSDK;

namespace ShapeDrawer;

public class MyRectangle : Shape
{
    // Fields
    private int _width;
    private int _height;

    // Default constructor
    public MyRectangle() : this(Color.Green, 0.0f, 0.0f, 181, 181)
    {
        // Using 181 (100 + 81, where 81 is the last two digits based on the original Shape constructor)
    }

    // Overloaded constructor
    public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
    {
        X = x;
        Y = y;
        _width = width;
        _height = height;
    }

    // Properties
    public int Width
    {
        get { return _width; }
        set { _width = value; }
    }

    public int Height
    {
        get { return _height; }
        set { _height = value; }
    }

    // Override Draw method
    public override void Draw()
    {
        if (Selected)
        {
            DrawOutline();
        }
        SplashKit.FillRectangle(Color, X, Y, _width, _height);
    }

    // Override DrawOutline method
    public override void DrawOutline()
    {
        // The outline is 6 pixels wider on all sides
        SplashKit.DrawRectangle(Color.Black, X - 6, Y - 6, _width + 12, _height + 12);
    }

    // Override IsAt method
    public override bool IsAt(Point2D pt)
    {
        return pt.X >= X && pt.X <= (X + _width) &&
               pt.Y >= Y && pt.Y <= (Y + _height);
    }

    // Step 6: Override SaveTo method
    public override void SaveTo(StreamWriter writer)
    {
        writer.WriteLine("Rectangle");
        base.SaveTo(writer);
        writer.WriteLine(Width);
        writer.WriteLine(Height);
    }

    // Step 14: Override LoadFrom method
    public override void LoadFrom(StreamReader reader)
    {
        base.LoadFrom(reader);
        Width = reader.ReadInteger();
        Height = reader.ReadInteger();
    }
}