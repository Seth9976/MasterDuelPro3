using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000492 RID: 1170
	[Serializable]
	internal class MessageDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06002590 RID: 9616 RVA: 0x00098B8D File Offset: 0x00096D8D
		public MessageDictionary(IMethodMessage message)
		{
			this._message = message;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x00098B9C File Offset: 0x00096D9C
		internal bool HasUserData()
		{
			if (this._internalProperties == null)
			{
				return false;
			}
			if (this._internalProperties is MessageDictionary)
			{
				return ((MessageDictionary)this._internalProperties).HasUserData();
			}
			return this._internalProperties.Count > 0;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06002592 RID: 9618 RVA: 0x00098BD4 File Offset: 0x00096DD4
		internal IDictionary InternalDictionary
		{
			get
			{
				if (this._internalProperties != null && this._internalProperties is MessageDictionary)
				{
					return ((MessageDictionary)this._internalProperties).InternalDictionary;
				}
				return this._internalProperties;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (set) Token: 0x06002593 RID: 9619 RVA: 0x00098C02 File Offset: 0x00096E02
		public string[] MethodKeys
		{
			set
			{
				this._methodKeys = value;
			}
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x00098C0B File Offset: 0x00096E0B
		protected virtual IDictionary AllocInternalProperties()
		{
			this._ownProperties = true;
			return new Hashtable();
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00098C19 File Offset: 0x00096E19
		public IDictionary GetInternalProperties()
		{
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			return this._internalProperties;
		}

		// Token: 0x06002596 RID: 9622 RVA: 0x00098C38 File Offset: 0x00096E38
		private bool IsOverridenKey(string key)
		{
			if (this._ownProperties)
			{
				return false;
			}
			foreach (string text in this._methodKeys)
			{
				if (key == text)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004C6 RID: 1222
		public object this[object key]
		{
			get
			{
				string text = (string)key;
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					if (this._methodKeys[i] == text)
					{
						return this.GetMethodProperty(text);
					}
				}
				if (this._internalProperties != null)
				{
					return this._internalProperties[key];
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x00098CD4 File Offset: 0x00096ED4
		protected virtual object GetMethodProperty(string key)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num == 1201911322U)
						{
							if (key == "__CallContext")
							{
								return this._message.LogicalCallContext;
							}
						}
					}
					else if (key == "__Uri")
					{
						return this._message.Uri;
					}
				}
				else if (num != 1619225942U)
				{
					if (num == 1637783905U)
					{
						if (key == "__Return")
						{
							return ((IMethodReturnMessage)this._message).ReturnValue;
						}
					}
				}
				else if (key == "__Args")
				{
					return this._message.Args;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num != 1960967436U)
				{
					if (num == 2010141056U)
					{
						if (key == "__TypeName")
						{
							return this._message.TypeName;
						}
					}
				}
				else if (key == "__OutArgs")
				{
					return ((IMethodReturnMessage)this._message).OutArgs;
				}
			}
			else if (num != 3166241401U)
			{
				if (num == 3679129400U)
				{
					if (key == "__MethodSignature")
					{
						return this._message.MethodSignature;
					}
				}
			}
			else if (key == "__MethodName")
			{
				return this._message.MethodName;
			}
			return null;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x00098E58 File Offset: 0x00097058
		protected virtual void SetMethodProperty(string key, object value)
		{
			uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
			if (num <= 1637783905U)
			{
				if (num <= 1201911322U)
				{
					if (num != 990701179U)
					{
						if (num != 1201911322U)
						{
							return;
						}
						key == "__CallContext";
						return;
					}
					else
					{
						if (!(key == "__Uri"))
						{
							return;
						}
						((IInternalMessage)this._message).Uri = (string)value;
						return;
					}
				}
				else
				{
					if (num == 1619225942U)
					{
						key == "__Args";
						return;
					}
					if (num != 1637783905U)
					{
						return;
					}
					key == "__Return";
					return;
				}
			}
			else if (num <= 2010141056U)
			{
				if (num == 1960967436U)
				{
					key == "__OutArgs";
					return;
				}
				if (num != 2010141056U)
				{
					return;
				}
				key == "__TypeName";
				return;
			}
			else
			{
				if (num == 3166241401U)
				{
					key == "__MethodName";
					return;
				}
				if (num != 3679129400U)
				{
					return;
				}
				key == "__MethodSignature";
				return;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x00098F50 File Offset: 0x00097150
		public ICollection Keys
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this._methodKeys[i]);
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties.Keys)
					{
						string text = (string)obj;
						if (!this.IsOverridenKey(text))
						{
							arrayList.Add(text);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x00098FEC File Offset: 0x000971EC
		public ICollection Values
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				for (int i = 0; i < this._methodKeys.Length; i++)
				{
					arrayList.Add(this.GetMethodProperty(this._methodKeys[i]));
				}
				if (this._internalProperties != null)
				{
					foreach (object obj in this._internalProperties)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!this.IsOverridenKey((string)dictionaryEntry.Key))
						{
							arrayList.Add(dictionaryEntry.Value);
						}
					}
				}
				return arrayList;
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0009909C File Offset: 0x0009729C
		public void Add(object key, object value)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					this.SetMethodProperty(text, value);
					return;
				}
			}
			if (this._internalProperties == null)
			{
				this._internalProperties = this.AllocInternalProperties();
			}
			this._internalProperties[key] = value;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000990FD File Offset: 0x000972FD
		public void Clear()
		{
			if (this._internalProperties != null)
			{
				this._internalProperties.Clear();
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00099114 File Offset: 0x00097314
		public bool Contains(object key)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					return true;
				}
			}
			return this._internalProperties != null && this._internalProperties.Contains(key);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x00099164 File Offset: 0x00097364
		public void Remove(object key)
		{
			string text = (string)key;
			for (int i = 0; i < this._methodKeys.Length; i++)
			{
				if (this._methodKeys[i] == text)
				{
					throw new ArgumentException("key was invalid");
				}
			}
			if (this._internalProperties != null)
			{
				this._internalProperties.Remove(key);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000991BA File Offset: 0x000973BA
		public int Count
		{
			get
			{
				if (this._internalProperties != null)
				{
					return this._internalProperties.Count + this._methodKeys.Length;
				}
				return this._methodKeys.Length;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x00002645 File Offset: 0x00000845
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000991E1 File Offset: 0x000973E1
		public void CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000991F0 File Offset: 0x000973F0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000991F0 File Offset: 0x000973F0
		public IDictionaryEnumerator GetEnumerator()
		{
			return new MessageDictionary.DictionaryEnumerator(this);
		}

		// Token: 0x04001201 RID: 4609
		private IDictionary _internalProperties;

		// Token: 0x04001202 RID: 4610
		protected IMethodMessage _message;

		// Token: 0x04001203 RID: 4611
		private string[] _methodKeys;

		// Token: 0x04001204 RID: 4612
		private bool _ownProperties;

		// Token: 0x02000493 RID: 1171
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060025A9 RID: 9641 RVA: 0x000991F8 File Offset: 0x000973F8
			public DictionaryEnumerator(MessageDictionary methodDictionary)
			{
				this._methodDictionary = methodDictionary;
				this._hashtableEnum = ((this._methodDictionary._internalProperties != null) ? this._methodDictionary._internalProperties.GetEnumerator() : null);
				this._posMethod = -1;
			}

			// Token: 0x170004CC RID: 1228
			// (get) Token: 0x060025AA RID: 9642 RVA: 0x00099234 File Offset: 0x00097434
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x060025AB RID: 9643 RVA: 0x00099244 File Offset: 0x00097444
			public bool MoveNext()
			{
				if (this._posMethod != -2)
				{
					this._posMethod++;
					if (this._posMethod < this._methodDictionary._methodKeys.Length)
					{
						return true;
					}
					this._posMethod = -2;
				}
				if (this._hashtableEnum == null)
				{
					return false;
				}
				while (this._hashtableEnum.MoveNext())
				{
					if (!this._methodDictionary.IsOverridenKey((string)this._hashtableEnum.Key))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x060025AC RID: 9644 RVA: 0x000992BF File Offset: 0x000974BF
			public void Reset()
			{
				this._posMethod = -1;
				this._hashtableEnum.Reset();
			}

			// Token: 0x170004CD RID: 1229
			// (get) Token: 0x060025AD RID: 9645 RVA: 0x000992D4 File Offset: 0x000974D4
			public DictionaryEntry Entry
			{
				get
				{
					if (this._posMethod >= 0)
					{
						return new DictionaryEntry(this._methodDictionary._methodKeys[this._posMethod], this._methodDictionary.GetMethodProperty(this._methodDictionary._methodKeys[this._posMethod]));
					}
					if (this._posMethod == -1 || this._hashtableEnum == null)
					{
						throw new InvalidOperationException("The enumerator is positioned before the first element of the collection or after the last element");
					}
					return this._hashtableEnum.Entry;
				}
			}

			// Token: 0x170004CE RID: 1230
			// (get) Token: 0x060025AE RID: 9646 RVA: 0x00099348 File Offset: 0x00097548
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x170004CF RID: 1231
			// (get) Token: 0x060025AF RID: 9647 RVA: 0x00099364 File Offset: 0x00097564
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x04001205 RID: 4613
			private MessageDictionary _methodDictionary;

			// Token: 0x04001206 RID: 4614
			private IDictionaryEnumerator _hashtableEnum;

			// Token: 0x04001207 RID: 4615
			private int _posMethod;
		}
	}
}
