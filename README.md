# Webové Api pro klienta VZP -  Matěj Roman

## Přehled

API funguje jako Windows služba. Systémy na různých pobočkách VZP odesílají data na specifické endpointy. Současně API ověřuje role uživatelů přes LDAP, kteří se přihlašují do systémů Kadlec Elektronika. Dále slouží jako hlavní vstupní bod pro filtrování dat z databáze. V budoucnu je plánováno další upravování endpointů, ale poslední verzi již nemám k dispozici.

U tohoto projektu jsem se především snažil vhodně přetransformovat přicházející data z poboček a uložit je do databáze. 
Většinu chyb se mi podařilo odchytnout díky jednotkovým testům. 
Díky náhodně generovaným událostem pomocí knihovny Bogus jsem mohl simulovat chování v produkčním prostředí a odhalit tak další chyby.


## Co bylo úkolem

Vytvořit API službu, která dokáže rozklíčovat vstupní data událostí ze systému Kadlec Elektronika z různých poboček VZP a uložit informace do navržených tabulek.  
Jednou za čas systém odešle do API 
všechny poslední události, která je následně zpracuje a uloží do databáze.

Události jsou ukládány do systému jako záznamy po 10 bajtech.
Příklad tabulky:

| Událost | Kód | P0 | P1 | P2 | P3 | P4 | P5 |
| --------- | --------- | --------- | --------- | --------- | --------- | --------- | --------- |
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
