using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;
using System.Text;

namespace System
{
	/// <summary>Represents one or more errors that occur during application execution.</summary>
	// Token: 0x020000BA RID: 186
	[DebuggerDisplay("Count = {InnerExceptionCount}")]
	[Serializable]
	public class AggregateException : Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a system-supplied message that describes the error.</summary>
		// Token: 0x06000486 RID: 1158 RVA: 0x0001832E File Offset: 0x0001652E
		public AggregateException()
			: base("One or more errors occurred.")
		{
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(Array.Empty<Exception>());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x06000487 RID: 1159 RVA: 0x0001834B File Offset: 0x0001654B
		public AggregateException(IEnumerable<Exception> innerExceptions)
			: this("One or more errors occurred.", innerExceptions)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x06000488 RID: 1160 RVA: 0x00018359 File Offset: 0x00016559
		public AggregateException(params Exception[] innerExceptions)
			: this("One or more errors occurred.", innerExceptions)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x06000489 RID: 1161 RVA: 0x00018367 File Offset: 0x00016567
		public AggregateException(string message, IEnumerable<Exception> innerExceptions)
			: this(message, (innerExceptions as IList<Exception>) ?? ((innerExceptions == null) ? null : new List<Exception>(innerExceptions)))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with a specified error message and references to the inner exceptions that are the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerExceptions">The exceptions that are the cause of the current exception.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="innerExceptions" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="innerExceptions" /> is null.</exception>
		// Token: 0x0600048A RID: 1162 RVA: 0x00018386 File Offset: 0x00016586
		public AggregateException(string message, params Exception[] innerExceptions)
			: this(message, innerExceptions)
		{
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00018390 File Offset: 0x00016590
		private AggregateException(string message, IList<Exception> innerExceptions)
			: base(message, (innerExceptions != null && innerExceptions.Count > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			Exception[] array = new Exception[innerExceptions.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = innerExceptions[i];
				if (array[i] == null)
				{
					throw new ArgumentException("An element of innerExceptions was null.");
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00018408 File Offset: 0x00016608
		internal AggregateException(IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
			: this("One or more errors occurred.", innerExceptionInfos)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00018416 File Offset: 0x00016616
		internal AggregateException(string message, IEnumerable<ExceptionDispatchInfo> innerExceptionInfos)
			: this(message, (innerExceptionInfos as IList<ExceptionDispatchInfo>) ?? ((innerExceptionInfos == null) ? null : new List<ExceptionDispatchInfo>(innerExceptionInfos)))
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00018438 File Offset: 0x00016638
		private AggregateException(string message, IList<ExceptionDispatchInfo> innerExceptionInfos)
			: base(message, (innerExceptionInfos != null && innerExceptionInfos.Count > 0 && innerExceptionInfos[0] != null) ? innerExceptionInfos[0].SourceException : null)
		{
			if (innerExceptionInfos == null)
			{
				throw new ArgumentNullException("innerExceptionInfos");
			}
			Exception[] array = new Exception[innerExceptionInfos.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionDispatchInfo exceptionDispatchInfo = innerExceptionInfos[i];
				if (exceptionDispatchInfo != null)
				{
					array[i] = exceptionDispatchInfo.SourceException;
				}
				if (array[i] == null)
				{
					throw new ArgumentException("An element of innerExceptions was null.");
				}
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> argument is null.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The exception could not be deserialized correctly.</exception>
		// Token: 0x0600048F RID: 1167 RVA: 0x000184C8 File Offset: 0x000166C8
		protected AggregateException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			Exception[] array = info.GetValue("InnerExceptions", typeof(Exception[])) as Exception[];
			if (array == null)
			{
				throw new SerializationException("The serialization stream contains no inner exceptions.");
			}
			this.m_innerExceptions = new ReadOnlyCollection<Exception>(array);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.AggregateException" /> class with serialized data.</summary>
		/// <param name="info">The object that holds the serialized object data. </param>
		/// <param name="context">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> argument is null.</exception>
		// Token: 0x06000490 RID: 1168 RVA: 0x00018520 File Offset: 0x00016720
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			Exception[] array = new Exception[this.m_innerExceptions.Count];
			this.m_innerExceptions.CopyTo(array, 0);
			info.AddValue("InnerExceptions", array, typeof(Exception[]));
		}

		/// <summary>Gets a read-only collection of the <see cref="T:System.Exception" /> instances that caused the current exception.</summary>
		/// <returns>Returns a read-only collection of the <see cref="T:System.Exception" /> instances that caused the current exception.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x00018569 File Offset: 0x00016769
		public ReadOnlyCollection<Exception> InnerExceptions
		{
			get
			{
				return this.m_innerExceptions;
			}
		}

		/// <summary>Flattens an <see cref="T:System.AggregateException" /> instances into a single, new instance.</summary>
		/// <returns>A new, flattened <see cref="T:System.AggregateException" />.</returns>
		// Token: 0x06000492 RID: 1170 RVA: 0x00018574 File Offset: 0x00016774
		public AggregateException Flatten()
		{
			List<Exception> list = new List<Exception>();
			List<AggregateException> list2 = new List<AggregateException>();
			list2.Add(this);
			int num = 0;
			while (list2.Count > num)
			{
				IList<Exception> innerExceptions = list2[num++].InnerExceptions;
				for (int i = 0; i < innerExceptions.Count; i++)
				{
					Exception ex = innerExceptions[i];
					if (ex != null)
					{
						AggregateException ex2 = ex as AggregateException;
						if (ex2 != null)
						{
							list2.Add(ex2);
						}
						else
						{
							list.Add(ex);
						}
					}
				}
			}
			return new AggregateException(this.Message, list);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00018600 File Offset: 0x00016800
		public override string Message
		{
			get
			{
				if (this.m_innerExceptions.Count == 0)
				{
					return base.Message;
				}
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				stringBuilder.Append(base.Message);
				stringBuilder.Append(' ');
				for (int i = 0; i < this.m_innerExceptions.Count; i++)
				{
					stringBuilder.Append('(');
					stringBuilder.Append(this.m_innerExceptions[i].Message);
					stringBuilder.Append(") ");
				}
				stringBuilder.Length--;
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}
		}

		/// <summary>Creates and returns a string representation of the current <see cref="T:System.AggregateException" />.</summary>
		/// <returns>A string representation of the current exception.</returns>
		// Token: 0x06000494 RID: 1172 RVA: 0x00018698 File Offset: 0x00016898
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			for (int i = 0; i < this.m_innerExceptions.Count; i++)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("---> ");
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "(Inner Exception #{0}) ", i);
				stringBuilder.Append(this.m_innerExceptions[i].ToString());
				stringBuilder.Append("<---");
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040002B6 RID: 694
		private ReadOnlyCollection<Exception> m_innerExceptions;
	}
}
