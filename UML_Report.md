# UML Report for ShapeDrawing Application

**Student Name:** Min Thu Kyaw Khaung (Markus)
**Student ID:** 105684881
**Features Documented:** All Three Features of Option 2

---

## 1. Introduction

This report presents comprehensive UML diagrams for the ShapeDrawing application implementing Option 2. The application is built using object-oriented programming principles in C# with the SplashKit library. This report documents all three features required for Option 2:

- **Feature 1:** Random Shapes - Draw random number of different shape types
- **Feature 2:** Draw Full First Name - Draw "MIN THU" on the canvas
- **Feature 3:** Scale Down All Shapes - Scale down all shapes by 20%

The report includes:
- Complete Class Diagram showing the structure and relationships between all classes
- Sequence Diagrams for all three features showing object interactions

---

## 2. UML Class Diagram

The class diagram below shows all the main classes in the application and their relationships.

```
┌─────────────────────────────────────────┐
│              Program                    │
├─────────────────────────────────────────┤
│ - enum ShapeKind                        │
│   + Rectangle                           │
│   + Circle                              │
│   + Line                                │
├─────────────────────────────────────────┤
│ + Main() : void                         │
└──────────────┬──────────────────────────┘
               │
               │ uses
               │ 1
               ▼
┌─────────────────────────────────────────┐
│             Drawing                     │
├─────────────────────────────────────────┤
│ - _shapes : List<Shape>                 │
│ - _background : Color                   │
├─────────────────────────────────────────┤
│ + Drawing()                             │
│ + Drawing(Color)                        │
│ + Draw() : void                         │
│ + AddShape(Shape) : void                │
│ + RemoveShape(Shape) : void             │
│ + SelectShapesAt(Point2D) : void        │
│ + DrawRandomShapes() : void             │
│ + DrawLetterM() : void                  │
│ + DrawFirstName() : void                │
│ + ChangeAllColorsRandom() : void        │
│ + ScaleDownShapes() : void              │
│ + ScaleUpShapes() : void                │
│ + SelectAll() : void                    │
│ + Save(string) : void                   │
│ + Load(string) : void                   │
├─────────────────────────────────────────┤
│ + SelectedShapes : List<Shape>          │
│ + ShapeCount : int                      │
│ + Background : Color                    │
│ + AllShapes : List<Shape>               │
└──────────────┬──────────────────────────┘
               │
               │ contains
               │ 0..*
               ▼
┌─────────────────────────────────────────┐
│      Shape (abstract)                   │
├─────────────────────────────────────────┤
│ - _color : Color                        │
│ - _x : float                            │
│ - _y : float                            │
│ - _selected : bool                      │
├─────────────────────────────────────────┤
│ + Shape()                               │
│ + Shape(Color)                          │
│ + Draw() : void {abstract}              │
│ + DrawOutline() : void {abstract}       │
│ + IsAt(Point2D) : bool {abstract}       │
│ + SaveTo(StreamWriter) : void {virtual} │
│ + LoadFrom(StreamReader) : void         │
├─────────────────────────────────────────┤
│ + Color : Color                         │
│ + X : float                             │
│ + Y : float                             │
│ + Selected : bool                       │
└──────────────┬──────────────────────────┘
               │
               │ inherits
       ┌───────┼────────┬────────────┐
       │       │        │            │
       ▼       ▼        ▼            ▼
┌──────────┐ ┌────────────┐ ┌──────────────┐
│MyRectangle│ │  MyCircle  │ │   MyLine     │
├──────────┤ ├────────────┤ ├──────────────┤
│- _width  │ │- _radius   │ │- _endX       │
│- _height │ │            │ │- _endY       │
├──────────┤ ├────────────┤ ├──────────────┤
│+ MyRect  │ │+ MyCircle()│ │+ MyLine()    │
│  angle() │ │+ MyCircle  │ │+ MyLine(...)│
│+ MyRect  │ │  (...)     │ │              │
│  angle   │ │+ Draw()    │ │+ Draw()      │
│  (...)   │ │+ DrawOut   │ │+ DrawOutline │
│+ Draw()  │ │  line()    │ │  ()          │
│+ DrawOut │ │+ IsAt      │ │+ IsAt(...)   │
│  line()  │ │  (...)     │ │+ SaveTo(...) │
│+ IsAt    │ │+ SaveTo    │ │+ LoadFrom    │
│  (...)   │ │  (...)     │ │  (...)       │
│+ SaveTo  │ │+ LoadFrom  │ │              │
│  (...)   │ │  (...)     │ │              │
│+ LoadFrom│ │            │ │              │
│  (...)   │ │            │ │              │
├──────────┤ ├────────────┤ ├──────────────┤
│+ Width   │ │+ Radius    │ │+ EndX        │
│+ Height  │ │            │ │+ EndY        │
└──────────┘ └────────────┘ └──────────────┘
```

