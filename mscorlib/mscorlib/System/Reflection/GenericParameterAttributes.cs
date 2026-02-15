using System;

namespace System.Reflection
{
	/// <summary>Describes the constraints on a generic type parameter of a generic type or method.</summary>
	// Token: 0x020005FF RID: 1535
	[Flags]
	public enum GenericParameterAttributes
	{
		/// <summary>There are no special flags.</summary>
		// Token: 0x0400170A RID: 5898
		None = 0,
		/// <summary>Selects the combination of all variance flags. This value is the result of using logical OR to combine the following flags: <see cref="F:System.Reflection.GenericParameterAttributes.Contravariant" /> and <see cref="F:System.Reflection.GenericParameterAttributes.Covariant" />.</summary>
		// Token: 0x0400170B RID: 5899
		VarianceMask = 3,
		/// <summary>The generic type parameter is covariant. A covariant type parameter can appear as the result type of a method, the type of a read-only field, a declared base type, or an implemented interface.</summary>
		// Token: 0x0400170C RID: 5900
		Covariant = 1,
		/// <summary>The generic type parameter is contravariant. A contravariant type parameter can appear as a parameter type in method signatures. </summary>
		// Token: 0x0400170D RID: 5901
		Contravariant = 2,
		/// <summary>Selects the combination of all special constraint flags. This value is the result of using logical OR to combine the following flags: <see cref="F:System.Reflection.GenericParameterAttributes.DefaultConstructorConstraint" />, <see cref="F:System.Reflection.GenericParameterAttributes.ReferenceTypeConstraint" />, and <see cref="F:System.Reflection.GenericParameterAttributes.NotNullableValueTypeConstraint" />.</summary>
		// Token: 0x0400170E RID: 5902
		SpecialConstraintMask = 28,
		/// <summary>A type can be substituted for the generic type parameter only if it is a reference type.</summary>
		// Token: 0x0400170F RID: 5903
		ReferenceTypeConstraint = 4,
		/// <summary>A type can be substituted for the generic type parameter only if it is a value type and is not nullable.</summary>
		// Token: 0x04001710 RID: 5904
		NotNullableValueTypeConstraint = 8,
		/// <summary>A type can be substituted for the generic type parameter only if it has a parameterless constructor.</summary>
		// Token: 0x04001711 RID: 5905
		DefaultConstructorConstraint = 16
	}
}
