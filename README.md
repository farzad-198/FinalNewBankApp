**Kort förklaring av projektet:**

* Projektet har ändrats från att spara data i **minnet (List)** till att använda en **databas med Entity Framework Core**.
* En **DbContext** skapades för att koppla programmet till **SQL Server**.
* Klasserna **AccountBase** och **BankTransaction** används som datamodeller i databasen.
* Med **Migration** skapades tabellerna `Accounts` och `BankTransactions`.
* Ett **Repository** lades till för att hantera all databaslogik på ett ställe.
* Menysystemet (`Bank` och `AccountHandler`) gör det möjligt att skapa konton, sätta in pengar, ta ut pengar och visa transaktioner.
* **Arv (Inheritance)** används så att flera kontotyper ärver från `AccountBase`.
* En **Factory (AccountFactory)** används för att skapa olika kontotyper.
* Två extra konton lades till: **StudentAccount** och **SavingsAccount**.

**Projektet förväntas kunna:**

* Skapa flera olika kontotyper.
* Spara konton och transaktioner i databasen.
* Hantera insättningar och uttag.
* Visa alla transaktioner för ett konto.
* Behålla data i databasen även när programmet stängs.
* Ha en tydlig struktur med **Factory Pattern, Repository Pattern och Entity Framework Core**.

