using Template_Task_3.DemoClasses;
using Template_Task_3.Helpers;
using Template_Task_3.StackAndHeap;

namespace Template_Task_3;

internal class Program
{
    // Dictionary: snabb uppslagning av produkter via produktkod (key = kod, value = produkt)
    static Dictionary<string, Product> products = new Dictionary<string, Product>();

    // List: enkel logg över vad som hänt i programmet — ordnad och växer dynamiskt
    static List<string> logMessages = new List<string>();

    // Queue: FIFO — kunder betjänas i den ordning de ställde sig i kön
    static Queue<Customer> customerQueue = new Queue<Customer>();

    // Stack: LIFO — används för att kunna ångra den senaste försäljningen
    static Stack<Sale> saleHistory = new Stack<Sale>();

    static string ReadLine => Console.ReadLine() ?? string.Empty;

    static void Main(string[] args)
    {
        //ToDo implementera 
        SeedProducts();

        bool running = true;

       do
        {
            PrintMenu();

            Console.Write("Välj: ");
            string choice = ReadLine;

            Console.WriteLine();

            switch (choice)
            {
                case MenuConstants.ShowProducts:
                    PrintProducts();
                    break;

                case MenuConstants.FindProduct:
                    FindProduct();
                    break;

                case MenuConstants.AddProduct:
                    AddProduct();
                    break;

                case MenuConstants.ChangeStock:
                    ChangeStock();
                    break;

                case MenuConstants.GetBetterPrice:
                    Console.Write("Ange produktkod: ");
                    GetPriceBetter(ReadLine.ToUpper());
                    break;

                case MenuConstants.AddCustomerToQueue:
                    AddCustomerToQueue();
                    break;

                case MenuConstants.ServeNextCustomer:
                    ServeNextCustomer();
                    break;

                case MenuConstants.PrintCustomerQueue:
                    PrintCustomerQueue();
                    break;

                case MenuConstants.SellProduct:
                    SellProduct();
                    break;

                case MenuConstants.UndoLastSale:
                    UndoLastSale();
                    break;

                case MenuConstants.PrintLog:
                    PrintLog();
                    break;

                case MenuConstants.ArrayLab:
                    ArrayLab();
                    break;

                case MenuConstants.ListLab:
                    ListLab();
                    break;

                case MenuConstants.ReverseTextLab:
                    ReverseTextLab();
                    break;

                case MenuConstants.WordCountLab:
                    WordCountLab();
                    break;

                case MenuConstants.ParenthesesLab:
                    ParenthesesLab();
                    break;

                case MenuConstants.MemoryLab:
                    MemoryLab();
                    break;

                case MenuConstants.RecursionLab:
                    RecursionLab();
                    break;

                case MenuConstants.SaveLogToFile:
                    SaveLogToFile();
                    break;

                case MenuConstants.Exit:
                    running = false;
                    break;

                default:
                    Console.WriteLine("Felaktigt val.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Tryck på valfri tangent för att fortsätta...");
            Console.ReadKey();
            Console.Clear();
        }
        while(running);
    }

    static void PrintMenu()
    {
        Console.WriteLine(MenuConstants.Title);
        Console.WriteLine();

        foreach (MenuItem item in MenuConstants.Items)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine();
    }

    #region Dictionary

    // ============================================================
    // DEL 1 - PRODUKTER OCH DICTIONARY
    // ============================================================

    static void SeedProducts()
    {
        // Exempel på hur du lägger till en produkt i dictionaryn:
        // products["KAFFE"] = new Product("KAFFE", "Kaffe", 15.00m, 50);
        //
        // Lägg till minst 10 produkter i products-dictionaryn.
        // Välj egna koder, namn, priser och lagersaldon.
        
        products["KAFFE"] = new Product("KAFFE", "kaffe", 15.00m, 50);
        products["TE"] = new Product("TE", "te", 10.4m, 80);
        
        products["COLA"] = new Product("COLA", "cola", 12.00m, 50);
        products["SAFT"] = new Product("SAFT", "saft", 5.00m, 50);
        products["BANAN"] = new Product("BANAN", "banan", 1.00m, 50);
        products["BRÖD"] = new Product("BRÖD", "bröd", 4.15m, 50);
        products["GODIS"] = new Product("GODIS", "godis", 1.00m, 5000);
        products["MJÖLK"] = new Product("MJÖLK", "mjölk", 5.00m, 50);
        products["OBOY"] = new Product("OBOY", "oboy", 15.00m, 50);
        products["POKEMON"] = new Product("POKEMON", "pokemon kort", 15.00m, 0);
    }

    static void PrintProducts()
    {
        Console.WriteLine("=== Produkter ===");

        foreach (var product in products)
        {
            var p = product.Value;
            Console.WriteLine($"Name:{p.Name} Total Value:{p.Price*p.Stock}");
        }

        // Fråga:
        // Varför passar Dictionary bra för ett produktregister?
        Console.WriteLine("Svar: Kan lagra och nå produkter snabbt och enkelt igenom att använda deras nyckel som kan vara produktkod");
        Console.WriteLine("Om man inte använde det så skulle man behövs söka igenom varje gång vilket skulle vara mer krävande");
    }

    static void FindProduct()
    {
        Console.Write("Ange produktkod: ");
        
        string line = Console.ReadLine() ?? string.Empty;
        if(products.TryGetValue(line.ToUpper(), out Product p))
        {
            Console.WriteLine(p.ToString());
        }
        else
            Console.WriteLine("ERROR: NO PRODUCT WITH THAT NAME");
        
        // Fråga:
        // Varför är TryGetValue bättre än att skriva products[code] direkt?
        Console.WriteLine("TryGetValue har inbyggd funkionalitet för hantera om värdet inte hittas");
    }

    static void AddProduct()
    {
        
        // Lägg till ett loggmeddelande i logMessages.
        string code = "";
        do
        {
            if (!code.IsWhiteSpace()) Console.WriteLine("Key already Exists");
            Console.WriteLine("Ange produktkod"); 
            code = Console.ReadLine() ?? string.Empty;
        }
        while(products.ContainsKey(code));

        string name = "";
        do
        {
            Console.WriteLine("Ange Namn"); 
            code = Console.ReadLine() ?? string.Empty;
        }
        while(code.IsWhiteSpace());
        
        decimal price;
        string price_line;
        do
        {
            Console.WriteLine("Ange Pris"); 
            price_line = Console.ReadLine() ?? string.Empty;
        }
        while(!decimal.TryParse(price_line, out price));
        
        int stock;
        do
        {
            Console.WriteLine("Ange Saldo"); 
            price_line = Console.ReadLine() ?? string.Empty;
        }
        while(!int.TryParse(price_line, out stock));
        
        Product newProduct = new Product(code, name, price, stock);
        products.Add(code,newProduct);
        logMessages.Add($"Added {newProduct.ToString()}"); 
        // Fråga:
        // Vad är nyckeln och vad är värdet i products?
        Console.WriteLine("Nyckeln är produktkoden i detta fallet (Kan dock tekniskt sett sätta vilken string som möjligt)");
        Console.WriteLine("Värdet är själva Product klassen man kan hämta ut via nyckeln");
    }

    static void ChangeStock()
    {
        string code = "";
        do
        {
            if (!code.IsWhiteSpace()) Console.WriteLine("Key already Exists");
            Console.WriteLine("Ange produktkod"); 
            code = Console.ReadLine() ?? string.Empty;
        }
        while(!products.ContainsKey(code));

        string line = "";
        int stock;
        do
        {
            Console.WriteLine("Ange Saldo"); 
            line = Console.ReadLine() ?? string.Empty;
        }
        while(!int.TryParse(line, out stock));

        products[code].Stock = stock;
        logMessages.Add($"Changed {code} stock to {stock}"); 
    }

    static decimal GetPriceBad(string code)
    {
        if (code == "KAF")
        {
            return 15;
        }
        else if (code == "TE")
        {
            return 12;
        }
        else if (code == "BUL")
        {
            return 18;
        }
        else if (code == "MCK")
        {
            return 35;
        }
        else
        {
            return -1;
        }
    }

    static decimal GetPriceBetter(string code)
    {
        var prices = new Dictionary<string, decimal>();
        prices.Add("KAF",15);
        prices.Add("TE",12);
        prices.Add("BUL",18);
        prices.Add("MCK",35);

        decimal output = -1;
        prices.TryGetValue(code, out output);
        
        // Fråga:
        // Varför är Dictionary-lösningen bättre än många if/else-satser?
        Console.WriteLine("Kan dynamiskt lägga in mer alternativ och spara det i t.ex. en databas");
        return output;
    }

    #endregion

    #region Queue

    // ============================================================
    // DEL 2 - QUEUE
    // ============================================================

    static void AddCustomerToQueue()
    {
        // Läs in kundens namn (använd InputHelpers.ReadString).
        // Skapa ett Customer-objekt med namnet.
        // Lägg kunden i customerQueue med Enqueue.
        // Skriv ut att kunden lagts till och vilken plats i kön de har.
        // Lägg till ett loggmeddelande i logMessages.

        string name = InputHelpers.ReadString("Lägg till namn: ");
        Customer customer = new Customer(name);
        customerQueue.Enqueue(customer);
        Console.WriteLine($"Kunden har lagt till på plats {customerQueue.Count}");
        
        logMessages.Add($"Added customer {name}");
        
        // Fråga:
        // Vad betyder FIFO?
        Console.WriteLine("First In First Out. När man tar ut något får man den som varit där längst");
    }

    static void ServeNextCustomer()
    {
        
        // Kontrollera om customerQueue är tom — skriv meddelande om den är det.
        // Om den inte är tom:
        // Använd Dequeue för att ta bort och hämta den första kunden.
        // Skriv ut vilken kund som blev betjänad.
        // Lägg till ett loggmeddelande i logMessages.
        if (customerQueue.TryDequeue( out Customer customer))
        {
            Console.WriteLine($"Servat {customer.Name}");
            logMessages.Add($"Dequeued {customer}");
        }
        else
        {
            Console.WriteLine("customerQueue är tom. lägg till customer");
        }
        // Fråga:
        // Varför passar Queue bättre än Stack för en kundkö?
        Console.WriteLine("I en kö vill du ta först den som har köat längs vilket queue är gjort för medans stack är omvänt");
    }

    static void PrintCustomerQueue()
    {
        Console.WriteLine("=== Kundkö ===");

        // Om customerQueue är tom, skriv att kön är tom.
        // Annars: loopa igenom customerQueue med en räknare.
        // Skriv ut platsnummer, namn och tidsstämpel för varje kund.
        //
        // Exempel:
        // 1. Kalle (2026-05-26 10:01)
        // 2. Greta (2026-05-26 10:02)
        // 3. Stina (2026-05-26 10:03)
        //
        // Tips: foreach fungerar på Queue utan att ta bort elementen.

        if (customerQueue.Count == 0)
        {
            Console.WriteLine("Kön är tom");
            return;
        }

        var queueArray = customerQueue.ToArray();
        for (int i = 0; i < customerQueue.Count; i++)
        {
            Console.WriteLine($"{i} {queueArray[i]}");
        }
    }

    #endregion

    #region Stack

    // ============================================================
    // DEL 3 - STACK OCH FÖRSÄLJNING
    // ============================================================

    static void SellProduct()
    {
        // Kontrollera om customerQueue är tom — skriv meddelande om den är det.
        // Använd Peek för att se vilken kund som står först (utan att ta bort dem).
        // Läs in produktkod.
        // Slå upp produkten med TryGetValue.
        // Kontrollera att produkten finns i lager (Stock > 0).
        // Minska produktens Stock med 1.
        // Skapa ett Sale-objekt med produktinfo och kundens namn.
        // Lägg Sale-objektet på saleHistory med Push.
        // Lägg till ett loggmeddelande i logMessages.
        //
        // Extra:
        // Bestäm om kunden ska tas bort från kön efter köp eller inte.
        // Motivera ditt val i kommentar.
        
        if (customerQueue.Count == 0)
        {
            Console.WriteLine("Kön är tom");
            return;
        }
        Customer customer = customerQueue.Peek();
        
        Product product = null;
        do
        {
            string input = InputHelpers.ReadString("Vad vill du köpa för något (skriv PRODUCTCODE): ");
            products.TryGetValue(input,out product);
        } while (product == null);

        if (product.Stock == 0)
        {
            product.Stock--;
        }
        Sale sale = new Sale(product.Code, product.Name, product.Price, customer.Name);
        
        saleHistory.Push(sale);
        string log = $"Sale {sale} processed";
        Console.WriteLine(log);
        logMessages.Add(log);
        // Fråga:
        // Varför sparar vi försäljningar i en Stack?
        Console.WriteLine("För att det är lättare att fysiskt ta tillbaka de senaste än längre tillbaka");
    }

    static void UndoLastSale()
    {

        // Kontrollera om saleHistory är tom — skriv meddelande om den är det.
        // Om den inte är tom:
        // Använd Pop för att hämta och ta bort senaste försäljningen.
        // Slå upp produkten i products med försäljningens ProductCode.
        // Öka produktens Stock med 1.
        // Logga vad som ångrades i logMessages.

        if (saleHistory.Count == 0)
        {
            Console.WriteLine("Ingen försäljning att ta bort");
            return;
        } 
        
        Sale sale = saleHistory.Pop();
        Product product = products[sale.ProductCode];

        product.Stock++;
        string log = $"Undo {sale}";
        Console.WriteLine(log);
        logMessages.Add(log);
        
        // Fråga:
        // Vad betyder LIFO?
        Console.WriteLine("Last In First Out - får ut först den senaste som läggs in");
    }

    static void ReverseTextLab()
    {
        Console.WriteLine("=== Stack-labb: vänd text ===");

        // Läs in en text från användaren.
        // Skriv ut texten bakofram använd en lämplig collektion.

        Stack<char> line =new Stack<char>(InputHelpers.ReadString("Skriv text: "));
        while (line.TryPop(out char c))
        {
            Console.Write(c);
        }
    }

    #endregion

    #region List

    // ============================================================
    // DEL 4 - LIST
    // ============================================================

    static void PrintLog()
    {
        Console.WriteLine("=== Logg ===");

        // Om logMessages är tom, skriv "Inga loggmeddelanden finns."
        // Annars: loopa igenom logMessages och skriv ut varje meddelande.
        foreach (var log in logMessages)
        {
            Console.WriteLine(log);
        }
        
        // Fråga:
        // Varför passar List bra för loggmeddelanden?
        Console.WriteLine("List är de mest generiska och flexibela sättet att lagra och är bra på att söka sortera om det behövs");
    }

    static void ListLab()
    {
        Console.WriteLine("=== List-labb ===");

        List<string> shoppingList = new List<string>();

        PrintListInfo(shoppingList, "Start");

        shoppingList.Add("Mjölk");
        PrintListInfo(shoppingList, "Efter Mjölk");

        shoppingList.Add("Bröd");
        PrintListInfo(shoppingList, "Efter Bröd");

        shoppingList.Add("Smör");
        PrintListInfo(shoppingList, "Efter Smör");

        shoppingList.Add("Ost");
        PrintListInfo(shoppingList, "Efter Ost");

        shoppingList.Add("Yoghurt");
        PrintListInfo(shoppingList, "Efter Yoghurt");

        shoppingList.Remove("Smör");
        PrintListInfo(shoppingList, "Efter Remove");
        
        // Lägg till minst 4 egna varor med en loop.
        // Skriv ut hela listan.
        for (int i = 1; i <= 4; i++)
        {
            shoppingList.Add($"{25*i}cl Läsk");
        }
        PrintListInfo(shoppingList,"Efter läsk");
        foreach (var item in shoppingList)
        {
            Console.WriteLine(item);
        }
        
        // Fråga 1:
        // Vad betyder Count?
        Console.WriteLine("Hur många element är i listan");

        // Fråga 2:
        // Vad betyder Capacity?
        Console.WriteLine("Hur mycket minnesplats som är reserverad för listan");

        // Fråga 3:
        // Varför ökar inte Capacity med exakt 1 varje gång?
        Console.WriteLine("För att allokera mer minne är mer krävande än använda minne vi vet vi kan använda");
        Console.WriteLine("Kabn använda EnsureCapacity för att manuellt säga hur stor vi vet att vi minst kommer ha.");

        // Fråga 4:
        // Minskar Capacity automatiskt när element tas bort?
        Console.WriteLine("Nej, den antar att du troligtvis kommer ha listan så stor i framtiden eftersom de var det en gång");
        Console.WriteLine("Om man vill ha mindre så använd TrimExcess för tvinga ner det");
    }

    static void PrintListInfo(List<string> list, string message)
    {
        Console.WriteLine($"{message}: Count = {list.Count}, Capacity = {list.Capacity}");
    }

    #endregion

    #region Array

    // ============================================================
    // DEL 5 - ARRAY
    // ============================================================

    static void ArrayLab()
    {
        Console.WriteLine("=== Array-labb ===");

        string[] weekdays = ["Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag"];

        // Skriv ut alla veckodagar med en for-loop.
        // Tips: använd weekdays.Length för att veta hur många element det finns.
        for (int i = 0; i < weekdays.Length; i++)
        {
            Console.WriteLine(weekdays[i]);
        }
        // Skriv ut alla veckodagar med foreach.

        foreach (string weekday in weekdays)
        {
            Console.WriteLine(weekday);
        }

        string f =weekdays[5];
        // Fråga 1:
        // När passar en array bättre än en List?
        Console.WriteLine("Är snabbare än en lista och ");

        // Fråga 2:
        // Vad händer om du försöker skriva weekdays[5]?
        Console.WriteLine("Får en IndexOutOfRangeExeption " +
                          "(Kunde varit värre, äldre programmingspråk tillåter det och kan skapa massa exploits som t.ex. kan hacka ens dator pga det)");

        // Fråga 3:
        // Varför måste arrayens storlek anges från början?
        Console.WriteLine("Eftersom den reserverar allt minne på ett ställe från början och kan inte ändra storlek (Om du inte overridar med en större array)");
    }

    #endregion

    #region Blandat_Stack_Heap_mm

    // ============================================================
    // DEL 6 - DICTIONARY SOM RÄKNARE
    // ============================================================

    static void WordCountLab()
    {
        Console.WriteLine("=== Dictionary-labb: räkna ord ===");

        Console.WriteLine("Skriv en mening:");
        string text = ReadLine;

        Dictionary<string, int> wordCounts = CountWords(text);

        Console.WriteLine("Resultat:");

        foreach (KeyValuePair<string, int> pair in wordCounts)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }

        // Fråga:
        // Varför passar Dictionary bra när vi ska räkna ord?
        Console.WriteLine("Eftersom vi vet om ett ord används mer än en gång och inte räknar det två gånger");
    }

