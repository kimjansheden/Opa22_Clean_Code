# Opa22_Clean_Code
My third home assignment in C# at the second semester of my education.

## Notes
See WebShopCleanCode_Org.zip for the original code before refactoring.

## ASSIGNMENT (In Swedish)
### Bakgrundsbeskrivning
För att kunna programmera professionellt så behövs en viss kapacitet att koda på ett sätt så att andra än en själv kan läsa koden. Processen att göra kod snyggare kallas för Refactoring, och är i sig själv en stor del av att vara programmerare. Det är även att kunna utöva Clean Code, vilket är kod som skall kunna vara mer lättläslig för mänskliga ögon. I vissa specifika fall så kan det även finnas speciellt skapade Design Patterns som löser problemet vi har på ett bra sätt.

### Varför ska ni utföra detta arbete?
Vi utför denna uppgift för att vi skall lära oss att koda i Clean Code eller med Design Patterns. Ni kommer vilja kunna dessa sakerna när ni kommer ut i verkliga livet och medarbetare vill titta på er kod.

### Vad ska ni leverera?
Ni skall leverera ett styck projekt WebShopCleanCode, antingen C# eller Java versionen, där ni har applicerat det ni lärt er inom Clean Code och Design Patterns för att göra detta till ett bättre projekt.

### Vad ska ni göra?
Ett projekt som heter WebShopCleanCode har laddats upp till Learnpoint. Det finns två versioner, en i Java och en i C#. Välj en utav dem.

Detta projekt är medvetet programmerat utan att följa Clean Code eller användande några Design Patterns. Eran uppgift är att gå igenom detta projekt och applicera Clean Code och Design Patterns.

Det viktigaste i detta projekt är att funktionaliteten av programmet är detsamma. Refactoring, som det heter, är att ändra hur koden ser ut utan att ändra vad den gör. Vi vill göra koden snyggare utan att ändra själva programmet.

### Hur löser ni uppgiften?
Använd debuggning! Sätt ut Break Points, kör programmet, kolla när saker och ting stannar. Extrahera metoder, även ifall ni inte hittar någon duplicerad kod. Kom ihåg att om det är kod som ser ut likadant men skillnaden är i vilka värden som används, så bör det vara möjligt att göra dessa som funktioner som tar de olika värdena som parametrar.

Om ni programmerar via Git och GitHub så kan ni skapa Commits som ni kan gå tillbaka till ifall ni gör några misstag. Det kan absolut vara en bra idé att göra kopior av era projekt så ni har någonting att gå tillbaka till om ni strular till någonting.

### Struktur för arbetet
Uppgiften utförs individuellt.

### BEDÖMNING OCH ÅTERKOPPLING

Bedömning sker med följande betygskriterier
#### För godkänt (G) på projektarbetet skall följande krav uppfyllas:

Ni har extraherat metoder från den stora metoden i WebShop-klassen. Ni har skapat nya klasser och/eller funktioner för att hantera duplicerad kod, och ni har följt principerna av Clean Code till en acceptabel grad. Jag tänker inte ge en definitiv mängd ”så här mycket”, för det finns ingen definitiv mängd i Clean Code, utan gör så gott ni kan så säger jag ifall det är bra eller inte sedan.

Utöver detta har ni implementerat minst två Design Patterns på relativt intelligenta ställen. Exempel för de som har problem, Builder och Proxy Design Patterns borde båda kunna appliceras. Det hjälper även ifall ni skriver vad för Design Pattern ni använder er av och varför.

När man kör programmet så händer samma saker som innan ni ändrade koden.

#### För väl godkänt (VG) på projektarbetet skall dessutom följande krav uppfyllas:
Ni har implementerat Design Patterns specifikt för att bli av med alla Switch Case eller Else If metoder som just nu används beroende på vilken meny som man är i. Ni skulle kanske vilja kolla på Command eller State Design Pattern för att lösa detta problem. Detta är utöver de två Design Patterns som behövs för G. Man skall kunna lösa detta utan en enda Switch Case.

Utöver detta skall ni även ha implementerat en algoritm. Det finns ett ställe i applikationen där jag applicerar Bubble Sort, en dålig sorteringsalgoritm. Ni skulle kunna byta den mot någon bättre. Alternativt kan ni programmera ett separat projekt där ni visar att ni gjort A*, eller någon annan algoritm, det skulle kunna funka utmärkt också.