### Class Relationships

**Inheritance:**
- MyRectangle, MyCircle, and MyLine all inherit from the abstract Shape class
- This demonstrates polymorphism where different shape types can be treated uniformly
- Each subclass implements abstract methods (Draw, DrawOutline, IsAt) according to their specific needs

**Association:**
- Program uses Drawing to manage the entire application
- Drawing contains multiple Shape objects stored in a List<Shape>
- The multiplicity is 0..* meaning Drawing can have zero or more shapes

**Abstraction:**
- Shape is an abstract class that defines common properties and methods
- Subclasses must implement abstract methods like Draw(), DrawOutline(), and IsAt()
- Virtual methods like SaveTo() and LoadFrom() can be optionally overridden

**Encapsulation:**
- All fields are private with controlled access through public properties
- Implementation details are hidden from other classes

---

## 3. Sequence Diagram for Feature 1: Random Shapes

This sequence diagram shows the interaction when a user presses the 'N' key to draw random shapes.

```
User      Program     Drawing    Random   MyRectangle  MyCircle   MyLine
 |           |           |          |          |           |         |
 |--Press 'N'->          |          |          |           |         |
 |           |           |          |          |           |         |
 |           |--KeyTyped(NKey)      |          |           |         |
 |           |           |          |          |           |         |
 |           |--DrawRandomShapes()->|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--new Random()       |           |         |
 |           |           |          |          |           |         |
 |           |           |--Next(5,16)-------->|          |           |         |
 |           |           |<-numShapes(e.g. 8)--|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--loop[for each shape (8 times)]           |
 |           |           |          |          |           |         |
 |           |           |--Next(0,3)--------->|          |           |         |
 |           |           |<-shapeType(0/1/2)---|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--RandomColor()       |          |           |         |
 |           |           |<-color---------------|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--Next(50,700)------->|          |           |         |
 |           |           |<-randomX-------------|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--Next(50,500)------->|          |           |         |
 |           |           |<-randomY-------------|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--if shapeType == 0  |           |         |
 |           |           |          |          |           |         |
 |           |           |--new MyRectangle(color,x,y,100,80)        |
 |           |           |          |          |           |         |
 |           |           |<-MyRectangle object--|          |           |         |
 |           |           |          |          |           |         |
 |           |           |--else if shapeType == 1         |         |
 |           |           |          |          |           |         |
 |           |           |--new MyCircle(color,x,y,50)               |
 |           |           |          |          |           |         |
 |           |           |<-MyCircle object----|-----------|         |
 |           |           |          |          |           |         |
 |           |           |--else (shapeType == 2)          |         |
 |           |           |          |          |           |         |
 |           |           |--new MyLine(color,x,y,x+100,y+50)         |
 |           |           |          |          |           |         |
 |           |           |<-MyLine object------|-----------|---------|
 |           |           |          |          |           |         |
 |           |           |--AddShape(newShape) |           |         |
 |           |           |          |          |           |         |
 |           |           |<-end loop-----------|          |           |         |
 |           |           |          |          |           |         |
 |           |<-return---|          |          |           |         |
 |           |           |          |          |           |         |
 |           |--Draw()-->|          |          |           |         |
 |           |           |          |          |           |         |
 |           |           |--Draw() on each shape                     |
 |           |           |          |          |           |         |
 |<-Display--|           |          |          |           |         |
 | Random    |           |          |          |           |         |
 | Shapes    |           |          |          |           |         |
```

