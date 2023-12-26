using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ValidationCombined
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Func<string, bool> IsMatch(string pattern) => (string input) => Regex.IsMatch(input, pattern);
			Func<string, bool> IsInclude(string pattern) => (string input) => input.Contains(pattern);

			Func<string, bool> CreateValidation(Func<string, bool> validate, Action showMessage) => (string input) => 
			{
				bool isMatch = validate(input);
				if (isMatch) showMessage();
				return isMatch;
			};

			const string userName = "Oshii Mamoru 64";

			bool match = new List<Func<string, bool>>
			{
				CreateValidation(IsMatch("[a-z|A-Z]"), ()=>Console.WriteLine("Alphabet Exists")),
				CreateValidation(IsMatch("[0-9]"), ()=>Console.WriteLine("Number Exists")),
				CreateValidation(IsInclude("Oshii"), ()=>Console.WriteLine("He is Mamoru"))
			}
			.Aggregate(false, (any, validate) => any |= validate(userName));

			Console.ReadLine();
		}
	}
}
