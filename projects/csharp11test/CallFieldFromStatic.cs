namespace Classes;

public record MyRed(int a, int b)
{
  public double doubleA() => a;
};

public class CallFieldFromStatic
{
  public static bool condition = true;
  public static int A(int b)
  {
    return CallFieldFromStatic.condition ? b : b - 1;
  }
}