### Sequence Flow Explanation for Feature 1

**Step 1: User Input**
- The user presses the 'N' key on the keyboard
- Program detects this using SplashKit.KeyTyped(KeyCode.NKey)

**Step 2: Method Call**
- Program calls the DrawRandomShapes() method on the Drawing object
- This method handles creating random shapes

**Step 3: Random Number Generation**
- Drawing creates a new Random object
- Generates a random number between 5 and 16 for the number of shapes to create

**Step 4: Loop Through Shape Creation**
- For each iteration (e.g., 8 times):
  - Generate random shape type (0, 1, or 2)
  - Generate random color using SplashKit.RandomColor()
  - Generate random X position between 50 and 700
  - Generate random Y position between 50 and 500

**Step 5: Create Shape Based on Type**
- If shapeType == 0: Create MyRectangle with dimensions 100x80
- If shapeType == 1: Create MyCircle with radius 50
- If shapeType == 2: Create MyLine from (x,y) to (x+100, y+50)

**Step 6: Add Shape to Drawing**
- Each created shape is added to the Drawing's shape list using AddShape()

**Step 7: Display Shapes**
- After all shapes are created, the Drawing object draws all shapes
- The user sees the random shapes on the canvas

---

## 4. Sequence Diagram for Feature 2: Draw Full First Name

This sequence diagram shows the interaction when a user presses the 'F' key to draw "MIN THU" on the canvas.

```
User      Program     Drawing       MyLine      MyLine      MyLine   ... (more MyLine objects)
 |           |           |             |           |           |
 |--Press 'F'->          |             |           |           |
 |           |           |             |           |           |
 |           |--KeyTyped(FKey)         |           |           |
 |           |           |             |           |           |
 |           |--DrawFirstName()------->|           |           |
 |           |           |             |           |           |
 |           |           |--Initialize startX=50, startY=150, spacing=70
 |           |           |             |           |           |
 |           |           |--Draw Letter M          |           |
 |           |           |             |           |           |
 |           |           |--new MyLine(Blue, 50,150, 50,250)   |
 |           |           |             |           |           |
 |           |           |<-Line1------|           |           |
 |           |           |             |           |           |
 |           |           |--AddShape(Line1)        |           |
 |           |           |             |           |           |
 |           |           |--new MyLine(Blue, 50,150, 75,200)   |
 |           |           |             |           |           |
 |           |           |<-Line2------|-----------|           |
 |           |           |             |           |           |
 |           |           |--AddShape(Line2)        |           |
 |           |           |             |           |           |
 |           |           |--new MyLine(Blue, 75,200, 100,150)  |
 |           |           |             |           |           |
 |           |           |<-Line3------|-----------|-----------|
 |           |           |             |           |           |
 |           |           |--AddShape(Line3)        |           |
 |           |           |             |           |           |
 |           |           |--new MyLine(Blue, 100,150, 100,250) |
 |           |           |             |           |           |
 |           |           |<-Line4------|           |           |
 |           |           |             |           |           |
 |           |           |--AddShape(Line4)        |           |
 |           |           |             |           |           |
 |           |           |--Update startX += 70 (now 120)      |
 |           |           |             |           |           |
 |           |           |--Draw Letter I          |           |
 |           |           |             |           |           |
 |           |           |--new MyLine (top horizontal)        |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (vertical)  |           |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (bottom horizontal)     |
 |           |           |--AddShape(Line)         |           |
 |           |           |             |           |           |
 |           |           |--Update startX += 70 (now 190)      |
 |           |           |             |           |           |
 |           |           |--Draw Letter N          |           |
 |           |           |             |           |           |
 |           |           |--new MyLine (left vertical)         |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (diagonal)  |           |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (right vertical)        |
 |           |           |--AddShape(Line)         |           |
 |           |           |             |           |           |
 |           |           |--Update startX += 70 (now 260)      |
 |           |           |             |           |           |
 |           |           |--Draw Letter T          |           |
 |           |           |             |           |           |
 |           |           |--new MyLine (top horizontal)        |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (vertical)  |           |
 |           |           |--AddShape(Line)         |           |
 |           |           |             |           |           |
 |           |           |--Update startX += 70 (now 330)      |
 |           |           |             |           |           |
 |           |           |--Draw Letter H          |           |
 |           |           |             |           |           |
 |           |           |--new MyLine (left vertical)         |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (middle horizontal)     |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (right vertical)        |
 |           |           |--AddShape(Line)         |           |
 |           |           |             |           |           |
 |           |           |--Update startX += 70 (now 400)      |
 |           |           |             |           |           |
 |           |           |--Draw Letter U          |           |
 |           |           |             |           |           |
 |           |           |--new MyLine (left vertical)         |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (bottom horizontal)     |
 |           |           |--AddShape(Line)         |           |
 |           |           |--new MyLine (right vertical)        |
 |           |           |--AddShape(Line)         |           |
 |           |           |             |           |           |
 |           |<-return---|             |           |           |
 |           |           |             |           |           |
 |           |--Draw()-->|             |           |           |
 |           |           |             |           |           |
 |           |           |--Draw() on each MyLine object       |
 |           |           |             |           |           |
 |<-Display--|           |             |           |           |
 | "MIN THU" |           |             |           |           |
```

