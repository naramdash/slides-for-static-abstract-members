public interface IFavorite {
  static int sizeAtAge(int age) {
    return age;
  }

  default int size(int age) {
    return age;
  }
}
