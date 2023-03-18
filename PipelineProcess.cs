using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
	internal class PipelineProcess<T> : IPipelineProcess<T> 
	{
		public PipelineProcess(Func<T,Func<T>,T> process) : this(process, null) { }

		public PipelineProcess(Func<T,Func<T>,T> process,IPipelineProcess<T>? previous) {
			Process = process;
			previous?.setNextProcess(this);
			_nextProcess = null;
		}

		//the next process in the chain of responsibility
		protected IPipelineProcess<T>? _nextProcess;

		//the developer defined process logic
		public Func<T,Func<T>,T> Process { get; set; }

		//execute this process
		public T Execute(T args) {
			return Process(args,() => {
				if (_nextProcess == null) return args;
				return _nextProcess.Execute(args);
			});
		}

		//set the next process in the chain
		public void setNextProcess(IPipelineProcess<T> nextProcess) {
			_nextProcess = nextProcess;
		}
	}
}
