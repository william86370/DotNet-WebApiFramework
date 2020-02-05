using System;
using System.Threading.Tasks;

namespace DotNet_WebApiFramework
{
	internal class Program
	{
		private static async Task Main(string[] args)
		{
			//Create the API Call with the base server
			Api WebApi = new Api("http://localhost:3000/");

			//Create the test Strucutre
			NewUser TestUser = new NewUser
			{
				username = "testnewhusger45",
				password = "testnewhusger45",
				email = "testnewugsehr45"
			};

			//NEWACCOUNT TEST
			//NOTE Can only be created once

			//call the create user
			if (await WebApi.CreateNewAccount(TestUser.username, TestUser.password, TestUser.email))
			{
				Console.WriteLine("\nNew Account and login Sucess ");
				//Call the test token Method
				if (await WebApi.TestToken())
				{
					Console.WriteLine("\nToken Works");
					//Get the first set of login values
					AccountData CurrentUser2 = WebApi.CurrentLogin();
					//print the first values of the flogin options
					Console.WriteLine("\nLets Print Everything\nUsername: " + CurrentUser2.username + "\nPassword: " + CurrentUser2.password + "\nApiToken: " + CurrentUser2.oauth2.token + "\nUDID: " + CurrentUser2.oauth2.udid);
				}
			}
			else
			{
				Console.WriteLine("\nCouldnt Create a new Account");
			}

			//LOGIN TEST

			//lets Get a New Login Token and pritnt the Token
			if (await WebApi.ApiLogin(TestUser.username, TestUser.password, TestUser.email))
			{
				if (await WebApi.TestToken())
				{
					Console.WriteLine("\nToken Works");
					//Get the current login Values
					AccountData CurrentUser = WebApi.CurrentLogin();
					//Print the new values
					Console.WriteLine("\nPrinting Login Results\nUsername: " + CurrentUser.username + "\nPassword: " + CurrentUser.password + "\nApiToken: " + CurrentUser.oauth2.token + "\nUDID: " + CurrentUser.oauth2.udid);
				}
			}

			//PROFILE UPDATE TEST

			//Create the users profile
			ProfileData UserNewProfile = new ProfileData
			{
				//Add the token to the body call
				firstname = "TestFirstname",
				lastname = "TestLastname",
				pfpurl = "TestURL",
				bio = "TestBio",
				usid = "TestUSID",
				status = "TestStatus",
				employment = "TestEmployment"
			};

			//Send that profile to the server with the token
			if (await WebApi.ApiPostAsync_Token("api/v1/data/ProfileData", UserNewProfile))
			{
				//The users profile was updated on the server
				Console.WriteLine("\nProfile updated");
				//Get the Users Profile
				ProfileData NewProfile = await WebApi.GetProfile();
				Console.WriteLine("\nPrinting Updated Profile\nfirstname: " + NewProfile.firstname + "\nLastname: " + NewProfile.lastname);
			}
			else
			{
				Console.WriteLine("\nProfile update Failed");
				//profile wasnt updated on the server
			}

		}
	}
}