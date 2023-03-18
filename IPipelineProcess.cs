using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline
{
	//defines the interface for a pipeline process which represents a single step in a pipeline
	//T a mutable reference type which is mutated as the pipeline is executed
	internal interface IPipelineProcess<T> 
	{
		public void setNextProcess(IPipelineProcess<T> next);
		public T Execute(T args);
		public Func<T, Func<T>, T> Process { get; set; }
	}
}
