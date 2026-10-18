# BasicWebServer
A small HTTP web server written from scratch in C#. It listens on a TCP socket, parses raw HTTP requests by hand and sends back responses, without using ASP.NET or any other web framework.

The goal of the project was to understand what a web framework does under the hood: how a request is read, routed and answered, and how cookies and sessions keep track of a user between requests.
