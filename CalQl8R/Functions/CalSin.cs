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
	public class CalSin : GeneralCalFunction
	{

		protected internal override int NumberOfNeededParameters
		{
			get
			{
				return 1;
			}
		}

		public override GeneralCalValue calculate()
		{
			return new GeneralCalValue(Math.Sin(parameters[0].Value));
		}

		public override string displayText()
		{
			return "sin";
		}

	}

}