    static Dictionary<string, int> CountWords(string text)
    {
        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

        // Dela upp text i ord med string.Split.
        // Separera på: mellanslag (ett eller flera), punkt, !, ?, :, ;
        string[] words = text.Split(new char[] { ' ', '.', '!', '?', ':', ';' },
                                           StringSplitOptions.RemoveEmptyEntries);
        
        // Loopa igenom orden.
        // Gör varje ord till gemener med .ToLower() så att "Hej" och "hej" räknas som samma.
        // Om ordet redan finns i wordCounts → öka värdet med 1.
        // Annars → lägg till ordet med värdet 1.
        foreach (string word in words)
        {
            string w = word.ToLower();
            if (wordCounts.ContainsKey(w))
                wordCounts[w]++;
            else
                wordCounts.Add(w,1);
        }

        // Fråga:
        // Vad är nyckeln och vad är värdet i wordCounts?
        Console.WriteLine("Nyckeln är ordet och värdet är hur många gång ordet har används");

        return wordCounts;
    }

    // ============================================================
    // DEL 7 - PARENTESKONTROLL - Använd lämpliga datastrukturer
    // ============================================================

    static void ParenthesesLab()
    {
        Console.WriteLine("=== Kontrollera parenteser ===");

        // Testfall att prova:
        // ([{}])                         true
        // ({)}                           false
        // List<int> lista = new();       true
        // (]                             false
        // ((()))                         true
        // (()                            false
        // (                              false
        // )                              false
        Console.WriteLine("Skriv en kodrad eller parentessträng:");
        string input = ReadLine;

        //ToDo skriv koden för CheckParantheses
        bool isCorrect = CheckParentheses(input);

        if (isCorrect)
        {
            Console.WriteLine("Strängen är välformad.");
        }
        else
        {
            Console.WriteLine("Strängen är INTE välformad.");
        }

        
    }

