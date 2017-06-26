# SocialNetwork
Twitter-like app
Implement a console-based social networking application (similar to Twitter) satisfying the scenarios below.
Application needs .NET Core 1.1 framework to be installed. To run it simply open the solution in Visual Studio and hit ctrl + f5.

Features

Posting: Alice can publish messages to a personal timeline  
&gt; Alice -> I love the weather today   
&gt; Bob -> Damn! We lost!  
&gt; Bob -> Good game though.  

Reading: I can view Alice and Bob's timelines  
&gt; Alice  
I love the weather today (5 minutes ago)  
&gt; Bob  
Good game though. (1 minute ago)  
Damn! We lost! (2 minutes ago)  

Following: Charlie can subscribe to Alice's and Bob's timelines, and view an aggregated list of all subscriptions  
&gt; Charlie -> I'm in New York today! Anyone want to have a coffee?  
&gt; Charlie follows Alice  
&gt; Charlie wall  
Charlie - I'm in New York today! Anyone want to have a coffee? (2 seconds ago)  
Alice - I love the weather today (5 minutes ago)  
&gt; Charlie follows Bob  
&gt; Charlie wall  
Charlie - I'm in New York today! Anyone wants to have a coffee? (15 seconds ago)  
Bob - Good game though. (1 minute ago)  
Bob - Damn! We lost! (2 minutes ago)  
Alice - I love the weather today (5 minutes ago)  

Details  

The application must use the console for input and output.  
Users submit commands to the application. There are four commands. "posting", "reading", etc.  
are not part of the commands; commands always start with the user's name.  
	 posting: <user name> -> <message> 
	 reading: <user name> 
	 following: <user name> follows <another user> 
	 wall: <user name> wall  
   

