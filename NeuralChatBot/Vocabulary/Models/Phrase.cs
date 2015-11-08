using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    public class Phrase
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public virtual List<Word> Words { get; set; }
        public virtual Phrase RelatedPhrase { get; set; }
    }
}
