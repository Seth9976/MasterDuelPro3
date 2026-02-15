using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</summary>
	// Token: 0x020002BC RID: 700
	public class DoWorkEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoWorkEventArgs" /> class.</summary>
		/// <param name="argument">Specifies an argument for an asynchronous operation.</param>
		// Token: 0x06001097 RID: 4247 RVA: 0x0004539C File Offset: 0x0004359C
		public DoWorkEventArgs(object argument)
		{
			this.argument = argument;
		}

		/// <summary>Gets or sets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x000453AB File Offset: 0x000435AB
		[SRDescription("Result from the worker function.")]
		public object Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x04000A6F RID: 2671
		private object result;

		// Token: 0x04000A70 RID: 2672
		private object argument;
	}
}
