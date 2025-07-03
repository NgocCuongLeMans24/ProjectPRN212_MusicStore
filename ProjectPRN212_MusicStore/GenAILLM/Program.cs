using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace GenAILLM
{
	public class Program
	{
		static async Task Main()
		{
			string calculatorPath = Path.Combine("..", "..", "..", "Calculator.cs");
			if (!File.Exists(calculatorPath))
			{
				Console.WriteLine("Calculator.cs not found.");
				return;
			}
		string methodCode = await File.ReadAllTextAsync(calculatorPath);
		Console.WriteLine("Loaded Calculator.cs");
		var prompt = $"""
						Write a real xUnit test for the following C# method: {methodCode}. Do not use Moq or mocking.
						Just create a real test that calls the method and asserts the result.
						""";

		var client = new HttpClient();

			var requestBody = new
			{
				model = "local-model", // Must match model loaded in LM Studio
				messages = new[]
				{
				new {role = "user",content = prompt}
				}
			};

			var json = JsonConvert. SerializeObject(requestBody);
			var content = new StringContent(json, Encoding. UTF8, "application/json");

			try
			{
				var response = await client.PostAsync("http://localhost:1234/v1/chat/completions", content);
				var responseText = await response.Content.ReadAsStringAsync();

				dynamic parsed = JsonConvert.DeserializeObject(responseText);
				var unitTestCode = parsed.choices[0].message.content.ToString();

				Console.WriteLine("\nGenerated Unit Test:\n");
				Console.WriteLine(unitTestCode);

				string solutionRoot = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..");
				string testProjectPath = Path.GetFullPath(Path.Combine(solutionRoot, "GenAILLMTests", "GeneratedTests.cs"));

				Directory.CreateDirectory(Path.GetDirectoryName(testProjectPath)!);
				await File.WriteAllTextAsync(testProjectPath, unitTestCode);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error calling LLM: " + ex.Message);
			}			
			}
	}

}
