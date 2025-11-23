# UML Report for ShapeDrawing Application

**Student Name:** Min Thu Kyaw Khaung (Markus)
**Student ID:** 105684881
**Feature Documented:** Feature 3 - Scale Down All Shapes

---

## 1. Introduction

This report presents the UML diagrams for the ShapeDrawing application. The application is built using object-oriented programming principles in C# with the SplashKit library. This report focuses on Feature 3, which allows users to scale down all shapes on the canvas by pressing the 'D' key.

The report includes two main diagrams:
- Class Diagram: Shows the structure and relationships between classes
- Sequence Diagram: Shows how objects interact when scaling down shapes

---

## 2. UML Class Diagram

The class diagram below shows the main classes in the application and their relationships.

```
┌─────────────────────────────┐
│         Program             │
├─────────────────────────────┤
│ - enum ShapeKind            │
├─────────────────────────────┤
│ + Main() : void             │
└──────────┬──────────────────┘
           │
           │ uses
           │
           ▼
┌─────────────────────────────┐
│         Drawing             │
├─────────────────────────────┤
│ - _shapes : List<Shape>     │
│ - _background : Color       │
├─────────────────────────────┤
│ + Drawing()                 │
│ + Drawing(Color)            │
│ + Draw() : void             │
│ + AddShape(Shape) : void    │
│ + RemoveShape(Shape) : void │
│ + ScaleDownShapes() : void  │
│ + ScaleUpShapes() : void    │
│ + SelectShapesAt(Point2D)   │
│ + Save(string) : void       │
│ + Load(string) : void       │
├─────────────────────────────┤
│ + SelectedShapes : List     │
│ + ShapeCount : int          │
│ + Background : Color        │
│ + AllShapes : List<Shape>   │
└──────────┬──────────────────┘
           │
           │ contains
           │ 0..*
           ▼
┌─────────────────────────────┐
│      Shape (abstract)       │
├─────────────────────────────┤
│ - _color : Color            │
│ - _x : float                │
│ - _y : float                │
│ - _selected : bool          │
├─────────────────────────────┤
│ + Shape()                   │
│ + Shape(Color)              │
│ + Draw() : void             │
│ + DrawOutline() : void      │
│ + IsAt(Point2D) : bool      │
│ + SaveTo(StreamWriter)      │
│ + LoadFrom(StreamReader)    │
├─────────────────────────────┤
│ + Color : Color             │
│ + X : float                 │
│ + Y : float                 │
│ + Selected : bool           │
└──────────┬──────────────────┘
           │
           │ inherits
      ┌────┴────┬────────┐
      │         │        │
      ▼         ▼        ▼
┌──────────┐ ┌────────┐ ┌──────────┐
│MyRectangle│ │MyCircle│ │  MyLine  │
├──────────┤ ├────────┤ ├──────────┤
│- _width  │ │- _radius│ │- _endX   │
│- _height │ └────────┘ │- _endY   │
└──────────┘            └──────────┘
│+ Width   │ │+ Radius │ │+ EndX    │
│+ Height  │ └────────┘ │+ EndY    │
└──────────┘            └──────────┘
```

### Class Relationships

**Inheritance:**
- MyRectangle, MyCircle, and MyLine all inherit from the abstract Shape class
- This shows polymorphism where different shape types can be treated uniformly

**Association:**
- Program uses Drawing to manage the application
- Drawing contains multiple Shape objects stored in a list
- The multiplicity is 0..* meaning Drawing can have zero or more shapes

**Abstraction:**
- Shape is an abstract class that defines common properties and methods
- Subclasses must implement abstract methods like Draw(), DrawOutline(), and IsAt()

---

## 3. Sequence Diagram for Feature 3

This sequence diagram shows what happens when a user presses the 'D' key to scale down all shapes.

