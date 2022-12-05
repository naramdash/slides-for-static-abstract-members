using System;

namespace Utils;

public static class ConsoleUtil
{
  public static void printHeader(string header)
  {
    var defaultBackgroundColor = Console.BackgroundColor;

    Console.BackgroundColor = ConsoleColor.DarkRed;
    Console.Write(header);
    Console.BackgroundColor = defaultBackgroundColor;
    Console.WriteLine();
  }
}
