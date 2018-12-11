Parallel Alphabetical Sort of Names

Parallel Programming 4CC3 Course Project 2018

Introduction

A very common job in programming is to provide an alphabetized list. This project has a very simple description –
read the file of 10,000 people names (first name followed by given name) and sort this list – by last name (first)
and then by first name (for any repeated last names).

Your Challenge

Your code must implement the sort using a parallel sorting technique in C# (or CUDA). You can use any sorting
technique that you like but it must be a parallel sort and it must NOT use a .NET sorting function. We will use a
.NET sort as a baseline (shown below). See if your code can sort faster than the implementation that I have
provided. You can work in groups of no more than TWO for this project. The project is due on the day of the final
exam.

In order to simplify your implementation, you can (and should) use a .NET method which will compare strings
alphabetically. In C#, this looks like:

string.Compare(string A, string B)

This function returns -1 if string A is first in alphabetical order, 0 if the strings are the same, and 1 if string B is first
in alphabetical order. Note that you will have to figure out how to perform the sort first by last names and then by
first names if the last names are the same.

Your code must determine how long the sort takes in milliseconds and output that time to the screen. To do that
you can use the stopwatch function:

Stopwatch stopwatch = Stopwatch.StartNew();
yourSortFunction();
stopwatch.Stop();
Console.WriteLine("Code took {0} milliseconds to execute", stopwatch.ElapsedMilliseconds);
