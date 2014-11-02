using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestureRecognition.Model
{
    public class FeatureRequest
    {
        /// <summary>
        /// Retrieve all elements, avoid the rest of parameters
        /// </summary>
        public bool RetrieveAll { get; set; }
        /// <summary>
        /// Letter Id stored in the Letter table
        /// </summary>
        public int IdLetter { get; set; }
        /// <summary>
        /// Name stored in the letter table
        /// </summary>
        public string Name { get; set; }
    }
}
