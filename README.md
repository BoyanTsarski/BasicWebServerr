# BasicWebServer

A small HTTP web server written from scratch in C#. It listens on a TCP socket, parses raw HTTP requests by hand and sends back responses, without using ASP.NET or any other web framework.

The goal of the project was to understand what a web framework does under the hood: how a request is read, routed and answered, and how cookies and sessions keep track of a user between requests.

## Features

- **TCP listener** built on `TcpListener`, handling each connection asynchronously with `async`/`await`
- **HTTP parsing** of the request line, headers, cookies and form data
- **Routing** of `GET` and `POST` requests to controller actions
- **Responses:** HTML, plain text, redirect, file download, 400, 401 and 404
- **Cookies and sessions**, including a simple login and logout
- **Mini MVC layer:** controllers, `.cshtml` views and model binding for forms

## Project structure

| Project | What it contains |
| --- | --- |
| `BasicWebServer.Server` | The server itself: HTTP classes, routing, responses and the base `Controller` |
| `BasicWebServer.Demo` | A demo app that uses the server, with `HomeController`, `UsersController` and their views |
| `BasicWebServer` | A first minimal version of the server in a single file |

## How to run

Requirements: [.NET 6 SDK](https://dotnet.microsoft.com/download) or newer.

```bash
git clone https://github.com/BoyanTsarski/BasicWebServerr.git
cd BasicWebServerr
dotnet run --project BasicWebServer.Demo
```

Then open http://localhost:8080 in your browser.

## Demo routes

| Route | Method | What it shows |
| --- | --- | --- |
| `/` | GET | Home page |
| `/HTML` | GET, POST | An HTML form and its submitted values |
| `/Redirect` | GET | A redirect response |
| `/Content` | GET, POST | Downloads website content as a text file |
| `/Cookies` | GET | Sets cookies and lists them on the next visit |
| `/Session` | GET | Stores the current date in the session |
| `/Login` | GET, POST | Login form (demo user: `user` / `user123`) |
| `/UserProfile` | GET | Visible only when logged in |
| `/Logout` | GET | Ends the session |

## What I learned

- How an HTTP request and response are structured at the text level
- How routing maps a URL and method to code
- How cookies and server-side sessions work together to remember a user
- Writing asynchronous network code in C#

## License

MIT
