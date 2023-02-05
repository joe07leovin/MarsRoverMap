using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover;

internal class Program
{

    public static Dictionary<string, Dictionary<string, string>>? directionMap { get; set; }
    public static int[][] terrain;
    public static int[] startposition = { 1, 1 };
    public static string startdirection = "north";
    static void Main(string[] args)
    {

        directionMap = getDirectionMap();
        moveRoverTo();

    }


    static Tuple<int, int> getCoordinates()
    {
        Console.WriteLine();

        int Latitudes, Longitudes;
        Console.Write("Enter the number of Latitudes: ");
        Latitudes = Convert.ToInt32(Console.ReadLine());
        Console.Write("Enter the number of Longitudes: ");
        Longitudes = Convert.ToInt32(Console.ReadLine());
        var coordinates = new Tuple<int, int>(Latitudes, Longitudes);
        return coordinates;
    }

    static T[][] CreateArray<T>(int rows, int cols)
    {

        T[][] array = new T[rows][];
        for (int i = 0; i < array.GetLength(0); i++)
        {
            array[i] = new T[cols];

        }

        return array;
    }
    static int[][] createTerrain(int x, int y)
    {
        int index = 0;
        int[][] terrain = CreateArray<int>(x, y);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                terrain[i][j] = index;
                index++;
            }
        }
        var k = terrain;

        printTerrain(x, y, terrain, startposition[0], startposition[1]);
        return terrain;
    }

    static void printTerrain(int x, int y, int[][] terrain, int xposition, int yposition)
    {
        Console.WriteLine();
        Console.Write("The rover on Map:");
        Console.WriteLine();

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                if (i == xposition && j == yposition)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("[o_o]-€ ");
                }
                else
                {
                    Console.Write(terrain[i][j] + " ");
                }
                Console.ForegroundColor = ConsoleColor.Black;
            }
            Console.WriteLine();
        }
    }

    static Dictionary<string, dynamic> getNextRoverPosition(int[] currentposition, string currentdirection, string pathcommand, int terrainboundsbylattitude, int terrainboundsbylongitutude)
    {
        var nextViableDirection = directionMap[key: currentdirection][pathcommand];
        var moveobject = new Dictionary<string, dynamic>();

        switch (nextViableDirection)
        {
            case "north":
                {
                    if (currentposition[0] > 0)
                    {
                        Console.WriteLine();

                        Console.Write("Reached Terrain Boundary. Unable to move");
                        break;
                    }

                    moveobject.Add("cell", terrain[currentposition[0] - 1][currentposition[1]]);
                    currentposition[0] = currentposition[0] - 1;
                    currentposition[1] = currentposition[1];
                    moveobject.Add("position", currentposition);
                    moveobject.Add("direction", "north");
                    break;
                }
            case "south":
                {
                    if (currentposition[0] > terrainboundsbylongitutude)
                    {
                        Console.WriteLine();

                        Console.Write("Reached Terrain Boundary. Unable to move");
                        break;
                    }

                    moveobject.Add("cell", terrain[currentposition[0] + 1][currentposition[1] + 1]);
                    currentposition[0] = currentposition[0] + 1;
                    currentposition[1] = currentposition[1];
                    moveobject.Add("position", currentposition);
                    moveobject.Add("direction", "south");
                    break;
                }
            case "east":
                {
                    if (currentposition[0] > terrainboundsbylattitude)
                    {
                        Console.WriteLine();

                        Console.Write("Reached Terrain Boundary. Unable to move");
                        break;
                    }

                    moveobject.Add("cell", terrain[currentposition[0]][currentposition[1] + 1]);
                    currentposition[0] = currentposition[0];
                    currentposition[1] = currentposition[1] + 1;
                    moveobject.Add("position", currentposition);
                    moveobject.Add("direction", "east");
                    break;
                }
            case "west":
                {
                    if (currentposition[0] <= 0)
                    {
                        Console.WriteLine();

                        Console.Write("Reached Terrain Boundary. Unable to move");
                        break;
                    }

                    moveobject.Add("cell", terrain[currentposition[0]][currentposition[1] - 1]);
                    currentposition[0] = currentposition[0];
                    currentposition[1] = currentposition[1] - 1;
                    moveobject.Add("position", currentposition);
                    moveobject.Add("direction", "west");
                    break;
                }
        }

        return moveobject;

    }
    static void moveRoverTo()
    {

        var heatmap = new Dictionary<string, string>();
        heatmap.Add("L", "left");
        heatmap.Add("R", "right");
        heatmap.Add("F", "forward");
        var coordinates = getCoordinates();
        terrain = createTerrain(coordinates.Item1, coordinates.Item2);
        Console.WriteLine();
        Console.Write("Enter path command with comma seperation:");

        var pathcommand = Console.ReadLine().Split(",");
        var currentposition = startposition;
        var currentdirection = startdirection;
        var pathcommandlength = pathcommand.Length;
        var roverobject = new Dictionary<string, dynamic>();
        for (int i = 0; i < pathcommandlength; i++)
        {
            roverobject = getNextRoverPosition(currentposition, currentdirection, heatmap[pathcommand[i]], coordinates.Item1, coordinates.Item2);
            currentposition = roverobject["position"];
            currentdirection = roverobject["direction"];


        }
        Console.WriteLine();

        Console.Write("Rover is at " + terrain[roverobject["position"][0]][roverobject["position"][1]] + " pointing towards " + roverobject["direction"] + " at X,Y cordiantes: (" + roverobject["position"][0] + "-" + roverobject["position"][1] + ")");

        Console.WriteLine();

        printTerrain(coordinates.Item1, coordinates.Item2, terrain, int.Parse(Convert.ToString(roverobject["position"][0])), int.Parse(Convert.ToString(roverobject["position"][1])));



    }



    static Dictionary<string, Dictionary<string, string>> getDirectionMap()
    {



        Dictionary<string, string> _north = new Dictionary<string, string>();
        _north.Add("left", "west");
        _north.Add("right", "east");
        _north.Add("forward", "north");
        Dictionary<string, string> _south = new Dictionary<string, string>();
        _south.Add("left", "east");
        _south.Add("right", "west");
        _south.Add("forward", "south");
        Dictionary<string, string> _east = new Dictionary<string, string>();
        _east.Add("left", "north");
        _east.Add("right", "south");
        _east.Add("forward", "east");
        Dictionary<string, string> _west = new Dictionary<string, string>();
        _west.Add("left", "south");
        _west.Add("right", "north");
        _west.Add("forward", "west");


        Dictionary<string, Dictionary<string, string>> rootMap = new Dictionary<string, Dictionary<string, string>>();
        rootMap.Add("north", _north);
        rootMap.Add("south", _south);
        rootMap.Add("east", _east);
        rootMap.Add("west", _west);

        return rootMap;
    }

}







