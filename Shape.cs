using System;
using System.IO;
using SplashKitSDK;

namespace ShapeDrawer;

public abstract class Shape
{
    // Fields
    private Color _color;
    private float _x;
    private float _y;
    private bool _selected;

    // Default constructor
    public Shape() : this(Color.Yellow)
    {
    }

    // Overloaded constructor that takes color as argument
    public Shape(Color color)
    {
        _color = color;
        _x = 0.0f;
        _y = 0.0f;
        _selected = false;
    }

    // Properties
    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public float X
    {
        get { return _x; }
        set { _x = value; }
    }

    public float Y
    {
        get { return _y; }
        set { _y = value; }
    }

    public bool Selected
    {
        get { return _selected; }
        set { _selected = value; }
    }

    // Abstract methods - must be implemented by derived classes
    public abstract void Draw();
    public abstract void DrawOutline();
    public abstract bool IsAt(Point2D pt);

    // Step 5: Virtual SaveTo method - can be overridden by derived classes
    public virtual void SaveTo(StreamWriter writer)
    {
        writer.WriteColor(Color);
        writer.WriteLine(X);
        writer.WriteLine(Y);
    }

    // Step 13: Virtual LoadFrom method - can be overridden by derived classes
    public virtual void LoadFrom(StreamReader reader)
    {
        Color = reader.ReadColor();
        X = reader.ReadSingle();
        Y = reader.ReadSingle();
    }
}