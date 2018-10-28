using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120Projekt
{
    static class APIDemo
    {
        #region KlasseA
        // Create
        public static void DemoACreate()
        {
            Debug.Print("--- DemoACreate ---");
            // KlasseA (lange Syntax)
            Data.Aufgabe klasseA1 = new Data.Aufgabe();
            klasseA1.Inhalt = "Artikel 1";
            klasseA1.HasPriorityHigh = true;
            klasseA1.Aufgabensammlung = Data.Aufgabensammlung.LesenAttributWie("Artikelgruppe 1").FirstOrDefault();
            Int64 klasseA1Id = klasseA1.Erstellen();
            Debug.Print("Artikel erstellt mit Id:" + klasseA1Id);
        }
        // Read
        public static void DemoARead()
        {
            Debug.Print("--- DemoARead ---");
            // Demo liest alle
            foreach (Data.Aufgabe klasseA in Data.Aufgabe.LesenAlle())
            {
                Debug.Print("Artikel Id:" + klasseA.AufgabeId + " Name:" + klasseA.Inhalt + " Artikelgruppe:" + klasseA.Aufgabensammlung.Name);
            }
        }
        // Update
        public static void DemoAUpdate()
        {
            Debug.Print("--- DemoAUpdate ---");
            // KlasseA ändert Attribute
            Data.Aufgabe klasseA1 = Data.Aufgabe.LesenID(1);
            klasseA1.Inhalt = "Artikel 1 nach Update";
            klasseA1.AufgabensammlungId = 2;  // Wichtig: Fremdschlüssel muss über Id aktualisiert werden!
            klasseA1.Aktualisieren();
        }
        // Delete
        public static void DemoADelete()
        {
            Debug.Print("--- DemoADelete ---");
            Data.Aufgabe.LesenID(1).Loeschen();
            Debug.Print("Artikel mit Id 1 gelöscht");
        }
        #endregion
        #region KlasseB
        // Create
        public static void DemoBCreate()
        {
            Debug.Print("--- DemoBCreate ---");
            // KlasseB (kurze Syntax)
            Data.Aufgabensammlung klasseB1 = new Data.Aufgabensammlung { Name = "Artikelgruppe 1", Erinnerungsdatum = DateTime.Today.AddDays(-1)};
            Int64 klasseB1Id = klasseB1.Erstellen();
            Debug.Print("Gruppe erstellt mit Id:" + klasseB1Id);
            Data.Aufgabensammlung klasseB2 = new Data.Aufgabensammlung { Name = "Artikelgruppe 2", Erinnerungsdatum = DateTime.Today };
            Int64 klasseB2Id = klasseB2.Erstellen();
            Debug.Print("Gruppe erstellt mit Id:" + klasseB2Id);
        }
        // Read
        public static void DemoBRead()
        {
            Debug.Print("--- DemoBRead ---");
            // Demo liest 1 Objekt
            Data.Aufgabensammlung klasseB = Data.Aufgabensammlung.LesenAttributGleich("Artikelgruppe 1").FirstOrDefault();
            Debug.Print("Auslesen einzelne Gruppe mit Name: " + klasseB.Name + " Datum" + klasseB.Erinnerungsdatum.ToString("dd.MM.yyyy"));
            // Liste auslesen
            foreach(Data.Aufgabe klasseA in klasseB.Aufgabe)
            {
                Debug.Print("Artikelgruppe: " + klasseB.Name + " enthält Artikel:" + klasseA.Inhalt);
            }
        }
        // Update
        public static void DemoBUpdate()
        {
            Debug.Print("--- DemoBUpdate ---");
            Data.Aufgabensammlung klasseB = Data.Aufgabensammlung.LesenID(1);
            klasseB.Name = "Artikelgruppe 2 nach Update";
            klasseB.Aktualisieren();
            Debug.Print("Gruppe mit Name 'Artikelgruppe 1' verändert");
        }
        // Delete
        public static void DemoBDelete()
        {
            Debug.Print("--- DemoBDelete ---");
            // Achtung! Referentielle Integrität darf nicht verletzt werden!
            try
            {
                Data.Aufgabensammlung klasseB = Data.Aufgabensammlung.LesenID(1);
                klasseB.Loeschen();
                Debug.Print("Gruppe mit Id 1 gelöscht");
            } catch (Exception ex)
            {
                Debug.Print("Fehler beim Löschen:" + ex.Message);
            }
        }
        #endregion
    }
}
