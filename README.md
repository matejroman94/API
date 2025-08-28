# Webové Api pro klienta VZP by Matěj Roman

## Co bylo úkolem

Vytvořit API službu, která dokáže rozklíčovat vstupní data událostí ze systému Kadlec Elektronika z různých poboček VZP a uložit informace do navržených tabulek. Události jsou ukládány do systému jako záznamy po 10 bajtech. Jednou za čas systém odešle do API všechny poslední události, která je následně zpracuje a uloží.

Příklad tabulky:

| Událost | Kód | P0 | P1 | P2 | P3 | P4 | P5 |
| --------- | --------- | --------- |
| Stisk tlačítka, zařazení k činnosti | 01h  | poř. číslo MSB   | poř. číslo LSB  | č. tiskárny | činnost | odhad ček. MSB | odhad ček. LSB |
| Vyvolání klienta | 02h  | poř. číslo MSB   | poř. číslo LSB  | přepážka | činnost | doba čekání MSB | doba čekání LSB |

Pracovník může získat následující statistiky:

- **Průměrná doba čekání klienta**
- **Průměrná doba odbavení klienta**
- **Navštěvovanost poboček**
- **Oblíbenost agend**
- **Aktuální obsazenost přepážek**
- **Přihlášení / Odhlášení pracovníka**
- **...**

## Hlavní částí projektu
- **VZPStatAPI** - hlavní projekt API

- **VZPStat.EventAsByte** - knihovna, obsahuje pouze třídu EventAsByte

- **TestProject**

- **Repository** - knihovna, abstrakce přístupu k datům

- **Logger** - knihovna, vlastní logování do souboru

- **Domain** - knihovna, modely

- **Database** - knihovna, context a migrační soubory

- **Common** - knihovna, třídy pro zpracování událostí

## Technologie

- **NET 6**
- **Entity framework, Code first**
- **MSSQL**
- **NUnit, Moq, Bogus**