    static bool CheckParentheses(string text)
    {
        // Använd en Stack<char> och en Dictionary<char, char>.
        //
        // Tips Dictionary:
        // Låt dictionaryn mappa varje stängande parentes till sin matchande öppnare.
        // Det gör matchningskontrollen till en enkel uppslagning istället för flera if-satser.
        Dictionary<char, char> parenthesesPair = new Dictionary<char, char>();
        parenthesesPair.Add('{','}');
        parenthesesPair.Add('[',']');
        parenthesesPair.Add('<','>');
        parenthesesPair.Add('(',')');
            
        // Tips Stack:
        // Stacken håller reda på vilka öppnare du sett men ännu inte stängt.
        // Tänk på vad LIFO innebär här — varför är det precis rätt egenskap för det här problemet?
        Stack<char> parenthesesStack = new Stack<char>();
        foreach (char c in parenthesesStack)
        {
            if (parenthesesStack.Count != 0 &&parenthesesStack.Peek() == c)
                parenthesesStack.Pop();
            else if (parenthesesPair.TryGetValue(c, out char pair))
                parenthesesStack.Append(pair);
        }
        
        // Fråga:
        // Varför är Dictionary + Stack bättre än bara Stack med if/else för matchningen?
        Console.WriteLine("Kan lägga till och ta bort beroende på vad som görs lättare och behöver inte göra massa if/switch för att kolla varje bracket");

        return false;
    }

