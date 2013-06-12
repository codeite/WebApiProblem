WebApiProblem
=============

A small framework for turning exceptions into good Rest responses in WebApi

It is based on the recomendations in http://tools.ietf.org/html/draft-nottingham-http-problem-03

To use, have the exceptions you raise inherit from BasicApiProblemException.

To create more elaborate problem message, inherit from ApiProblemException<T> and define a serializable model that ingerits from ApiProblem for T.