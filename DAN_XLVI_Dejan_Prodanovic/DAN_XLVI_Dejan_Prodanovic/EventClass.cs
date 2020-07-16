using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVI_Dejan_Prodanovic
{
    class EventClass
    {
        public Stopwatch stopWatch = new Stopwatch();
        EndProgramNotification endProgramNotification = new EndProgramNotification();
        
        public delegate void ProgramEndedEventHandler(object source, ProgramExecutionTimeArgs e);
        public event ProgramEndedEventHandler ProgramEnded;

        public void OnProgramEnded(long programDuration)
        {
            if (ProgramEnded!=null)
            {
                ProgramEnded(this, new ProgramExecutionTimeArgs() { ExecutionTime = programDuration });
            }
        }
    }
}
