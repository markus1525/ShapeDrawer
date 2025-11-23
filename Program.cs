using System;
using System.Collections.Generic;
using SplashKitSDK;

namespace ShapeDrawer;

public class Program
{
    // Private enumeration for shape kinds
    private enum ShapeKind
    {
        Rectangle,
        Circle,
        Line
    }

    public static void Main()
    {
        Window window = new Window("Shape Drawing - Final Custom Program", 800, 600);

        // Create a new Drawing object
        Drawing myDrawing = new Drawing();

        // Variable to track which kind of shape to add
        ShapeKind kindToAdd = ShapeKind.Circle;

        // Number of parallel lines (last digit of student ID is 1, so X=1)
        int parallelLines = 1;

        // File path for saving/loading
        // string filePath = "/Users/minthukyawkhaung/Desktop/TestDrawing.txt"; // Mac
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "TestDrawing.txt"); //Cross-platform

        string statusMessage = "";

        do
        {
            SplashKit.ProcessEvents();
            SplashKit.ClearScreen();

            // Check for R key to select Rectangle
            if (SplashKit.KeyTyped(KeyCode.RKey))
            {
                kindToAdd = ShapeKind.Rectangle;
            }

            // Check for C key to select Circle
            if (SplashKit.KeyTyped(KeyCode.CKey))
            {
                kindToAdd = ShapeKind.Circle;
            }

            // Check for L key to select Line
            if (SplashKit.KeyTyped(KeyCode.LKey))
            {
                kindToAdd = ShapeKind.Line;
            }


            // Option 1 & 2 - Feature 1: Press N to draw random shapes
            if (SplashKit.KeyTyped(KeyCode.NKey))
            {
                myDrawing.DrawRandomShapes();

                statusMessage = "Random Shapes Drawn.(Option 1 + 2)";
            }

            // Option 1 - Feature 2: Press M to draw letter M
            if (SplashKit.KeyTyped(KeyCode.MKey))
            {
                myDrawing.DrawLetterM();

                statusMessage = "Letter M Drawn.(Option 1)";
            }

            // Option 2 - Feature 2: Press F to draw first name MIN THU
            if (SplashKit.KeyTyped(KeyCode.FKey))
            {
                myDrawing.DrawFirstName();

                statusMessage = "My First Name Drawn.(Option 2)";
            }

            // Option 1 - Feature 3: Press K to change all colors
            if (SplashKit.KeyTyped(KeyCode.KKey))
            {
                myDrawing.ChangeAllColorsRandom();

                statusMessage = "All Shapes Colors Changed.(Option 1)";
            }

            // Option 2 - Feature 3: Press D to scale down all shapes
            if (SplashKit.KeyTyped(KeyCode.DKey))
            {
                myDrawing.ScaleDownShapes();

                statusMessage = "All Shapes Scaled Down.(Option 2)";
            }

            // Additional - Press U to scale up all shapes
            if (SplashKit.KeyTyped(KeyCode.UKey))
            {
                myDrawing.ScaleUpShapes();

                statusMessage = "All Shapes Scaled Up.(Additional)";
            }

            // Additional - Press A to select all shapes
            if (SplashKit.KeyTyped(KeyCode.AKey))
            {
                myDrawing.SelectAll();

                statusMessage = "All Shapes Selected.(Additional)";
            }


            // Check for S key to save
            if (SplashKit.KeyTyped(KeyCode.SKey))
            {
                try
                {
                    myDrawing.Save(filePath);
                    Console.WriteLine("Drawing saved successfully to: " + filePath);
                    statusMessage = "Drawing Saved to File.";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Error saving file: {0}", e.Message);
                }
            }

            // Check for O key to open/load
            if (SplashKit.KeyTyped(KeyCode.OKey))
            {
                try
                {
                    myDrawing.Load(filePath);
                    Console.WriteLine("Drawing loaded successfully from: " + filePath);
                    statusMessage = "Drawing Loaded from File.";
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine("Error loading file: {0}", e.Message);
                }
            }

            // Check if left mouse button is clicked
            if (SplashKit.MouseClicked(MouseButton.LeftButton))
            {
                // Get mouse position
                float mouseX = SplashKit.MouseX();
                float mouseY = SplashKit.MouseY();

                // Declare myShape variable
                Shape? myShape = null;

                if (kindToAdd == ShapeKind.Rectangle)
                {
                    myShape = new MyRectangle();
                }
                else if (kindToAdd == ShapeKind.Circle)
                {
                    myShape = new MyCircle();
                }
                else  // Line
                {
                    // Draw parallelLines number of lines at the same time
                    for (int i = 0; i < parallelLines; i++)
                    {
                        Shape lineShape = new MyLine(Color.Red, mouseX, mouseY + (i * 10), mouseX + 100, mouseY + (i * 10));
                        myDrawing.AddShape(lineShape);
                    }
                }

                // Add the shape to the drawing
                if (myShape != null)
                {
                    myShape.X = mouseX;
                    myShape.Y = mouseY;
                    myDrawing.AddShape(myShape);
                }
                // Clear message when shape is added
                statusMessage = "";
            }

            // Check if spacebar is pressed
            if (SplashKit.KeyTyped(KeyCode.SpaceKey))
            {
                // Change the background color to a new random color
                myDrawing.Background = SplashKit.RandomColor();

                // Clear message when background color is changed
                statusMessage = "";
            }

            // Check if right mouse button is clicked
            if (SplashKit.MouseClicked(MouseButton.RightButton))
            {
                // Get current mouse position
                Point2D mousePos = SplashKit.MousePosition();
                // Tell myDrawing to SelectShapesAt the current mouse pointer position
                myDrawing.SelectShapesAt(mousePos);
            }

            // Check if Delete key or Backspace key is pressed
            if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
            {
                // Get all selected shapes and remove them from the drawing
                List<Shape> selectedShapes = myDrawing.SelectedShapes;
                foreach (Shape shape in selectedShapes)
                {
                    myDrawing.RemoveShape(shape);
                }

                // Clear message when shapes are deleted
                statusMessage = "";
            }

            // Tell myDrawing to Draw
            myDrawing.Draw();

            // Display status message if not empty
            if (statusMessage != "")
            {
                int textWidth = statusMessage.Length * 8;
                int xPosition = (800 - textWidth) / 2;
                int yPosition = 30;

                SplashKit.FillRectangle(Color.RGBAColor(255, 255, 255, 200), xPosition - 15, yPosition - 8, textWidth + 30, 25);
                SplashKit.DrawRectangle(Color.Black, xPosition - 15, yPosition - 8, textWidth + 30, 25);
                SplashKit.DrawText(statusMessage, Color.SwinburneRed, xPosition, yPosition);
            }

            // Draw instructions box in top-right corner
            int boxX = 580;
            int boxY = 10;
            int boxWidth = 210;
            int boxHeight = 260;

            // Draw semi-transparent background box
            SplashKit.FillRectangle(Color.RGBAColor(255, 255, 255, 200), boxX, boxY, boxWidth, boxHeight);
            SplashKit.DrawRectangle(Color.Black, boxX, boxY, boxWidth, boxHeight);

            // Draw instructions text
            SplashKit.DrawText("CONTROLS", Color.SwinburneRed, "Arial", 14, boxX + 60, boxY + 8);
            SplashKit.DrawText("R - Rectangle", Color.Black, boxX + 10, boxY + 30);
            SplashKit.DrawText("C - Circle", Color.Black, boxX + 10, boxY + 48);
            SplashKit.DrawText("L - Line", Color.Black, boxX + 10, boxY + 66);
            SplashKit.DrawText("N - Random Shapes", Color.Black, boxX + 10, boxY + 84);
            SplashKit.DrawText("M - Draw Letter M", Color.Black, boxX + 10, boxY + 102);
            SplashKit.DrawText("F - Draw First Name", Color.Black, boxX + 10, boxY + 120);
            SplashKit.DrawText("K - Change Colors", Color.Black, boxX + 10, boxY + 138);
            SplashKit.DrawText("D - Scale Down", Color.Black, boxX + 10, boxY + 156);
            SplashKit.DrawText("U - Scale Up", Color.Black, boxX + 10, boxY + 174);
            SplashKit.DrawText("A - Select All", Color.Black, boxX + 10, boxY + 192);
            SplashKit.DrawText("S - Save", Color.Black, boxX + 10, boxY + 210);
            SplashKit.DrawText("O - Load", Color.Black, boxX + 10, boxY + 228);
            SplashKit.DrawText("Space - Background", Color.Black, boxX + 10, boxY + 246);

            // Draw student info box at bottom-left corner
            int infoBoxX = 10;
            int infoBoxY = 540;
            int infoBoxWidth = 270;
            int infoBoxHeight = 50;

            // Draw semi-transparent background box
            SplashKit.FillRectangle(Color.RGBAColor(255, 255, 255, 200), infoBoxX, infoBoxY, infoBoxWidth, infoBoxHeight);
            SplashKit.DrawRectangle(Color.Black, infoBoxX, infoBoxY, infoBoxWidth, infoBoxHeight);

            // Draw student information text
            SplashKit.DrawText("Min Thu Kyaw Khaung (Markus)", Color.DarkBlue, infoBoxX + 10, infoBoxY + 10);
            SplashKit.DrawText("SUT ID: 105684881 | Section: C1", Color.DarkGoldenrod, infoBoxX + 10, infoBoxY + 28);

            SplashKit.RefreshScreen();

        } while (!window.CloseRequested);
    }
}