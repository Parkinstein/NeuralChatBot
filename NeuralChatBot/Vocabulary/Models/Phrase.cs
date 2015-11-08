using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Models
{
    class Phrase
    {
        public string Author { get; set; }
        public List<Word> Words { get; set; }
        public Phrase RelatedPhrase { get; set; }
    }
}
