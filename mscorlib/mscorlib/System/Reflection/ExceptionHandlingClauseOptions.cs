using System;

namespace System.Reflection
{
	/// <summary>Identifies kinds of exception-handling clauses.</summary>
	// Token: 0x020005FC RID: 1532
	[Flags]
	public enum ExceptionHandlingClauseOptions
	{
		/// <summary>The clause accepts all exceptions that derive from a specified type.</summary>
		// Token: 0x040016F1 RID: 5873
		Clause = 0,
		/// <summary>The clause contains user-specified instructions that determine whether the exception should be ignored (that is, whether normal execution should resume), be handled by the associated handler, or be passed on to the next clause.</summary>
		// Token: 0x040016F2 RID: 5874
		Filter = 1,
		/// <summary>The clause is executed whenever the try block exits, whether through normal control flow or because of an unhandled exception.</summary>
		// Token: 0x040016F3 RID: 5875
		Finally = 2,
		/// <summary>The clause is executed if an exception occurs, but not on completion of normal control flow.</summary>
		// Token: 0x040016F4 RID: 5876
		Fault = 4
	}
}
