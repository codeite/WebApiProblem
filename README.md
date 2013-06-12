WebApiProblem
=============

A small framework for turning exceptions into good Rest responses in WebApi

It is based on the recomendations in http://tools.ietf.org/html/draft-nottingham-http-problem-03

## Use

 1) Install the package from nuget:
		
		```
		PM > Install-Package WebApiProblem
		```

 2) Register the ResponseExceptionFilterAttribute (either in `ApplicationStart` or wherever you register your filters)
		
		``` c#
		GlobalConfiguration.Configuration.Filters.Add(new WebApiProblem.ResponseExceptionFilterAttribute());
		```

 3) Throw an exception of type `BasicApiProblemException`
		``` c#
		 // GET api/values/5
        public string Get(int id)
        {
             throw new BasicApiProblemException(HttpStatusCode.BadRequest, "That was a bad request", "http://www.api-problems.com/really-bad-request");
        }
		```
 
 4) View the well understood response and be happy
 
		_400 - Bad request_
		``` json
		{
			"problemType": "http://www.api-problems.com/really-bad-request",
			"title": "That was a bad request",
			"detail": "Please send me BETTER requests in the future"
		}
		```
 
To create more elaborate problem message, inherit from ApiProblemException<T> and define a serializable model that inherits from ApiProblem for T.