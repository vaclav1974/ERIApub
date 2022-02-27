using System.Diagnostics;

namespace ERIA.Models
{
    public class Watches
    {
        protected Stopwatch Stopwatches { get; set; } = new Stopwatch();
        public double Elapsed { get; set; } = 0;
        public bool IsRunning { get; set; } = false;

        public TimeSpan ElapsedTime { get; set; }

        public string ElTime { get; set; }

        public Watches()
        {
           
        }
        public bool StartWatches()
        {
            Stopwatches.Start();
            return IsRunning = true;
        }

        public bool StopWatches()
        {
            Stopwatches.Stop();
            return IsRunning = false;
        }

        public string Seconds()
        {
            ElapsedTime = Stopwatches.Elapsed;
            ElTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ElapsedTime.Hours, ElapsedTime.Minutes, ElapsedTime.Seconds,
            ElapsedTime.Milliseconds / 10);

            return ElTime;

        }





    }
}
