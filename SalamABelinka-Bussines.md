 # Videohra v CLI: Salám a Belinka - Bussines požadavky
* *SSPŠ*
* *Verze 1*
* *Václav Bohdanecký*
* *17.9.2024*

## Obsah
1. Historie Dokumentu
2. Úvod
3. Požadavky

## Historie Dokumentu
### Verze 1
* **Autor:** Václav Bohdanecký
* **Komentář:** První verze dokumentu

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
* 1 nebo 2 hráči ovládají hada/y klávesami
 **Priorita:** Vysoká
* Hadi se **hýbají bez přestání**
 **Priorita:** Vysoká
* Po celém poli se náhodně objevuje ovoce, které když je pozřeno hadem, tak se přičítá skóre
 **Priorita:** Vysoká
* Had je stejne dlouhý jako cislo **x skore + 1**
 **Priorita:** Vysoká
* Na poli se při spuštění hry objeví náhodně rozmístěné kameny a když do nich had narazí tak hra končí
 **Priorita:** Nižší
* Jakmile had vyjede z hracího pole tak hra končí
 **Priorita:** Vysoká
* Na konci hry se vypíše skóre obou hadů a program se zeptá na jméno obou hadu a uloží skóre se jmény do souboru
 **Priorita:** Střední
* Po skončení hry se zároveň vypíše 5 nejlepších skóre společne se jmény hadů, které daného skóre dosáhli
 **Priorita:** Nižší
* **Zobrazení** - aplikace by se měla zobrazovat jako čtvercové okno CLI
 **Priorita:** Střední

### Nefunkční požadavky
* **CLI Aplikace** - aplikace bude vyvinuta pro prostředí příkazové řádky
* **Podporované zařízení** - aplikace bude optimalizována jen na zařízení co podporují .NET v příkazovém prostředí verze 4.8 a novější
* **Vývojové prostředí** - aplikace bude vyvíjena ve Visual Studio 2022 v .Net frameworku
