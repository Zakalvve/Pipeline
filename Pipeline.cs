namespace Pipeline
{
	//defines a pipeline which takes a mutable input object and performs a series of processes upon it before returning the mutated output
	class Pipeline<T>
	{
		//the collection of processes which form the pipeline
		public List<PipelineProcess<T>> processes = new List<PipelineProcess<T>>();

		//add a new process to the list
		public void AddProcess(Func<T,Func<T>,T> process) {
			processes.Add(new PipelineProcess<T>(process,processes.LastOrDefault()));
		}

		//if no endpoint is found an error is thrown
		//an endpoint is a process that returns an output
		public T Execute(T data) {
			try {
				return processes[0].Execute(data);
			} catch (IndexOutOfRangeException error) {
				Console.WriteLine(error);
				return data;
			}
		}
	}
}
