namespace Pipeline
{
	//defines the interface for a pipeline process which represents a single step in a pipeline
	//T a mutable reference type which is mutated as the pipeline is executed
	interface IPipelineProcess<T> 
	{
		public void setNextProcess(IPipelineProcess<T> next);
		public T Execute(T data);
	}
}
