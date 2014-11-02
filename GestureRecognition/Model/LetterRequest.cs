using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestureRecognition.Model
{
    public class LetterRequest
    {
        /// <summary>
        /// Retrieve all the Letters stored if true, it will avoid all the rest of parameters
        /// </summary>
        public bool RetrieveAll { get; set; }
        /// <summary>
        /// Retrieve the letter with the specified Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Retrieve a letter with the specified Name
        /// </summary>
        public string Name { get; set; }
    }
}
