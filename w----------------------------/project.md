# Projekt Zaliczeniowy

- każdy etap wypada zaliczyć (binarnie)
- Ocena końcowa bierze pod uwagę zaliczenie wszystkich etapów
- Projekt można zrobić max w grupie 2 osobowej
- By zaliczyć kurs, trzeba zaliczyć projekt

## Etap 1

- wybór tematu projektu, rozpoznanie domeny
- stworzenie bazy danych (1 wersja bazy danych projektu)
- Zasilenie fragmentu bazy danymi z użyciem REST API
- Odczytanie danych z bazy danych z użyciem REST API
- Modyfikacja danych w bazie danych z użyciem REST API

## Etap 2

- zaimplementuj logowanie do aplikacji (widok) (polecam "ASP.NET Core Identity")
- Nawigacja między widokami dla zalogowanych użytkowników
- Wyświetlanie danych z bazy w jakimś widoku
- Dodawanie danych do bazy z użyciem formularza
- Modyfikacja danych w bazie z użyciem formularza
- Usuwanie danych z bazy z użyciem formularza

Proszę to zrobić bez "fajerwerków" to mają być rzeczy, które pomogą Państwu przy implementacji projektu.
Jeżeli ktoś chce, może pisać już na poważnie, tak jak planuje zrobić w projekcie.

## Etap 3

- min 3 / 4 feature'y zgodne z projektem
- zabezpieczenia przed dostępem do zasobów (logowanie)
- min 2 role
- sesja JWT
- modyfikacja danych w bazie z użyciem dodatkowego REST API wystawionego przez aplikację
- widoki nie musza byc piekne, ale super jakby dzialaly :)

---

# Referencyjne laboratoria

## lab 08

1. [3 punkty] Napisz metodę wczytującą dane z pliku CSV do dowolnej struktury danych (na przykład na List<List<String>>). Metoda ma zwrócić tą strukturę danych oraz informację o nazwach kolumn - nazwy kolumn proszę wczytać z headera pliku. Metoda jako argument ma przyjmować nazwę pliku csv oraz separator dzielący od siebie poszczególne wartości w kolumnach. Zakładamy, że ten separator nie występuje nigdzie jako wartość kolumny, na przykład, jeśli separatorem jest przecinek, to przecinek nie występuje w kolumnach z napisami.

2. [2 punkty] Napisz metodę, która jako parametr pobiera dane zwrócone przez metodę z punktu 1. Metoda na podstawie tych danych ma zwracać typy danych dla poszczególnych kolumn oraz czy kolumna może przyjmować wartości NULL czy też nie. Zakładamy, że jeśli kolumna nigdy nie ma wartości NULL, to nie może przyjmować wartości NULL. Jeżeli wszystkie pola tej kolumny można zrzutować na int, kolumna jest typu INTEGER, jeżeli kolumna nie jest typu INTEGER a wszystkie jej pola można zrzutować na double, to jest typu REAL. W pozostałym przypadku kolumna jest typu TEXT.

3. [2 punkty] Napisz metodę, która jako parametry przyjmuje dane zwrócone przez metodę z punktu 2, nazwę tabeli do utworzenia oraz obiekt klasy SqliteConnection. Metoda na podstawie danych ma utworzyć w bazie odpowiednią tabelę (o zdanych nazwach kolumn i typach) i nazwie zgodnej z przekazaną. Połączenie z bazą przekazywane jest w obiekcie SqliteConnection.

4. [2 punkty] Napisz metodę, która jako parametry przyjmuje dane, które mają znaleźć się w tabeli (dane zostały wczytane metodą z punktu 1), nazwę tabeli oraz obiekt klasy SqliteConnection. Metoda ma wypełnić tabelę utworzoną w punkcie 3 tymi danymi.

5. [1 punkt] Napisz metodę, która jako parametr przyjmuje nazwę tabeli oraz obiekt klasy SqliteConnection. Metoda przy pomocy kwerendy SELECT ma wypisać do konsoli wszystkie dane, które znajdują się w tej tabeli. Proszę wypisać również nazwy kolumn.

## lab 10

Celem laboratorium jest zapoznanie z podstawami pisania aplikacji webowych przy pomocy ASP.NET core MVC przy wykorzystaniu kontrolerów i widoków. Aplikacja będzie wykorzystywała mechanizm sesji do zapamiętania faktu zalogowania przez użytkownika. Dane wprowadzane do aplikacji będą zapamiętywane w bazie danych SQLite. Hasło w bazie będzie przechowywane w postaci skrótu (hash-u).

1. [4 punktów] Stwórz nową aplikację ASP.NET core MVC:
- Stwórz kontroler (albo użyj istniejącego w szablonie kontrolera) i dodaj do niego metodę obsługującą sprawdzanie prawidłowości wpisanego hasła i loginu. Stwórz dla tej metody odpowiedni widok Razor, który będzie zawierał formularz z polami, które będą pobierały login i hasło. Para hasło i login mogą być na razie wpisane w kodzie kontrolera na potrzeby testów (w poleceniu 2 zastąpimy je poprzez zapisanie loginu i hasła w bazie). 
- Jeśli login i hasło zostały wprowadzone prawidłowo, po zatwierdzeniu formularza powinien zostać wyświetlony widok Razor informujący o fakcie zalogowania. Na widoku informującym o fakcie zalogowania powinien być formularz umożliwiający wylogowanie. Fakt zalogowania powinien być zapamiętany w sesji. 
- Jedynie zalogowani użytkownicy powinni mieć dostęp do innych zasobów aplikacji poza stroną logowania. "Zasoby aplikacja" to metody kontrolera, widoki Razor i strony Razor. 
- Jeżeli ktoś nie jest zalogowany a wpisze adres URL jakiegoś zasobu powinien być przekierowywany na stronę logowania.
- Proszę tak zmodyfikować plik _Layout.cshtml, aby menu znajdująca się tagu <header> zawierało linki do istniejących zasobów aplikacji. Niezalogowany użytkownik powinien widzieć jedynie link do strony logowania.

