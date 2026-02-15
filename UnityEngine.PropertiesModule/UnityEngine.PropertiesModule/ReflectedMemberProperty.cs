using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Unity.Properties
{
	// Token: 0x02000020 RID: 32
	public class ReflectedMemberProperty<TContainer, TValue> : Property<TContainer, TValue>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000401F File Offset: 0x0000221F
		public override string Name { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004027 File Offset: 0x00002227
		public override bool IsReadOnly { get; }

		// Token: 0x06000080 RID: 128 RVA: 0x00004030 File Offset: 0x00002230
		internal ReflectedMemberProperty(IMemberInfo info, string name)
		{
			this.Name = name;
			this.m_Info = info;
			this.m_IsStructContainerType = TypeTraits<TContainer>.IsValueType;
			base.AddAttributes(info.GetCustomAttributes());
			bool isReadOnly = this.m_Info.IsReadOnly;
			bool flag = base.HasAttribute<CreatePropertyAttribute>();
			if (flag)
			{
				CreatePropertyAttribute createProperty = base.GetAttribute<CreatePropertyAttribute>();
				isReadOnly |= createProperty.ReadOnly;
			}
			this.IsReadOnly = isReadOnly;
			IMemberInfo memberInfo = this.m_Info;
			FieldMember fieldMember;
			bool flag2;
			if (memberInfo is FieldMember)
			{
				fieldMember = (FieldMember)memberInfo;
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			bool flag3 = flag2;
			if (flag3)
			{
				FieldInfo fieldInfo = fieldMember.m_FieldInfo;
				DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, fieldInfo.FieldType, new Type[] { this.m_IsStructContainerType ? fieldInfo.ReflectedType.MakeByRefType() : fieldInfo.ReflectedType }, true);
				ILGenerator ilGenerator = dynamicMethod.GetILGenerator();
				ilGenerator.Emit(OpCodes.Ldarg_0);
				ilGenerator.Emit(OpCodes.Ldfld, fieldInfo);
				ilGenerator.Emit(OpCodes.Ret);
				bool isStructContainerType = this.m_IsStructContainerType;
				if (isStructContainerType)
				{
					this.m_GetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction));
				}
				else
				{
					this.m_GetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction));
				}
				bool flag4 = !isReadOnly;
				if (flag4)
				{
					dynamicMethod = new DynamicMethod(string.Empty, typeof(void), new Type[]
					{
						this.m_IsStructContainerType ? fieldInfo.ReflectedType.MakeByRefType() : fieldInfo.ReflectedType,
						fieldInfo.FieldType
					}, true);
					ilGenerator = dynamicMethod.GetILGenerator();
					ilGenerator.Emit(OpCodes.Ldarg_0);
					ilGenerator.Emit(OpCodes.Ldarg_1);
					ilGenerator.Emit(OpCodes.Stfld, fieldInfo);
					ilGenerator.Emit(OpCodes.Ret);
					bool isStructContainerType2 = this.m_IsStructContainerType;
					if (isStructContainerType2)
					{
						this.m_SetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction));
					}
					else
					{
						this.m_SetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction)dynamicMethod.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction));
					}
				}
			}
			else
			{
				memberInfo = this.m_Info;
				PropertyMember propertyMember;
				bool flag5;
				if (memberInfo is PropertyMember)
				{
					propertyMember = (PropertyMember)memberInfo;
					flag5 = true;
				}
				else
				{
					flag5 = false;
				}
				bool flag6 = flag5;
				if (flag6)
				{
					bool isStructContainerType3 = this.m_IsStructContainerType;
					if (isStructContainerType3)
					{
						MethodInfo getMethod = propertyMember.m_PropertyInfo.GetGetMethod(true);
						this.m_GetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction), getMethod);
						bool flag7 = !isReadOnly;
						if (flag7)
						{
							MethodInfo setMethod = propertyMember.m_PropertyInfo.GetSetMethod(true);
							this.m_SetStructValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction), setMethod);
						}
					}
					else
					{
						MethodInfo getMethod2 = propertyMember.m_PropertyInfo.GetGetMethod(true);
						this.m_GetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction), getMethod2);
						bool flag8 = !isReadOnly;
						if (flag8)
						{
							MethodInfo setMethod2 = propertyMember.m_PropertyInfo.GetSetMethod(true);
							this.m_SetClassValueAction = (ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction)Delegate.CreateDelegate(typeof(ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction), setMethod2);
						}
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000435C File Offset: 0x0000255C
		public override TValue GetValue(ref TContainer container)
		{
			bool isStructContainerType = this.m_IsStructContainerType;
			TValue tvalue;
			if (isStructContainerType)
			{
				tvalue = ((this.m_GetStructValueAction == null) ? ((TValue)((object)this.m_Info.GetValue(container))) : this.m_GetStructValueAction(ref container));
			}
			else
			{
				tvalue = ((this.m_GetClassValueAction == null) ? ((TValue)((object)this.m_Info.GetValue(container))) : this.m_GetClassValueAction(container));
			}
			return tvalue;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000043E4 File Offset: 0x000025E4
		public override void SetValue(ref TContainer container, TValue value)
		{
			bool isReadOnly = this.IsReadOnly;
			if (isReadOnly)
			{
				throw new InvalidOperationException("Property is ReadOnly.");
			}
			bool isStructContainerType = this.m_IsStructContainerType;
			if (isStructContainerType)
			{
				bool flag = this.m_SetStructValueAction == null;
				if (flag)
				{
					object boxed = container;
					this.m_Info.SetValue(boxed, value);
					container = (TContainer)((object)boxed);
				}
				else
				{
					this.m_SetStructValueAction(ref container, value);
				}
			}
			else
			{
				bool flag2 = this.m_SetClassValueAction == null;
				if (flag2)
				{
					this.m_Info.SetValue(container, value);
				}
				else
				{
					this.m_SetClassValueAction(container, value);
				}
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly IMemberInfo m_Info;

		// Token: 0x0400003C RID: 60
		private readonly bool m_IsStructContainerType;

		// Token: 0x0400003D RID: 61
		private ReflectedMemberProperty<TContainer, TValue>.GetStructValueAction m_GetStructValueAction;

		// Token: 0x0400003E RID: 62
		private ReflectedMemberProperty<TContainer, TValue>.SetStructValueAction m_SetStructValueAction;

		// Token: 0x0400003F RID: 63
		private ReflectedMemberProperty<TContainer, TValue>.GetClassValueAction m_GetClassValueAction;

		// Token: 0x04000040 RID: 64
		private ReflectedMemberProperty<TContainer, TValue>.SetClassValueAction m_SetClassValueAction;

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x06000084 RID: 132
		private delegate TValue GetStructValueAction(ref TContainer container);

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x06000086 RID: 134
		private delegate void SetStructValueAction(ref TContainer container, TValue value);

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x06000088 RID: 136
		private delegate TValue GetClassValueAction(TContainer container);

		// Token: 0x02000024 RID: 36
		// (Invoke) Token: 0x0600008A RID: 138
		private delegate void SetClassValueAction(TContainer container, TValue value);
	}
}
