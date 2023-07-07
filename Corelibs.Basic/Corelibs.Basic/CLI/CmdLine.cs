namespace Corelibs.Basic.CLI;

public static class CmdLine
{
    public static string WriteLine(string line)
    {
        Console.WriteLine(line);
        return line;
    }

    public static T WriteLine<T>(this T @object)
    {
        Console.WriteLine(@object);
        return @object;
    }

    public static T WriteLines<T>(this T objects)
    {
        if (objects is IEnumerable<object> enumerable)
            foreach (var @object in enumerable)
                Console.WriteLine(@object);

        return objects;
    }
}
