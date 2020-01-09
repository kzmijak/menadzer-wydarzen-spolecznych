using MWS.Lines;

namespace MWS.Pages
{
    class DBConfig : _Page
    {
        public DBConfig(StaticLine note = null) : base(note)
        {
            Contents.Add(new StaticLine("KONFIGURACJA BAZY DANYCH"));
            Contents.Add(new ActiveLine("Stwórz bazę danych", "Wykonaj skypt utworzenia tworzenia modelu bazy danych."));
            Contents.Add(new ActiveLine("Stwórz zbiór procedur", "Wykonaj skrypt tworzenia zbioru procedur dla bazy danych."));
            Contents.Add(new ActiveLine("Usun bazę danych", "Wykonaj skrypt usunięcia modelu bazy danych."));
            Contents.Add(new ActiveLine("Usuń zbiór procedur", "Wykonaj skrypt usuwania zbioru procedur dla bazy danych."));
            Contents.Add(new ActiveLine("Powrót", "Powrót do panelu logowania"));
            Contents.Add(Note);
        }

        public override void React(_Line line)
        {
            switch(line.Index)
            {
                case 1:
                    DbHelper.InstallDatabase();
                    break;
                case 2:
                    DbHelper.InstallProcedures();
                    break;
                case 3:
                    DbHelper.RemoveDatabase();
                    break;
                case 4:
                    DbHelper.RemoveProcedures();
                    break;
                case 5:
                    if(DbHelper.CheckConnection())
                    {
                        DisplayAdapter.Display(new Login());
                    }
                    else
                    {
                        DisplayAdapter.Display(new DBConfig(new StaticLine("Przed kontynuacją musisz utworzyć bazę danych i procedury", System.ConsoleColor.Red)));
                    }
                    break;

            }
            DisplayAdapter.Display(new DBConfig(new StaticLine("Operacja przebiegła pomyślnie", System.ConsoleColor.Green)));
        }
    }
}