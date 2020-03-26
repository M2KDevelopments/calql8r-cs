//========================================================================
// This conversion was produced by the Free Edition of
// Java to C# Converter courtesy of Tangible Software Solutions.
// Order the Premium Edition at https://www.tangiblesoftwaresolutions.com
//========================================================================

using System;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
namespace calql8r.Functions
{
	using GeneralCalValue = calql8r.Values.GeneralCalValue;

	/// 
	/// <summary>
	/// @author MARTIN
	/// </summary>
	public abstract class GeneralCalFunction : GeneralCalButton
	{

		public GeneralCalFunction()
		{
			parameters = new GeneralCalValue[0];
		}

		protected internal GeneralCalValue[] parameters;

		protected internal abstract int NumberOfNeededParameters {get;}

		public abstract GeneralCalValue calculate();

		public virtual void addParameter(GeneralCalValue value)
		{

			GeneralCalValue[] par = new GeneralCalValue[parameters.Length + 1];
			//add item to array
			Array.Copy(parameters, 0, par, 0, parameters.Length);
			par[par.Length - 1] = value;
			parameters = par;
		}

		/// <summary>
		/// The class seems to store the same parameters for other functions. There is a need to clear the memory
		/// </summary>
		public virtual void clearParameterMemory()
		{
			 parameters = new GeneralCalValue[0];
		}

		public virtual bool parameterCountIsOK()
		{
			return NumberOfNeededParameters == parameters.Length;
		}
	}

}