using System;
using System.IO;
using SplashKitSDK;

namespace ShapeDrawer;

public class MyCircle : Shape
{
    // Fields
    private int _radius;

    // Default constructor
    public MyCircle() : this(Color.Blue, 0.0f, 0.0f, 131)
    {
        // Using 131 (50 + 81, where 81 is the last two digits)
    }

    // Overloaded constructor
    public MyCircle(Color color, float x, float y, int radius) : base(color)
    {
        X = x;
        Y = y;
        _radius = radius;
    }

    // Properties
    public int Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

    // Override Draw method
    public override void Draw()
    {
        if (Selected)
        {
            DrawOutline();
        }
        SplashKit.FillCircle(Color, X, Y, _radius);
    }

    // Override DrawOutline method
    public override void DrawOutline()
    {
        // Draw a black circle with a radius 2 pixels larger
        SplashKit.DrawCircle(Color.Black, X, Y, _radius + 2);
    }

    // Override IsAt method
    public override bool IsAt(Point2D pt)
    {
        // Check if the point is within the circle using SplashKit's helper method
        return SplashKit.PointInCircle(pt, SplashKit.CircleAt(X, Y, _radius));
    }

    // Step 7: Override SaveTo method
    public override void SaveTo(StreamWriter writer)
    {
        writer.WriteLine("Circle");
        base.SaveTo(writer);
        writer.WriteLine(Radius);
    }

    // Step 15: Override LoadFrom method
    public override void LoadFrom(StreamReader reader)
    {
        base.LoadFrom(reader);
        Radius = reader.ReadInteger();
    }
}