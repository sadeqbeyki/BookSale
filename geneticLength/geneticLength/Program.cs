
//first Way
//string inputX = "AAUAUGUUUCAAUAG";
//string first = "AUG";
//string last = "UAG";
//int condA = inputX.IndexOf(first);
//int condB = inputX.LastIndexOf(last);    
//inputX = inputX.Substring(condA, condB);

//Console.WriteLine(inputX, first, last);
//Console.ReadLine();


//secound Way
Console.WriteLine("Please Enter Genetic Code : ");
var inputX = Console.ReadLine();
string first = "AUG";
string last = "UAG";
int condA = inputX.IndexOf(first)+first.Length;
int condB = inputX.LastIndexOf(last);    
string result = inputX.Substring(condA, condB - condA);
Console.WriteLine($"Substring Between \"AUG\" and \"UAG\": 'AUG{result}'");
Console.ReadLine();









