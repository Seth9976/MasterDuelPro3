using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	/// <summary>Describes the source and destination of a given serialized stream, and provides an additional caller-defined context.</summary>
	// Token: 0x020004CE RID: 1230
	[ComVisible(true)]
	[Serializable]
	public readonly struct StreamingContext
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />. </param>
		// Token: 0x06002731 RID: 10033 RVA: 0x0009DD16 File Offset: 0x0009BF16
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.StreamingContext" /> class with a given context state, and some additional information.</summary>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> values that specify the source or destination context for this <see cref="T:System.Runtime.Serialization.StreamingContext" />. </param>
		/// <param name="additional">Any additional information to be associated with the <see cref="T:System.Runtime.Serialization.StreamingContext" />. This information is available to any object that implements <see cref="T:System.Runtime.Serialization.ISerializable" /> or any serialization surrogate. Most users do not need to set this parameter. </param>
		// Token: 0x06002732 RID: 10034 RVA: 0x0009DD20 File Offset: 0x0009BF20
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this.m_state = state;
			this.m_additionalContext = additional;
		}

		/// <summary>Gets context specified as part of the additional context.</summary>
		/// <returns>The context specified as part of the additional context.</returns>
		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x0009DD30 File Offset: 0x0009BF30
		public object Context
		{
			get
			{
				return this.m_additionalContext;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances contain the same values.</summary>
		/// <returns>true if the specified object is an instance of <see cref="T:System.Runtime.Serialization.StreamingContext" /> and equals the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with the current instance. </param>
		// Token: 0x06002734 RID: 10036 RVA: 0x0009DD38 File Offset: 0x0009BF38
		public override bool Equals(object obj)
		{
			return obj is StreamingContext && (((StreamingContext)obj).m_additionalContext == this.m_additionalContext && ((StreamingContext)obj).m_state == this.m_state);
		}

		/// <summary>Returns a hash code of this object.</summary>
		/// <returns>The <see cref="T:System.Runtime.Serialization.StreamingContextStates" /> value that contains the source or destination of the serialization for this <see cref="T:System.Runtime.Serialization.StreamingContext" />.</returns>
		// Token: 0x06002735 RID: 10037 RVA: 0x0009DD6D File Offset: 0x0009BF6D
		public override int GetHashCode()
		{
			return (int)this.m_state;
		}

		/// <summary>Gets the source or destination of the transmitted data.</summary>
		/// <returns>During serialization, the destination of the transmitted data. During deserialization, the source of the data.</returns>
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06002736 RID: 10038 RVA: 0x0009DD6D File Offset: 0x0009BF6D
		public StreamingContextStates State
		{
			get
			{
				return this.m_state;
			}
		}

		// Token: 0x040012A4 RID: 4772
		internal readonly object m_additionalContext;

		// Token: 0x040012A5 RID: 4773
		internal readonly StreamingContextStates m_state;
	}
}
