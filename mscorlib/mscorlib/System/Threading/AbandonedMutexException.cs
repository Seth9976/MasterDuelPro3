using System;
using System.Runtime.Serialization;

namespace System.Threading
{
	/// <summary>The exception that is thrown when one thread acquires a <see cref="T:System.Threading.Mutex" /> object that another thread has abandoned by exiting without releasing it.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200020C RID: 524
	[Serializable]
	public class AbandonedMutexException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with default values.</summary>
		// Token: 0x06001435 RID: 5173 RVA: 0x000529A3 File Offset: 0x00050BA3
		public AbandonedMutexException()
			: base("The wait completed due to an abandoned mutex.")
		{
			base.HResult = -2146233043;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		// Token: 0x06001436 RID: 5174 RVA: 0x000529C2 File Offset: 0x00050BC2
		public AbandonedMutexException(string message)
			: base(message)
		{
			base.HResult = -2146233043;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message and inner exception. </summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		// Token: 0x06001437 RID: 5175 RVA: 0x000529DD File Offset: 0x00050BDD
		public AbandonedMutexException(string message, Exception inner)
			: base(message, inner)
		{
			base.HResult = -2146233043;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified index for the abandoned mutex, if applicable, and a <see cref="T:System.Threading.Mutex" /> object that represents the mutex.</summary>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x06001438 RID: 5176 RVA: 0x000529F9 File Offset: 0x00050BF9
		public AbandonedMutexException(int location, WaitHandle handle)
			: base("The wait completed due to an abandoned mutex.")
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message, the index of the abandoned mutex, if applicable, and the abandoned mutex. </summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x06001439 RID: 5177 RVA: 0x00052A20 File Offset: 0x00050C20
		public AbandonedMutexException(string message, int location, WaitHandle handle)
			: base(message)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with a specified error message, the inner exception, the index for the abandoned mutex, if applicable, and a <see cref="T:System.Threading.Mutex" /> object that represents the mutex.</summary>
		/// <param name="message">An error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception.</param>
		/// <param name="location">The index of the abandoned mutex in the array of wait handles if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, or –1 if the exception is thrown for the <see cref="Overload:System.Threading.WaitHandle.WaitOne" /> or <see cref="Overload:System.Threading.WaitHandle.WaitAll" /> methods.</param>
		/// <param name="handle">A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex.</param>
		// Token: 0x0600143A RID: 5178 RVA: 0x00052A43 File Offset: 0x00050C43
		public AbandonedMutexException(string message, Exception inner, int location, WaitHandle handle)
			: base(message, inner)
		{
			base.HResult = -2146233043;
			this.SetupException(location, handle);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.AbandonedMutexException" /> class with serialized data.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that holds the serialized object data about the exception being thrown. </param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains contextual information about the source or destination.</param>
		// Token: 0x0600143B RID: 5179 RVA: 0x00052A68 File Offset: 0x00050C68
		protected AbandonedMutexException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00052A79 File Offset: 0x00050C79
		private void SetupException(int location, WaitHandle handle)
		{
			this._mutexIndex = location;
			if (handle != null)
			{
				this._mutex = handle as Mutex;
			}
		}

		/// <summary>Gets the abandoned mutex that caused the exception, if known.</summary>
		/// <returns>A <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex, or null if the abandoned mutex could not be identified.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x00052A91 File Offset: 0x00050C91
		public Mutex Mutex
		{
			get
			{
				return this._mutex;
			}
		}

		/// <summary>Gets the index of the abandoned mutex that caused the exception, if known.</summary>
		/// <returns>The index, in the array of wait handles passed to the <see cref="Overload:System.Threading.WaitHandle.WaitAny" /> method, of the <see cref="T:System.Threading.Mutex" /> object that represents the abandoned mutex, or –1 if the index of the abandoned mutex could not be determined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00052A99 File Offset: 0x00050C99
		public int MutexIndex
		{
			get
			{
				return this._mutexIndex;
			}
		}

		// Token: 0x040009E8 RID: 2536
		private int _mutexIndex = -1;

		// Token: 0x040009E9 RID: 2537
		private Mutex _mutex;
	}
}