Poniższy kod dodany do Program.cs powoduje, że jeżeli użytkownik wpisze adres nieistniejącego zasobu zostaje przekierowany do zasobu "/IO/Logowanie".

```cs

app.Use(async (ctx, next) =>
{
    await next();

    if(ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/IO/Logowanie";
        await next();
    }
});

```

2. [2 punkty] Dodaj do aplikacji obsługę bazy danych SQLite. Przy starcie aplikacji mają być tworzone tabele: "loginy" (tabela zawiera informacje o loginie i haśle) oraz "dane" (tabela ma mieć pole tekstowe z danymi). Dodaj klucze główne! Wypełnij obie tabele przykładowymi danymi. Prawidłowość logowania powinna być sprawdzana na podstawie danych, które są w tabeli loginy.

3. [2 punkty] Dodaj metodę kontrolera + widok razor pozwalający na dodawanie nowych danych do tabeli "dane" oraz wyświetlanie rekordów, które się już znajdują w tej tabeli.

4. [2 punkty] Zmodyfikuj aplikację w taki sposób, aby hasła trzymane w tabeli "loginy" były przechowywane jako skróty obliczone algorytmem MD5.

## lab 11

Celem laboratorium jest wykonanie projektu aplikacji internetowej napisanej w technologii ASP.NET core MVC oraz REST API. Aplikacja będzie wykorzystywała mechanizm sesji do zapamiętania faktu zalogowania przez użytkownika. Dane wprowadzane do aplikacji będą zapamiętywane w bazie danych SQLite. Hasło w bazie będzie przechowywane w postaci skrótu (hash-u). Dostęp do bazy powinien odbywać się przy pomocy modelu danych w ramach MVC (nie poprzez natywne kwerendy do bazy).

- Projekt należy wykonać w grupach co najwyżej dwuosobowych.
- Projekt należy oddać na kolejnych zajęciach laboratoryjnych. Jeżeli projekt zostanie oddany po tym terminie, można otrzymać z niego co najwyżej ocenę dostateczną (3.0).
- Projekt musi spełniać następujące kryteria formalne:
    - Aplikacja internetowa musi korzystać z co najmniej 4 tabel bazy danych.
    - Zarządzanie danymi w tabelach musi być wykonane w całości przez interfejs webowy.
    - Pierwsze uruchomienie aplikacji musi dodawać wszystkie ewentualnie potrzebne do działania dane do bazy.
    - Aplikacja musi "robić coś więcej" niż tylko wyświetlać zawartości poszczególnych tabel - proszę przygotować jakieś ciekawe ich zestawienia. Na przykład jeżeli aplikacja gromadzi informacje o rozgrywkach ligowych drużyn piłkarskich mogłaby mieć możliwość wyświetlenia najskuteczniejszych zawodników, kalendarz nadchodzących rozgrywek, ranking drużyn itp.
    - Do poszczególnych widoków i funkcjonalności aplikacji powinno być możliwe dostanie się z poziomu menu / dostępnych na stronie linków itp., to znaczy że jeśli użytkownik zna domenę pod którą jest dostępna aplikacja, to czytając zawartość strony może po niej swobodnie nawigować bez wpisywania "ręcznie" jakiś adresów www.
    - Do aplikacji musi być dołączona dokumentacja (może być w formie strony internetowej, pliku w formacie md lub pdf) na której znajdzie się tytuł projektu, informacje o autorach, opis, do czego aplikacja służy oraz opis jej funkcjonalności / sposobu użycia. 

Wszystkie powyższe kryteria są obowiązkowe, aby aplikacja mogła zostać oceniona. Tematykę projektu proszę uzgodnić z prowadzącymi zajęcia!

Kryteria oceny:
- [5 punktów] Stworzenie aplikacji która implementuje zaproponowany temat w architekturze ASP.NET MVC core. Zamodeluj powiązania relacyjne pomiędzy tabelami w modelach!
- [2 punkty] Aby zrobić ten podpunkt należy wykonać wcześniejszy podpunkt. Należy wykonać zabezpieczenie aplikacji przed nieautoryzowanym dostępem poprzez dodanie logowania poprzez login i hasło. Jedynie zalogowani użytkownicy mają mieć możliwość dostępu do poszczególnych zasobów (widoków) projektu. Tabele potrzebne do obsługi logowania nie wliczają się do limitu co najmniej 4 tabel projektu. Podczas pierwszego uruchomienia aplikacji ma być stworzony pierwszy użytkownik, który będzie pełnił rolę administratora. Tylko administrator ma prawo dodawać nowych użytkowników oraz wyświetlać informacje o istniejących w systemie użytkownikach. Hasło w bazie proszę przechowywać w postaci skrótu (hashu).
- [3 punkty] Aby zrobić ten podpunkt należy wykonać również oba wcześniejsze podpunkty. Dodaj do aplikacji możliwość dodawania, usuwania, modyfikacji oraz wyświetlania danych przy użyciu REST API. Autentykacja oraz autoryzacja żądań REST powinna się odbywać w oparciu o dodatkowo przesyłane w żądaniach nazwę użytkownika oraz klucz/token, który jest niepustym ciągiem znaków. Każdy użytkownik ma przypisany do swojego konta taki klucz. Przygotuj programy konsolowe demonstrujące prawidłowość działania REST API.