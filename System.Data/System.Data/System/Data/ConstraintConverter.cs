using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace System.Data
{
	// Token: 0x0200002F RID: 47
	internal sealed class ConstraintConverter : ExpandableObjectConverter
	{
		// Token: 0x0600037B RID: 891 RVA: 0x00011F29 File Offset: 0x00010129
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000131F0 File Offset: 0x000113F0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(InstanceDescriptor) && value is Constraint)
			{
				if (value is UniqueConstraint)
				{
					UniqueConstraint uniqueConstraint = (UniqueConstraint)value;
					ConstructorInfo constructor = typeof(UniqueConstraint).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string[]),
						typeof(bool)
					});
					if (constructor != null)
					{
						return new InstanceDescriptor(constructor, new object[] { uniqueConstraint.ConstraintName, uniqueConstraint.ColumnNames, uniqueConstraint.IsPrimaryKey });
					}
				}
				else
				{
					ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)value;
					ConstructorInfo constructor2 = typeof(ForeignKeyConstraint).GetConstructor(new Type[]
					{
						typeof(string),
						typeof(string),
						typeof(string[]),
						typeof(string[]),
						typeof(AcceptRejectRule),
						typeof(Rule),
						typeof(Rule)
					});
					if (constructor2 != null)
					{
						return new InstanceDescriptor(constructor2, new object[]
						{
							foreignKeyConstraint.ConstraintName,
							foreignKeyConstraint.ParentKey.Table.TableName,
							foreignKeyConstraint.ParentColumnNames,
							foreignKeyConstraint.ChildColumnNames,
							foreignKeyConstraint.AcceptRejectRule,
							foreignKeyConstraint.DeleteRule,
							foreignKeyConstraint.UpdateRule
						});
					}
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
