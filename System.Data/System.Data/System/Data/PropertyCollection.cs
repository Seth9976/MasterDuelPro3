using System;
using System.Collections;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents a collection of properties that can be added to <see cref="T:System.Data.DataColumn" />, <see cref="T:System.Data.DataSet" />, or <see cref="T:System.Data.DataTable" />. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000087 RID: 135
	[Serializable]
	public class PropertyCollection : Hashtable, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		// Token: 0x060006EB RID: 1771 RVA: 0x00022640 File Offset: 0x00020840
		public PropertyCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.PropertyCollection" /> class.</summary>
		/// <param name="info">The data needed to serialize or deserialize an object.</param>
		/// <param name="context">The source and destination of a given serialized stream.</param>
		// Token: 0x060006EC RID: 1772 RVA: 0x00022648 File Offset: 0x00020848
		protected PropertyCollection(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Data.PropertyCollection" /> object.</summary>
		/// <returns>Returns <see cref="T:System.Object" />, a shallow copy of the <see cref="T:System.Data.PropertyCollection" /> object.</returns>
		// Token: 0x060006ED RID: 1773 RVA: 0x00022654 File Offset: 0x00020854
		public override object Clone()
		{
			PropertyCollection propertyCollection = new PropertyCollection();
			foreach (object obj in this)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				propertyCollection.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			return propertyCollection;
		}
	}
}