### Sequence Flow Explanation for Feature 2

**Step 1: User Input**
- The user presses the 'F' key on the keyboard
- Program detects this using SplashKit.KeyTyped(KeyCode.FKey)

**Step 2: Method Call**
- Program calls the DrawFirstName() method on the Drawing object
- This method will draw "MIN THU" using MyLine shapes

**Step 3: Initialize Variables**
- Set letterColor to Blue
- Set startX = 50, startY = 150
- Set spacing = 70 (space between letters)

**Step 4: Draw Letter 'M'**
- Create 4 MyLine objects to form the letter M:
  - Line 1: Left vertical line (50,150) to (50,250)
  - Line 2: Diagonal left-to-center (50,150) to (75,200)
  - Line 3: Diagonal center-to-right (75,200) to (100,150)
  - Line 4: Right vertical line (100,150) to (100,250)
- Add each line to the Drawing using AddShape()
- Increment startX by 70

**Step 5: Draw Letter 'I'**
- Create 3 MyLine objects to form the letter I:
  - Top horizontal line
  - Center vertical line
  - Bottom horizontal line
- Add each line to the Drawing
- Increment startX by 70

**Step 6: Draw Letter 'N'**
- Create 3 MyLine objects to form the letter N:
  - Left vertical line
  - Diagonal line from bottom-left to top-right
  - Right vertical line
- Add each line to the Drawing
- Increment startX by 70

**Step 7: Draw Letter 'T'**
- Create 2 MyLine objects to form the letter T:
  - Top horizontal line
  - Center vertical line
- Add each line to the Drawing
- Increment startX by 70

**Step 8: Draw Letter 'H'**
- Create 3 MyLine objects to form the letter H:
  - Left vertical line
  - Middle horizontal line
  - Right vertical line
- Add each line to the Drawing
- Increment startX by 70

**Step 9: Draw Letter 'U'**
- Create 3 MyLine objects to form the letter U:
  - Left vertical line
  - Bottom horizontal line
  - Right vertical line
- Add each line to the Drawing

**Step 10: Display Name**
- After all letters are created, the Drawing object draws all shapes
- The user sees "MIN THU" displayed on the canvas

---

