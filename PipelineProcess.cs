namespace Pipeline
{
	class PipelineProcess<T> : IPipelineProcess<T> 
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
		protected Func<T,Func<T>,T> Process { get; set; }

		//execute this process
		public T Execute(T data) {
			return Process(data,() => {
				if (_nextProcess == null) return data;
				return _nextProcess.Execute(data);
			});
		}

		//set the next process in the chain
		public void setNextProcess(IPipelineProcess<T> nextProcess) {
			_nextProcess = nextProcess;
		}
	}
}
