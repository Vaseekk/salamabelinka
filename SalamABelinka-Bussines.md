 # Videohra v CLI: Salám a Belinka - Bussines požadavky
* *SSPŠ*
* *Verze 3*
* *Václav Bohdanecký*
* *6.11.2024*

## Obsah
1. Historie Dokumentu
2. Úvod
3. Požadavky

## Historie Dokumentu
### Verze 1
* **Autor:** Václav Bohdanecký
* **Komentář:** První verze dokumentu

### Verze 2
* **Autor:** Václav Bohdanecký
* **Komentář:** Očíslování požadavků

## Verze 3
* **Autor:** Václav Bohdanecký
* **Komentář:** Úprava požadavků

## Úvod
* **Účel dokumentu** – Účelem dokumentu je popsání všech požadovaný funkcí programu a nefunkčních požadavků.
* **Cílová skupina:** Pan učitel programování
* **Kontakt:** bohdanecky.va.2022@skola.ssps.cz
* **Odkazy na další dokumenty:** SalamABelinka-Funkcni
* **Strany:** 
    1. **Provozovatel** - SSPŠ 
    2. **Uživatel** - Hráč

## Požadavky
### Funkční požadavky
1. 1 nebo 2 hráči ovládají hada/y klávesami

     **Priorita:** Vysoká

3. Hadi se **hýbají bez přestání**

     **Priorita:** Vysoká

4. Po celém poli se náhodně objevuje ovoce, které když je pozřeno hadem, tak se přičítá skóre

     **Priorita:** Vysoká

5. Had je stejne dlouhý jako cislo **x skore + 1**

     **Priorita:** Vysoká

6. Na poli se při spuštění hry objeví náhodně rozmístěné kameny a když do nich had narazí tak hra končí

     **Priorita:** Nižší

7. Jakmile had vyjede z hracího pole tak hra končí

     **Priorita:** Vysoká

8. Na konci hry se vypíše skóre obou hadů a program se zeptá na jméno obou hadu a uloží skóre se jmény do souboru

     **Priorita:** Střední

9. Po skončení hry se zároveň vypíše 5 nejlepších skóre společne se jmény hadů, které daného skóre dosáhli

    **Priorita:** Nižší

10. **Zobrazení** - herní pole by se měla zobrazovat jako čtverec v CLI oknu

     **Priorita:** Střední

### Nefunkční požadavky
1. **CLI Aplikace** - aplikace bude vyvinuta pro prostředí příkazové řádky
2. **Podporované zařízení** - aplikace bude optimalizována jen na zařízení co podporují .NET v příkazovém prostředí verze 4.8 a novější
3. **Vývojové prostředí** - aplikace bude vyvíjena ve Visual Studio 2022 v .Net frameworku