## 5. Sequence Diagram for Feature 3: Scale Down All Shapes

This sequence diagram shows the interaction when a user presses the 'D' key to scale down all shapes.

```
User          Program       Drawing       Shape       MyRectangle   MyCircle   MyLine
 |               |             |            |              |            |         |
 |--Press 'D'-->|             |            |              |            |         |
 |               |             |            |              |            |         |
 |               |--KeyTyped(DKey)         |              |            |         |
 |               |             |            |              |            |         |
 |               |--ScaleDownShapes()----->|              |            |         |
 |               |             |            |              |            |         |
 |               |             |--loop[for each shape in _shapes]      |         |
 |               |             |            |              |            |         |
 |               |             |--is MyRectangle?-------->|            |         |
 |               |             |            |              |            |         |
 |               |             |--if yes                  |            |         |
 |               |             |            |              |            |         |
 |               |             |--Get Width-------------->|            |         |
 |               |             |<-current width-----------|            |         |
 |               |             |            |              |            |         |
 |               |             |--Set Width = Width * 0.8->            |         |
 |               |             |            |              |            |         |
 |               |             |--Get Height------------->|            |         |
 |               |             |<-current height----------|            |         |
 |               |             |            |              |            |         |
 |               |             |--Set Height = Height*0.8->            |         |
 |               |             |            |              |            |         |
 |               |             |--else if is MyCircle?--------------->|         |
 |               |             |            |              |            |         |
 |               |             |--if yes                  |            |         |
 |               |             |            |              |            |         |
 |               |             |--Get Radius------------------------>|         |
 |               |             |<-current radius---------------------|         |
 |               |             |            |              |            |         |
 |               |             |--Set Radius = Radius * 0.8---------->|         |
 |               |             |            |              |            |         |
 |               |             |--else if is MyLine?----------------------------->|
 |               |             |            |              |            |         |
 |               |             |--if yes                  |            |         |
 |               |             |            |              |            |         |
 |               |             |--Get X, Y, EndX, EndY------------------------- >|
 |               |             |<-current values----------------------------------|
 |               |             |            |              |            |         |
 |               |             |--Calculate deltaX = EndX - X---------------------->|
 |               |             |--Calculate deltaY = EndY - Y---------------------->|
 |               |             |            |              |            |         |
 |               |             |--Set EndX = X + (deltaX * 0.8)----------------->|
 |               |             |--Set EndY = Y + (deltaY * 0.8)----------------->|
 |               |             |            |              |            |         |
 |               |             |<-end loop--|              |            |         |
 |               |             |            |              |            |         |
 |               |<-return----|            |              |            |         |
 |               |             |            |              |            |         |
 |               |--Draw()---->|            |              |            |         |
 |               |             |            |              |            |         |
 |               |             |--Draw() on each shape--->|            |         |
 |               |             |            |              |            |         |
 |<--Display-----|             |            |              |            |         |
 |  Updated      |             |            |              |            |         |
 |  Canvas       |             |            |              |            |         |
```

### Sequence Flow Explanation for Feature 3

**Step 1: User Input**
- The user presses the 'D' key on the keyboard
- Program detects this using SplashKit.KeyTyped(KeyCode.DKey)

**Step 2: Method Call**
- Program calls the ScaleDownShapes() method on the Drawing object
- This method handles the scaling logic for all shapes

**Step 3: Loop Through Shapes**
- Drawing iterates through each shape in the _shapes list
- For each shape, it checks the type using the 'is' operator

**Step 4: Type-Specific Scaling for MyRectangle**
- If the shape is a MyRectangle:
  - Get current Width value
  - Set Width = Width * 0.8 (reduce by 20%)
  - Get current Height value
  - Set Height = Height * 0.8 (reduce by 20%)

**Step 5: Type-Specific Scaling for MyCircle**
- If the shape is a MyCircle:
  - Get current Radius value
  - Set Radius = Radius * 0.8 (reduce by 20%)

