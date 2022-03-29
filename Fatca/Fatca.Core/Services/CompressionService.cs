using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Fatca.Core.Services
{
    /// <summary>
    /// Used for file compression
    /// </summary>
    public class ProcessingService
    {
        public event EventHandler<ProcessingEventArgs> FileProcessed;
        public EventHandler FileCompressing;
        public List<string> FiredEvents=new List<string>();

        public void Process(ProcessedFile file, string message = " processing")
        {
            file.Message = message;
           // Console.WriteLine($"{message} {file.FileName}");
           //Thread.Sleep(5000);
            OnFileProceessed(this, new ProcessingEventArgs() { CompressibleFile = file });

        }
        public virtual void OnFileProceessed(object sender, ProcessingEventArgs args)
         => FileProcessed?.Invoke(this, args);

    }
    public class ProcessingEventArgs : EventArgs
    {
        public ProcessedFile CompressibleFile { get; set; }
    }
    public class ProcessedFile
    {
        public long OriginFileZise { get; set; }
        public string FileName { get; set; }
        public long NewFileSize { get; set; }
        public string Message { get; set; }
    }
    public class ProcessSubscriber
    {
        public virtual void OnFileCompressed(object source, ProcessingEventArgs args)
        {
            
            Console.WriteLine($"{args.CompressibleFile.Message}: {args.CompressibleFile.FileName}");
            
        }
    }
}


