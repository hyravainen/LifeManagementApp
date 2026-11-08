import java.util.ArrayList;
import java.util.Arrays;
import java.util.Scanner;

public class Main {
    int i = 3;
    public static void main(String[] args){
    String answer;
    ArrayList<Integer> table = new ArrayList<>(Arrays.asList(1, 2, 3));
    Scanner scanner = new Scanner(System.in);
    do {System.out.println("Alkuperäinen taulukko:" + table);
    System.out.print("Anna uusi numero: ");
    table.add(scanner.nextInt());
    scanner.nextLine();
    System.out.println("Uusi taulukko: " + table);
    System.out.println("Haluatko jatkaa tätä taulukkoa? Y/N");
    answer = scanner.nextLine();
    } while (answer.equalsIgnoreCase("y"));
    scanner.close();
    
    }
}