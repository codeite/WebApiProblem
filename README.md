WebApiProblem
=============

## application/api-problem

A small framework for turning exceptions into good Rest responses in MVC WebApi (.net).

It is based on the recomendations in http://tools.ietf.org/html/draft-nottingham-http-problem-03

It supports responses in json:

application/api-problem+json

and xml:

application/api-problem+xml

## Use

 1) Install the package from nuget:
		

		PM > Install-Package WebApiProblem


 2) Register the ResponseExceptionFilterAttribute (either in `ApplicationStart` or wherever you register your filters)
		

		GlobalConfiguration.Configuration.Filters.Add(new WebApiProblem.ResponseExceptionFilterAttribute());


 3) Throw an exception of type `BasicApiProblemException`

 
		 // GET api/values/5
        public string Get(int id)
        {
             throw new BasicApiProblemException(HttpStatusCode.BadRequest, 
				"That was a bad request", "http://www.api-problems.com/really-bad-request");
        }

		
 
 4) View the well understood response and be happy
 
		400 - Bad request
		{
			"problemType": "http://www.api-problems.com/really-bad-request",
			"title": "That was a bad request",
			"detail": "Please send me BETTER requests in the future"
		}

 
## More elaborate problem messages
 
To create more elaborate problem message, inherit from ApiProblemException<T> and define a serializable model that inherits from ApiProblem for T.