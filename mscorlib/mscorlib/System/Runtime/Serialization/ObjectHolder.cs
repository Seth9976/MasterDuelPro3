using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020004BD RID: 1213
	internal sealed class ObjectHolder
	{
		// Token: 0x060026A8 RID: 9896 RVA: 0x0009C89E File Offset: 0x0009AA9E
		internal ObjectHolder(long objID)
			: this(null, objID, null, null, 0L, null, null)
		{
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x0009C8B0 File Offset: 0x0009AAB0
		internal ObjectHolder(object obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (idOfContainingObj != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainingObj == objID)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			this.SetFlags();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0009C96C File Offset: 0x0009AB6C
		internal ObjectHolder(string obj, long objID, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainingObj, FieldInfo field, int[] arrayIndex)
		{
			this.m_object = obj;
			this.m_id = objID;
			this.m_flags = 0;
			this.m_missingElementsRemaining = 0;
			this.m_missingDecendents = 0;
			this.m_dependentObjects = null;
			this.m_next = null;
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			this.m_markForFixupWhenAvailable = false;
			if (idOfContainingObj != 0L && arrayIndex != null)
			{
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainingObj, field, arrayIndex);
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0009C9F5 File Offset: 0x0009ABF5
		private void IncrementDescendentFixups(int amount)
		{
			this.m_missingDecendents += amount;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0009CA05 File Offset: 0x0009AC05
		internal void DecrementFixupsRemaining(ObjectManager manager)
		{
			this.m_missingElementsRemaining--;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(-1, manager);
			}
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x0009CA25 File Offset: 0x0009AC25
		internal void RemoveDependency(long id)
		{
			this.m_dependentObjects.RemoveElement(id);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x0009CA34 File Offset: 0x0009AC34
		internal void AddFixup(FixupHolder fixup, ObjectManager manager)
		{
			if (this.m_missingElements == null)
			{
				this.m_missingElements = new FixupHolderList();
			}
			this.m_missingElements.Add(fixup);
			this.m_missingElementsRemaining++;
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(1, manager);
			}
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x0009CA74 File Offset: 0x0009AC74
		private void UpdateDescendentDependencyChain(int amount, ObjectManager manager)
		{
			ObjectHolder objectHolder = this;
			do
			{
				objectHolder = manager.FindOrCreateObjectHolder(objectHolder.ContainerID);
				objectHolder.IncrementDescendentFixups(amount);
			}
			while (objectHolder.RequiresValueTypeFixup);
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x0009CA9F File Offset: 0x0009AC9F
		internal void AddDependency(long dependentObject)
		{
			if (this.m_dependentObjects == null)
			{
				this.m_dependentObjects = new LongList();
			}
			this.m_dependentObjects.Add(dependentObject);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x0009CAC0 File Offset: 0x0009ACC0
		internal void UpdateData(object obj, SerializationInfo info, ISerializationSurrogate surrogate, long idOfContainer, FieldInfo field, int[] arrayIndex, ObjectManager manager)
		{
			this.SetObjectValue(obj, manager);
			this.m_serInfo = info;
			this.m_surrogate = surrogate;
			if (idOfContainer != 0L && ((field != null && field.FieldType.IsValueType) || arrayIndex != null))
			{
				if (idOfContainer == this.m_id)
				{
					throw new SerializationException(Environment.GetResourceString("The ID of the containing object cannot be the same as the object ID."));
				}
				this.m_valueFixup = new ValueTypeFixupInfo(idOfContainer, field, arrayIndex);
			}
			this.SetFlags();
			if (this.RequiresValueTypeFixup)
			{
				this.UpdateDescendentDependencyChain(this.m_missingElementsRemaining, manager);
			}
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x0009CB4B File Offset: 0x0009AD4B
		internal void MarkForCompletionWhenAvailable()
		{
			this.m_markForFixupWhenAvailable = true;
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x0009CB54 File Offset: 0x0009AD54
		internal void SetFlags()
		{
			if (this.m_object is IObjectReference)
			{
				this.m_flags |= 1;
			}
			this.m_flags &= -7;
			if (this.m_surrogate != null)
			{
				this.m_flags |= 4;
			}
			else if (this.m_object is ISerializable)
			{
				this.m_flags |= 2;
			}
			if (this.m_valueFixup != null)
			{
				this.m_flags |= 8;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x060026B4 RID: 9908 RVA: 0x0009CBD4 File Offset: 0x0009ADD4
		// (set) Token: 0x060026B5 RID: 9909 RVA: 0x0009CBE1 File Offset: 0x0009ADE1
		internal bool IsIncompleteObjectReference
		{
			get
			{
				return (this.m_flags & 1) != 0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= 1;
					return;
				}
				this.m_flags &= -2;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x060026B6 RID: 9910 RVA: 0x0009CC04 File Offset: 0x0009AE04
		internal bool RequiresDelayedFixup
		{
			get
			{
				return (this.m_flags & 7) != 0;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x060026B7 RID: 9911 RVA: 0x0009CC11 File Offset: 0x0009AE11
		internal bool RequiresValueTypeFixup
		{
			get
			{
				return (this.m_flags & 8) != 0;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x060026B8 RID: 9912 RVA: 0x0009CC1E File Offset: 0x0009AE1E
		// (set) Token: 0x060026B9 RID: 9913 RVA: 0x0009CC52 File Offset: 0x0009AE52
		internal bool ValueTypeFixupPerformed
		{
			get
			{
				return (this.m_flags & 32768) != 0 || (this.m_object != null && (this.m_dependentObjects == null || this.m_dependentObjects.Count == 0));
			}
			set
			{
				if (value)
				{
					this.m_flags |= 32768;
				}
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0009CC69 File Offset: 0x0009AE69
		internal bool HasISerializable
		{
			get
			{
				return (this.m_flags & 2) != 0;
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x060026BB RID: 9915 RVA: 0x0009CC76 File Offset: 0x0009AE76
		internal bool HasSurrogate
		{
			get
			{
				return (this.m_flags & 4) != 0;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x0009CC83 File Offset: 0x0009AE83
		internal bool CanSurrogatedObjectValueChange
		{
			get
			{
				return this.m_surrogate == null || this.m_surrogate.GetType() != typeof(SurrogateForCyclicalReference);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060026BD RID: 9917 RVA: 0x0009CCA9 File Offset: 0x0009AEA9
		internal bool CanObjectValueChange
		{
			get
			{
				return this.IsIncompleteObjectReference || (this.HasSurrogate && this.CanSurrogatedObjectValueChange);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0009CCC5 File Offset: 0x0009AEC5
		internal int DirectlyDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining;
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x060026BF RID: 9919 RVA: 0x0009CCCD File Offset: 0x0009AECD
		internal int TotalDependentObjects
		{
			get
			{
				return this.m_missingElementsRemaining + this.m_missingDecendents;
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0009CCDC File Offset: 0x0009AEDC
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x0009CCE4 File Offset: 0x0009AEE4
		internal bool Reachable
		{
			get
			{
				return this.m_reachable;
			}
			set
			{
				this.m_reachable = value;
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0009CCED File Offset: 0x0009AEED
		internal bool TypeLoadExceptionReachable
		{
			get
			{
				return this.m_typeLoad != null;
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x0009CCF8 File Offset: 0x0009AEF8
		// (set) Token: 0x060026C4 RID: 9924 RVA: 0x0009CD00 File Offset: 0x0009AF00
		internal TypeLoadExceptionHolder TypeLoadException
		{
			get
			{
				return this.m_typeLoad;
			}
			set
			{
				this.m_typeLoad = value;
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x0009CD09 File Offset: 0x0009AF09
		internal object ObjectValue
		{
			get
			{
				return this.m_object;
			}
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x0009CD11 File Offset: 0x0009AF11
		internal void SetObjectValue(object obj, ObjectManager manager)
		{
			this.m_object = obj;
			if (obj == manager.TopObject)
			{
				this.m_reachable = true;
			}
			if (obj is TypeLoadExceptionHolder)
			{
				this.m_typeLoad = (TypeLoadExceptionHolder)obj;
			}
			if (this.m_markForFixupWhenAvailable)
			{
				manager.CompleteObject(this, true);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060026C7 RID: 9927 RVA: 0x0009CD4E File Offset: 0x0009AF4E
		// (set) Token: 0x060026C8 RID: 9928 RVA: 0x0009CD56 File Offset: 0x0009AF56
		internal SerializationInfo SerializationInfo
		{
			get
			{
				return this.m_serInfo;
			}
			set
			{
				this.m_serInfo = value;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060026C9 RID: 9929 RVA: 0x0009CD5F File Offset: 0x0009AF5F
		internal ISerializationSurrogate Surrogate
		{
			get
			{
				return this.m_surrogate;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0009CD67 File Offset: 0x0009AF67
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x0009CD6F File Offset: 0x0009AF6F
		internal LongList DependentObjects
		{
			get
			{
				return this.m_dependentObjects;
			}
			set
			{
				this.m_dependentObjects = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x0009CD78 File Offset: 0x0009AF78
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x0009CD9F File Offset: 0x0009AF9F
		internal bool RequiresSerInfoFixup
		{
			get
			{
				return ((this.m_flags & 4) != 0 || (this.m_flags & 2) != 0) && (this.m_flags & 16384) == 0;
			}
			set
			{
				if (!value)
				{
					this.m_flags |= 16384;
					return;
				}
				this.m_flags &= -16385;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0009CDC9 File Offset: 0x0009AFC9
		internal ValueTypeFixupInfo ValueFixup
		{
			get
			{
				return this.m_valueFixup;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x0009CDD1 File Offset: 0x0009AFD1
		internal bool CompletelyFixed
		{
			get
			{
				return !this.RequiresSerInfoFixup && !this.IsIncompleteObjectReference;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0009CDE6 File Offset: 0x0009AFE6
		internal long ContainerID
		{
			get
			{
				if (this.m_valueFixup != null)
				{
					return this.m_valueFixup.ContainerID;
				}
				return 0L;
			}
		}

		// Token: 0x04001270 RID: 4720
		private object m_object;

		// Token: 0x04001271 RID: 4721
		internal long m_id;

		// Token: 0x04001272 RID: 4722
		private int m_missingElementsRemaining;

		// Token: 0x04001273 RID: 4723
		private int m_missingDecendents;

		// Token: 0x04001274 RID: 4724
		internal SerializationInfo m_serInfo;

		// Token: 0x04001275 RID: 4725
		internal ISerializationSurrogate m_surrogate;

		// Token: 0x04001276 RID: 4726
		internal FixupHolderList m_missingElements;

		// Token: 0x04001277 RID: 4727
		internal LongList m_dependentObjects;

		// Token: 0x04001278 RID: 4728
		internal ObjectHolder m_next;

		// Token: 0x04001279 RID: 4729
		internal int m_flags;

		// Token: 0x0400127A RID: 4730
		private bool m_markForFixupWhenAvailable;

		// Token: 0x0400127B RID: 4731
		private ValueTypeFixupInfo m_valueFixup;

		// Token: 0x0400127C RID: 4732
		private TypeLoadExceptionHolder m_typeLoad;

		// Token: 0x0400127D RID: 4733
		private bool m_reachable;
	}
}