**Step 6: Type-Specific Scaling for MyLine**
- If the shape is a MyLine:
  - Get current X, Y, EndX, EndY values
  - Calculate deltaX = EndX - X (horizontal distance)
  - Calculate deltaY = EndY - Y (vertical distance)
  - Set EndX = X + (deltaX * 0.8) (scale horizontal distance)
  - Set EndY = Y + (deltaY * 0.8) (scale vertical distance)
  - This keeps the start point fixed and moves the end point closer

**Step 7: Display Updated Shapes**
- After all shapes are scaled, the Drawing object draws all shapes
- The user sees the updated canvas with smaller shapes (80% of original size)

---

## 6. Object Collaboration Analysis

### How Objects Work Together

**Program Object:**
- Acts as the main controller and entry point
- Detects user input through keyboard and mouse events
- Delegates all shape management and drawing operations to the Drawing object
- Maintains the main game loop and window management

**Drawing Object:**
- Manages the collection of all Shape objects in a List<Shape>
- Implements the three main features:
  - DrawRandomShapes(): Creates and adds random shapes to the collection
  - DrawFirstName(): Creates MyLine objects to form letters
  - ScaleDownShapes(): Modifies properties of existing shapes
- Handles file I/O operations for saving and loading
- Controls background color and overall canvas rendering

**Shape Objects (MyRectangle, MyCircle, MyLine):**
- Each shape type inherits from the abstract Shape class
- Stores its own specific properties:
  - MyRectangle: Width and Height
  - MyCircle: Radius
  - MyLine: EndX and EndY
- Each shape knows how to:
  - Draw itself on the canvas
  - Determine if a point is within its bounds
  - Save and load its state from files

### Polymorphism in Action

**Feature 1 - Random Shapes:**
- Uses polymorphism to create different shape types based on random numbers
- All created shapes are stored in the same List<Shape> regardless of their actual type
- The Drawing.Draw() method can iterate through all shapes and call Draw() on each, without knowing the specific type

**Feature 2 - Draw First Name:**
- Creates multiple MyLine objects with different coordinates
- All lines are treated uniformly when added to the Drawing
- Demonstrates composition - complex letters are built from simple line primitives

**Feature 3 - Scale Down:**
- Uses runtime type checking (is operator) to determine the actual shape type
- Each shape type has different properties that need scaling
- Demonstrates polymorphism with type-specific behavior
- The method works with the Shape list but handles each concrete type appropriately

### Encapsulation

The implementation demonstrates strong encapsulation:

**Private Fields with Public Properties:**
- Shape class: _color, _x, _y, _selected are private with public accessors
- MyRectangle: _width, _height are private with public Width/Height properties
- MyCircle: _radius is private with public Radius property
- MyLine: _endX, _endY are private with public EndX/EndY properties
- Drawing: _shapes list is private, exposed only through controlled methods

**Method Encapsulation:**
- Drawing class hides the internal list implementation
- Only necessary methods are exposed to the Program class
- Internal operations like shape iteration are hidden from external users

### Abstraction

**Abstract Shape Class:**
- Defines the common interface that all shapes must follow
- Abstract methods (Draw, DrawOutline, IsAt) ensure all shapes implement required functionality
- Virtual methods (SaveTo, LoadFrom) provide default implementations that can be overridden
- Concrete shape classes provide specific implementations

**Feature Implementation:**
- Each feature method encapsulates complex operations behind simple method calls
- Program doesn't need to know how shapes are created, scaled, or drawn
- Implementation details are hidden behind clean interfaces

---

## 7. Design Patterns and Principles

### Object-Oriented Principles Used

**Inheritance:**
- All shape classes (MyRectangle, MyCircle, MyLine) inherit from the abstract Shape class
- Common properties like Color, X, Y, and Selected are defined once in the base class
- Reduces code duplication and provides a unified interface