```
User          Program       Drawing       Shape       MyRectangle   MyCircle   MyLine
 |               |             |            |              |            |         |
 |--Press 'D'-->|             |            |              |            |         |
 |               |             |            |              |            |         |
 |               |--KeyTyped(DKey)         |              |            |         |
 |               |             |            |              |            |         |
 |               |--ScaleDownShapes()----->|              |            |         |
 |               |             |            |              |            |         |
 |               |             |--loop[for each shape]    |            |         |
 |               |             |            |              |            |         |
 |               |             |--is MyRectangle?-------->|            |         |
 |               |             |            |              |            |         |
 |               |             |--Width * 0.8------------>|            |         |
 |               |             |            |              |            |         |
 |               |             |--Height * 0.8----------->|            |         |
 |               |             |            |              |            |         |
 |               |             |--is MyCircle?------------------------>|         |
 |               |             |            |              |            |         |
 |               |             |--Radius * 0.8------------------------>|         |
 |               |             |            |              |            |         |
 |               |             |--is MyLine?------------------------------------>|
 |               |             |            |              |            |         |
 |               |             |--Calculate deltaX, deltaY--------------------- >|
 |               |             |            |              |            |         |
 |               |             |--EndX = X + deltaX * 0.8----------------------->|
 |               |             |            |              |            |         |
 |               |             |--EndY = Y + deltaY * 0.8----------------------->|
 |               |             |            |              |            |         |
 |               |             |<-end loop--|              |            |         |
 |               |             |            |              |            |         |
 |               |<--return----|            |              |            |         |
 |               |             |            |              |            |         |
 |               |--Draw()---->|            |              |            |         |
 |               |             |            |              |            |         |
 |               |             |--Draw() on each shape--->|            |         |
 |               |             |            |              |            |         |
 |<--Display-----|             |            |              |            |         |
 |  Updated      |             |            |              |            |         |
 |  Canvas       |             |            |              |            |         |
```

### Sequence Flow Explanation

**Step 1: User Input**
- The user presses the 'D' key on the keyboard
- Program detects this using SplashKit.KeyTyped(KeyCode.DKey)

**Step 2: Method Call**
- Program calls the ScaleDownShapes() method on the Drawing object
- This method handles the scaling logic for all shapes

**Step 3: Loop Through Shapes**
- Drawing iterates through each shape in the _shapes list
- For each shape, it checks the type using the 'is' operator

**Step 4: Type-Specific Scaling**
- If the shape is a MyRectangle, it reduces Width and Height by 20% (multiplies by 0.8)
- If the shape is a MyCircle, it reduces Radius by 20%
- If the shape is a MyLine, it calculates the difference between start and end points, then scales the line length by 20%

**Step 5: Display Updated Shapes**
- After all shapes are scaled, the Drawing object draws all shapes
- The user sees the updated canvas with smaller shapes

---

## 4. Object Collaboration Analysis

### How Objects Work Together

**Program Object:**
- Acts as the controller
- Detects user input and triggers actions
- Delegates shape management to the Drawing object

**Drawing Object:**
- Manages the collection of all shapes
- Implements the scaling logic
- Uses polymorphism to handle different shape types

**Shape Objects:**
- Each shape type (MyRectangle, MyCircle, MyLine) stores its own properties
- The Drawing object modifies these properties directly
- Each shape can draw itself when requested

### Polymorphism in Action

The ScaleDownShapes() method shows polymorphism because:
- It works with a list of Shape objects
- At runtime, it determines the actual type of each shape
- Each shape type has different properties that need scaling
- The method handles each type appropriately using type checking

### Encapsulation

The implementation uses encapsulation:
- Shape properties are private with public getters and setters
- The Drawing class hides the internal list of shapes
- Only necessary methods are exposed to the Program class

---

## 5. Design Patterns and Principles

### Object-Oriented Principles Used

**Inheritance:**
- All shape classes inherit from the abstract Shape class
- Common properties like Color, X, Y, and Selected are defined once in the base class

**Polymorphism:**
- Different shapes can be stored in the same list
- Type-specific behavior is handled at runtime

**Encapsulation:**
- Private fields with public properties control access to data
- Implementation details are hidden from other classes

**Abstraction:**
- The Shape class defines the interface that all shapes must follow
- Abstract methods ensure all shape types implement required functionality

---

## 6. Conclusion

The UML diagrams show how the ShapeDrawing application uses object-oriented principles to implement the scale down feature. The class diagram displays the structure and relationships between classes, while the sequence diagram shows the dynamic interaction when scaling shapes.

Key points:
- The application uses inheritance to create a flexible shape hierarchy
- Polymorphism allows different shape types to be managed uniformly
- The Drawing class acts as a container and manager for all shapes
- Type checking at runtime enables appropriate scaling for each shape type

This design makes the application easy to extend with new shape types or new features in the future.