    // ============================================================
    // DEL 8 - STACKEN OCH HEAPEN
    // ============================================================

    static void MemoryLab()
    {
        Console.WriteLine("=== Value type: int ===");

        int number1 = 10;
        int number2 = number1;

        number2 = 99;

        Console.WriteLine($"number1: {number1}");
        Console.WriteLine($"number2: {number2}");

        Console.WriteLine();
        Console.WriteLine("=== Value type: struct ===");

        ScoreValue score1 = new ScoreValue(10);
        ScoreValue score2 = score1;

        score2.Points = 99;

        Console.WriteLine($"score1.Points: {score1.Points}");
        Console.WriteLine($"score2.Points: {score2.Points}");

        Console.WriteLine();
        Console.WriteLine("=== Reference type: class ===");

        ScoreReference refScore1 = new ScoreReference(10);
        ScoreReference refScore2 = refScore1;

        refScore2.Points = 99;

        Console.WriteLine($"refScore1.Points: {refScore1.Points}");
        Console.WriteLine($"refScore2.Points: {refScore2.Points}");

        Console.WriteLine();
        Console.WriteLine("=== Reference type: Product ===");

        Product product1 = new Product("KAF", "Kaffe", 15, 20);
        Product product2 = product1;

        product2.Stock = 0;

        Console.WriteLine(product1);
        Console.WriteLine(product2);

        // Fråga 1:
        // Varför ändras inte number1 när number2 ändras?
        Console.WriteLine("int är en value typ så när man sätter = så säger man att 10 = 10 men det ändras inte");

        // Fråga 2:
        // Varför ändras inte score1.Points när score2.Points ändras?
        Console.WriteLine("ScoreValue är en struct vilket är en value type. ");

        // Fråga 3:
        // Varför ändras product1.Stock när product2.Stock ändras?
        Console.WriteLine("Stock är en referenstyp så när du satte product2 så pekar den bara på samma referens");

        // Fråga 4:
        // Är Product en value type eller reference type?
        Console.WriteLine("Det är en class så det är en referens typ");

        // Fråga 5:
        // Vad ligger på heapen i Product-exemplet?
        Console.WriteLine("Alla variabler. Code, Name,Price och Stock. Det enda som storas i stack är referensen");

        // Fråga 6:
        // Vad innebär det att två variabler kan peka på samma objekt?
        Console.WriteLine("Betyder att om du kan nå samma värden från två olika variabler.");

        // Fråga 7:
        // Vad är skillnaden mellan stacken i minnet och Stack<T> som datastruktur?
        Console.WriteLine("Både använder LIFO. Stack i minnet är vad datorn ser men Stack<T> är en struktur för att lätt använda LIFO så den ligger i heap istället");
    }