**Polymorphism:**
- Different shapes can be stored in the same List<Shape>
- Type-specific behavior is handled at runtime using the 'is' operator
- Virtual and abstract methods allow different implementations for each shape type
- Feature 1 demonstrates polymorphism in object creation
- Feature 3 demonstrates polymorphism in type-specific operations

**Encapsulation:**
- Private fields with public properties control access to data
- Implementation details are hidden from other classes
- Each class manages its own state and behavior
- The Drawing class encapsulates the shape collection management

**Abstraction:**
- The Shape class defines the interface that all shapes must follow
- Abstract methods ensure all shape types implement required functionality
- Program works with Shape abstractions rather than concrete types
- High-level operations (DrawRandomShapes, DrawFirstName, ScaleDownShapes) hide complex implementations

### Design Patterns

**Factory Pattern (Implicit):**
- Feature 1 (DrawRandomShapes) uses factory-like logic to create different shape types based on random values
- Switch statement in Load() method acts as a factory for creating shapes based on type strings

**Collection Pattern:**
- Drawing class manages a collection of Shape objects
- Provides methods to add, remove, and iterate through shapes
- Encapsulates the underlying List<Shape> implementation

**Template Method Pattern:**
- Shape class defines virtual SaveTo() and LoadFrom() methods
- Each subclass extends the base implementation by calling base.SaveTo() or base.LoadFrom()
- Ensures consistent save/load structure across all shape types

---

## 8. Feature-Specific Analysis

### Feature 1: Random Shapes

**Key Design Decisions:**
- Uses Random class to generate unpredictable values
- Number of shapes varies between 5 and 16 for visual variety
- Three shape types (Rectangle, Circle, Line) provide diversity
- Fixed dimensions (100x80 for rectangles, 50 for circles, 100x50 for lines) ensure consistency
- Random positions (50-700 for X, 50-500 for Y) keep shapes within visible canvas area
- Random colors using SplashKit.RandomColor() for visual appeal

**Benefits:**
- Demonstrates polymorphism in object creation
- Shows how different types can be created and managed uniformly
- Provides visual variety and user engagement

### Feature 2: Draw Full First Name

**Key Design Decisions:**
- Uses only MyLine objects to draw all letters (M-I-N-T-H-U)
- Consistent spacing of 70 pixels between letters for readability
- Fixed starting position (50, 150) ensures name appears in visible area
- All lines use blue color for visual coherence
- Each letter is composed of 2-4 lines depending on complexity
- Coordinates are carefully calculated to form recognizable letters

**Benefits:**
- Demonstrates composition - complex shapes from simple primitives
- Shows sequential object creation and addition to collection
- Maintains clean, readable code structure with consistent spacing

### Feature 3: Scale Down All Shapes

**Key Design Decisions:**
- Uniform scaling factor of 0.8 (20% reduction) for all shapes
- Type-specific scaling logic for each shape type:
  - Rectangles: Scale both width and height
  - Circles: Scale radius
  - Lines: Scale distance from start to end point while keeping start point fixed
- In-place modification of existing shapes rather than creating new ones
- Maintains aspect ratios and proportions

**Benefits:**
- Demonstrates runtime type checking and polymorphism
- Shows how to manipulate different types with type-specific logic
- Preserves shape identity and other properties (color, position, selection state)
- Can be called multiple times for progressive scaling

---

## 9. Code Quality and Best Practices

### Naming Conventions
- Clear, descriptive method names (DrawRandomShapes, DrawFirstName, ScaleDownShapes)
- Consistent property names (Width, Height, Radius, EndX, EndY)
- Private fields prefixed with underscore (_shapes, _color, _width)
- Public properties use PascalCase

### Code Organization
- Each class has a single, well-defined responsibility
- Related methods grouped together in the Drawing class
- Feature implementations are separate methods, not mixed together
- Clear separation between data (fields) and behavior (methods)

### Error Handling
- File operations include try-finally blocks to ensure resources are closed
- Load() method handles unknown shape types with InvalidDataException
- Type checking using 'is' operator prevents runtime errors

