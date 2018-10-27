using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace M120Projekt.Data
{
    public class Aufgabe
    {
        #region Datenbankschicht
        [Key]
        public Int64 AufgabeId { get; set; }
        [Required]
        public String Inhalt { get; set; }
        [Required]
        public DateTime Erstellungsdatum { get; set; }
        [Required]
        public Boolean HasPriorityHigh { get; set; }
        public Int64 AufgabensammlungId { get; set; }
        public Aufgabensammlung Aufgabensammlung { get; set; }
        #endregion
        #region Applikationsschicht
        public Aufgabe() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Data.Aufgabe> LesenAlle()
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabe.Include(x => x.Aufgabensammlung) select record).ToList();
            }
        }
        public static Data.Aufgabe LesenID(Int64 klasseAId)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabe.Include(x => x.Aufgabensammlung) where record.AufgabeId == klasseAId select record).FirstOrDefault();
            }
        }
        public static List<Data.Aufgabe> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabe.Include(x => x.Aufgabensammlung) where record.Inhalt == suchbegriff select record).ToList();
            }
        }
        public static List<Data.Aufgabe> LesenAttributWie(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabe.Include(x => x.Aufgabensammlung) where record.Inhalt.Contains(suchbegriff) select record).ToList();
            }
        }
        public static List<Data.Aufgabe> LesenFremdschluesselGleich(Data.Aufgabensammlung suchschluessel)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabe.Include(x => x.Aufgabensammlung) where record.Aufgabensammlung == suchschluessel select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.Inhalt == null || this.Inhalt == "") this.Inhalt = "leer";
            // Option mit Fehler statt Default Value
            // if (klasseA.TextAttribut == null) throw new Exception("Null ist ungültig");
            if (this.Erstellungsdatum == null) this.Erstellungsdatum = DateTime.MinValue;
            using (var context = new Data.Context())
            {
                context.Aufgabe.Add(this);
                //TODO Check ob mit null möglich, sonst throw Ex
                if (this.Aufgabensammlung != null) context.Aufgabensammlung.Attach(this.Aufgabensammlung);
                context.SaveChanges();
                return this.AufgabeId;
            }
        }
        public Int64 Aktualisieren()
        {
            using (var context = new Data.Context())
            {
                //TODO null Checks?
                this.Aufgabensammlung = null;
                context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return this.AufgabeId;
            }
        }
        public void Loeschen()
        {
            using (var context = new Data.Context())
            {
                context.Entry(this).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public override string ToString()
        {
            return AufgabeId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
