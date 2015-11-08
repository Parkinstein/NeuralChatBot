using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary.DbContext;
using Vocabulary.Models;
namespace Vocabulary
{
    class VocabularyService: IDisposable
    {
        VocabularyDb Context = new VocabularyDb();


        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();                   
                }              
                disposedValue = true;
            }
        }
             
        public void Dispose()
        {            
            Dispose(true);         
        }
        #endregion
    }
}
