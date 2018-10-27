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
    public class Aufgabensammlung
    {
        #region Datenbankschicht
        [Key]
        public Int64 AufgabensammlungId { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public DateTime Erinnerungsdatum { get; set; }
        public ICollection<Aufgabe> Aufgabe { get; set; }
        #endregion
        #region Applikationsschicht
        public Aufgabensammlung() { }
        [NotMapped]
        public String BerechnetesAttribut
        {
            get
            {
                return "Im Getter kann Code eingefügt werden für berechnete Attribute";
            }
        }
        public static List<Data.Aufgabensammlung> LesenAlle()
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabensammlung.Include(x => x.Aufgabe) select record).ToList();
            }
        }
        public static Data.Aufgabensammlung LesenID(Int64 klasseBId)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabensammlung.Include(x => x.Aufgabe) where record.AufgabensammlungId == klasseBId select record).FirstOrDefault();
            }
        }
        public static List<Data.Aufgabensammlung> LesenAttributGleich(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                var klasseBquery = (from record in context.Aufgabensammlung.Include(x => x.Aufgabe) where record.Name == suchbegriff select record).ToList();
                return klasseBquery;
            }
        }
        public static List<Data.Aufgabensammlung> LesenAttributWie(String suchbegriff)
        {
            using (var context = new Data.Context())
            {
                return (from record in context.Aufgabensammlung.Include(x => x.Aufgabe) where record.Name.Contains(suchbegriff) select record).ToList();
            }
        }
        public Int64 Erstellen()
        {
            if (this.Name == null || this.Name == "") this.Name = "leer";
            // Option mit Fehler statt Default Value
            // if (klasseB.TextAttribut == null) throw new Exception("Null ist ungültig");
            if (this.Erinnerungsdatum == null) this.Erinnerungsdatum = DateTime.MinValue;
            using (var context = new Data.Context())
            {
                context.Aufgabensammlung.Add(this);
                context.SaveChanges();
                return this.AufgabensammlungId;
            }
        }
        public void Aktualisieren()
        {
            using (var context = new Data.Context())
            {
                //TODO null Checks?
                context.Entry(this).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
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
            return AufgabensammlungId.ToString(); // Für bessere Coded UI Test Erkennung
        }
        #endregion
    }
}
