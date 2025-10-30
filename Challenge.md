Code Challenge

Challenge Title:
Build a Real-Time Task Management Web Application with AI-Powered Task Summarization

Challenge Description:
You are tasked with building a simplified real-time task management application that demonstrates your ability to:

- Develop a backend API using C# /.NET Core Minimal APIs with Entity Framework Core for data persistence.
- Implement real-time updates using SignalR.
- Build a React frontend that interacts with the backend API and receives real-time notifications.
- Integrate a simple AI summarization feature using a free public AI API (OpenAI GPT-3.5 via OpenAI’s free tier or an alternative free summarization API) to generate a summary of all tasks.
- Demonstrate clean code, component reusability, and proper API design.

Requirements:
Backend (.NET Core Minimal API):

Create endpoints to:
Get all tasks.
Add a new task (with title and description).

Use Entity Framework Core with an in-memory or SQLite database.
Implement SignalR hub to broadcast new task additions to all connected clients.

Frontend (React):
Display a list of tasks fetched from the backend.
Provide a form to add new tasks.
Receive real-time updates via SignalR when new tasks are added by any client.
Include a button to generate a summary of all tasks by calling the AI summarization API.
Display the AI-generated summary below the task list.

AI Integration:
- Use a free AI summarization API to generate a concise summary of all task descriptions.
- If OpenAI API is used, the candidate can use the free tier or mock the call with a simple - summarization logic if API keys are unavailable.

Constraints:
- The entire challenge should be solvable within 30 minutes.
- Use only free resources/APIs.
- SignalR must be used for real-time communication.

The AI summarization can be a simple call to OpenAI’s GPT-3.5 or a mock function if API access is unavailable.

Technologies Relevant to the Challenge
Backend: C# , .NET 6/7 Minimal APIs, Entity Framework Core (InMemory or SQLite), SignalR

Frontend: React (hooks, functional components), SignalR client

AI Integration: 
- OpenAI API (free tier) or alternative free summarization API

Others: 
- REST API design, JSON, WebSockets (via SignalR)
