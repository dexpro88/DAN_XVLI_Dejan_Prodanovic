using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVI_Dejan_Prodanovic
{
    class EndProgramNotification
    {
        /// <summary>
        /// method that prints message when appilcatin ends
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void OnProgramEnded(object source,ProgramExecutionTimeArgs e)
        {
            Console.WriteLine("Execution  time of aplication: {0} ms",e.ExecutionTime);
        }
    }
}
