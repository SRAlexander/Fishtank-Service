# Notes

- Intital brief is located in the FishTankServices project > models
- Added a bit of flair by building a rest layer on top using depedency injection and swagger to view API calls
- Two forms of unit testing can be found, service testing and rest layer testing using Moq service Mocker.
- Possible Further development - Saving of tank states to a DB through an Entity Framework layer and injection.




# Fishtank-Service

Demo code base off the following brief...
 
I need a new C# class library (any .net version) to help me manage my fish tank. Don’t worry about the UI: I will build that. The library should satisfy the following user stories and demonstrate your design, coding and testing abilities.
 
Here are the user stories:
                A user should be able to add 3 types of fish to the tank (Gold fish, Angel fish, Babel fish) and name the fish
                A user should be able to see how much food to put in the tank with a Tank.Feed()  method.
                                This should return the weight in grams of the total required fish food.
 
0.1   g for each Gold  fish
0.2   g for each Angel fish
0.3   g for each Babel fish
 
Ensure the design allows me to add more types of fish in the future without having to change the tank class.

Intial Thoughts...

- "without having to change the tank class" - A form of stratergy pattern will be required.
- Adding flair - Will build a rest layer on top to turn it into a web service. 



