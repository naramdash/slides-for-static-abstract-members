public class App {
    public static void main(String[] args) throws Exception {
        whatIsYourFavorite(new Cat());
    }

    static <T extends IFavorite> void whatIsYourFavorite(T iHaveAFavorite) {
        System.out.println("i call you " + IFavorite.sizeAtAge(4));
        // System.out.println(T.sizeAtAge(4));
    }
}
