using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DotNet_WebApiFramework
{
	internal class Api
	{
		//Declare global varables
		private static HttpClient client = new HttpClient();//The Http Client

		public String ApiToken;//The Api Global Token For the login
		private String ApiUrl;//The base APi URL to make requests to
		private bool IsLoggedIn;//true/false if user is logged in
		private AccountData AccountData = new AccountData();//the strucutre containing the current logged in users info

		// Constructor for the Api
		//Takes in the Base URL
		public Api(String ApiUrl_In)
		{
			ApiUrl = ApiUrl_In;
			client.BaseAddress = new Uri(ApiUrl_In);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));
		}

		// Constructor for the Api
		//Takes in the Base URL and Token
		public Api(String ApiUrl_In, String ApiToken_In)
		{
			ApiUrl = ApiUrl_In;
			ApiToken = ApiToken_In;
			client.BaseAddress = new Uri(ApiUrl_In);
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(
			new MediaTypeWithQualityHeaderValue("application/json"));
		}

		//Returns the Current Userlogin Struct or a blank object if the user isnt logged in
		public AccountData CurrentLogin()
		{
			if (IsLoggedIn)
			{
				return AccountData;
			}
			Console.WriteLine("Error: User is not logged in");
			return AccountData;
		}

		//This will call a post To the URL with the object converted to a json
		//returns the HTTP responce for parsing
		public async Task<HttpResponseMessage> ApiPostAsync(String Url, Object Obj_In)
		{
			HttpResponseMessage response = await client.PostAsJsonAsync(
				Url, Obj_In);
			// return Responce
			return response;
		}

		//This will call a post To the URL with the object converted to a json
		//This function will parse the token into the params
		//returns a true or false depending on the result
		public async Task<bool> ApiPostAsync_Token(String Url, Object Obj_In)
		{
			HttpResponseMessage response = await client.PostAsJsonAsync(
				(Url + "?token=" + ApiToken), Obj_In);
			try
			{
				response.EnsureSuccessStatusCode();
				return true;
			}
			catch
			{
				//The client received an error
				Console.WriteLine("Error in POST");
			}
			// return Responce
			return false;
		}

		//This will call a post To the URL with the object converted to a json
		//returns a true or false depending on the result
		public async Task<bool> ApiPostAsync_WOToken(String Url, Object Obj_In)
		{
			HttpResponseMessage response = await client.PostAsJsonAsync(
				(Url), Obj_In);
			try
			{
				response.EnsureSuccessStatusCode();
				return true;
			}
			catch
			{
				//The client received an error
				Console.WriteLine("Error in POST");
			}
			// return Responce
			return false;
		}

		//This will call a post To the URL with the object converted to a json
		//returns the ERROR Struct responce
		public async Task<Error> ApiGet(String Url)
		{
			Error result = new Error();
			HttpResponseMessage response = await client.GetAsync(Url + "?token=" + ApiToken);
			if (response.IsSuccessStatusCode)
			{
				result = await response.Content.ReadAsAsync<Error>();
			}
			return result;
		}

		public async Task<HttpResponseMessage> CallGetWobj(String Url)
		{
			HttpResponseMessage response = await client.GetAsync(Url + "?token=" + ApiToken);
			response.EnsureSuccessStatusCode();
			return response;
		}

		//new CreateAccount Method
		public async Task<bool> CreateNewAccount(String Username, String Password, String Email)
		{
			//Create strucutre for user JSON
			NewUser Tempuser = new NewUser();
			Tempuser.username = Username;
			Tempuser.email = Email;
			Tempuser.password = Password;
			//Send the login request
			try
			{
				HttpResponseMessage NewuserRequest = await ApiPostAsync("api/v1/Auth/CreateAccount", Tempuser);
				//Check if the responce returns created
				NewuserRequest.EnsureSuccessStatusCode();
				AccountData.username = Username;
				AccountData.password = Password;
				AccountData.email = Email;
			}
			catch (Exception e)
			{//CreateAccount was unsucessfull
				Console.WriteLine("Creating New account was Unsucessfull With Error" + e.ToString());
				return false;
			}
			//user account has been created Lets Login to get the APiToken
			Console.WriteLine("Created New Account Sucessfully\nAttempting Login...");
			bool LoginResponse = await ApiLogin(Username, Password, Email);
			if (!LoginResponse)
			{
				return false;
			}
			return true;
		}

		//Login Async Function
		//Takes in a username and password and returns true if user is logged in
		//or false if the login failed
		public async Task<bool> ApiLogin(String Username, String Password, String Email)
		{
			//Create strucutre for user JSON
			NewUser Tempuser = new NewUser();
			oauth2 LoginResponse = new oauth2();
			Tempuser.username = Username;
			Tempuser.email = Email;
			Tempuser.password = Password;
			//Send the login request
			try
			{
				HttpResponseMessage LoginRequest = await ApiPostAsync("api/v1/Auth/Login", Tempuser);
				//Check if the responce returns created
				LoginRequest.EnsureSuccessStatusCode();
				// Deserialize the updated product from the response body.
				LoginResponse = await LoginRequest.Content.ReadAsAsync<oauth2>();
				//parse the Api key into the framework
				ApiToken = LoginResponse.token;
				//Set the login varable
				IsLoggedIn = true;
				//Save the struct
				AccountData.oauth2 = LoginResponse;
				AccountData.username = Username;
				AccountData.password = Password;
			}
			catch (Exception e)
			{//Login was unsucessfull
				Console.WriteLine("Logging in was Unsucessfull With Error" + e.ToString());
				return false;
			}
			return true;
		}

		//async APi Token Testing function
		public async Task<bool> TestToken()
		{
			//call the get request with the token appended
			if (!IsLoggedIn)
			{
				return false;
			}
			//user is logged in so we have a token??
			Error Result = new Error();
			try
			{
				Result = await ApiGet("api/v1/Auth/TestToken");
			}
			catch (Exception e)
			{
				return false;
			}
			return true;
		}

		//This function Attempts to get the profile for the current user
		//will return the profile of the user if sucessfull
		public async Task<ProfileData> GetProfile()
		{
			//user is logged in so we have a token??
			HttpResponseMessage Result = new HttpResponseMessage();
			try
			{
				Result = await CallGetWobj("api/v1/data/ProfileData");
			}
			catch (Exception e)
			{
				Console.WriteLine("Getting Profile Returned an error" + e.ToString());
			}
			return await Result.Content.ReadAsAsync<ProfileData>();
		}
	}
}