### Maintainability
- Adding new shape types requires:
  1. Create new class inheriting from Shape
  2. Implement abstract methods
  3. Add case in Load() method
  4. Add logic in scale methods if needed
- Features are independent and can be modified without affecting others
- Clear separation of concerns between Program, Drawing, and Shape classes

---

## 10. Additional Features

Beyond the three required features for Option 2, the application includes two additional enhancements that improve user experience and functionality:

### Feature 4: Scale Up All Shapes (U Key)

**Purpose:**
This feature provides the inverse operation to Feature 3 (Scale Down). When the user presses the 'U' key, all shapes on the canvas are scaled up by 25%, increasing their size.

**Implementation:**
```
User presses 'U' → Program.KeyTyped(UKey) → Drawing.ScaleUpShapes()
```

**Key Design Decisions:**
- Scaling factor of 1.25 (25% increase) for all shapes
- Type-specific scaling logic similar to ScaleDownShapes():
  - **Rectangles:** Width and Height are multiplied by 1.25
  - **Circles:** Radius is multiplied by 1.25
  - **Lines:** Distance from start point to end point is multiplied by 1.25, keeping start point fixed
- In-place modification preserves all other shape properties (color, position, selection state)
- Can be used multiple times for progressive enlargement

**Benefits:**
- Complements the scale down functionality
- Provides users with full control over shape sizing
- Uses the same polymorphic approach as ScaleDownShapes()
- Maintains consistency with existing scaling behavior

### Feature 5: Select All Shapes (A Key)

**Purpose:**
This feature allows users to quickly select all shapes currently on the canvas at once, making batch operations more efficient.

**Implementation:**
```
User presses 'A' → Program.KeyTyped(AKey) → Drawing.SelectAll()
```

**Key Design Decisions:**
- Iterates through all shapes in the _shapes collection
- Sets the Selected property to true for each shape
- Works with the existing selection system (shapes draw with outline when selected)
- Enables efficient batch operations like deleting all shapes

**Benefits:**
- Improves user experience by reducing repetitive right-click operations
- Works seamlessly with existing selection and deletion features
- Demonstrates iteration through polymorphic collections
- Follows the same selection mechanism as individual shape selection

### Combined Workflow

These additional features integrate seamlessly with the existing functionality:

1. **User draws multiple shapes** (rectangles, circles, lines)
2. **User presses 'A'** → All shapes are selected
3. **User presses 'U'** multiple times → All shapes grow larger
4. **User presses 'D'** multiple times → All shapes shrink smaller
5. **User presses Delete/Backspace** → All selected shapes are removed

This demonstrates how the additional features complement the core functionality and provide users with more control over shape manipulation.

---

## 11. Conclusion

This comprehensive UML report demonstrates how the ShapeDrawing application implements all three features of Option 2 using object-oriented programming principles. The diagrams and analysis show:

**Class Diagram:**
- Clear hierarchical structure with Shape as the abstract base class
- Well-defined relationships between Program, Drawing, and Shape classes
- Proper encapsulation with private fields and public properties

**Sequence Diagrams:**
- Feature 1 shows polymorphic object creation with random generation
- Feature 2 demonstrates sequential composition of complex structures from simple primitives
- Feature 3 illustrates runtime type checking and type-specific operations

**Key Achievements:**
- **Inheritance:** Shape hierarchy enables code reuse and polymorphism
- **Polymorphism:** Different shapes can be managed uniformly while maintaining type-specific behavior
- **Encapsulation:** Implementation details are hidden behind well-defined interfaces
- **Abstraction:** High-level operations hide complex implementation details

**Application Strengths:**
- Flexible design that can easily accommodate new shape types
- Clean separation of concerns between classes
- Reusable components that can be extended for new features
- Consistent coding style and naming conventions
- Proper error handling and resource management

This design makes the application maintainable, extensible, and demonstrates a solid understanding of object-oriented programming principles applied to a real-world graphics application using the SplashKit library.

---

**End of Report**