    #endregion

    #region ExtraUppgifter


    // ============================================================
    // DEL 9 - REKURSION OCH ITERATION EXTRA om tid finns
    // ============================================================

    static void RecursionLab()
    {
        Console.WriteLine("=== Rekursion och iteration ===");

        Console.Write("Ange n: ");

        if (!int.TryParse(ReadLine, out int n))
        {
            Console.WriteLine("Du måste skriva ett heltal.");
            return;
        }

        if (n <= 0)
        {
            Console.WriteLine("n måste vara större än 0.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"RecursiveOdd({n}) = {RecursiveOdd(n)}");

        // När du har implementerat metoderna nedan kan du avkommentera raderna.

        Console.WriteLine($"RecursiveEven({n}) = {RecursiveEven(n)}");
        Console.WriteLine($"IterativeEven({n}) = {IterativeEven(n)}");
        Console.WriteLine($"FactorialRecursive({n}) = {FactorialRecursive(n)}");
        Console.WriteLine($"SumRecursive({n}) = {SumRecursive(n)}");
        Console.WriteLine($"SumIterative({n}) = {SumIterative(n)}");
        Console.WriteLine($"FibonacciRecursive({n}) = {FibonacciRecursive(n)}");
        Console.WriteLine($"FibonacciIterative({n}) = {FibonacciIterative(n)}");

        Console.WriteLine();
        Console.WriteLine("Trace av rekursion:");
        RecursiveOddWithTrace(n);

        Console.WriteLine();
        Console.WriteLine("Trace med indrag (visar rekursionsdjup):");
        RecursiveOddWithDepth(n, 0);

        // Fråga 1:
        // Vad är ett basfall?
        Console.WriteLine("Fall när vi väldigt lätt vet vad det borde vara eller ver att det inte kan forsätta så vi returnerar.");

        // Fråga 2:
        // Varför måste en rekursiv metod ha ett basfall?
        Console.WriteLine("Det agerar som ett stop så det kan ta slut");

        // Fråga 3:
        // Vad händer på stacken när en metod anropar sig själv?
        Console.WriteLine("Den lägger in varje metod i stack och sen poppar alla när den nått en basfall");

        // Fråga 4:
        // Vilken version är mest minnesvänlig: rekursion eller iteration? Varför?
        Console.WriteLine("Interation, Datorn behöver inte spara alla gånger den kallar sig själv och backa tillbaka massa gånger");
    }

    static int RecursiveOdd(int n)
    {
        if (n <= 0)
        {
            throw new ArgumentException("n måste vara större än 0.");
        }

        if (n == 1)
        {
            return 1;
        }

        return RecursiveOdd(n - 1) + 2;
    }

    static int RecursiveEven(int n)
    {
        // Om n <= 0, kasta ArgumentException med meddelandet "n måste vara större än 0."
        // Om n == 1, returnera 2.
        // Annars returnera RecursiveEven(n - 1) + 2.
        //
        // Exempel:
        // RecursiveEven(1) = 2
        // RecursiveEven(2) = 4
        // RecursiveEven(3) = 6
        if (n <= 0)
        {
            throw new ArgumentException("n måste vara större än 0.");
        }

        if (n == 1)
        {
            return 2;
        }

        return RecursiveEven(n - 1) + 2;       
        
        return 0;
    }

    static int IterativeEven(int n)
    {
        // TODO:
        // Om n <= 0, kasta ArgumentException.
        // Använd en for-loop för att räkna fram det n:te jämna talet.
        //
        // Exempel:
        // IterativeEven(1) = 2
        // IterativeEven(2) = 4
        // IterativeEven(3) = 6
        if (n <= 0)
        {
            throw new ArgumentException("n måste vara större än 0.");
        }

        int value = 0;
        for (int i = 0; i < n; i++)
        {
            value += 2;
        }
        return value;
    }

    static int FactorialRecursive(int n)
    {
        // Fakultet:
        // 5! = 5 * 4 * 3 * 2 * 1 = 120
        //
        // Om n < 0, kasta ArgumentException.
        // Om n == 0 eller n == 1, returnera 1.
        // Annars returnera n * FactorialRecursive(n - 1).

        if (n < 0)
            throw new ArgumentException("n måste vara större än 0");
        if (n == 0 || n == 1)
            return 1;
        
        return FactorialRecursive(n-1)*n;
    }

    static int SumRecursive(int n)
    {
        // Summera alla tal från 1 till n med rekursion.
        //
        // SumRecursive(5)
        // = 5 + 4 + 3 + 2 + 1
        // = 15
        if (n < 0)
            throw new ArgumentException("n måste vara större än 0");
        if (n == 0) return 0;
        // could add n==1 to add an extra cycle but this works fine 
        return SumRecursive(n-1)+n;
    }

    static int SumIterative(int n)
    {
        // Summera alla tal från 1 till n med en loop.
        if (n < 0)
            throw new ArgumentException("n måste vara större än 0");

        int sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i;
        }
        return sum;
    }

    static int FibonacciRecursive(int n)
    {
        // Fibonacci:
        // 0, 1, 1, 2, 3, 5, 8, 13 ...
        //
        // Om n < 0, kasta ArgumentException.
        // Om n == 0, returnera 0.
        // Om n == 1, returnera 1.
        // Annars returnera FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2).
        if (n < 0)
            throw new ArgumentException("n måste vara större än 0");
        
        if (n == 0) return 0;
        if (n == 1) return 1;
        
        return FibonacciRecursive(n-1) + FibonacciRecursive(n-2);
    }

    static int FibonacciIterative(int n)
    {
        // Implementera Fibonacci med loop.
        // Denna version ska vara mer minnesvänlig än den rekursiva.
        if (n < 0)
            throw new ArgumentException("n måste vara större än 0");
        
        if (n == 0) return 0;
        if (n == 1) return 1;
        int lastSum = 0;
        int sum = 1;
        for (int i = 1; i < n; i++)
        {
            sum += lastSum;
            lastSum = sum;
        }
        return 0;
    }

    static int RecursiveOddWithTrace(int n)
    {
        Console.WriteLine($"Anropar RecursiveOddWithTrace({n})");

        if (n == 1)
        {
            Console.WriteLine("Basfall nått. Returnerar 1.");
            return 1;
        }

        int result = RecursiveOddWithTrace(n - 1) + 2;

        Console.WriteLine($"RecursiveOddWithTrace({n}) returnerar {result}");

        return result;
    }

    static int RecursiveOddWithDepth(int n, int depth)
    {
        string indentation = new string(' ', depth * 2);

        Console.WriteLine($"{indentation}RecursiveOddWithDepth({n})");

        // Lägg till basfall: om n == 1, skriv ut med indrag att basfallet nåtts och returnera 1.
        // Annars: anropa RecursiveOddWithDepth(n - 1, depth + 1) rekursivt.
        // Spara resultatet, skriv ut med indrag vad metoden returnerar, och returnera resultatet.
        // Jämför utskriften med RecursiveOddWithTrace — vad tillför indraget?

        if (n == 1)
        {
            Console.WriteLine("Base found. returning 1");
            return 1;
        }
        
        return RecursiveOddWithDepth(n-1,depth+1);
    }

    // ============================================================
    // DEL 10 - FILHANTERING, EXTRA
    // ============================================================

    static void SaveLogToFile()
    {
        // TODO:
        // Kontrollera om logMessages är tom — skriv meddelande om den är det.
        // Annars: spara alla loggmeddelanden till en fil som heter "logg.txt".
        // Skriv ut hur många rader som sparades och var filen finns.
        //
        // Tips:
        // File.WriteAllLines("logg.txt", logMessages);
        // Console.WriteLine($"Sparade {logMessages.Count} rader till logg.txt");

        Console.WriteLine("TODO: Implementera SaveLogToFile.");
    }

    #endregion
}
