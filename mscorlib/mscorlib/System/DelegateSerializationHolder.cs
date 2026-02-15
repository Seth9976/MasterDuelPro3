using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	internal class DelegateSerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x06001218 RID: 4632 RVA: 0x0004966C File Offset: 0x0004786C
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			int num = 0;
			DelegateSerializationHolder.DelegateEntry delegateEntry2 = delegateEntry;
			while (delegateEntry2 != null)
			{
				delegateEntry2 = delegateEntry2.delegateEntry;
				num++;
			}
			if (num == 1)
			{
				this._delegate = delegateEntry.DeserializeDelegate(info, 0);
				return;
			}
			Delegate[] array = new Delegate[num];
			delegateEntry2 = delegateEntry;
			for (int i = 0; i < num; i++)
			{
				array[i] = delegateEntry2.DeserializeDelegate(info, i);
				delegateEntry2 = delegateEntry2.delegateEntry;
			}
			this._delegate = Delegate.Combine(array);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000496FC File Offset: 0x000478FC
		public static void GetDelegateData(Delegate instance, SerializationInfo info, StreamingContext ctx)
		{
			Delegate[] invocationList = instance.GetInvocationList();
			DelegateSerializationHolder.DelegateEntry delegateEntry = null;
			for (int i = 0; i < invocationList.Length; i++)
			{
				Delegate @delegate = invocationList[i];
				string text = ((@delegate.Target != null) ? ("target" + i.ToString()) : null);
				DelegateSerializationHolder.DelegateEntry delegateEntry2 = new DelegateSerializationHolder.DelegateEntry(@delegate, text);
				if (delegateEntry == null)
				{
					info.AddValue("Delegate", delegateEntry2);
				}
				else
				{
					delegateEntry.delegateEntry = delegateEntry2;
				}
				delegateEntry = delegateEntry2;
				if (@delegate.Target != null)
				{
					info.AddValue(text, @delegate.Target);
				}
				info.AddValue("method" + i.ToString(), @delegate.Method);
			}
			info.SetType(typeof(DelegateSerializationHolder));
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x000339FF File Offset: 0x00031BFF
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000497B2 File Offset: 0x000479B2
		public object GetRealObject(StreamingContext context)
		{
			return this._delegate;
		}

		// Token: 0x04000750 RID: 1872
		private Delegate _delegate;

		// Token: 0x020001CC RID: 460
		[Serializable]
		private class DelegateEntry
		{
			// Token: 0x0600121C RID: 4636 RVA: 0x000497BC File Offset: 0x000479BC
			public DelegateEntry(Delegate del, string targetLabel)
			{
				this.type = del.GetType().FullName;
				this.assembly = del.GetType().Assembly.FullName;
				this.target = targetLabel;
				this.targetTypeAssembly = del.Method.DeclaringType.Assembly.FullName;
				this.targetTypeName = del.Method.DeclaringType.FullName;
				this.methodName = del.Method.Name;
			}

			// Token: 0x0600121D RID: 4637 RVA: 0x00049840 File Offset: 0x00047A40
			public Delegate DeserializeDelegate(SerializationInfo info, int index)
			{
				object obj = null;
				if (this.target != null)
				{
					obj = info.GetValue(this.target.ToString(), typeof(object));
				}
				string text = "method" + index.ToString();
				MethodInfo methodInfo = (MethodInfo)info.GetValueNoThrow(text, typeof(MethodInfo));
				Type type = Assembly.Load(this.assembly).GetType(this.type);
				if (obj != null)
				{
					if (RemotingServices.IsTransparentProxy(obj) && !Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName).IsInstanceOfType(obj))
					{
						throw new RemotingException("Unexpected proxy type.");
					}
					if (!(methodInfo == null))
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					return Delegate.CreateDelegate(type, obj, this.methodName);
				}
				else
				{
					if (methodInfo != null)
					{
						return Delegate.CreateDelegate(type, obj, methodInfo);
					}
					Type type2 = Assembly.Load(this.targetTypeAssembly).GetType(this.targetTypeName);
					return Delegate.CreateDelegate(type, type2, this.methodName);
				}
			}

			// Token: 0x04000751 RID: 1873
			private string type;

			// Token: 0x04000752 RID: 1874
			private string assembly;

			// Token: 0x04000753 RID: 1875
			private object target;

			// Token: 0x04000754 RID: 1876
			private string targetTypeAssembly;

			// Token: 0x04000755 RID: 1877
			private string targetTypeName;

			// Token: 0x04000756 RID: 1878
			private string methodName;

			// Token: 0x04000757 RID: 1879
			public DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
