
using LibGit2Sharp;
namespace Project;

public class Program{

// See https://aka.ms/new-console-template for more information

public static void Main(string[] args){

Console.WriteLine("Please enter path to repository");
var pathName = Console.ReadLine();

var repo = new UserRepo(pathName);
   Console.WriteLine("Press 1 for Commit fequency. Press 2 for Commit Author");
   string consoleInput = Console.ReadLine()!;
   if(consoleInput == "1")
   {
      repo.CommitFequency(repo.authorList);
   }
   if(consoleInput =="2")
   {
      repo.CommitAuthor(repo.authorList);
   }



