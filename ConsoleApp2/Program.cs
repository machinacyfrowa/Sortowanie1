// Funkcja sortuje przekazaną tablicę metodą bąbelkową.
// Funkcja modyfikuje tablicę przekazaną przez referencję i zwraca ją po zakończeniu sortowania.
int[] sortowanieBabelkowe(int[] tablica)
{
    // Pętla zewnętrzna powtarza pełny przebieg tablicy, dopóki w jednym przebiegu
    // nie zostanie wykonana żadna zamiana sąsiednich elementów.
    while(true)
    {
        // Flaga informuje, czy podczas bieżącego przebiegu wystąpiła choć jedna zamiana.
        // Po wystąpieniu zamiany algorytm musi wykonać kolejny przebieg.
        bool czyZmienione = false;

        // Porównujemy każdą parę sąsiednich elementów:
        // bieżący element tablica[i] i następny element tablica[i + 1].
        for(int i = 0; i < tablica.Length-1; i++)
        {
            // Jeżeli bieżący element jest większy od następnego, kolejność jest błędna
            // i oba elementy muszą zostać zamienione miejscami.
            if (tablica[i] > tablica[i+1])
            {
                // Ustawienie flagi oznacza, że tablica wciąż nie jest posortowana
                // i kolejny przebieg musi zostać wykonany.
                czyZmienione = true;

                // Tymczasowa zmienna jest potrzebna, ponieważ bez niej pierwotna wartość
                // tablica[i] zostałaby utracona podczas pierwszego przypisania.
                int temp = tablica[i];

                // Przenosimy mniejszy element do lewej pozycji.
                tablica[i] = tablica[i + 1];

                // Przenosimy większy element do prawej pozycji.
                tablica[i + 1] = temp;
            }
        }

        // Brak zamian w całym przebiegu oznacza, że wszystkie sąsiednie elementy
        // są już uporządkowane, więc dalsze wykonywanie pętli nie jest potrzebne.
        if (!czyZmienione) break;
    }

    // Zwracamy tę samą tablicę, która została przearanżowana przez algorytm.
    return tablica;
}
// Funkcja sortuje tablicę losowo ustawiając jej elementy aż do otrzymania
// permutacji spełniającej warunek niemalejącego porządku (Bogo Sort).
int[] bogoSortowanie(int[] tablica)
{
    // Licznik pokazuje, ile losowych ustawień zostało sprawdzonych do momentu sukcesu.
    int licznik = 0;

    // Zapamiętujemy długość tablicy, aby wielokrotnie nie odczytywać jej z właściwości Length.
    int length = tablica.Length;

    // Wynikowa tablica będzie przechowywać losowo ułożone elementy wejściowe.
    int[] posortowanaTablica = new int[length];

    // Tablica flag pilnuje, aby każdy element tablicy wejściowej został użyty dokładnie raz
    // podczas jednej próby losowego ułożenia.
    bool[] tablicaUzyc = new bool[length];

    // Wspólny generator losowy jest używany w każdej iteracji, dzięki czemu nie tworzymy
    // osobnego generatora przy każdym losowaniu i nie tracimy czasu na jego inicjalizację.
    Random r = new Random();
    // Pętla tworzy kolejne losowe próby, dopóki wynik nie zostanie znaleziony.
    while(true)
    {
        // Na początku każdej nowej próby wszystkie indeksy muszą być ponownie dostępne.
        // Bez tego resetu wszystkie operacje z poprzedniej próby pozostałyby oznaczone jako użyte.
        Array.Fill(tablicaUzyc, false);

        // Tworzymy losową permutację elementów tablicy wejściowej.
        for (int i = 0; i < length; i++)
        {
            // Losujemy indeks w zakresie od 0 do length - 1, czyli dokładnie na tablicę wejściową.
            int losowyIndex = r.Next(0, length);

            // Losowy indeks jest dostępny tylko wtedy, gdy odpowiadający mu element
            // nie został jeszcze przeniesiony do bieżącej permutacji.
            if (!tablicaUzyc[losowyIndex])
            {
                // Przenosimy element z tablicy wejściowej na bieżącą pozycję wyniku.
                posortowanaTablica[i] = tablica[losowyIndex];

                // Oznaczamy indeks jako wykorzystany, aby ten sam element nie został
                // dodany ponownie w tej samej losowej próbie.
                tablicaUzyc[losowyIndex] = true;
            }
            else
            {
                // Element został już użyty, więc bieżąca pozycja musi pozostać niezapełniona.
                // Dekrementacja i-- cofa pętlę o jeden krok; kolejny krok for ponownie
                // zwiększy i, co spowoduje ponowne losowanie dla tej samej pozycji.
                i--;
            }
        }

        // Jedno pełne, losowe ustawienie wszystkich elementów to jedna iteracja algorytmu.
        licznik++;
        Console.WriteLine("Bogo sortowanie wykonało " + licznik + " iteracji");

        // Zakładamy początkowo, że wynikowa tablica jest posortowana.
        // Weryfikacja poniżej może obalić to założenie po znalezieniu błędnej pary.
        bool czyPosortowana = true;
        // Sprawdzamy wszystkie sąsiednie pary wylosowanej tablicy.
        for (int i = 0; i < length - 1; i++)
        {
            // Jeżeli lewy element jest większy od prawego, porządek jest błędny.
            if (posortowanaTablica[i] > posortowanaTablica[i + 1])
            {
                // Zmiana flagi oznacza, że bieżąca losowa permutacja nie jest rozwiązaniem.
                czyPosortowana = false;

                // Po znalezieniu pierwszej nieprawidłowej pary dalsza weryfikacja nie jest potrzebna,
                // więc przerywamy tę pętlę i przechodzimy do kolejnej losowej próby.
                break;
            }
        }

        // Jeżeli żadna para nie naruszyła porządku rosnącego, znaleźliśmy posortowaną permutację.
        if(czyPosortowana) 
            break;
    }
    // Zwracamy wynik dopiero po potwierdzeniu, że wszystkie sąsiednie elementy
    // są ułożone w kolejności niemalejącej.
    return posortowanaTablica;
}

// Inicjalizujemy tablicę o siedmiu elementach. To pierwsze przypisanie jest później
// zastępowane inną inicjalizacją, dlatego końcowa długość i zawartość pochodzi z drugiej wartości.
int[] tablica = new int[7];

// Zadajemy konkretną zawartość tablicy, która zostanie przesortowana przez pierwszy algorytm.
tablica = new int[] { 8, 5, 4, 3, 6, 7, 2 };

// Wywołujemy sortowanie bąbelkowe i zapisujemy wynik w osobnej zmiennej.
// Ponieważ sortowanie działa na tej samej tablicy, zmienna tablica zostaje również zmieniona.
int[] posortowanaTablica = sortowanieBabelkowe(tablica);

// Przygotowujemy trzyelementową tablicę jako osobny przykład dla Bogo Sort.
int[] malaTablica = new int[3] { 4,3,2 };

// Uruchamiamy Bogo Sort na posortowanej wcześniej tablicy siedmiu elementów.
// Oryginalny kod przekazuje tutaj zmienną tablica, a nie utworzoną wyżej zmienną malaTablica.
int[] bogoTablica  = bogoSortowanie(tablica);

// Oczekiwanie na naciśnięcie klawisza utrzymuje konsolę otwartą po zakończeniu obu sortowań,
// dzięki czemu użytkownik może zobaczyć komunikaty wypisane przez Bogo Sort.
Console.ReadKey();
