using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Configuration
{
	/// <summary>Contains a collection of <see cref="T:System.Configuration.PropertyInformation" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000035 RID: 53
	[Serializable]
	public sealed class PropertyInformationCollection : NameObjectCollectionBase
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000667E File Offset: 0x0000487E
		internal PropertyInformationCollection()
			: base(StringComparer.Ordinal)
		{
		}

		/// <summary>Gets the <see cref="T:System.Configuration.PropertyInformation" /> object in the collection, based on the specified property name.</summary>
		/// <returns>A <see cref="T:System.Configuration.PropertyInformation" /> object.</returns>
		/// <param name="propertyName">The name of the configuration attribute contained in the <see cref="T:System.Configuration.PropertyInformationCollection" />object.</param>
		// Token: 0x17000074 RID: 116
		public PropertyInformation this[string propertyName]
		{
			get
			{
				return (PropertyInformation)base.BaseGet(propertyName);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object, which is used to iterate through this <see cref="T:System.Configuration.PropertyInformationCollection" />.</returns>
		// Token: 0x06000177 RID: 375 RVA: 0x00006699 File Offset: 0x00004899
		public override IEnumerator GetEnumerator()
		{
			return new PropertyInformationCollection.PropertyInformationEnumerator(this);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000066A1 File Offset: 0x000048A1
		internal void Add(PropertyInformation pi)
		{
			base.BaseAdd(pi.Name, pi);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to serialize the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object that contains the source and destination of the serialized stream associated with the <see cref="T:System.Configuration.PropertyInformationCollection" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null.</exception>
		// Token: 0x06000179 RID: 377 RVA: 0x000059C3 File Offset: 0x00003BC3
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x02000036 RID: 54
		private class PropertyInformationEnumerator : IEnumerator
		{
			// Token: 0x0600017A RID: 378 RVA: 0x000066B0 File Offset: 0x000048B0
			public PropertyInformationEnumerator(PropertyInformationCollection collection)
			{
				this.collection = collection;
				this.position = -1;
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600017B RID: 379 RVA: 0x000066C6 File Offset: 0x000048C6
			public object Current
			{
				get
				{
					if (this.position < this.collection.Count && this.position >= 0)
					{
						return this.collection.BaseGet(this.position);
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x0600017C RID: 380 RVA: 0x000066FC File Offset: 0x000048FC
			public bool MoveNext()
			{
				int num = this.position + 1;
				this.position = num;
				return num < this.collection.Count;
			}

			// Token: 0x0600017D RID: 381 RVA: 0x0000672A File Offset: 0x0000492A
			public void Reset()
			{
				this.position = -1;
			}

			// Token: 0x040000B7 RID: 183
			private PropertyInformationCollection collection;

			// Token: 0x040000B8 RID: 184
			private int position;
		}
	}
}
